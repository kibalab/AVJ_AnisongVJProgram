using UnityEngine.PlayerLoop;

namespace DefaultNamespace
{
    public class Window : Layer
    {
        private bool isFold = false;

        public bool IsFold
        {
            set
            {
                isFold = false;
            }
            get => isFold;
        }

        public void Update()
        {
            base.Update();

            if (isFold) overlayActiveTime = 0;
            else overlayActiveTime = 3;
        }
    }
}