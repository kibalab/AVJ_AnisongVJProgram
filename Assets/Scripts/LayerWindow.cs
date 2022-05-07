using System;
using AVJ.UIElements;
using UnityEngine;
using UnityEngine.UI;

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

        public InterectableUI TargetLayer
        {
            get => layer;
            set
            {
                layer = value;
                Title.text = value.gameObject.name;
                SwitchPanel(value);
            }
        }

        private void LateUpdate()
        {
            throw new NotImplementedException();
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
        
        public void SetGrayScale(float grayScale)
        {
            
        }
        
        public void SetPlaySpeed(float speed)
        {
            ((VideoLayer) layer).player.playbackSpeed = speed * 2;
        }
    }
}   