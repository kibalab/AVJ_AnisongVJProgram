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

        public InterectableUI TargetLayer
        {
            get => layer;
            set
            {
                layer = value;
                Title.text = value.gameObject.name;
            }
        }

        protected override void Start()
        {
            LayerWindowUtil.window = this;
        }

        public void SetAlpha(float alpha)
        {
            layer.UIObject.color = new Color(layer.UIObject.color.r, layer.UIObject.color.g, layer.UIObject.color.b, alpha);
        }
    }
}   