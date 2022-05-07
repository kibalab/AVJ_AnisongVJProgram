using System;
using System.Collections;
using System.Collections.Generic;
using AVJ;
using AVJ.UIElements;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Object = UnityEngine.Object;

[Serializable]
public struct LayerData
{
    [SerializeField] public LayerType Type;
    [SerializeField] public string sourcePath;
    [SerializeField] public string layerName;
    [SerializeField] public Vector2 layerPosition;
    [SerializeField] public Vector2 layerScale;
    [SerializeField] public List<CuePoint> CuePoints;
}

public class Layer : InterectableUI, IUIInitializer
{
    public LayerData Data = new LayerData();
    
    private Outline OutlineEffect;

    public float overlayActiveTime = 3.0f;

    public float SourceRatio = 1;
    
    public object media;

    public TimelineGroup Group = null;

    public void Initialize()
    {
        InitLayer(true);

        IsReady = true;
    }

    public void InitLayer(bool ResizeCollider)
    {
        // Outline Component Vailed Check
        if (!OutlineEffect) OutlineEffect = SetComponent<Outline>();
        if (!UIObject) UIObject = SetComponent<RawImage>();
        if (!collider) collider = SetComponent<BoxCollider2D>();

        if(ResizeCollider) Size = rectTransform.sizeDelta;
        
        Debug.Log($"[Layer, {gameObject.name}] Initializing Layer");
        Data.layerName = gameObject.name;
    }

    public void InitScaler()
    {
        var resizers = GetComponentsInChildren<LayerResizer>();

        foreach (var vaResizer in resizers)
        {
            vaResizer.Layer = this;
        }
        
        var texts = GetComponentsInChildren<Text>();

        foreach (var text in texts)
        {
            if (text.gameObject.name == "Title")
            {
                text.text = $"[{Data.Type.ToString()}] {Data.layerName}";
            }
        }
    }

    #region Events

    public void Update()
    {
        base.Update();

        if (IsHovered)
        {
            overlayActiveTime = 3;
        }

        DrawRectScaler(overlayActiveTime >= 0);

        overlayActiveTime -= Time.deltaTime;
        

        Data.layerPosition = rectTransform.localPosition;
        Data.layerScale = rectTransform.sizeDelta;
    }

    public override void OnUIDrag(IDragDropHandler UIConponent)
    {
        OutlineEffect.effectColor = Color.white;
        OutlineEffect.enabled = true;
        IsSelected = true;
        
    }

    public override void OnUIDrop(IDragDropHandler UIConponent)
    {
        OutlineEffect.effectColor = Color.cyan;
        OutlineEffect.enabled = false;
        IsSelected = false;
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

    protected override void OnDestroy()
    {
        Destroy(UIObject.gameObject);
    }

    #endregion
}
