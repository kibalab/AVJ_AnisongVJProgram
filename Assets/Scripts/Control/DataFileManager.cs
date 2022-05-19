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
        public static string SaveDir = "SerializeData";
        public static string SaveFileFormat = "{0}.{1}";

        
        public static void Save(string name, object obj, string format) => m_Save(name, obj, format);
        public static void Save(string name, object obj) => m_Save(name, obj, "dat");
        public static void Save(Object obj, string format) => m_Save(obj.name , obj, format);
        public static void Save(Object obj) => m_Save(obj.name , obj, "dat");

        public static T Load<T>(string name, string format) => (T) m_Load(name, format);
        public static T Load<T>(string name) => (T) m_Load(name, "dat");

        
        private static void m_Save(string name, object obj, string format)
        {
            FileStream file = null;
            try {
                BinaryFormatter bf = new BinaryFormatter();
                file = File.Open(Application.persistentDataPath + '/' + SaveDir + '/' + String.Format(SaveFileFormat, name, format), FileMode.OpenOrCreate);
                bf.Serialize(file, obj);
                file.Close();
            } catch(Exception exp) {
                Debug.LogError(exp);
            } finally {
                if (file != null) {
                    file.Close();
                }
            }
        }

        
        private static object m_Load(string name, string format)
        {
            FileStream file = null;
            try {
                if (File.Exists (Application.persistentDataPath + String.Format(SaveDir, name, format))) {
                    BinaryFormatter bf = new BinaryFormatter ();
                    file = File.Open(Application.persistentDataPath + '/' + SaveDir + '/' + String.Format(SaveFileFormat, name, format), FileMode.Open);
                    var serializableSaveData = bf.Deserialize(file);
                    file.Close();
                    return(serializableSaveData);
                } else {
                    return(null);
                }
            } catch(Exception exp) {
                Debug.LogError(exp);
                return(null);
            } finally {
                if (file != null) {
                    file.Close();
                }
            }
        }
    }
}