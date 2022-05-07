using AVJ.UIElements;
using UnityEngine;

namespace AVJ
{
    public class CuePoint : UIButton
    {
        public float Time;
        public LayerWindow window;

        public override void Click()
        {
            window.SetPlayTime(Time);
        }
    }
}