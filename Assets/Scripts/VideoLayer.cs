using AVJ.UIElements;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class VideoLayer : Layer, IUIInitializer
{
    public VideoPlayer player;

    public void Initialize()
    {
        Type = LayerType.Video;
        InitLayer(true); // Layer Initialize Codes
        InitScaler();
        InitPlayer();
        PlayClip((string)media);
        
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
        player.Play();

        player.started += StartVideo;
    }

    public void StartVideo(VideoPlayer source)
    {
        Debug.Log($"[VideoLayer] Size Ratio : ({source.texture.width}, {source.texture.height})");
        ScalingToRatio(new Vector2(source.texture.width, source.texture.height));
    }

    public void Update()
    {
        base.Update();

        if (player.isPlaying)
        {
            ((RawImage)UIObject).texture = player.texture;
            UIObject.color = Color.white;
            
        }
        else
        {
            UIObject.color = Color.black;
        }
    }
}
