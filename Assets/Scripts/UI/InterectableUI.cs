using System;
using System.Collections;
using System.Collections.Generic;
using AVJ;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;

namespace AVJ.UIElements
{
    public class InterectableUI : UIBehaviour, IDragDropHandler
    {
        public Graphic UIObject;

        public Collider2D collider;
        
        public RectTransform rectTransform;

        public bool IsSelected = false;
        public bool IsHovered = false;

        public bool IsReady = false;
        
        public Vector2 clickedPosition;

        public float MouseSensitivity = 108.0f;

        #region Properties
    
        public Vector2 Size 
        {
            set
            {
                Debug.Log(value);
                ((BoxCollider2D)collider).size = value;
                rectTransform.sizeDelta = value;
                UIObject.rectTransform.sizeDelta = value;
            }
            get => rectTransform.sizeDelta;
        }
    
        #endregion
        
        protected override void OnEnable()
        {
            rectTransform = (RectTransform) transform;

            IsReady = true;
        }

        private void OnMouseDown()
        {
            IsSelected = true;
            collider.enabled = false;
            clickedPosition = rectTransform.localPosition - Camera.main.ScreenToWorldPoint(Input.mousePosition) * MouseSensitivity;
            
            OnUIDrag(this);
        }

        private void OnMouseUp()
        {
            IsSelected = false;
            collider.enabled = true;
            clickedPosition = new Vector2(0, 0);
            OnUIDrop(this);
        }

        private void OnMouseEnter()
        {
            IsHovered = true;
            UIControl.HoveredUIs.Add(this);
            OnUIEnter();
        }

        private void OnMouseExit()
        {
            IsHovered = false;
            UIControl.HoveredUIs.Remove(this);
            OnUIExit();
        }

        private void OnUIDropOnSelf(InterectableUI UIComponent)
        {
            
        }

        public virtual void OnUIEnter() { }
        public virtual void OnUIExit() { }

        #region Interface Methods

        public virtual void OnUIDrag(IDragDropHandler UIConponent) { }

        public virtual void OnUIDrop(IDragDropHandler UIConponent) { }

        public void Update()
        {
            if (UIObject)
            {
                UIObject.rectTransform.pivot = rectTransform.pivot;
                UIObject.rectTransform.localPosition = rectTransform.localPosition;
            }
            
            if (IsSelected)
            {
                rectTransform.localPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition) * MouseSensitivity + new Vector3(clickedPosition.x, clickedPosition.y, 0);
                rectTransform.localPosition -= new Vector3(0, 0, rectTransform.localPosition.z);
            }
        }

        #endregion
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

