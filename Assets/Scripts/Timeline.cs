using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timeline : MonoBehaviour
{
    public Layer layer;

    public Text Title;
    public RawImage line;
    
    // Start is called before the first frame update
    void Start()
    {
        Title.text = $"[{layer.Type.ToString()}] {layer.gameObject.name}";

        DrawLine();
    }

    public void DrawLine()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
