using System;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

namespace AVJ
{
    public class URILoadWindow : Window
    {
        public InputField InputField;

        public VideoPlayer previewPlayer;
        public RawImage previewDisplay;

        public Image LoadButton;
        public Text ButtonText;

        private bool isVideo = false;
        private string formattedUrl;

        public void OnClick()
        {
            if(isVideo) LoadVideo();
            else CheckVideo();
        }

        public void Clear()
        {
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
        }

        private void OnPreviewPlayerOnstarted(VideoPlayer source)
        {
            LoadButton.color = new Color(0.03f, 0.5f, 0);
            ButtonText.text = "Load";
            isVideo = true;
        }

        public void LateUpdate()
        {
            if(previewPlayer.isPlaying) previewDisplay.texture = previewPlayer.texture;
            
        }
    }
}