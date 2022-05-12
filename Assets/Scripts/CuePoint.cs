using System;
using AVJ.Settings;
using AVJ.UIElements;
using UnityEngine;

namespace AVJ
{
    public class CuePoint : UIButton
    {
        public CueData Cue;
        public Setting Binder;
        public LayerWindow window;

        public override void Initialize()
        {
            UpdateCuePosition();
            Binder.bind = Cue.Key;
        }

        public void UpdateCuePosition()
        {
            rectTransform.anchorMin = new Vector2(0, 0);
            rectTransform.anchorMax = new Vector2(0, 1);
            rectTransform.pivot = new Vector2(0, 0.5f);
            
            rectTransform.anchoredPosition = new Vector3(((RectTransform) rectTransform.parent).rect.width * Cue.Time, 0);
        }

        public override void Click()
        {
            window.SetPlayTime(Cue.Time);
        }
    }
}