using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Machine : MonoBehaviour {

    public static Machine instance;

    public int numSlots;
    public float minSlotSpeed;
    public float maxSlotSpeed;
    public float slotSlowdownStartTime;
    public float slotSlowdownStartNext;
    public float slotSlowdownInterval;
    public float slowSlowdownDelta;
    public float slotsSlowestSpinSpeed;
    public GameObject slotPefab;
    private bool isSpinning = false;
    public Face[] faces;
    private GameObject[] slots;
    private int slotsSpinning;

    void Awake()
    {

        if(instance!=null && instance != this)
        {
            Debug.Log("Machine singleton is already initialized");
            Destroy(this.gameObject);
        }

     else if(instance != this)
        {
            instance = this;
        }   
    }


    // Use this for initialization
    void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Init()
    {
        SpawnSlots();
    }

    public int GetNumFaces()
    {
        return faces.Length;
    }

    public int GetNumSlots()
    {
        return numSlots;
    }

    public Face GetFace(int i)
    {
        return faces[i];
    }

    private void SpawnSlots()
    {
        slots = new GameObject[numSlots];

        for (int i = 0; i < numSlots; i++)
        {
            slots[i] = Instantiate(slotPefab) as GameObject;

            Slot slotScript = slots[i].GetComponent<Slot>();

            if (slotScript == null)
            {
                Debug.Log("madamch script 3al objet ");
            }
            else
            {
                slotScript.Init(i);
            }

        }

    }

    public void StartSpinning()
    {

        for (int i = 0; i < numSlots; i++)
        {
            float speed = Random.Range(minSlotSpeed, maxSlotSpeed);

            slots[i].BroadcastMessage("StartSpinning",speed);
        }

        slotsSpinning = numSlots;

        isSpinning = true;

        StartCoroutine(SlotsSlowdownTimers(slotSlowdownStartTime, slotSlowdownStartNext));
    }

    public void SlotStopped()
    {
        slotsSpinning--;
        if (slotsSpinning == 0)
        {
            GameManager.instance.ReadyToMatch();
        }
    }

    public int[] FindMatches()
    {
        int[] faceCountArray;
        faceCountArray = new int[GetNumFaces()];
        for(int i = 0; i < numSlots; i++)
        {
            Slot slotscript = slots[i].GetComponent<Slot>();
            // faceCountArray[i] = (int)slotscript.getfacetype();
            faceCountArray[(int)slotscript.getfacetype()]++;
               }
        return faceCountArray;
    }

    IEnumerator SlotsSlowdownTimers(float slotsSlowdownStartTime, float slotsSlowdownStartNext)
    {
        yield return new WaitForSeconds(slotsSlowdownStartTime);
        for (int i=0 ; i < numSlots; i++)
        {
            slots[i].BroadcastMessage("StopSpinnning");

            yield return new WaitForSeconds(slotsSlowdownStartNext);
        }

        yield break;
    }
}
