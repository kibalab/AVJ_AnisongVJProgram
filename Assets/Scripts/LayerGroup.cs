using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;

public class LayerGroup : Layer
{
    public LayoutElement LayoutController;

    public Text Title;
    public Text Footer;
    public Vector3 lastPos;
    
    private List<Layer> memberLayers = new List<Layer>();

    public GameObject MemberView;

    public void ToggleFold() => MemberView.SetActive(!MemberView.activeSelf);
    
    void Start()
    {
        Title.text = $"[Group] {gameObject.name}";
        Footer.text = Title.text;
        InitLayer(false);
        LayoutController = SetComponent<LayoutElement>();
        UIObject.color = Color.clear;
    }
    
    public void Add(Layer layer)
    {
        layer.Group = this;
    }

    public void Ignore(Layer layer)
    {
        var index = memberLayers.FindIndex(0, x => x.Equals(layer));
        
        memberLayers.RemoveAt(index);
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
        /*
        layer.rectTransform.SetSiblingIndex(rectTransform.GetSiblingIndex());
        layer.LayerImage.rectTransform.SetSiblingIndex(rectTransform.GetSiblingIndex());
        */
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

    public void DeleteGroup()
    {
    }

    public void FixedUpdate()
    {
        overlayActiveTime = 3;
    }
}
