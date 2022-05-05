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
        public Vector2 MinMax = new Vector2(0.0f, 1.0f);
        public float Value = 0;
        private float lastYPos = 0;
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
            Value = ClampValue(Value + Camera.main.ScreenToWorldPoint(Input.mousePosition).x - lastYPos);
            lastYPos = Camera.main.ScreenToWorldPoint(Input.mousePosition).x;
            LayerImage.rectTransform.sizeDelta = new Vector2(Value * rectTransform.sizeDelta.x * MinMax.y, 0);
        }

        private float ClampValue(float value) => Mathf.Clamp(value, MinMax.x, MinMax.y);
    }
}