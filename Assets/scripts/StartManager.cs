using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartManager : MonoBehaviour {

    public GameObject fader;

    public void PlayButtonPressed()
    {
        fader.SetActive(true);
    }

    public void Startgame()
    {

        SceneManager.LoadScene(0);
    }
}
