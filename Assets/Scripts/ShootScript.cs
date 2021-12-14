using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ShootScript : MonoBehaviour
{

    private Vector2 startPos;
    private bool tiro = false, mirando = false, bateuAro = false, bateuTabela = false, liberaSky;
    [SerializeField]
    private GameObject dotsGo;
    private List<GameObject> caminho;

    [SerializeField]
    private Rigidbody2D myRBody;
    [SerializeField]
    private Collider2D myCollider;

    //Variaveis aux
    [SerializeField]
    private float valorX, valorY, forca = 2.0f;

    //Marcou Ponto
    public static bool fezPonto;
    [SerializeField]
    private Animator anim, anim1, anim2, anim3,anim10PT, anim50PT, anim100PT, anim150PT, anim300PT;


    void Start()
    {
        //Animação nome do pontos
        anim = GameObject.FindWithTag("rimTxt").GetComponent<Animator>();
        anim1 = GameObject.FindWithTag("SwishTxt").GetComponent<Animator>();
        anim2 = GameObject.FindWithTag("SkyTxt").GetComponent<Animator>();
        anim3 = GameObject.FindWithTag("HotTxt").GetComponent<Animator>();

        //Animação valor dos pontos 
        anim50PT = GameObject.FindWithTag("50Pontos").GetComponent<Animator>();
        anim100PT = GameObject.FindWithTag("100Pontos").GetComponent<Animator>();
        anim150PT = GameObject.FindWithTag("150Pontos").GetComponent<Animator>();
        anim300PT = GameObject.FindWithTag("300Pontos").GetComponent<Animator>();
        anim10PT = GameObject.FindWithTag("10Pontos").GetComponent<Animator>();


        fezPonto = false;
        liberaSky = false;
        dotsGo = GameObject.FindWithTag("dots");
        myRBody.isKinematic = true;
        myCollider.enabled = false;
        startPos = transform.position;
        caminho = dotsGo.transform.Cast<Transform>().ToList().ConvertAll(t => t.gameObject);
        for (int x = 0; x < caminho.Count; x++)
        {
            caminho[x].GetComponent<Renderer>().enabled = false;
        }

    }

    private void FixedUpdate()
    {
        if (GameManager.instance.jogoExecutando == true)
        {
            Vector2 wp = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(wp, Vector2.zero);

            if (hit.collider == null)
            {
                if (!myRBody.gameObject.CompareTag("bolaclone"))
                {
                    Mirando();
                }
            }
        }
    }

    private void Update()
    {
        if (GameManager.instance.jogoExecutando == true)
        {

            if (!myRBody.isKinematic)
            {
                if (bateuTabela == false)
                {
                    //RimShot
                    if (bateuAro == true && fezPonto == true && liberaSky == false)
                    {
                        GameManager.instance.rimShot = true;
                        print("RimShot");
                        GameManager.instance.moedasIntSave += GameManager.instance.pontos + 50;
                        UIManager.instance.moedasUI.text = (GameManager.instance.moedasIntSave).ToString();
                        fezPonto = false;
                        //Instantiate(IdentPonto.instance.pontosImg,transform.position,Quaternion.identity);
                        GameManager.instance.desafioNum1RimShot--;
                        GameManager.instance.DesafioDeFase(OndeEstou.instance.fase);
                        anim50PT.Play("50PontosAnim");
                        anim.Play("RimShotAnim");

                    }
                    //SwishShot
                    else if (fezPonto == true && liberaSky == false && bateuAro == false)
                    {
                        GameManager.instance.swishShot = true;
                        print("swishShot");
                        GameManager.instance.moedasIntSave += GameManager.instance.pontos + 100;
                        UIManager.instance.moedasUI.text = (GameManager.instance.moedasIntSave).ToString();
                        fezPonto = false;
                        GameManager.instance.desafioNum2SwishShot--;
                        GameManager.instance.DesafioDeFase(OndeEstou.instance.fase);
                        anim100PT.Play("100PontosAnim");
                        anim1.Play("SwishShotAnim");
                    }
                }
            }

            //SkyHook

            if (liberaSky == true && fezPonto == true)
            {
                GameManager.instance.skyHook = true;
                print("SkyHook");
                GameManager.instance.moedasIntSave += GameManager.instance.pontos + 150;
                UIManager.instance.moedasUI.text = (GameManager.instance.moedasIntSave).ToString();
                fezPonto = false;

                GameManager.instance.desafioNum3SkyHook--;
                GameManager.instance.DesafioDeFase(OndeEstou.instance.fase);
                anim150PT.Play("150PontosAnim");
                anim2.Play("SkyHookAnim");
            }
            //HotTable
            else if (bateuTabela == true && fezPonto == true && bateuAro == false && liberaSky == false)
            {
                GameManager.instance.hotTable = true;
                print("HotTable");
                GameManager.instance.moedasIntSave += GameManager.instance.pontos + 200;
                UIManager.instance.moedasUI.text = (GameManager.instance.moedasIntSave).ToString();
                fezPonto = false;

                GameManager.instance.desafioNum4HotTable--;
                GameManager.instance.DesafioDeFase(OndeEstou.instance.fase);
                anim300PT.Play("300PontosAnim");
                anim3.Play("HotTableAnim");
            }
            //Ponto comum
            else if (bateuTabela == true && bateuAro == true && fezPonto == true)
            {
                print("Ponto Comum");
                GameManager.instance.moedasIntSave += GameManager.instance.pontos + 10;
                UIManager.instance.moedasUI.text = (GameManager.instance.moedasIntSave).ToString();
                fezPonto = false;
                anim10PT.Play("10PontosAnim");
            }
        }

    }


        

    //Metodos

    void MostraCaminho()
    {
        for (int x=0; x < caminho.Count;x++)
        {
            caminho[x].GetComponent<Renderer>().enabled = true;
        }
    }

    void EscondeCaminho()
    {
        for (int x=0; x < caminho.Count;x++)
        {
            caminho[x].GetComponent<Renderer>().enabled = false;
        }
    }

    Vector2 PegaForca(Vector3 mouse)
    {
        return (new Vector2(startPos.x + valorX, startPos.y + valorY) - new Vector2(mouse.x, mouse.y)) * forca;
    }

    Vector2 CaminhoPonto(Vector2 posInicial,Vector2 velInicial,float tempo)
    {
        return posInicial + velInicial * tempo + 0.5f * Physics2D.gravity * tempo * tempo;
    }

    void CalculoCaminho()
    {
        Vector2 vel = PegaForca(Input.mousePosition) * Time.fixedDeltaTime / myRBody.mass;

        for (int x=0; x < caminho.Count; x++)
        {
            caminho[x].GetComponent<Renderer>().enabled = true;
            float t = x / 20f;
            Vector3 point = CaminhoPonto(transform.position, vel, t);
            point.z = 1.0f;
            caminho[x].transform.position = point;
        }
    }

    void Mirando()
    {
        if (tiro == true)
          return;
        
        if (Input.GetMouseButton(0))
        {
            
            if (GameManager.instance.primeiraVez == 0)
            {
                GameManager.instance.DesligaTuto();
            }
            
            if (mirando == false)
            {
                mirando = true;
                startPos = Input.mousePosition;
                CalculoCaminho();
                MostraCaminho();
            }
            else
            {
                CalculoCaminho();
            }
        }else if (mirando && tiro == false)
        {
            
            StartCoroutine(Life());
            myRBody.isKinematic = false;
            myCollider.enabled = true;
            tiro = true;
            mirando = false;
            myRBody.AddForce(PegaForca(Input.mousePosition));
            myRBody.AddTorque(PegaForca(Input.mousePosition).x / 5.0f);
            EscondeCaminho();

        }
    }

    private void OnBecameInvisible()
    {
        SegueBola.alvoInvisivel = true;
    }

    private void OnBecameVisible()
    {
        SegueBola.alvoInvisivel = false;
    }

    private void OnCollisionEnter2D(Collision2D col)
    {

        if (col.gameObject.CompareTag("Aro"))
        {
            bateuAro = true;
        }

        if (col.gameObject.CompareTag("tabela"))
        {
            bateuTabela = true;
        }

    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Sky"))
        {
            liberaSky = true;
        }
    }
    IEnumerator Life()
    {
        yield return new WaitForSeconds(10f);
        Destroy(this.gameObject);
        if (GameManager.instance != null && GameManager.instance.numJogadas > 0)
        {
            GameManager.instance.NascBolas();
        }
    }
}
