using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using AVJ.UIElements;
using B83.Win32;
using UnityEngine;
using UnityEngine.UI;

public enum LayerType
{
    Image,
    Video,
    Layer
}

public class LayerManager : MonoBehaviour
{
    public GameObject LayerField;
    public GameObject RenderField;
    public GameObject TimelineField;
    
    public GameObject LayerPrefab;
    public GameObject TimelinePrefab;
    public Transform WindowField;


    public Layer AddLayer<T>(string name, string path) where T : Layer
    {
        var layerObject = Instantiate(LayerPrefab, LayerField.transform);
        var LayerScreen = Instantiate(new GameObject(), RenderField.transform);

        layerObject.gameObject.name = name;

        var LayerImage = LayerScreen.AddComponent<RawImage>();

        var AddedLayer = layerObject.AddComponent<T>();;

        AddedLayer.UIObject = LayerImage;

        if (typeof(T) == typeof(VideoLayer)) AddedLayer.media = path;
        else AddedLayer.media = LoadImage(path);

        var timeline = Instantiate(TimelinePrefab, TimelineField.transform);
        var timelineConponent = timeline.GetComponent<Timeline>();
        
        timelineConponent.layer = AddedLayer;
        
        UIUtility.InitializeUI(timelineConponent);
        UIUtility.InitializeUI(AddedLayer);

        return AddedLayer;
    }

    void OnEnable ()
    {
        // must be installed on the main thread to get the right thread id.
        UnityDragAndDropHook.InstallHook();
        UnityDragAndDropHook.OnDroppedFiles += OnFiles;
    }
    void OnDisable()
    {
        UnityDragAndDropHook.UninstallHook();
    }
    void OnFiles(List<string> aFiles, POINT aPos)
    {
        string str = "Dropped " + aFiles.Count + " files at: " + aPos + "\n\t" +
                     aFiles.Aggregate((a, b) => a + "\n\t" + b);
        Debug.Log(str);
        
        foreach (var afile in aFiles)
        {
            if(Path.GetExtension(afile) == ".mp4")
                AddLayer<VideoLayer>(Path.GetFileName(afile), afile);
            else
                AddLayer<ImageLayer>(Path.GetFileName(afile), afile);
        }
    }
    
    private Texture2D LoadImage(string path)

    {

        byte[] byteTexture = System.IO.File.ReadAllBytes(path);

        Texture2D texture = new Texture2D(0, 0);

        texture.LoadImage(byteTexture);

        return texture;
    }

    private void LateUpdate()
    {
        if(EventManager.Events.Count <= 0) return;
        
        var layerEvent = EventManager.Events.Dequeue();

        switch (layerEvent.EventType)
        {
            case LayerEventType.Delete :
                Destroy(layerEvent.layer.gameObject);
                break;
            default:
                Debug.Log("[LayerManagher] Can not run for event");
                break;
        }
    }
}
