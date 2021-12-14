using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class AdsUnity : MonoBehaviour , IUnityAdsListener
{
	public static AdsUnity instance;

	[SerializeField] string _gameID = "4191409"; //id da googleplay
	[SerializeField] string myPlacementId = "rewardedVideo";
	[SerializeField] private Button btnAds;
	[SerializeField] bool testeMode = true;
	public bool adsBtnAcionado = false;

	void Start()
	{
		Advertisement.AddListener(this);
		Advertisement.Initialize(_gameID, testeMode);
	}

	void Awake()
	{
		if (instance == null)
		{
			instance = this;
			DontDestroyOnLoad(this.gameObject);
		}
		else
		{
			Destroy(this.gameObject);
		}

		SceneManager.sceneLoaded += PegaBtn;
	}
	
	void PegaBtn(Scene cena, LoadSceneMode modo)
	{
		
		if (OndeEstou.instance.fase >= 4)
		{
			btnAds = GameObject.FindWithTag("AdsBtn").GetComponent<Button>();
			btnAds.onClick.AddListener(AdsBtn);

		}
		
}


public void OnUnityAdsDidFinish(string placementId, ShowResult showResult)
	{
		
		if (showResult == ShowResult.Finished)
		{
			ScoreManager.instance.SalvaMoedas (+1000);// jogador ganha 1000 moedas após ver video
			Debug.Log("Você ganhou 1000.00 moedas !!");
		}
		else if (showResult == ShowResult.Skipped)
		{
			Debug.Log("Você pulou o anúncio !!");
		}
		else if (showResult == ShowResult.Failed)
		{
			Debug.LogWarning("Failed.");
		}
		
	}


void AdsBtn()
{

	if (Advertisement.IsReady(myPlacementId))
	{
		Advertisement.Show(myPlacementId);
		adsBtnAcionado = true;
	}
	else
	{
		Debug.Log("Rewarded video is not ready at the moment! Please try again later!");
	}

}

public void showAds()
	{
		if (ZPlayerPrefs.HasKey("AdsUnity"))
		{
			if (ZPlayerPrefs.GetInt("AdsUnity") == 3)
			{
				if (Advertisement.IsReady("video"))
				{
					Advertisement.Show("video");
				}
				ZPlayerPrefs.SetInt("AdsUnity", 1);
			}
			else
			{
				ZPlayerPrefs.SetInt("AdsUnity", ZPlayerPrefs.GetInt("AdsUnity") + 1);
			}
		}
		else
		{
			ZPlayerPrefs.SetInt("AdsUnity", 1);
		}
	}
	
	public void OnDestroy()
	{
		Advertisement.RemoveListener(this);
	}
	
	public void OnUnityAdsReady(string placementId)
	{
		Debug.Log("Ready: " + placementId);
	}

	public void OnUnityAdsDidError(string message)
	{
		Debug.Log("Error: " + message);
	}

	public void OnUnityAdsDidStart(string placementId)
	{
		Debug.Log("Start: " + placementId);
	}
}
