using System;
using AVJ.UIElements;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.Windows;
using Input = UnityEngine.Input;

namespace UI
{
    public class UIInputField : UIButton
    {
        public Text InputText;
        public UnityEvent OnChangeInput;
        public UnityEvent OnEndInput;

        public override void Click()
        {
            base.Click();
            EventManager.SoloInputable = this;
        }

        private void Update()
        {
            if(EventManager.SoloInputable != this) return;

            if (Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.V))
            {
                
            }
            
            InputText.text += Input.inputString;
        }
    }
}