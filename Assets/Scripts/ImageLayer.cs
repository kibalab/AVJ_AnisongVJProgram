using System.Collections;
using System.Collections.Generic;
using AVJ.UIElements;
using UnityEngine;
using UnityEngine.UI;

public class ImageLayer : Layer, IUIInitializer
{
    public RawImage UIObject;

    public void Initialize()
    {
        Type = LayerType.Image;
        InitLayer(true); // Layer Initialize Codes
        InitScaler();
        SetImage((Texture2D)media);
        
        IsReady = true;
    }

    public void SetImage(Texture2D media)
    {
        this.media = media;

        UIObject.texture = media;
        Debug.Log($"[VideoLayer] Size Ratio : ({media.width}, {media.height})");

        ScalingToRatio(new Vector2(media.width, media.height));
        
    }
}
