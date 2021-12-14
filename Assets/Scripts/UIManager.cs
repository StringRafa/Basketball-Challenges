using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;



public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    public Text desafio1, desafio2, desafio3, desafio4, desafio1ap, desafio2ap, desafio3ap, desafio4ap, txtWL, moedasUI, numBolas;
    public Toggle desafio1T, desafio2T, desafio3T, desafio4T;
    public Button voltarBtn, novamenteBtn, entendiBtn, avancarBtn;

    //Loja

    public List<int> bolas;
    public Sprite []imagemSp;
    public int aux = 0;
    public Image menuImg;

    public Button[] compraBtn;

    public Button sobe, desce;

    private void Awake()
    {
        ZPlayerPrefs.Initialize("123456789", "basketballchallenges");
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        bolas = new List<int>();
        bolas.Add(0);

        if (!ZPlayerPrefs.HasKey("Bola0"))
        {
            ZPlayerPrefs.SetInt("Bola0", bolas[0]);
            ZPlayerPrefs.SetInt("list_Count", 1);
            print("salvo");

        }

        for (int i = 1; i < ZPlayerPrefs.GetInt("list_Count"); i++)
        {
            bolas.Add(ZPlayerPrefs.GetInt("Bola" + i));
        }

        if (OndeEstou.instance.fase >= 4) 
        {
            menuImg = GameObject.FindWithTag("imgBolasLoja").GetComponent<Image>();
                //Desafios
                desafio1T = GameObject.FindWithTag("togg1").GetComponent<Toggle>();
                desafio2T = GameObject.FindWithTag("togg2").GetComponent<Toggle>();
                desafio3T = GameObject.FindWithTag("togg3").GetComponent<Toggle>();
                desafio4T = GameObject.FindWithTag("togg4").GetComponent<Toggle>();

                desafio1 = GameObject.FindWithTag("d1").GetComponent<Text>();
                desafio2 = GameObject.FindWithTag("d2").GetComponent<Text>();
                desafio3 = GameObject.FindWithTag("d3").GetComponent<Text>();
                desafio4 = GameObject.FindWithTag("d4").GetComponent<Text>();           
        }

        SceneManager.sceneLoaded += Carrega;
    }

    void Carrega(Scene cena, LoadSceneMode modo)
    {
        if (OndeEstou.instance.fase >= 4)
        {

                voltarBtn = GameObject.FindWithTag("btnvoltar").GetComponent<Button>();
                novamenteBtn = GameObject.FindWithTag("btnnovamente").GetComponent<Button>();

                txtWL = GameObject.FindWithTag("TxtWL").GetComponent<Text>();
                avancarBtn = GameObject.FindWithTag("btnAvancar").GetComponent<Button>();

                entendiBtn = GameObject.FindWithTag("BtnEntendi").GetComponent<Button>();

                numBolas = GameObject.FindWithTag("NumBolas").GetComponent<Text>();
                numBolas.text = GameManager.instance.numJogadas.ToString();

                desafio1T = GameObject.FindWithTag("togg1").GetComponent<Toggle>();
                desafio2T = GameObject.FindWithTag("togg2").GetComponent<Toggle>();
                desafio3T = GameObject.FindWithTag("togg3").GetComponent<Toggle>();
                desafio4T = GameObject.FindWithTag("togg4").GetComponent<Toggle>();

                desafio1 = GameObject.FindWithTag("d1").GetComponent<Text>();
                desafio2 = GameObject.FindWithTag("d2").GetComponent<Text>();
                desafio3 = GameObject.FindWithTag("d3").GetComponent<Text>();
                desafio4 = GameObject.FindWithTag("d4").GetComponent<Text>();


                desafio1ap = GameObject.FindWithTag("desafio1ap").GetComponent<Text>();
                desafio2ap = GameObject.FindWithTag("desafio2ap").GetComponent<Text>();
                desafio3ap = GameObject.FindWithTag("desafio3ap").GetComponent<Text>();
                desafio4ap = GameObject.FindWithTag("desafio4ap").GetComponent<Text>();

            
            //Loja

            menuImg = GameObject.FindWithTag("imgBolasLoja").GetComponent<Image>();
            menuImg.sprite = imagemSp[ZPlayerPrefs.GetInt("Bola" + bolas[0])];

            //Sobe e Desce BTN

            sobe = GameObject.FindWithTag("btncima").GetComponent<Button>();
            desce = GameObject.FindWithTag("btnbaixo").GetComponent<Button>();

            //Evento Click Sobe e Desce BTN

            sobe.onClick.AddListener(CimaBolas);
            desce.onClick.AddListener(BaixoBolas);
            aux = 0;

        }
        moedasUI = GameObject.FindWithTag("coinUI").GetComponent<Text>();
        moedasUI.text = ScoreManager.instance.LoadDados().ToString();

        AtualizaBtnBola();

    }

    void Start()
    {
        //PlayerPrefs
        //ZPlayerPrefs.DeleteAll();
        if (OndeEstou.instance.fase >= 4)
        {

                numBolas.text = GameManager.instance.numJogadas.ToString();
            

            menuImg.sprite = imagemSp[ZPlayerPrefs.GetInt("Bola" + bolas[0])];
        }

        //ScoreManager.instance.SalvaMoedas(0);
    }

    public void Compra(int id)
    {
        if (id == 1)
        {
            if (ScoreManager.instance.LoadDados() >= 1000)
            {
                ChamaCompra(1);
                ScoreManager.instance.PerdeMoedas  (1000);
                moedasUI.text = ScoreManager.instance.LoadDados().ToString();
            }
            else
            {
                print("Sem Dinheiro");
            }

            
        }else if(id == 2){
            if (ScoreManager.instance.LoadDados() >= 5000)
            {
                ChamaCompra(2);
                ScoreManager.instance.PerdeMoedas(5000);
                moedasUI.text = ScoreManager.instance.LoadDados().ToString();
            }
            else
            {
                print("Sem Dinheiro");
            }
        }
    }
    void ChamaCompra(int id)
    {
        bolas.Add(id);
        ZPlayerPrefs.SetInt("list_Count", bolas.Count);
        ZPlayerPrefs.SetInt("Bola" + id, id);
        compraBtn[id - 1].interactable = false;

        if (id != 2)
        {
            compraBtn[id].interactable = true;
        }
        if (bolas.Contains(id))
        {
            compraBtn[id - 1].GetComponentInChildren<Text>().text = "Buyed";
            compraBtn[id - 1].GetComponentInChildren<Text>().color = new Color(0, 1, 0, 1);
        }
    }
    void AjustaBolasBtn(int x)
    {
        compraBtn[x].interactable = false;
        compraBtn[x].GetComponentInChildren<Text>().text = "Buyed";
        compraBtn[x].GetComponentInChildren<Text>().color = new Color(0,1,0,1);
    }

    void BaixoBolas()
    {
        if (aux < bolas.Count - 1)
        {
            aux++;
            menuImg.sprite = imagemSp[ZPlayerPrefs.GetInt("Bola" + aux)];
        }
    }

    void CimaBolas()
    {
        if (aux >= 1)
        {
            aux--;
            menuImg.sprite = imagemSp[ZPlayerPrefs.GetInt("Bola" + aux)];
        }
    }

    void AtualizaBtnBola()
    {
        if (OndeEstou.instance.fase == 1)
        {
            compraBtn = new Button[2];

            compraBtn[0] = GameObject.FindWithTag("btncompra1").GetComponent<Button>();
            compraBtn[1] = GameObject.FindWithTag("btncompra2").GetComponent<Button>();

            compraBtn[0].onClick.AddListener(() => Compra(1));
            compraBtn[1].onClick.AddListener(() => Compra(2));

            if (bolas.Contains(1))
            {
                AjustaBolasBtn(0);

                if (!bolas.Contains(2))
                {
                    compraBtn[1].interactable = true;
                }
            }

            if (bolas.Contains(2))
            {
                AjustaBolasBtn(1);
            }
        }
    }

}
