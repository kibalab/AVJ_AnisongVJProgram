using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using AVJ;
using AVJ.UIElements;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;

public class TimelineGroup : Timeline
{
    public Transform FooterLine;
    public Text Footer;
    
    private List<Timeline> memberTimeline = new List<Timeline>();
    
    public Vector2 InitializScale = Vector2.zero;

    public GameObject MemberView;

    public CanvasGroup Canvas;

    public LayoutElement Blank;

    public void ToggleFold() => MemberView.SetActive(!MemberView.activeSelf);
    
    
    void Start()
    {
        Title.text = $"[Group] {gameObject.name}";
        Footer.text = Title.text;
        InitTimeline();
        LayoutController = SetComponent<LayoutElement>();
        UIObject.color = Color.clear;
        InitializScale = rectTransform.sizeDelta;
    }
    
    public void Add(Timeline timeline)
    {
        if(timeline.layer) timeline.layer.Group = this;
        if(!memberTimeline.Contains(timeline)) memberTimeline.Add(timeline);
        timeline.transform.parent = MemberView.transform;
        
        ReCalGroupScale();
        OnUIDrop(this);

        Blank.ignoreLayout = false;
    }

    public void Ignore(Timeline timeline)
    {
        var index = memberTimeline.FindIndex(0, x => x.Equals(timeline));
        
        memberTimeline.RemoveAt(index);
        
        timeline.transform.parent = transform.parent;

        ReCalGroupScale();
        
        Blank.ignoreLayout = true;
    }

    public void ReCalGroupScale()
    {
        var yScale = InitializScale.y;

        for (int i = 0; i < MemberView.transform.childCount; i++)
        {
            var child = MemberView.transform.GetChild(i);
            if(child.gameObject.name != "Blank" && child.gameObject.name != "Blank_S") yScale += ((RectTransform) child).rect.height + 1;
        }

        Size = new Vector2(rectTransform.rect.width, yScale);
    }
    
    public override void OnUIDrag(IDragDropHandler UIConponent)
    {
        LayoutController.ignoreLayout = true;
        var blank = rectTransform.parent.Find("Blank");
        blank.SetSiblingIndex(rectTransform.GetSiblingIndex());
        blank.GetComponent<LayoutElement>().ignoreLayout = false;
        rectTransform.SetSiblingIndex(rectTransform.parent.childCount);
        
        lastPos = rectTransform.localPosition;
    }

    public override void OnUIDrop(IDragDropHandler UIConponent)
    {
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

    public override void OnUIEnter()
    {
        
    }

    public override void OnUIExit()
    {
        base.OnUIExit();
    }

    private void OnMouseDrag()
    {
        if (IsSelected)
        {
            var sib = CalTargetSiblingIndex();
            var blank = rectTransform.parent.Find("Blank"); 
            blank.SetSiblingIndex(sib);
            FooterLine.SetSiblingIndex(MemberView.transform.childCount);
        }
    }

    public int CalTargetSiblingIndex()
    {
        var start = rectTransform.parent.GetChild(0).localPosition.y;
        for (int i = 0; i < rectTransform.parent.childCount; i++)
        {
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
        base.FixedUpdate();
        
        var blank = MemberView.transform.Find("Blank_S");
        blank.SetSiblingIndex(MemberView.transform.childCount-1);
        FooterLine.SetSiblingIndex(FooterLine.parent.childCount);
    }
}
