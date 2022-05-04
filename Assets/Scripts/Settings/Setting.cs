using System;
using AVJ.Control.MIDI;
using MidiJack;
using UnityEngine;
using UnityEngine.Events;

namespace AVJ.Settings
{
    public class Setting : MonoBehaviour
    {
        public UnityEvent<float> OnChange;
        public ISettingControl control;

        private void Start()
        {
            control = new BindMidi(null, this);
        }

        public void OnChangeValue(float value)
        {
            OnChange.Invoke(value);
        }
        
        public void ChangeValue(ISettingControl Control)
        {
            var value = Control.GetValue();
            OnChangeValue(value);
        }

        public void EnterBindMode() => EventManager.BindTarget = control;
    }
}