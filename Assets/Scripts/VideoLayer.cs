﻿using System.Collections.Generic;
using AVJ;
using AVJ.UIElements;
using UnityEngine;
using UnityEngine.Experimental.Video;
using UnityEngine.UI;
using UnityEngine.Video;

public class VideoLayer : Layer, IUIInitializer
{
    public VideoPlayer player;

    public void Initialize()
    {
        Data.Type = LayerType.Video;
        InitLayer(true); // Layer Initialize Codes
        InitScaler();
        InitPlayer();
        PlayClip((string)media);
        Data.LoadData();

        IsReady = true;
    }

    public void InitPlayer()
    {
        player = SetComponent<VideoPlayer>();

        player.playOnAwake = true;
        player.isLooping = true;
    }

    
    public void PlayClip(string media)
    {
        this.media = media;
        player.source = VideoSource.Url;
        player.url = media;
        player.SetDirectAudioVolume(0, 0);
        player.Play();
        
        player.started += StartVideo;
    }

    public void StartVideo(VideoPlayer source)
    {
        Debug.Log($"[VideoLayer] Size Ratio : ({source.texture.width}, {source.texture.height})");
        rectTransform.localPosition = rectTransform.parent.localScale;
        rectTransform.sizeDelta = new Vector2(((RectTransform)rectTransform.parent).sizeDelta.y, ((RectTransform)rectTransform.parent).sizeDelta.y);
        ScalingToRatio(new Vector2(source.texture.width, source.texture.height));
        UIObject.color = Color.white;
    }

    public void Update()
    {
        base.Update();

        if (player.isPlaying)
        {
            ((RawImage)UIObject).texture = player.texture;
            
        }
        else
        {
            UIObject.color = Color.black;
        }
    }
}
