using System;
using Klak.Spout;
using UnityEngine;

namespace AVJ.Control
{
    public class StreamManager : MonoBehaviour
    {
        public SpoutSender Spout;
        private void OnEnable()
        {
#if PLATFORM_STANDALONE_WIN || UNITY_EDITOR_WIN
            Spout.enabled = true;
#else
            Syphon.enabled = true;
#endif
        }
    }
}