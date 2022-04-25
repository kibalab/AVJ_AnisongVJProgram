using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class Layer : UIBehaviour
{
    public RawImage LayerImage;
    public RectTransform rectTransform;
    private BoxCollider2D collider;
    
    private Outline OutlineEffect;
    private Vector2 clickedPosition;

    public bool IsResizeable = true;
    
    public bool IsSelected = false;
    public bool IsHovered = false;

    public float MouseSensitivity = 85.0f;

    public float overlayActiveTime = 3.0f;

    public float SourceRatio = 1;
    
    
    #region Properties
    
    public Vector2 Size
    {
        set
        {
            collider.size = value;
            rectTransform.sizeDelta = value;
            LayerImage.rectTransform.sizeDelta = value;
        }
        get => rectTransform.sizeDelta;
    }
    
    #endregion
    
    void Start()
    {
        InitLayer();
    }

    public void InitLayer()
    {
        // Outline Component Vailed Check
        if (!OutlineEffect) OutlineEffect = SetComponent<Outline>();
        if (!LayerImage) LayerImage = SetComponent<RawImage>();
        if (!collider) collider = SetComponent<BoxCollider2D>();
        
        rectTransform = (RectTransform)transform; // UI used RectTransform

        Size = rectTransform.sizeDelta;
        
        Debug.Log($"[Layer, {gameObject.name}] Initializing Layer");
    }

    public void InitScaler()
    {
        var resizers = GetComponentsInChildren<LayerResizer>();

        foreach (var vaResizer in resizers)
        {
            vaResizer.Layer = this;
        }
    }

    #region Events

    public void Update()
    {
        LayerImage.rectTransform.pivot = rectTransform.pivot;
        LayerImage.rectTransform.localPosition = rectTransform.localPosition;
        
        if (IsSelected)
        {
            
            rectTransform.localPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition) * MouseSensitivity + new Vector3(clickedPosition.x, clickedPosition.y, 0);

            rectTransform.localPosition -= new Vector3(0, 0, rectTransform.localPosition.z);
        }

        if (IsHovered)
        {
            overlayActiveTime = 3;
        }

        DrawRectScaler(overlayActiveTime >= 0);

        overlayActiveTime -= Time.deltaTime;
    }

    public void OnMouseDown()
    {
        Debug.Log("[Layer] MouseDown");
        OutlineEffect.effectColor = Color.white;
        OutlineEffect.enabled = true;
        IsSelected = true;
        clickedPosition = rectTransform.localPosition - Camera.main.ScreenToWorldPoint(Input.mousePosition) * MouseSensitivity;
    }

    public void OnMouseUp()
    {
        Debug.Log("[Layer] MouseUp");
        OutlineEffect.effectColor = Color.cyan;
        OutlineEffect.enabled = false;
        IsSelected = false;
        clickedPosition = new Vector2(0, 0);
    }

    private void OnMouseEnter()
    {
        Debug.Log("[Layer] MouseEnter");
        IsHovered = true;
    }

    private void OnMouseExit()
    {
        Debug.Log("[Layer] MouseExit");
        IsHovered = false;
    }

    #endregion

    public void DrawRectScaler(bool b)
    {
        transform.GetChild(0).gameObject.SetActive(b);
    }
    
    #region Utils

    public void ScalingToRatio(Vector2 Ratio)
    {
        SourceRatio = (Ratio.x / Ratio.y);
        Size = new Vector2( SourceRatio * Size.x, Size.y);
    }
    
    public T SetComponent<T>() where T : Component
    {
        Debug.Log($"[Layer, {gameObject.name}] Add Component : {typeof(T).Name}");
        var component = GetComponent<T>();
        if (component == null)
        {
            component = gameObject.AddComponent<T>();
        }
        return component;
    }
    
    #endregion
}
