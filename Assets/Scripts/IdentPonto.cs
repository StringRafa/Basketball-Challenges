using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdentPonto : MonoBehaviour
{
    public static IdentPonto instance;

    [SerializeField]
    private AudioSource audioS;
    [SerializeField]
    private AudioClip clip;



   public void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("bola") || col.gameObject.CompareTag("bolaclone"))
        {
           ShootScript.fezPonto = true;
           TocaAudio.TocadordeAudio(audioS, clip);
        }
    }
}
