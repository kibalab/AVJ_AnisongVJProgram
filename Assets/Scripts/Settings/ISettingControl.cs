using AVJ.Control;

namespace AVJ.Settings
{
    public interface ISettingControl
    {
        public float GetValue();
        public void Setup(IInput Midi, Setting setting);
    }
}