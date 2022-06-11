using System;
using System.Collections;
using System.Collections.Generic;
using AVJ;
using AVJ.Control;
using AVJ.Data;
using AVJ.UIElements;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Object = UnityEngine.Object;

[Serializable]
public class LayerData
{
    [SerializeField] public LayerType Type;
    [SerializeField] public string sourcePath;
    [SerializeField] public string layerName;
    [SerializeField] public SerializableVector2 layerPosition = new Vector2(0, 0);
    [SerializeField] public SerializableVector2 layerScale = new Vector2(0, 0);
    [SerializeField] public List<CueData> CuePoints;
    [NonSerialized] public RectTransform rectTransform;

    public void LoadData()
    {
         var loadedData = DataFileManager.Load<LayerData>(layerName, "layer");
         if (loadedData == null)
         {
             
             return;
         }
         
         Type = loadedData.Type;
         sourcePath = loadedData.sourcePath;
         layerName = loadedData.layerName;
         layerPosition = loadedData.layerPosition;
         layerScale = loadedData.layerScale;
         CuePoints = loadedData.CuePoints;

         rectTransform.position = (Vector2)layerPosition;
         rectTransform.sizeDelta = layerScale;
         
         Debug.LogError($"[LayerData] LoadData : {ToString()}");
    }

    public void SaveData()
    {
        DataFileManager.Save(layerName, this,"layer");
    }

    public override string ToString()
    {
        return $"{Type}, {sourcePath}, {layerName}, {layerPosition}, {layerScale}, CuePoints({CuePoints.Count})";
    }
}

public class Layer : InterectableUI, IUIInitializer
{
    public LayerData Data = new LayerData();
    
    private Outline OutlineEffect;

    public float overlayActiveTime = 3.0f;

    public float SourceRatio = 1;
    
    public object media;

    public TimelineGroup Group = null;

    public virtual void Initialize()
    {
        // Currently not used
        InitLayer(true);
        
        Data.LoadData();

        IsReady = true;
        
    }

    public virtual void InitLayer(bool ResizeCollider)
    {
        if (!OutlineEffect) OutlineEffect = SetComponent<Outline>();
        if (!UIObject) UIObject = SetComponent<RawImage>();
        if (!collider) collider = SetComponent<BoxCollider2D>();

        if(ResizeCollider) Size = rectTransform.sizeDelta;
        
        Debug.Log($"[Layer, {gameObject.name}] Initializing Layer");
        Data.layerName = gameObject.name;

        Data.CuePoints = new List<CueData>();
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
