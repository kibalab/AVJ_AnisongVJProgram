using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timeline : MonoBehaviour
{
    public Layer layer;

    public Text Title;
    public RawImage line;

    public RawImage View;
    
    // Start is called before the first frame update
    void Start()
    {
        Title.text = $"[{layer.Type.ToString()}] {layer.gameObject.name}";

        DrawLine();
    }

    public void DrawLine()
    {
    }

    public void DeleteLayer()
    {
        var layerEvent = new LayerEvent();
        layerEvent.EventType = LayerEventType.Delete;
        layerEvent.layer = layer;
        
        EventManager.Events.Enqueue(layerEvent);
        
        Destroy(gameObject);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (layer.Type == LayerType.Video)
        {
            View.texture = layer.LayerImage.texture;
        }
    }
}
