using AVJ.Settings;
using MidiJack;
using UnityEngine;

namespace AVJ.MIDI
{
    public struct Midi
    {
        public MidiChannel Channel;
        public int Note;
        public float Value;
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

        public void Setup(Midi Midi, Setting setting)
        {
            MidiOnDelegate += MidiOnEvent;
            MidiOffDelegate += MidiOffEvent;
            KnobDelegate += MidiRangeEvent;
            

            this.setting = setting;
            this.Midi = Midi;
        }

        public virtual void MidiOnEvent(MidiChannel channel, int note, float velocity)
        {
            if (Midi.Equals(channel) && Midi.Note.Equals(note) && !EventManager.isWaitForBinding)
            {
                Midi.Value = 1;
                setting.ChangeValue(this);
            }
        }
        
        public virtual void MidiOffEvent(MidiChannel channel, int note)
        {
            if (Midi.Equals(channel) && Midi.Note.Equals(note) && !EventManager.isWaitForBinding)
            {
                Midi.Value = 0;
                setting.ChangeValue(this);
            }
        }
        
        public virtual void MidiRangeEvent(MidiChannel channel, int note, float value)
        {
            if (Midi.Equals(channel) && Midi.Note.Equals(note) && !EventManager.isWaitForBinding)
            {
                Midi.Value = value;
                setting.ChangeValue(this);
            }
        }
        
        public float GetValue() => Midi.Value;
    }
}