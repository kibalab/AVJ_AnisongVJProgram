using System;
using AVJ.Settings;
using UI;
using UnityEngine;

namespace AVJ
{
    [Serializable]
    public class CueData : Behaviour
    {
        [SerializeField] public float Time = 0.0f;
        [SerializeField] public string Name = "";
        [SerializeField] public Midi Key;

        public CuePoint Point;
        public CueListElement Element;

        public CueData(string Name, float Time)
        {
            this.Name = Name;
            this.Time = Time;
        }

        public void DeleteCue()
        {
            ((Layer) LayerWindowUtil.window.layer).Data.CuePoints.Remove(this);
            Destroy(Element.gameObject);
            Destroy(Point.gameObject);
            
        }
    }
}