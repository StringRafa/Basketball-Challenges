using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LimpaSkyHook : MonoBehaviour
{
    void EventoLimpaSkyHook()
    {
        GameManager.instance.skyHook = false;
    }
}
