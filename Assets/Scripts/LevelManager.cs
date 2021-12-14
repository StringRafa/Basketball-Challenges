using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour {

	public static LevelManager instance;

	void Awake()
	{
		ZPlayerPrefs.Initialize("123456789", "basketballchallenges");
        if (instance == null)
        {
			instance = this;			
        }

	}

	[System.Serializable]
	public class Level
	{
		public string levelText;
		public bool habilitado;
		public int desbloqueado;
		public bool txtAtivo;
	}

	public GameObject botao;
	public Transform localBtn;
	public List<Level> levelList;

	void Start()
	{
		ListaAdd();
	}
	void ListaAdd()
	{
		foreach(Level level in levelList)
		{
			GameObject btnNovo = Instantiate (botao) as GameObject;
			BotaoLevel btnNew = btnNovo.GetComponent<BotaoLevel> ();
			btnNew.levelTxtBtn.text = level.levelText;

			if(ZPlayerPrefs.GetInt("Level"+btnNew.levelTxtBtn.text) == 1)
			{
				level.desbloqueado = 1;
				level.habilitado = true;
				level.txtAtivo = true;
			}

			btnNew.desbloqueadoBTN = level.desbloqueado;
			btnNew.GetComponent<Button> ().interactable = level.habilitado;
			btnNew.GetComponentInChildren<Text> ().enabled = level.txtAtivo;
			btnNew.GetComponent<Button> ().onClick.AddListener (() => ClickLevel ("Level" + btnNew.levelTxtBtn.text));

			btnNovo.transform.SetParent (localBtn,false);
		}
	}

	void ClickLevel(string level)
	{
		SceneManager.LoadScene (level);
		AudioManager.instance.SonsFXToca(0);
	}

}
