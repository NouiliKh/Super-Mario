﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    public static GameManager instance;

    public GameObject machinePrefab;
    public Reward[] rewards;
    public int costPerSpin;
    public int startingCoins;
    private int playerCoins;
    public GameObject cheerBoxUI;
    public Text cheerText;
    public Text CointextUi;
    public float cheerDelay;
    public GameObject faderUI;
    public GameObject cheerConfetti;
    public GameObject mainCam;
    public GameObject starFireworksprfab;
    public bool canspin=true;


    void Awake()
    {

        if (instance != null && instance != this)
        {
            Debug.Log("GameManaer singleton is already initialized");
            Destroy(this.gameObject);
        }

        else if (instance != this)
        {
            instance = this;
        }
        Instantiate(machinePrefab);
        Machine.instance.Init();
    }
    // Use this for initialization
    void Start () {
        playerCoins = startingCoins;
        CointextUi.text = playerCoins.ToString();
        Debug.Log("player 3talou " + startingCoins + "ech yebda bihom");
		
	}
	
	// Update is called once per frame
	void Update () {

        
        if (Input.GetKeyDown("a"))
        {
            int[] matches;
            matches = Machine.instance.FindMatches();

            for(int i = 0; i < Machine.instance.GetNumFaces(); i++)
            {
                Debug.Log(matches[i]);
            }
        }
		
	}

    public void SpinButtonPressed()
    {
        if (canspin && playerCoins >= costPerSpin)
        {
            playerCoins -= costPerSpin;
            CointextUi.text = playerCoins.ToString();
            Debug.Log("Player Spent" + costPerSpin + "coins to spin!");
            canspin = false;
            Machine.instance.StartSpinning();
        }
    }

    public void ReadyToMatch()
    {
        Debug.Log("slots kamlou ydourou. bara matchi");
        StartCoroutine("Rewards");
    }

    public void CheckCoins()
    {
        if(playerCoins<= 0)
        {
            playerCoins = 0;
            faderUI.SetActive(true);
        }
    }

    public void GameOver()
    {
        SceneManager.LoadScene(2);

    }

    public void starFireworks()
    {
        List<Vector3> positions = new List<Vector3>();
        positions.AddRange(Machine.instance.FindStars());
        foreach(Vector3 position in positions)
        {
            Instantiate(starFireworksprfab,position,starFireworksprfab.transform.rotation);
        }

    }

    public void ProcessReward(REWARD_TYPE rewardType, int rewardcount, FACE_TYPE facerType, int faceCount)
    {
        string strcheer = " ";

        switch (rewardType)
        {
            case REWARD_TYPE.WinCoins:
                playerCoins += rewardcount;
                GameObject Confetti = Instantiate(cheerConfetti,mainCam.transform);
                Confetti.transform.SetParent(mainCam.transform);
                strcheer += "You Win";
                soundManager.instance.StopGameMusic();
                soundManager.instance.PlayWinMoney();
                break;

            case REWARD_TYPE.LoseCoins:
                playerCoins -= rewardcount;
                strcheer += "Oh no! You lost ";
                CheckCoins();
                soundManager.instance.StopGameMusic();
                soundManager.instance.PlayLoseMoney();
                break;

            case REWARD_TYPE.Multiplier:
                starFireworks();
                playerCoins += rewardcount;
                
                strcheer += "BONUS MULTIPLIER! YOU WIN!!! ";
                soundManager.instance.PlayStars();
                soundManager.instance.StopGameMusic();
                soundManager.instance.playfireworks();

                break;

            default:
                break;
        }

        CointextUi.text = playerCoins.ToString();

        strcheer += rewardcount;

        if(rewardType == REWARD_TYPE.Multiplier)
        {
            strcheer += "extra coins for having ";
        }
        else if (rewardType == REWARD_TYPE.LoseCoins)
        {
            strcheer += "coins for having ";
        }
        else
        {
            strcheer += "coins for matching ";
        }
        strcheer += faceCount;
        strcheer += " " + facerType.ToString() + "s! ";
        Debug.Log(strcheer);
        cheerText.text = strcheer;
        cheerBoxUI.SetActive(true);
    }

    IEnumerator Rewards()
    {
        int[] matches;
        int starCount =0;
        int multiplier = 0;
        int rewardTotal =0;
        matches = Machine.instance.FindMatches();
        yield return new WaitForSeconds(1);

        for (int i = 0; i < Machine.instance.GetNumFaces(); i++)
        {
            foreach(Reward reward in rewards)
            {
                if(reward.faceTyoe == (FACE_TYPE)i && reward.repMatches == matches[i])
                {
                    if (reward.faceTyoe != FACE_TYPE.Star)
                    {
                        if(reward.rewardType == REWARD_TYPE.WinCoins)
                        {
                            rewardTotal += reward.rewardAmount;
                        }
         
                        //  Debug.Log("matchi" + matches[i] + (((FACE_TYPE)i).ToString()) +". " + reward.rewardType.ToString() + "  " +reward.rewardAmount);
                        ProcessReward(reward.rewardType,reward.rewardAmount,reward.faceTyoe,matches[i]);
                        yield return new WaitForSeconds(cheerDelay);
                    } else
                    {
                        multiplier = reward.rewardAmount;
                        starCount = matches[i];
                    }
                }
            }
        }

        if(multiplier>0 && starCount >0 && rewardTotal > 0)
        {
            //  Debug.Log("el jayza" + rewardTotal +"el reward tdharbet *" + multiplier+" walet"+ (rewardTotal*multiplier)+'.');
            ProcessReward(REWARD_TYPE.Multiplier, rewardTotal*multiplier,FACE_TYPE.Star,starCount);
            yield return new WaitForSeconds(cheerDelay);


        }
        canspin = true;

        if (!soundManager.instance.MusicIsPlaying())
        {
            soundManager.instance.PlayGameMusic();

        }
    }

}
