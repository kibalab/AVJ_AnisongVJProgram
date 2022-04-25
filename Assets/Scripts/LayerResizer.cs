using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.PlayerLoop;

public class LayerResizer : UIBehaviour
{
    public Layer Layer;

    public Vector2 Pivot;
    public Vector2 Weight;

    private bool isSelect = false;
    private bool m_Alt = false;
    private bool m_Ctrl = false;

    private Vector3 lastMousePosition;

    public bool isAltMode
    {
        set
        {
            if (value)
            {
                SetPivot(new Vector2(0.5f, 0.5f));
            }
            else
            {
                SetPivot(Pivot);
            }

            m_Alt = value;
        }

        get => m_Alt;
    }

    public bool isCtrlMode
    {
        set
        {
            m_Ctrl = value;
        }

        get => m_Ctrl;
    }
    
    public void Update()
    {
        if (isSelect)
        {
            Layer.overlayActiveTime = 3;
            var scale = Camera.main.ScreenToWorldPoint(Input.mousePosition) * Layer.MouseSensitivity - lastMousePosition;
            Layer.Size += new Vector2(scale.x, scale.y) * Weight;
            if (isCtrlMode) Layer.Size = new Vector2(Layer.Size.y * Layer.SourceRatio, Layer.Size.y);

            lastMousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition) * Layer.MouseSensitivity;

            isAltMode = Input.GetKey(KeyCode.LeftAlt);
            isCtrlMode = Input.GetKey(KeyCode.LeftControl);
        }
    }

    private void OnMouseDown()
    {
        SetPivot(Pivot);
        isSelect = true;
        lastMousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition) * Layer.MouseSensitivity;
    }

    public void OnMouseUp()
    {
        SetPivot(new Vector2(0.5f, 0.5f));
        isSelect = false;
    }

    public void SetPivot(Vector2 pivot)
    {
        SetPivotSmart(Layer.rectTransform, pivot.x, 0, true, false);
        SetPivotSmart(Layer.rectTransform, pivot.y, 1, true, false);
    }

    #region Credit: https://github.com/Unity-Technologies/UnityCsReference/blob/4fc5eb0fb2c7f5fb09f990fc99f162c8d06d9570/Editor/Mono/Inspector/RectTransformEditor.cs

    public static void SetPivotSmart(RectTransform rect, float value, int axis, bool smart, bool parentSpace)
    {
        Vector3 cornerBefore = GetRectReferenceCorner(rect, !parentSpace);

        Vector2 rectPivot = rect.pivot;
        rectPivot[axis] = value;
        rect.pivot = rectPivot;

        if (smart)
        {
            Vector3 cornerAfter = GetRectReferenceCorner(rect, !parentSpace);
            Vector3 cornerOffset = cornerAfter - cornerBefore;
            rect.anchoredPosition -= (Vector2)cornerOffset;

            Vector3 pos = rect.transform.position;
            pos.z -= cornerOffset.z;
            rect.transform.position = pos;
        }
    }
    
    static Vector3 GetRectReferenceCorner(RectTransform gui, bool worldSpace)
    {
        var s_Corners = new Vector3[4];
        if (worldSpace)
        {
            Transform t = gui.transform;
            gui.GetWorldCorners(s_Corners);
            if (t.parent)
                return t.parent.InverseTransformPoint(s_Corners[0]);
            else
                return s_Corners[0];
        }
        return (Vector3)gui.rect.min + gui.transform.localPosition;
    }

    #endregion 
}
