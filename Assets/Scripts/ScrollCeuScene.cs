using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollCeuScene : MonoBehaviour
{
    public RawImage back;

    private void Start()
    {
        back = GetComponent<RawImage>();
    }

    void Update()
    {

        back.uvRect = new Rect(0.01f * Time.time, 0, 1, 1);
   

    }
}
