using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class killcheers : MonoBehaviour {
    public float seconds;
	// Use this for initialization
	void Start () {
        StartCoroutine(killaftertime(seconds));
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    IEnumerator killaftertime(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        Destroy(gameObject);
    }
}
