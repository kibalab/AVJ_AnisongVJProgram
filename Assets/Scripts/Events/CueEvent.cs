using AVJ;

namespace a
{
    public enum CueEventType
    {
        Delete,
        Move,
        Add
    }
    public class CueEvent
    {
        public CueData Cue;
        public CueEventType Type;
    }
}