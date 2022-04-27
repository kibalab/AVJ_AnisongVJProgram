using AVJ.UIElements;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;

namespace DefaultNamespace
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
        
            Debug.Log($"[Window, {gameObject.name}] Initializing Window");
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