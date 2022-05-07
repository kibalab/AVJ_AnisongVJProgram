using AVJ.UIElements;
using UnityEngine;

namespace AVJ
{
    public class CuePoint : UIButton
    {
        public float Time;
        public LayerWindow window;

        public override void Initialize()
        {
            UpdateCuePosition();
        }

        public void UpdateCuePosition()
        {
            rectTransform.anchorMin = new Vector2(0, 0);
            rectTransform.anchorMax = new Vector2(0, 1);
            rectTransform.pivot = new Vector2(0, 0.5f);
            
            rectTransform.anchoredPosition = new Vector3(((RectTransform) rectTransform.parent).rect.width * Time, 0);
        }

        public override void Click()
        {
            window.SetPlayTime(Time);
        }
    }
}