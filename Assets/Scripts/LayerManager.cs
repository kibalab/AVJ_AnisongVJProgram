using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Object = UnityEngine.Object;

public enum MediaType
{
    Image,
    Video
}

public class LayerManager : MonoBehaviour
{
    public GameObject LayerField;
    public GameObject RenderField;
    
    public GameObject LayerPrefab;
    
    private List<Layer> Layers = new List<Layer>();

    public Layer[] GetLayers { get => Layers.ToArray(); }

    public Layer AddLayer<T>(MediaType type, string name, Object media) where T : Layer
    {
        var layerObject = Instantiate(LayerPrefab, LayerField.transform);
        var LayerScreen = Instantiate(new GameObject(), RenderField.transform);

        layerObject.gameObject.name = name;

        var LayerImage = LayerScreen.AddComponent<RawImage>();

        var AddedLayer = layerObject.AddComponent<T>();;

        AddedLayer.LayerImage = LayerImage;
        
        AddedLayer.media = media;
        
        Layers.Add(AddedLayer);

        return AddedLayer;
    }
}
