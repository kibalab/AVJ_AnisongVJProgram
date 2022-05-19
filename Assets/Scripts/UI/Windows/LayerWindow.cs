using System;
using System.Linq;
using a;
using AVJ.UIElements;
using UI.UIElements;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

namespace AVJ
{
    public static class LayerWindowUtil
    {
        public static LayerWindow window;
    }
    
    public class LayerWindow : Window
    {
        public InterectableUI layer;

        public CueWindow CueWindow;
        
        public Text Title;
        public GameObject SettingPanel;
        public GameObject ErrorPanel;

        public GameObject CuePanel;
        public GameObject CuePrefab;

        public RawImage PreviewScreen;
        public UISlider PreviewTimeline;
        public UISlider LayerTimeline;

        public VideoPlayer player;
        
        public InterectableUI TargetLayer
        {
            get => layer;
            set
            {
                layer = value;
                Title.text = value.gameObject.name;
                SwitchPanel(value);

                CueWindow.layer = (Layer)value;
                CueWindow.Initialize();

                if (((Layer) layer).Data.Type == LayerType.Video)
                {
                    player = SetComponent<VideoPlayer>();
                    player.url = ((VideoLayer) layer).player.url;
                    player.isLooping = true;

                    void OnPlayerOnstarted(VideoPlayer source)
                    {
                        source.time = ((VideoLayer) layer).player.time;
                    }

                    player.started += OnPlayerOnstarted;
                    player.Play();
                }
                
                ReDrawCuePoints();
            }
        }

        private void LateUpdate()
        {
            if(!player) return;
            
            PreviewTimeline.Value = (float) (player.time / player.length);
            
            if(!layer) return;
            
            PreviewScreen.texture = player.texture;
            
            LayerTimeline.Value =  (float) (((VideoLayer) layer).player.time / ((VideoLayer) layer).player.length);
            
            
            player.playbackSpeed = ((VideoLayer) layer).player.playbackSpeed;
            
            if(EventManager.CueEvents.Count <= 0) return;

            var cueEvent = EventManager.CueEvents.Dequeue();

            switch (cueEvent.Type)
            {
                case CueEventType.Add :
                    CueWindow.Initialize();
                    SortCues();
                    ((Layer)layer).Data.SaveData();
                    ReDrawCuePoints();
                    break;
                
                case CueEventType.Delete :
                    cueEvent.Cue.DeleteCue();
                    CueWindow.Initialize();
                    SortCues();
                    ((Layer)layer).Data.SaveData();
                    ReDrawCuePoints();
                    break;
                
                case CueEventType.Move :
                    SortCues();
                    ((Layer)layer).Data.SaveData();
                    ReDrawCuePoints();
                    break;
                
                default:
                    break;
            }
        }

        public void AddNewCue()
        {
            if (((VideoLayer)layer).Data.CuePoints.Count >= 20) return;
            
            var e = new CueEvent();
            e.Cue = CreateCue(PreviewTimeline.Value);
            e.Type = CueEventType.Add;
            EventManager.CueEvents.Enqueue(e);
        }

        private CueData CreateCue(float Time)
        {
            var newCue = new CueData("", Time);
            ((VideoLayer)layer).Data.CuePoints.Add(newCue);

            return newCue;
        }

        public void ReDrawCuePoints()
        {
            ClearCuePoints();
            DrawCuePoints();
        }

        public void ClearCuePoints()
        {
            for (int i = 0; i < CuePanel.transform.childCount; i++)
            {
                Destroy(CuePanel.transform.GetChild(i).gameObject);
            }
            foreach (var cue in ((VideoLayer) layer).Data.CuePoints)
            {
                if(cue.Point == null) continue;
                Destroy(cue.Point.gameObject);
                cue.Point = null;
            }
        }

        public void DrawCuePoints()
        {
            if (((Layer) layer).Data.Type != LayerType.Video) return;
            
            foreach (var cue in ((VideoLayer) layer).Data.CuePoints)
            {
                var newCuePoint = Instantiate(CuePrefab, CuePanel.transform).GetComponent<CuePoint>();
                newCuePoint.Cue = cue;
                newCuePoint.Cue.Point = newCuePoint;
                newCuePoint.window = this;
                UIUtility.InitializeUI(newCuePoint);
            }
        }

        private void SortCues()
        {
            ((VideoLayer) layer).Data.CuePoints = ((VideoLayer) layer).Data.CuePoints.OrderBy(x => x.Time).ToList();
        }

        public void SetSyncedPlayTime()
        {
            player.time = ((VideoLayer) layer).player.time;
        }

        public void SetPlayTime(float timePer)
        {
            Debug.LogError($"[LayerWindow] VideoPlayer Movde Time to {timePer}");
            
            player.time = player.length * timePer;
            if (Input.GetKey(KeyCode.LeftAlt))
                ((VideoLayer) layer).player.time = ((VideoLayer) layer).player.length * timePer;
        }

        public void SwitchPanel(bool b)
        {
            SettingPanel.SetActive(b);
            ErrorPanel.SetActive(!b);
        }

        protected override void Start()
        {
            LayerWindowUtil.window = this;
            SwitchPanel(layer);
        }

        public void SetAlpha(float alpha) => layer.UIObject.color = new Color(layer.UIObject.color.r, layer.UIObject.color.g, layer.UIObject.color.b, alpha);
        
        public void SetVolume(float volume) => ((VideoLayer) layer).player.SetDirectAudioVolume(0, volume);

        public void SetGrayScale(float grayScale)
        {  }

        public void SetPlaySpeed(float speed) => ((VideoLayer) layer).player.playbackSpeed = speed * 2;
    }
}   