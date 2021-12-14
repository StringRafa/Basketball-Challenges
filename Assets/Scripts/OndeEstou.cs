using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OndeEstou : MonoBehaviour {

	public int fase = -1;

	private float orthosize = 5;
	[SerializeField]
	private float aspect = 1.78f;

	public static OndeEstou instance;


	void Awake()
	{
		if(instance == null)
		{
			instance = this;
			DontDestroyOnLoad (this.gameObject);
		}
		else if (instance != this)
		{
			Destroy (gameObject);
		}

		SceneManager.sceneLoaded += VerificaFase;



	}

	public void VerificaFase(Scene cena, LoadSceneMode modo)
	{
		fase = SceneManager.GetActiveScene ().buildIndex;
		Camera.main.projectionMatrix = Matrix4x4.Ortho(-orthosize * aspect, orthosize * aspect, -orthosize, orthosize, Camera.main.nearClipPlane, Camera.main.farClipPlane);
	}

}
