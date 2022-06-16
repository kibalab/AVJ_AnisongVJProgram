using System;
using System.Collections;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using UnityEngine.Video;
using YoutubePlayer;
using Random = System.Random;
using YoutubePlayer = YoutubePlayer.YoutubePlayer;

namespace AVJ
{
    public class URILoadWindow : Window
    {
        public InputField InputField;

        public VideoPlayer previewPlayer;
        public RawImage previewDisplay;

        public Image LoadButton;
        public Text ButtonText;

        public LayerManager LayerManager;

        private bool isVideo = false;
        private bool isDownloading = false;
        private string formattedUrl;

        private Task<string> DownloadTask;

        public void OnClick()
        {
            if(isVideo) LoadVideo();
            else CheckVideo();
        }

        public void Clear()
        {
            previewDisplay.color = Color.clear;
            LoadButton.color = new Color(0.08490568f, 0.08490568f, 0.08490568f);
            ButtonText.text = "Check";
            isVideo = false;
        }

        public void CheckVideo()
        {
            formattedUrl = Regex.Replace(InputField.text,
                @"^((?:https?:)?\/\/)?((?:www|m)\.)?((?:youtube\.com|youtu.be))(\/?)", "");
            formattedUrl = $"https://unity-youtube-dl-server.herokuapp.com/{(formattedUrl.Contains("watch?v=") ? "": "watch?v=")}{formattedUrl}";
            previewPlayer.url = formattedUrl;
            previewPlayer.Play();
            ButtonText.text = "Loading";

            previewPlayer.started += OnPreviewPlayerOnstarted;
        }

        public void LoadVideo()
        {
            var layer = (VideoLayer)LayerManager.AddLayer<VideoLayer>($"URL Downloaded {UnityEngine.Random.Range(0, 999999)}", formattedUrl);
            Clear();

            
            layer.player.PlayYoutubeVideoAsync(formattedUrl);
        }

        private void OnPreviewPlayerOnstarted(VideoPlayer source)
        {
            previewDisplay.color = Color.white;
            LoadButton.color = new Color(0.03f, 0.5f, 0);
            ButtonText.text = "Load";
            isVideo = true;
        }
 
        public void LateUpdate()
        {
            if(previewPlayer.isPlaying) previewDisplay.texture = previewPlayer.texture;
            if (DownloadTask != null && DownloadTask.IsCompleted)
            {
                DownloadTask.Dispose();
                DownloadTask = null;
                LayerManager.AddLayer<VideoLayer>($"URL Downloaded {UnityEngine.Random.Range(0, 999999)}", DownloadTask.Result);
            }
        }
    }
}