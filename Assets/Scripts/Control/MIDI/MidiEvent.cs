using AVJ.Settings;
using MidiJack;
using UnityEngine;

namespace AVJ.Control.MIDI
{
    public class Midi : IInput
    {
        public MidiChannel Channel;
        public int Note;
        public float Value;

        public Midi(MidiChannel Channel, int Note, float Value)
        {
            this.Channel = Channel;
            this.Note = Note;
            this.Value = Value;
        }
    }

    public class MidiEvent : ISettingControl
    {
        public MidiDriver.NoteOnDelegate MidiOnDelegate = null;
        public MidiDriver.NoteOffDelegate MidiOffDelegate = null;
        public MidiDriver.KnobDelegate KnobDelegate = null;
        
        public Setting setting;
        public Midi Midi;

        
        public MidiEvent(Midi Midi, Setting setting)
        {
            Setup(Midi, setting);
        }

        public void Setup(IInput Midi, Setting setting)
        {
            MidiOnDelegate += MidiOnEvent;
            MidiOffDelegate += MidiOffEvent;
            KnobDelegate += MidiRangeEvent;
            

            this.setting = setting;
            this.Midi = (Midi)Midi;
        }

        public virtual void MidiOnEvent(MidiChannel channel, int note, float velocity)
        {
            if (EventManager.BindTarget == this) Bind(channel, note);
            if (Midi.Equals(channel) && Midi.Note.Equals(note) && !EventManager.IsWaitForBinding)
            {
                Midi.Value = 1;
                setting.ChangeValue(this);
            }
        }
        
        public virtual void MidiOffEvent(MidiChannel channel, int note)
        {
            if (EventManager.BindTarget == this) Bind(channel, note);
            if (Midi.Equals(channel) && Midi.Note.Equals(note) && !EventManager.IsWaitForBinding)
            {
                Midi.Value = 0;
                setting.ChangeValue(this);
            }
        }
        
        public virtual void MidiRangeEvent(MidiChannel channel, int note, float value)
        {
            if (EventManager.BindTarget == this) Bind(channel, note);
            if (Midi.Equals(channel) && Midi.Note.Equals(note) && !EventManager.IsWaitForBinding)
            {
                Midi.Value = value;
                setting.ChangeValue(this);
            }
        }

        public void Bind(MidiChannel channel, int note)
        {
            Midi.Channel = channel;
            Midi.Note = note;
            EventManager.BindTarget = null;
        }
        
        public float GetValue() => Midi.Value;
    }
}