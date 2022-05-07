using System.Collections;
using System.Collections.Generic;
using AVJ.Control;
using AVJ.Settings;
using UnityEngine;

public static class EventManager
{
    public static Queue<LayerEvent> Events = new Queue<LayerEvent>();

    /*
    public static bool IsWaitForBinding
    {
        get => BindTarget == null;
    }
    
    public static MidiEvent BindTarget = null;
    */
    
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

    public static void Clear() => Events = new Queue<LayerEvent>();
}
