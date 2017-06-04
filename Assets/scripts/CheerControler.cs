using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheerControler : MonoBehaviour {

    public float cheerBoxDelay;

    void OnEnable()
    {
        StartCoroutine("DisableSelf");
    }
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    IEnumerator DisableSelf()
    {
        yield return new WaitForSeconds(cheerBoxDelay);
        this.gameObject.SetActive(false);
    }

}

