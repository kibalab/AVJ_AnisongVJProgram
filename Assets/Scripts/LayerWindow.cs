using System;
using AVJ.UIElements;
using UI.UIElements;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

namespace AVJ
{
    public static class LayerWindowUtil
    {
        public static LayerWindow window;
    }
    
    public class LayerWindow : Window
    {
        public InterectableUI layer;
        public Text Title;
        public GameObject SettingPanel;
        public GameObject ErrorPanel;

        public RawImage PreviewScreen;
        public UISlider Timeline;

        public VideoPlayer player;
        
        public InterectableUI TargetLayer
        {
            get => layer;
            set
            {
                layer = value;
                Title.text = value.gameObject.name;
                SwitchPanel(value);

                if (((Layer) layer).Type == LayerType.Video)
                {
                    player = SetComponent<VideoPlayer>();
                    player.url = ((VideoLayer) layer).player.url;
                    player.isLooping = true;

                    void OnPlayerOnstarted(VideoPlayer source)
                    {
                        source.time = ((VideoLayer) layer).player.time;
                    }

                    player.started += OnPlayerOnstarted;
                    player.Play();
                }
            }
        }

        private void LateUpdate()
        {
            if(!player) return;
            Timeline.Value = (float) (player.time / player.length);
            
            PreviewScreen.texture = player.texture;
            
            player.playbackSpeed = ((VideoLayer) layer).player.playbackSpeed;
        }

        public void SetSyncedPlayTime()
        {
            player.time = ((VideoLayer) layer).player.time;
        }

        public void SetPlayTime(float timePer)
        {
            player.time = player.length * timePer;
            if (Input.GetKey(KeyCode.LeftAlt))
                ((VideoLayer) layer).player.time = ((VideoLayer) layer).player.length * timePer;
        }

        public void SwitchPanel(bool b)
        {
            SettingPanel.SetActive(b);
            ErrorPanel.SetActive(!b);
        }

        protected override void Start()
        {
            LayerWindowUtil.window = this;
            SwitchPanel(layer);
        }

        public void SetAlpha(float alpha) => layer.UIObject.color = new Color(layer.UIObject.color.r, layer.UIObject.color.g, layer.UIObject.color.b, alpha);
        
        public void SetVolume(float volume) => ((VideoLayer) layer).player.SetDirectAudioVolume(0, volume);

        public void SetGrayScale(float grayScale)
        {  }

        public void SetPlaySpeed(float speed) => ((VideoLayer) layer).player.playbackSpeed = speed * 2;
    }
}   