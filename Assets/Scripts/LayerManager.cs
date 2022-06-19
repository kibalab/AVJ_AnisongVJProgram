using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using AVJ.UIElements;
using B83.Win32;
using Shibuya24.Utility;
using UnityEngine;
using UnityEngine.UI;
using System.Text.RegularExpressions;

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
    public GameObject LayerGroupPrefab;
    public GameObject TimelinePrefab;
    public Transform WindowField;

    Regex rx = new Regex(".mp4|.mov");

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

        AddedLayer.Data.sourcePath = path;
        AddedLayer.Data.rectTransform = (RectTransform)AddedLayer.transform;

        var timeline = Instantiate(TimelinePrefab, TimelineField.transform);
        var timelineConponent = timeline.GetComponent<Timeline>();
        
        timelineConponent.layer = AddedLayer;
        
        UIUtility.InitializeUI(timelineConponent);
        UIUtility.InitializeUI(AddedLayer);

        AddedLayer.ParentRatio = 2.856362f;

        return AddedLayer;
    }

    public TimelineGroup AddGroup(string name)
    {
        var timeline = Instantiate(TimelinePrefab, TimelineField.transform);
        var groupLayer = Instantiate(new GameObject(), RenderField.transform);

        timeline.gameObject.name = name;
        
        var AddedGroup = timeline.AddComponent<TimelineGroup>();
        
        AddedGroup.Canvas = groupLayer.AddComponent<CanvasGroup>();
        
        UIUtility.InitializeUI(AddedGroup);

        return AddedGroup;
    }

    void OnEnable ()
    {
        string operatingSystem = SystemInfo.operatingSystem;
        Debug.Log("OS: " + operatingSystem);

        if (operatingSystem.Contains("Windows"))
        {
            // must be installed on the main thread to get the right thread id.
            UnityDragAndDropHook.InstallHook();
            UnityDragAndDropHook.OnDroppedFiles += OnFilesWindows;
        }
        else if (operatingSystem.Contains("Mac"))
        {
            OnFilesMacOS(@"/Users/bada/Movies/2022-04-10 06-58-27.mov");
            //TODO 여러 파일 지원해야 함. 현재 2개 이상 파일을 올릴 경우 튕김
            UniDragAndDrop.onDragAndDropFilePath = x =>
            {
                OnFilesMacOS(x);
            };

            UniDragAndDrop.Initialize();
        }
    }
    void OnDisable()
    {
        UnityDragAndDropHook.UninstallHook();
    }
    void OnFilesWindows(List<string> aFiles, POINT aPos)
    {
        string str = "Dropped " + aFiles.Count + " files at: " + aPos + "\n\t" +
                     aFiles.Aggregate((a, b) => a + "\n\t" + b);
        Debug.Log(str);


        foreach (var afile in aFiles)
        {
            if (rx.IsMatch(Path.GetExtension(afile)))
                    AddLayer<VideoLayer>(Path.GetFileName(afile), afile);
            else
                AddLayer<ImageLayer>(Path.GetFileName(afile), afile);
        }
    }
    void OnFilesMacOS(string afile) {
        
        string str = "Dropped a file at: " + afile;
        Debug.Log(str);

        { // 초기 테스트 (복수 파일은 API 수정 필요해 보임)
            if (rx.IsMatch(Path.GetExtension(afile)))
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
        if(EventManager.LayerEvents.Count <= 0) return;
        
        var layerEvent = EventManager.LayerEvents.Dequeue();

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
