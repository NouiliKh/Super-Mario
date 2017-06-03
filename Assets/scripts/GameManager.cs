using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public static GameManager instance;

    public GameObject machinePrefab;
    public Reward[] rewards;
    public int costPerSpin;
    public int startingCoins;
    private int playerCoins;

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
		
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKeyDown("space"))
        {
            Machine.instance.StartSpinning();

        }
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

    public void ReadyToMatch()
    {
        Debug.Log("slots kamlou ydourou. bara matchi");
        StartCoroutine("Rewards");
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

                        Debug.Log("matchi" + matches[i] + (((FACE_TYPE)i).ToString()) +". " + reward.rewardType.ToString() + "  " +reward.rewardAmount);
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
            Debug.Log("el jayza" + rewardTotal +"el reward tdharbet *" + multiplier+" walet"+ (rewardTotal*multiplier)+'.');
        }

    }

}
