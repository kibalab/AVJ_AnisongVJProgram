

using System;
using System.Collections.Generic;
using AVJ.UIElements;
using UnityEngine.EventSystems;

namespace AVJ
{
    public class UIControl
    {
        private static InterectableUI m_Selected;
        public static InterectableUI LastSelected;

        public static List<InterectableUI> HoveredUIs = new List<InterectableUI>();
        
        public static InterectableUI CurrentSelected
        {
            set
            {
                LastSelected = m_Selected;
                m_Selected = value;
            }

            get => m_Selected;
        }
    }
}