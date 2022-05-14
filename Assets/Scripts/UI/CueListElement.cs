using System;
using AVJ;
using AVJ.UIElements;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class CueListElement : UIButton
    {
        public Image Background;
        private CueData m_Cue;
        public Text Title;
        public Text TimeStamp;

        public CueData Cue
        {
            get => m_Cue;
            set
            {
                m_Cue = value;
                Title.text = value.Name;
                
                Debug.LogError($"[CueElement] Set Cue {value.Time}");
                
                var videoPlayer = ((VideoLayer) LayerWindowUtil.window.layer).player;
                if (videoPlayer != null)
                    TimeStamp.text = TimeSpan.FromSeconds(value.Time * videoPlayer.length).ToString(@"hh\:mm\:ss\.fff");
            }
        }

        private void Update()
        {
            if(!LayerWindowUtil.window || !LayerWindowUtil.window.player) return;
            if (LayerWindowUtil.window.player.time > m_Cue.Time * LayerWindowUtil.window.player.length)
            {
                Background.color = Color.black;
            }
            else
            {
                Background.color = new Color(0.2641509f, 0.2641509f, 0.2641509f, 0.4f);
            }
        }

        public override void Click()
        {
            LayerWindowUtil.window.SetPlayTime(m_Cue.Time);
        }

        public void DeleteCue()
        {
            if (!m_Cue)
            {
                Debug.LogError("[CueListElement] Can not find cue!");
            }
            m_Cue.DeleteCue();
        }
        
    }
}