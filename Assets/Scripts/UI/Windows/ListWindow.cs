using System.Collections;
using System.Collections.Generic;
using AVJ;
using UnityEngine;

public class ListWindow : Window
{
    public LayerManager LayerManager;
    public void AddGroup()
    {
        LayerManager.AddGroup($"InstanceName({Random.Range(0,9999)})");
    }
}
