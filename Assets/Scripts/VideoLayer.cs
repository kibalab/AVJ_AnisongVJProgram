using UnityEngine;
using UnityEngine.Video;

public class VideoLayer : Layer
{
    public VideoPlayer player;

    void Start()
    {
        Type = LayerType.Video;
        InitLayer(true); // Layer Initialize Codes
        InitScaler();
        InitPlayer();
        PlayClip((string)media);
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
            LayerImage.texture = player.texture;
            LayerImage.color = Color.white;
            
        }
        else
        {
            LayerImage.color = Color.black;
        }
    }
}
