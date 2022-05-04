using AVJ.UIElements;
using UnityEngine.UI;

namespace UI.UIElements
{
    public class UIToggle : UIButton
    {
        public Image MarkImage;
        public bool IsOn = false;

        public override void OnMouseUp()
        {
            IsOn = !IsOn;
            base.OnMouseUp();
            UpdateState();
        }

        public void UpdateState()
        {
            MarkImage.enabled = IsOn;
        }
    }
}