using System;
using System.Collections.Generic;
using AVJ.UIElements;
using UI;
using UnityEngine;

namespace AVJ
{
    public class CueWindow : Window
    {
        public Layer layer;
        public GameObject CueListPrefab;
        public GameObject CuePanel;

        private List<CueListElement> Elements = new List<CueListElement>();


        public void Initialize()
        {
            base.Initialize();

            ClearCueListElements();
            DisplayCueList(layer.Data.CuePoints);
        }

        public void DisplayCueList(List<CueData> points)
        {

            foreach (var point in points)
            {
                var element = SpawnCueElement(point);
                Elements.Add(element);
            }
            
        }

        public void ClearCueListElements()
        {
            for (var i = 0; i < Elements.Count; i++)
            {
                if(Elements[i]) Destroy(Elements[i].gameObject);
            }
            Elements.Clear();
        }

        public CueListElement SpawnCueElement(CueData cuePoint)
        {
            var elementObject = Instantiate(CueListPrefab, CuePanel.transform);
            var element = elementObject.GetComponent<CueListElement>();
            cuePoint.Element = element;
            element.Cue = cuePoint;
            element.layer = layer;
            return element;
        }
    }
}