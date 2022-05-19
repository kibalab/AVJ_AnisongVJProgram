using System;
using Klak.Spout;
using Klak.Syphon;
using UnityEngine;

namespace AVJ.Control
{
    public class StreamManager : MonoBehaviour
    {
        public SpoutSender Spout;
        public SyphonServer Syphon;
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