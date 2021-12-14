using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LimpaRimShot : MonoBehaviour
{
   void EventoLimpaRimShot()
    {
        GameManager.instance.rimShot = false;
    }
}
