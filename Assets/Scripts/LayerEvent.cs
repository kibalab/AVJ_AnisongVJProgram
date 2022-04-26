using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum LayerEventType
{
    Delete,
    Effect
}

public struct LayerEvent
{
    public LayerEventType EventType;
    public Layer layer;
}
