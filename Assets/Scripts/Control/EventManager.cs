using System.Collections;
using System.Collections.Generic;
using a;
using AVJ.Control;
using AVJ.Settings;
using UnityEngine;

public static class EventManager
{
    public static Queue<LayerEvent> LayerEvents = new Queue<LayerEvent>();

    public static Queue<CueEvent> CueEvents = new Queue<CueEvent>();

    private static IBindableHandler m_BindTarget = null;

    public static IBindableHandler BindTarget
    {
        set
        {
            if(m_BindTarget != null) m_BindTarget.LeaveBindMode();
            m_BindTarget = value;
        }

        get => m_BindTarget;
    }

    public static void Clear() => LayerEvents = new Queue<LayerEvent>();
}
