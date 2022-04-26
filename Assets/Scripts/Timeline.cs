using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timeline : Layer
{
    public Layer layer;

    public LayoutElement LayoutController;

    public Text Title;
    public RawImage line;

    public RawImage View;

    public Vector3 lastPos;
    
    // Start is called before the first frame update
    void Start()
    {
        Title.text = $"[{layer.Type.ToString()}] {layer.gameObject.name}";
        InitLayer(false);
        DrawLine();
        LayoutController = SetComponent<LayoutElement>();
        
        if (layer.Type == LayerType.Image && LayerImage)
        {
            View.texture = (Texture2D)layer.media;
        }
    }

    public void DrawLine()
    {
    }

    public void OnMouseDown()
    {
        base.OnMouseDown();

        LayoutController.ignoreLayout = true;
        var blank = rectTransform.parent.Find("Blank");
        blank.SetSiblingIndex(rectTransform.GetSiblingIndex());
        blank.GetComponent<LayoutElement>().ignoreLayout = false;
        rectTransform.SetSiblingIndex(rectTransform.parent.childCount);
        
        lastPos = rectTransform.localPosition;
    }

    public void OnMouseUp()
    {
        base.OnMouseUp();
        
        var blank = rectTransform.parent.Find("Blank");
        rectTransform.SetSiblingIndex(blank.GetSiblingIndex());
        blank.SetSiblingIndex(rectTransform.parent.childCount);
        blank.GetComponent<LayoutElement>().ignoreLayout = true;
        LayoutController.ignoreLayout = false;
        
        layer.rectTransform.SetSiblingIndex(rectTransform.GetSiblingIndex());
        layer.LayerImage.rectTransform.SetSiblingIndex(rectTransform.GetSiblingIndex());
    }

    private void OnMouseDrag()
    {
        if (IsSelected)
        {
            var sib = CalTargetSiblingIndex();
            Debug.Log(sib);
            var blank = rectTransform.parent.Find("Blank"); 
            blank.SetSiblingIndex(sib);
        }
    }

    public int CalTargetSiblingIndex()
    {
        var start = rectTransform.parent.GetChild(0).localPosition.y;
        for (int i = 0; i < rectTransform.parent.childCount; i++)
        {
            Debug.Log(start);
            if (start <= rectTransform.localPosition.y) return i;
            start += ((RectTransform) rectTransform.parent.GetChild(i)).rect.y * 2;
        }
        
        Debug.Log("End");
        
            

        return rectTransform.parent.childCount;
    }

    public void DeleteLayer()
    {
        var layerEvent = new LayerEvent();
        layerEvent.EventType = LayerEventType.Delete;
        layerEvent.layer = layer;
        
        EventManager.Events.Enqueue(layerEvent);
        
        Destroy(gameObject);
    }

    private void Update()
    {
        base.Update();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        overlayActiveTime = 3;
        if (layer.Type == LayerType.Video && LayerImage)
        {
            View.texture = layer.LayerImage.texture;
        }
    }
}
