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

    public void OnMouseDrag()
    {
        rectTransform.localPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    public void OnMouseDown()
    {
        Debug.Log("[Layer] MouseDown");
        OutlineEffect.effectColor = Color.white;
    }

    public void OnMouseUp()
    {
        Debug.Log("[Layer] MouseUp");
        OutlineEffect.effectColor = Color.cyan;
    }

    private void OnMouseEnter()
    {
        Debug.Log("[Layer] MouseEnter");
        OutlineEffect.enabled = true;
    }

    private void OnMouseExit()
    {
        Debug.Log("[Layer] MouseExit");
        OutlineEffect.enabled = false;
    }
}
