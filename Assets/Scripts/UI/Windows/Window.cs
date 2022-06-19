using AVJ.UIElements;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;

namespace AVJ
{
    public class Window : InterectableUI, IUIInitializer
    {
        private bool isFold = false;

        protected override void Start()
        {
            Initialize();
        }

        public void Initialize()
        {
            InitWindow();
        }
        
        public void InitWindow()
        {
            if (!UIObject) UIObject = SetComponent<RawImage>();
            if (!collider) collider = SetComponent<BoxCollider2D>();
            
            Size = rectTransform.sizeDelta;
        
            Debug.Log($"[Window, {gameObject.name}] Initializing Window");
        }

        public override void OnUIDrag(IDragDropHandler UIConponent)
        {
            rectTransform.SetAsLastSibling();
        }

        public bool IsFold
        {
            set
            {
                isFold = false;
            }
            get => isFold;
        }
    }
}