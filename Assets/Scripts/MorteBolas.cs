using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MorteBolas : MonoBehaviour
{
    //Variaveis Morte
    public static MorteBolas instance;

    [SerializeField]
    public float vidaBola = 1f;
    [SerializeField]
    public Color cor;
    [SerializeField]
    public Renderer bolaRender;
    [SerializeField]
    private bool tocouChao = false;
    void Start()
    {
        bolaRender = gameObject.GetComponent<Renderer>();
        cor = bolaRender.material.GetColor("_Color");
    }

    private void Update()
    {
        if (tocouChao == true)
        {
            MataBola();
        }
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("chao"))
        {
            tocouChao = true;
        }
    }


    public void MataBola()
    {
        if (vidaBola > 0)
        {
            vidaBola -= Time.deltaTime;
            bolaRender.material.SetColor("_Color", new Color(cor.r, cor.g, cor.b, vidaBola));
        }

        if (vidaBola <= 0)
        {
            GameManager.instance.bolaEmCena = false;
            Destroy(gameObject);
            
            if (gameObject.CompareTag("bola"))
            {
                GameManager.instance.numJogadas--;
                UIManager.instance.numBolas.text = GameManager.instance.numJogadas.ToString();

                if (GameManager.instance != null && GameManager.instance.numJogadas > 0)
                {
                    GameManager.instance.NascBolas();
                }
            }        
        }
    }

}
