using System;
using AVJ.UIElements;
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
                    player = CopyComponent(((VideoLayer) layer).player);
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
            
            PreviewScreen.texture = player.texture;
            
            player.playbackSpeed = ((VideoLayer) layer).player.playbackSpeed;
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

        public void SetAlpha(float alpha)
        {
            layer.UIObject.color = new Color(layer.UIObject.color.r, layer.UIObject.color.g, layer.UIObject.color.b, alpha);
        }
        
        public void SetVolume(float volume)
        {
            ((VideoLayer) layer).player.SetDirectAudioVolume(0, volume);
        }

        public void SetGrayScale(float grayScale)
        {
            
        }
        
        public void SetPlaySpeed(float speed)
        {
            ((VideoLayer) layer).player.playbackSpeed = speed * 2;
        }
        
        public T CopyComponent<T>(T original) where T : Component
        {
            Type type = original.GetType();
            Component copy = SetComponent<T>();
            System.Reflection.FieldInfo[] fields = type.GetFields();
            foreach (System.Reflection.FieldInfo field in fields)
            {
                field.SetValue(copy, field.GetValue(original));
            }
            return copy as T;
        }
    }
}   