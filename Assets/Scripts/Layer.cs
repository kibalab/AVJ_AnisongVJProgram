using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Layer : UIBehaviour
{
    public RectTransform rectTransform;
    private Outline OutlineEffect;
    private Vector2 clickedPosition;

    public bool IsResizeable = true;
    
    public bool IsSelected = false;
    public bool IsHovered = false;

    public float MouseSensitivity = 85.0f;
    
    void Start()
    {
        // Outline Component Vailed Check
        OutlineEffect = GetComponent<Outline>();
        if (!OutlineEffect)
        {
            OutlineEffect = gameObject.AddComponent<Outline>();
        }

        rectTransform = (RectTransform)transform; // UI used RectTransform
    }

    private void Update()
    {
        if (IsSelected)
        {
            rectTransform.localPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition) * MouseSensitivity + new Vector3(clickedPosition.x, clickedPosition.y, 0);

            rectTransform.localPosition -= new Vector3(0, 0, rectTransform.localPosition.z);
        }
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
}
