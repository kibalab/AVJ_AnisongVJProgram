using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace AVJ.Control
{
    public static class DataFileManager
    {
        public static string SaveDirFormat = "/SerializeData/%s.%s";

        public static void Save(string name, object obj, string format) => m_Save(name, obj, format);
        public static void Save(string name, object obj) => m_Save(name, obj, "dat");
        public static void Save(Object obj, string format) => m_Save(obj.name , obj, format);
        public static void Save(Object obj) => m_Save(obj.name , obj, "dat");

        public static object Load(string name, string format) => m_Load(name, format);
        public static object Load(string name) => m_Load(name, "dat");

        
        private static void m_Save(string name, object obj, string format)
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + String.Format(SaveDirFormat, name, format), FileMode.OpenOrCreate);
            bf.Serialize(file, obj);
            file.Close();
        }

        
        private static object m_Load(string name, string format)
        {
            if (File.Exists (Application.persistentDataPath + String.Format(SaveDirFormat, name, format))) {
                BinaryFormatter bf = new BinaryFormatter ();
                FileStream file = File.Open(Application.persistentDataPath + String.Format(SaveDirFormat, name, format), FileMode.Open);
                var serializableSaveData = bf.Deserialize(file);
                file.Close();
                return(serializableSaveData);
            } else {
                return(null);
            }
        }
    }
}