using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImageLayer : Layer
{
    // Start is called before the first frame update
    protected override void Start()
    {
        Type = LayerType.Image;
        InitLayer(true); // Layer Initialize Codes
        InitScaler();
        SetImage((Texture2D)media);
    }

    public void SetImage(Texture2D media)
    {
        this.media = media;

        LayerImage.texture = media;
        Debug.Log($"[VideoLayer] Size Ratio : ({media.width}, {media.height})");

        ScalingToRatio(new Vector2(media.width, media.height));
        
    }
}
