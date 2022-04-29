using AVJ.Settings;
using MidiJack;

namespace AVJ.MIDI
{
    public class BindMidi : MidiEvent
    {
        public BindMidi(Midi Midi, Setting setting) : base(Midi, setting)
        {
            Setup(Midi, setting);
        }

        public void Bind(MidiChannel channel, int note)
        {
            var midi = new Midi();
            midi.Channel = channel;
            midi.Note = note;
            
            EventManager.InputEvent.Add(new MidiEvent(midi, setting));
        }

        public override void MidiOffEvent(MidiChannel channel, int note) => Bind(channel, note);
        public override void MidiRangeEvent(MidiChannel channel, int note, float value) => Bind(channel, note);
        public override void MidiOnEvent(MidiChannel channel, int note, float velocity) => Bind(channel, note);
    }
}