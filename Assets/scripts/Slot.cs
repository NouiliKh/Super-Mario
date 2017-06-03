using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slot : MonoBehaviour {
    private const float CENTER = 3.0f;
    private int slotNumber;
    private bool isSpinning =false;
    private GameObject[] faces;


    public void Init(int slotNumber)
    {
        this.slotNumber = slotNumber;
        faces = new GameObject[Machine.instance.GetNumFaces()];
        for(int i = 0; i < Machine.instance.GetNumFaces();i++)
        {
            faces[i] = Instantiate(Machine.instance.GetFace(i).facePrefab) as GameObject;
            faces[i].transform.position += new Vector3((float)slotNumber,i,0);
            faces[i].transform.parent = this.gameObject.transform;

            FaceController facescript = faces[i].GetComponent<FaceController>();
            facescript.SetSlotRef(this);
        }
            }


    public void StartSpinning()
    {
        isSpinning = true;
        Debug.Log("wsel lel slot el message ech yspinni");
    }

    public void stoppedSpinning()
    {
        if (isSpinning)
        {
            isSpinning = false;
            Machine.instance.SlotStopped();
        }
    }

    //traja3li eli fel centre
    public FACE_TYPE getfacetype()
    {
        FACE_TYPE faceType = 0;
        for(int i = 0; i < Machine.instance.GetNumFaces(); i++)
        {
            if(Mathf.Round(faces[i].transform.position.y) == CENTER)
            {
                faceType = faces[i].GetComponent<FaceController>().GetFaceType();
                return faceType;
            }
        }

        Debug.LogError("error! rja3li el default facetype!!!!");
        return faceType;

    }
}


