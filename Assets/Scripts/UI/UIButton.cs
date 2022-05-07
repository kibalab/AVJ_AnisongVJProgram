using System;
using System.Collections;
using System.Collections.Generic;
using AVJ.Control;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace AVJ.UIElements
{
    public class UIButton : UIBehaviour, IUIInitializer
    {
        public Image LayerImage;
        public RectTransform rectTransform;
        private BoxCollider2D collider;

        public Color ClickedColor;
        public Color HoveredColor;
        public Color OriginColor;

        public UnityEvent OnClick;

        public bool isClicked = false;

        void Start() => UIUtility.InitializeUI(this);

        public virtual void Initialize()
        {
            if (OnClick == null)
                OnClick = new UnityEvent();
            if (!LayerImage) LayerImage = SetComponent<Image>();
            if (!collider) collider = SetComponent<BoxCollider2D>();

            OriginColor = LayerImage.color;
        }

        public virtual void OnMouseDown()
        {
            isClicked = true;
            LayerImage.color = ClickedColor;
        }

        public virtual void OnMouseUp()
        {
            isClicked = false;
            LayerImage.color = OriginColor;

            Click();
        }

        public virtual void OnMouseOver()
        {
            if (isClicked) return;
            LayerImage.color = HoveredColor;
        }

        public virtual void OnMouseEnter()
        {
        }

        public virtual void OnMouseExit()
        {
            LayerImage.color = OriginColor;
        }
        
        public virtual void Click() => OnClick.Invoke();
        

        public T SetComponent<T>() where T : Component
        {
            Debug.Log($"[Layer, {gameObject.name}] Add Component : {typeof(T).Name}");
            var component = GetComponent<T>();
            if (component == null)
            {
                component = gameObject.AddComponent<T>();
            }

            return component;
        }
    }
}