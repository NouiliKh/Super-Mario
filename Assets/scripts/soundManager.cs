using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class soundManager : MonoBehaviour {
    public static soundManager instance;

    public AudioSource gameMusic;
    public AudioSource starsMusic;
    public AudioSource winMoney;
    public AudioSource loseMoney;
    public AudioSource fireworks;
    public AudioClip clicksound;
    void Awake()

    {
        if (!instance)
        {
            instance = this;
        }
        else
        {
            Debug.Log("Sound Manager instance deja mawjouda");
        }
    }

    public void PlayLoseMoney()
    {
        loseMoney.Play();
    }

    public void PlayWinMoney()
    {
        winMoney.Play();
    }


    public void PlayStars()
    {
        starsMusic.Play();
    }

    public void PlayGameMusic()
    {
        gameMusic.Play();
    }

    public void playfireworks()
    {
        fireworks.Play();
    }
    public void StopGameMusic()
    {
        gameMusic.Pause();
    }

    public bool MusicIsPlaying()
    {
        return gameMusic.isPlaying;
    }

    public void PlaySlotClick(Vector3 positionToPlayFrom)
    {
        AudioSource.PlayClipAtPoint(clicksound, positionToPlayFrom);
    }
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
