using System;
using System.Numerics;
using AVJ.UIElements;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Vector2 = UnityEngine.Vector2;

namespace UI.UIElements
{
    public class UISlider : UIButton
    {
        private float m_Value = 0;
        private float lastYPos = 0;

        public UnityEvent<float> OnChangeValue;

        public float Value
        {
            set
            {
                if (m_Value != value)
                {
                    m_Value = value;
                    UpdateUI();
                    OnChangeValue.Invoke(value);
                }
            }

            get => m_Value;
        }
        
        public override void Initialize()
        {
            base.Initialize();
            rectTransform = GetComponent<RectTransform>();

        }

        public override void OnMouseDown()
        {   
            lastYPos = Camera.main.ScreenToWorldPoint(Input.mousePosition).x;
        }

        private void OnMouseDrag()
        {
            Value = ClampValue(m_Value + Camera.main.ScreenToWorldPoint(Input.mousePosition).x - lastYPos);
            lastYPos = Camera.main.ScreenToWorldPoint(Input.mousePosition).x;
        }

        public void UpdateUI()
        {
            LayerImage.rectTransform.sizeDelta = new Vector2(m_Value * rectTransform.sizeDelta.x, 0);
        }

        private float ClampValue(float value) => Mathf.Clamp(value, 0, 1);
    }
}