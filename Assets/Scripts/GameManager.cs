using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [System.Serializable]
    public class DesafiosTxt
    {

        public string desafioRim, desafioSwish, desafioSky, desafioHot;
        public int desafioInt1RimShot = 0, desafioInt2SwishShot = 0, desafioInt3SkyHook = 0, desafioInt4HotTable = 0, numeroJogadas, ondeEstou;

    }

    public List<DesafiosTxt> desafiosList;

    void ListaAdd()
    {
        foreach (DesafiosTxt desaf in desafiosList)
        {
            if (desaf.ondeEstou == OndeEstou.instance.fase)
            {
                UIManager.instance.desafio1.text = desaf.desafioRim;
                UIManager.instance.desafio2.text = desaf.desafioSwish;
                UIManager.instance.desafio3.text = desaf.desafioSky;
                UIManager.instance.desafio4.text = desaf.desafioHot;
                desafioNum1RimShot = desaf.desafioInt1RimShot;
                desafioNum2SwishShot = desaf.desafioInt2SwishShot;
                desafioNum3SkyHook = desaf.desafioInt3SkyHook;
                desafioNum4HotTable = desaf.desafioInt4HotTable;
                numJogadas = desaf.numeroJogadas;
                UIManager.instance.desafio1ap.text = desaf.desafioRim;
                UIManager.instance.desafio2ap.text = desaf.desafioSwish;
                UIManager.instance.desafio3ap.text = desaf.desafioSky;
                UIManager.instance.desafio4ap.text = desaf.desafioHot;
                break;
            }
        }
    }

    public static GameManager instance;

    public int desafioNum1RimShot, desafioNum2SwishShot, desafioNum3SkyHook, desafioNum4HotTable, numJogadas;
    public GameObject []bolaPrefab;
    [SerializeField]
    private Transform posDireita, posEsquerda, posCima, posBaixo;
    public bool jogoExecutando = true, win = false, lose = false, bolaEmCena;
    public int pontos = 0, moedasIntSave, primeiraVez = 0;
    public bool rimShot = false, swishShot = false,skyHook = false,hotTable = false, adsUmaVez = false;
    [SerializeField]
    private GameObject fundo, tela,telaWL, mao, bolinhas;
    [SerializeField]
    private Animator maoAnim, bolinhasAnim, animCont;

    public void LiberaContagem()
    {
        fundo.gameObject.SetActive(false);
        tela.gameObject.SetActive(false);
        telaWL.SetActive(false);
        animCont.Play("ContadorAnim");        
    }

    private void Awake()
    {
        ZPlayerPrefs.Initialize("123456789", "basketballchallenges");

        
        if (ZPlayerPrefs.HasKey("PrimeiraVez") == false)
        {
            ZPlayerPrefs.SetInt("PrimeiraVez",0);
        }
        else
        {
            primeiraVez = ZPlayerPrefs.GetInt("PrimeiraVez");
        }
        
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        SceneManager.sceneLoaded += Carrega;
    }
 

    void Carrega(Scene cena, LoadSceneMode modo)
    {
        if (OndeEstou.instance.fase >= 4)
        {

            win = false;
            StartGame();
            ListaAdd();
            posDireita = GameObject.FindWithTag("direita_pos").GetComponent<Transform>();
            posEsquerda = GameObject.FindWithTag("esquerda_pos").GetComponent<Transform>();
            posCima = GameObject.FindWithTag("cima_pos").GetComponent<Transform>();
            posBaixo = GameObject.FindWithTag("baixo_pos").GetComponent<Transform>();

            fundo = GameObject.FindWithTag("FundoC");
            tela = GameObject.FindWithTag("TelaFundo");
            animCont = GameObject.FindWithTag("contador").GetComponent<Animator>();

            //tela Win e Lose
            telaWL = GameObject.FindWithTag("telaWL");

            maoAnim = GameObject.FindWithTag("mao").GetComponent<Animator>();
            bolinhasAnim = GameObject.FindWithTag("bolinhas").GetComponent<Animator>();

            primeiraVez = ZPlayerPrefs.GetInt("PrimeiraVez");
            if (primeiraVez == 0 || primeiraVez == 1)
            {
                PegaSpritesTuto();

                if (primeiraVez == 1)
                {
                    Matador(mao.gameObject, bolinhas.gameObject);
                }
            }

        }
    }

    void Start()
    {
        StartGame();
        ListaAdd();
        //ZPlayerPrefs.DeleteAll();
        bolaEmCena = true;
        
    }
    void Update()
    {/*
        if (Input.GetKeyDown(KeyCode.Space))
        {
            OndeEstou.instance.fase++;
            SceneManager.LoadScene(OndeEstou.instance.fase);
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
           
            SceneManager.LoadScene(OndeEstou.instance.fase);
        }
*/
        //Vencr ou perder

        if (numJogadas <= 0)
        {
            if (desafioNum1RimShot > 0 || desafioNum2SwishShot > 0 || desafioNum3SkyHook > 0 || desafioNum4HotTable > 0)
            {
                
                YouLose();
            }
        }
        else if (numJogadas > 0 && desafioNum1RimShot <= 0 && desafioNum2SwishShot <= 0 && desafioNum3SkyHook <= 0 && desafioNum4HotTable <= 0)
        {
            
            YouWin();

        }
    }

    public void NascBolas()
    {
        Instantiate(bolaPrefab[UIManager.instance.aux], new Vector2(Random.Range(posEsquerda.position.x, posDireita.position.x), Random.Range(posCima.position.y, posBaixo.position.y)), Quaternion.identity);
        bolaEmCena = true;
    }

    
    public void DesligaTuto()
    {
        Matador(mao.gameObject,bolinhas.gameObject);
        ZPlayerPrefs.SetInt("PrimeiraVez", 1);
    }
    
    void Matador(GameObject obj,GameObject obj2)
    {
        Destroy(obj);
        Destroy(obj2);
    }
    

    void PegaSpritesTuto()
    {
        mao = GameObject.FindWithTag("mao");
        bolinhas = GameObject.FindWithTag("bolinhas");
    }

    void Novamente()
    {
        SceneManager.LoadScene(OndeEstou.instance.fase);
    }

    void Avancar()
    {      
            int temp = OndeEstou.instance.fase + 1;
            SceneManager.LoadScene(temp);      
    }

    void Voltar()
    {
        SceneManager.LoadScene("MenuFases");
    }

    void StartGame()
    {
        if (OndeEstou.instance.fase >= 4)
        {

            UIManager.instance.novamenteBtn.onClick.AddListener(Novamente);
            UIManager.instance.avancarBtn.onClick.AddListener(Avancar);
            UIManager.instance.voltarBtn.onClick.AddListener(Voltar);
            UIManager.instance.entendiBtn.onClick.AddListener(LiberaContagem);
            jogoExecutando = false;
            pontos = 0;
            win = false;
            lose = false;
            moedasIntSave = ScoreManager.instance.LoadDados();
            UIManager.instance.moedasUI.text = moedasIntSave.ToString();
            adsUmaVez = false;
        }
    }

    public void DesafioDeFase(int fase)
    {
        if (OndeEstou.instance.fase == fase)
        {
            if (desafioNum1RimShot == 0)
            {
                UIManager.instance.desafio1T.isOn = true;
                print("RimShot Completo");
            }
            if (desafioNum2SwishShot == 0)
            {
                UIManager.instance.desafio2T.isOn = true;
                print("SwishShot Completo");
            }
            if (desafioNum3SkyHook == 0)
            {
                UIManager.instance.desafio3T.isOn = true;
                print("SkyHook Completo");
            }
            if (desafioNum4HotTable == 0)
            {
                UIManager.instance.desafio4T.isOn = true;
                print("HotTable Completo");
            }

        }
    }

    void YouWin()
    {
        int temp = OndeEstou.instance.fase -2;
        ZPlayerPrefs.SetInt("Level" + temp, 1);

        if (jogoExecutando == true)
        {
            win = true;
            jogoExecutando = false;
            print("WIN");
            AudioManager.instance.SonsFXToca(2);
            ScoreManager.instance.SalvarDados(moedasIntSave);
            telaWL.SetActive(true);
            UIManager.instance.txtWL.text = "YOU WIN!";
            GooglePlay.instance.AddToLeaderboard();         
        }
    }

    public void YouLose()
    {
        
        if (jogoExecutando == true)
        {
            lose = true;
            jogoExecutando = false;
            print("LOSE");
            telaWL.SetActive(true);
            AudioManager.instance.SonsFXToca(3);
            UIManager.instance.txtWL.text = "YOU LOSE!";
            UIManager.instance.avancarBtn.gameObject.SetActive(false);
            
            if (adsUmaVez == false)
            {
                AdsUnity.instance.showAds();
                adsUmaVez = true;
            }
        }
    }
    
    public void PrimeiraJogada()
    {
        if (jogoExecutando == true && primeiraVez == 0)
        {
            if (mao != null && bolinhas != null)
            {
                maoAnim.Play("MaoAnim");
                bolinhasAnim.Play("BolinhaAnim");
                print("animando");
            }
            print(primeiraVez);
        }
    }
    
}
