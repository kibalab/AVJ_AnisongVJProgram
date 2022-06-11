using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleObject : MonoBehaviour
{
    public GameObject obj;
    public bool isOn = false;

    public void Toggle()
    {
        isOn = !isOn;

        obj.SetActive(isOn);
    }
}
