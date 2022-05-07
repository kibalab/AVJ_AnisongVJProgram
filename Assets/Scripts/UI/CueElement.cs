using AVJ;
using AVJ.UIElements;
using UnityEngine.UI;

namespace UI
{
    public class CueElement : UIButton
    {
        public CuePoint CuePoint;

        public Text TimeStamp;
        public Text CueName;

        public override void Initialize()
        {
            TimeStamp.text = "";
            CueName.text = "";
        }

        public void DeleteCue()
        {
            /* TODO */
        }
    }
}