using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;
using UnityEngine.Video;

public class VideoLayer : Layer
{
    public VideoPlayer player;
    public VideoClip media;

    protected override void Start()
    {
        InitLayer(); // Layer Initialize Codes
        InitScaler();
        InitPlayer();
        PlayClip(media);
    }

    public void InitPlayer()
    {
        player = SetComponent<VideoPlayer>();

        player.playOnAwake = true;
        player.isLooping = true;
    }

    public void PlayClip(VideoClip media)
    {
        this.media = media;
        player.clip = media;
        player.Play();
        Debug.Log($"[VideoLayer] Size Ratio : ({media.width}, {media.height})");

        ScalingToRatio(new Vector2(media.width, media.height));
        
    }

    public void Update()
    {
        base.Update();

        if (player.isPlaying)
        {
            LayerImage.texture = player.texture;
            LayerImage.color = Color.white;
        }
        else
        {
            LayerImage.color = Color.black;
        }
    }
}
