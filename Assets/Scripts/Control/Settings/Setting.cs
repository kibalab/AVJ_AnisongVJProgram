using System;
using AVJ.Control;
using Minis;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.UIElements;

namespace AVJ.Settings
{
    public class Midi
    {
        public int Channel;
        public int Note;
        public float Value;

        public Midi(int? Channel, int Note, float Value)
        {
            this.Channel = (int) Channel;
            this.Note = Note;
            this.Value = Value;
        }

        public bool IsEqual(MidiNoteControl note) =>
            Channel == (note.device as Minis.MidiDevice)?.channel && Note == note.noteNumber;
        
        public bool IsEqual(MidiValueControl note) =>
            Channel == (note.device as Minis.MidiDevice)?.channel && Note == note.controlNumber;

        public override string ToString() => $"(MIDI {Channel.ToString()}, {Note})";
    }
    
    public class Setting : MonoBehaviour, IBindableHandler
    {
        public String SettingName = "";
        public Text ValueState;
        public Text BindState;
        public UnityEvent<float> OnChangeFloat = new UnityEvent<float>();
        public UnityEvent<bool> OnChangeBool = new UnityEvent<bool>();

        public Midi bind = new Midi(-1, -1, 0);

        private void Start()
        {
            SetMidiBind();
        }

        public void OnChangeValue(float value)
        {
            if(value == null) return;
            OnChangeFloat.Invoke(value);
            OnChangeBool.Invoke(value > 0.5f);
            if(ValueState) ValueState.text = $"{SettingName} ({(value * 100).ToString("000.0")}%)";
        }
        
        private void ChangeValue(float value)
        {
            OnChangeValue(value);
        }

        public void EnterBindMode()
        {
            BindState.text = "=";
            EventManager.BindTarget = this;
        }

        public void LeaveBindMode()
        {
            BindState.text = $"{bind.Note}";
        }
        

        private void SetMidiBind()
        {
            InputSystem.onDeviceChange += (device, change) =>
            {
                if (change != InputDeviceChange.Added) return;

                var midiDevice = device as Minis.MidiDevice;
                if (midiDevice == null) return;

                midiDevice.onWillNoteOn += (note, velocity) => {
                    // Note that you can't use note.velocity because the state
                    // hasn't been updated yet (as this is "will" event). The note
                    // object is only useful to specify the target note (note
                    // number, channel number, device name, etc.) Use the velocity
                    // argument as an input note velocity.
                    
                    if(!bind.IsEqual(note)) return;
                    
                    Debug.Log(string.Format(
                        "Note On #{0} ({1}) vel:{2:0.00} ch:{3} dev:'{4}'",
                        note.noteNumber,
                        note.shortDisplayName,
                        velocity,
                        (note.device as Minis.MidiDevice)?.channel,
                        note.device.description.product
                    ));
                    ChangeValue(1);
                };

                midiDevice.onWillControlChange += (note, value) =>
                {
                    if (this.Equals(EventManager.BindTarget))
                    {
                        bind = new Midi((note.device as Minis.MidiDevice)?.channel, note.controlNumber, 0);
                        EventManager.BindTarget = null;
                        BindState.text = note.controlNumber.ToString();
                        return;
                    }

                    if (!bind.IsEqual(note)) return;
                    Debug.Log(string.Format(
                        "Control Change !{0} ({1}) val:{2:0.00} ch:{3} dev:'{4}'",
                        note.controlNumber,
                        note.shortDisplayName,
                        value,
                        (note.device as Minis.MidiDevice)?.channel,
                        note.device.description.product
                    ));
                    ChangeValue(value);
                };

                midiDevice.onWillNoteOff += (note) => {
                    
                    if (this.Equals(EventManager.BindTarget))
                    {
                        bind = new Midi((note.device as Minis.MidiDevice)?.channel, note.noteNumber, 0);
                        EventManager.BindTarget = null;
                        BindState.text = note.noteNumber.ToString();
                        return;
                    }
                    
                    if(!bind.IsEqual(note)) return;
                    
                    Debug.Log(string.Format(
                        "Note Off #{0} ({1}) ch:{2} dev:'{3}'",
                        note.noteNumber,
                        note.shortDisplayName,
                        (note.device as Minis.MidiDevice)?.channel,
                        note.device.description.product
                    ));
                    ChangeValue(0);
                };
            };
        }
    }
}