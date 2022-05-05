using System;
using AVJ.Control.MIDI;
using Minis;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

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

        public override string ToString() => $"(MIDI {Channel.ToString()}, {Note})";
    }
    
    public class Setting : MonoBehaviour
    {
        public UnityEvent<float> OnChangeFloat;
        public UnityEvent<bool> OnChangeBool;

        public Midi bind = null;

        private void Start()
        {
            SetMidiBind();
        }

        public void OnChangeValue(float value)
        {
            OnChangeFloat.Invoke(value);
            OnChangeBool.Invoke(value > 0.5f);
        }
        
        private void ChangeValue(float value)
        {
            OnChangeValue(value);
        }

        public void EnterBindMode() => EventManager.BindTarget = this;

        public void SetMidiBind()
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
                    if (this.Equals(EventManager.BindTarget))
                    {
                        bind = new Midi((note.device as Minis.MidiDevice)?.channel, note.noteNumber, 0);
                        EventManager.BindTarget = null;
                        return;
                    }
                    
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

                midiDevice.onWillNoteOff += (note) => {
                    
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