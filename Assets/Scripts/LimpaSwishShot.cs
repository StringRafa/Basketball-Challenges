using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LimpaSwishShot : MonoBehaviour
{
  void EventoLimpaSwishShot()
    {
        GameManager.instance.swishShot = false;
    }
}
