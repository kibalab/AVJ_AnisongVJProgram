using AVJ.Settings;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Layouts;

namespace AVJ.Control.MIDI
{
    /*
    public class Midi : IInput
    {
        public int Channel;
        public int Note;
        public float Value;

        public Midi(int Channel, int Note, float Value)
        {
            this.Channel = Channel;
            this.Note = Note;
            this.Value = Value;
        }

        public override string ToString() => $"(MIDI {Channel.ToString()}, {Note})";
    }

    public class MidiEvent// : ISettingControl
    {
        public Setting setting;
        public Midi Midi;
        
        public MidiEvent(Midi Midi, Setting setting)
        {
            Setup(Midi, setting);
        }

        public void Setup(IInput Midi, Setting setting)
        {
                        this.setting = setting;
            this.Midi = (Midi)Midi;
        }
        
        public void MidiOnEvent(int channel, int note, float velocity)
        {
            if (EventManager.BindTarget == this) Bind(channel, note);
            if (Midi.Equals(channel) && Midi.Note.Equals(note) && !EventManager.IsWaitForBinding)
            {
                Midi.Value = 1;
                setting.ChangeValue(this);
            }
        }
        
        public void MidiOffEvent(int channel, int note)
        {
            if (EventManager.BindTarget == this) Bind(channel, note);
            if (Midi.Equals(channel) && Midi.Note.Equals(note) && !EventManager.IsWaitForBinding)
            {
                Midi.Value = 0;
                setting.ChangeValue(this);
            }
        }
        
        public void MidiRangeEvent(int channel, int note, float value)
        {
            Debug.Log($"[MidiEvent] Get Input {Midi.ToString()} : {value}");
            if (EventManager.BindTarget == this) Bind(channel, note);
            if (Midi.Equals(channel) && Midi.Note.Equals(note) && !EventManager.IsWaitForBinding)
            {
                Midi.Value = value;
                setting.ChangeValue(this);
            }
        }

        public void Bind(int channel, int note)
        {
            Midi.Channel = channel;
            Midi.Note = note;
            EventManager.BindTarget = null;
        }
        
        public float GetValue() => Midi.Value;
        
    }*/
}