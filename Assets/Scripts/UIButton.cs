using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIButton : UIBehaviour
{
    public Image LayerImage;
    public RectTransform rectTransform;
    private BoxCollider2D collider;

    public Color ClickedColor;
    public Color HoveredColor;
    public Color OriginColor;

    public UnityEvent OnClick;

    public bool isClicked = false;
    
    void Start()
    {
        if (OnClick == null)
            OnClick = new UnityEvent ();
        if (!LayerImage) LayerImage = SetComponent<Image>();
        if (!collider) collider = SetComponent<BoxCollider2D>();

        OriginColor = LayerImage.color;
    }

    public void OnMouseDown()
    {
        isClicked = true;
        LayerImage.color = ClickedColor;
    }

    public void OnMouseUp()
    {
        isClicked = false;
        LayerImage.color = OriginColor;
        
        OnClick.Invoke();
    }

    private void OnMouseOver()
    {
        if(isClicked) return;
        LayerImage.color = HoveredColor;
    }

    private void OnMouseEnter()
    {
    }

    private void OnMouseExit()
    {
        LayerImage.color = OriginColor;
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
}
