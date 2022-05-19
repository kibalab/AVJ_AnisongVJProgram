using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace AVJ.Data
{
    [Serializable]
    public class SerializableVector2
    {
        [SerializeField] public float x;
        [SerializeField] public float y;
        
        public SerializableVector2(Vector2 data)
        {
            SetVector(data);
        }
        
        public SerializableVector2(float x, float y)
        {
            this.x = x;
            this.y = y;
        }

        public void SetVector(Vector2 data)
        {
            x = data.x;
            y = data.y;
        }
        
        public static implicit operator SerializableVector2(Vector2 node)
        {
            return new SerializableVector2(node);
        }
        
        public static implicit operator SerializableVector2(Vector3 node)
        {
            return new SerializableVector2(node);
        }
        
        public static implicit operator SerializableVector2(Vector4 node)
        {
            return new SerializableVector2(node);
        }
        
        public static implicit operator Vector2(SerializableVector2 node)
        {
            return new Vector2(node.x, node.y);
        }
    }
}