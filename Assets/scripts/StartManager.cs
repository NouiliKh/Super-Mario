using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartManager : MonoBehaviour {
    public AudioSource Startsound;
    public GameObject fader;

    public void PlayButtonPressed()
    {
        Startsound.Play();
        fader.SetActive(true);
    }

    public void Startgame()
    {

        SceneManager.LoadScene(1);
    }




}
