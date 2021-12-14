using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {

	//Músicas
	public AudioClip[] clips;
	public AudioSource musicaBG;
	//SonsFX
	public AudioClip[] clipsFX;
	public AudioSource sonsFX;


	public static AudioManager instance;

	void Awake()
	{
		if(instance == null)
		{
			instance = this;
			DontDestroyOnLoad (this.gameObject);
		}
		else
		{
			Destroy (gameObject);
		}
	}

	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        if (OndeEstou.instance.fase <= 3 && !musicaBG.isPlaying)
        {
			musicaBG.clip = GetRandom();
			musicaBG.Play();
		}
		else if(OndeEstou.instance.fase >= 4 && musicaBG.isPlaying)
		{

			musicaBG.Stop();

		}

	}

	AudioClip GetRandom()
	{
		return clips [Random.Range (0, clips.Length)];
	}

	public void SonsFXToca(int index)
	{
		if (OndeEstou.instance.fase >= 0 && !sonsFX.isPlaying)
		{
			sonsFX.clip = clipsFX[index];
			sonsFX.Play();
		}

	}
}
