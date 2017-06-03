using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceController : MonoBehaviour {
    public FACE_TYPE facetype;

    private bool isSpinning = false;
    private bool isStopping = false;
    private bool isSlowning = false;
    // postion win ta9ef m3a el axe y
    private float stopPoint = 0;
    private float spinSpeed = 0;
    private Slot slotref;
	// Use this for initialization
	void Start () {
		
	}

    // Update is called once per frame
    void Update()
    {
        if (spinSpeed < Machine.instance.slotsSlowestSpinSpeed)
        {
            spinSpeed = Machine.instance.slotsSlowestSpinSpeed;
            isSlowning = false;
            isStopping = true;
            stopPoint = Mathf.Floor(transform.position.y);
            StopCoroutine("slowSpinOverTime");
        }

        if (isSpinning)
        {
            transform.Translate(Vector3.down * Time.deltaTime * spinSpeed, Space.World);
            if (!isStopping)
            {
                if (transform.position.y < 0)
                {
                    transform.position += new Vector3(0, Machine.instance.GetNumFaces(), 0);
                }
            }
        }
        if (isStopping)
        {
            if (transform.position.y <= stopPoint)
            {
                transform.position = new Vector3(transform.position.x, stopPoint, transform.position.z);

                isSpinning = false;
                isStopping = false;
                stopPoint = 0;

                slotref.stoppedSpinning();
            }

        }
    }


    public void StartSpinning(float slotSpeed)
    {
        StopAllCoroutines();
        Debug.Log("wsel el message ech yspinni");
        isSpinning = true;
        isStopping = false;
        isSlowning = false;
        spinSpeed = slotSpeed;
    }
    public void StopSpinnning()
    {
        isSlowning = true;
        StartCoroutine(SlowSpinOverTime(Machine.instance.slotSlowdownInterval,Machine.instance.slowSlowdownDelta
            ));
    }

  
    public FACE_TYPE GetFaceType()
    {
        return (facetype);
    }


    public void SetSlotRef(Slot slot)
    {
        slotref = slot;
    }


    IEnumerator SlowSpinOverTime(float interval,float delta)
    {
        while (true)
        {
            yield return new WaitForSeconds(interval);
            if (isStopping || !isSpinning) yield break;
            spinSpeed -= delta;
        }
    }
}
