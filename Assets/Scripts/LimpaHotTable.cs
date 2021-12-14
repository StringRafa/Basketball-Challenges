using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LimpaHotTable : MonoBehaviour
{
    void EventoLimpaHotTable()
    {
        GameManager.instance.hotTable = false;
    }
}
