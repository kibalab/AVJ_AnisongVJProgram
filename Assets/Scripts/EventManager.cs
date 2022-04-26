using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class EventManager
{
    public static Queue<LayerEvent> Events = new Queue<LayerEvent>();

    public static void Clear() => Events = new Queue<LayerEvent>();
}
