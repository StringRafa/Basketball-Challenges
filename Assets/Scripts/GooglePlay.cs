using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;
using GooglePlayGames;
using GooglePlayGames.BasicApi;


public class GooglePlay : MonoBehaviour
{
    public static GooglePlay instance;
    public const string placarMelhor = "CgkIr9nfgtUJEAIQAA";
    private void Awake()
    {
        if (instance == null)
        {
            instance = null;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void Start()
    {
        GooglePlayGames.PlayGamesPlatform.Activate();
        Login();
    }

   
    void Update()
    {
        
    }

    public void Login()
    {
        Social.localUser.Authenticate((bool success) => { });
    } 

    public void AddToLeaderboard()
    {
        Social.ReportScore(ScoreManager.instance.LoadDados(), placarMelhor, (bool success) => { });
    }

    public void ShowLeaderboard()
    {
        if (Social.localUser.authenticated)
        {
            GooglePlayGames.PlayGamesPlatform.Instance.ShowLeaderboardUI(placarMelhor);
        }
        else
        {
            Login();
        }
    }
}
