using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class itemOfIntrest : MonoBehaviour
{
 


    Vector3 lastcamPos;
    public GameObject IOI;
    public TextMeshPro textMeshObject;
    public string TextToDisplay;

    public bool CloseToPlayer;

    





    // Start is called before the first frame update
    void Start()
    {
        CloseToPlayer = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (CloseToPlayer)
        {
            FaceTextMeshToCamera();
            if (Input.GetKeyDown(KeyCode.X))
            {
                TextToDisplay = " You have triggered this event! if your seeing this bother Seaney he will add in the trigger";
                textMeshObject.SetText(TextToDisplay);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("player"))
        {
            EnablePrompt();
        }
    }


    public void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("player"))
        {
            disablePrompt();
        }
    }


    void EnablePrompt()
    {
        CloseToPlayer = true;
        textMeshObject.SetText(TextToDisplay);
        textMeshObject.gameObject.SetActive(true);
    }

    void disablePrompt()
    {
        CloseToPlayer = false;
        textMeshObject.gameObject.SetActive(false);
    }

    void FaceTextMeshToCamera()
    {
        Vector3 origRot = textMeshObject.transform.eulerAngles;
        textMeshObject.transform.LookAt(Camera.main.transform);
        origRot.y = textMeshObject.transform.eulerAngles.y + 180;
        textMeshObject.transform.eulerAngles = origRot;
    }
}
