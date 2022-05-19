using System;
using AVJ.Settings;
using UI;
using UnityEngine;

namespace AVJ
{
    [Serializable]
    public class CueData
    {
        [SerializeField] public float Time = 0.0f;
        [SerializeField] public string Name = "";
        [SerializeField] public Midi Key;

        [NonSerialized] public CuePoint Point;
        [NonSerialized] public CueListElement Element;

        public CueData(string Name, float Time)
        {
            this.Name = Name;
            this.Time = Time;
        }

        public void DeleteCue()
        {
            ((Layer) LayerWindowUtil.window.layer).Data.CuePoints.Remove(this);
            Point.Cue = null;
            Element.Cue = null;
            
        }
    }
}