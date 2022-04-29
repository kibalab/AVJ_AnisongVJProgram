using AVJ.MIDI;
using UnityEngine;
using UnityEngine.Events;

namespace AVJ.Settings
{
    public class Setting : MonoBehaviour
    {
        public UnityEvent<float> OnChange;
        public ISettingControl control;

        public void OnChangeValue(float value)
        {
            
        }
        
        public void ChangeValue(ISettingControl Control)
        {
            var value = Control.GetValue();
            OnChangeValue(value);
            OnChange.Invoke(value);
        }
    }
}