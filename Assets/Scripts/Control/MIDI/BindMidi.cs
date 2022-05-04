using AVJ.Settings;
using AVJ.UIElements;
using MidiJack;

namespace AVJ.Control.MIDI
{
    public class BindMidi : MidiEvent
    {
        public BindMidi(Midi Midi, Setting setting) : base(Midi, setting)
        {
            Setup(Midi, setting);
        }

        public void Bind(MidiChannel channel, int note)
        {
            var midi = new Midi(channel, note, 0);
            
            EventManager.InputEvent.Add(this);
        }
    }
}