using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamraMove : MonoBehaviour
{

    public float sensitivity = 1f;
    public GameObject Player;
    //public Transform pivit;
    public int neckhight = 2;

    private float currentRotateHorizontal = 0;
    private float rotateHorizontal;
    private float rotateVertical;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void LateUpdate()
    {
        rotateHorizontal = Input.GetAxis ("Mouse X") * sensitivity;
        rotateVertical = Input.GetAxis ("Mouse Y") * sensitivity;
    }

    void Update()
    {
        
        


        if (rotateHorizontal != 0 || rotateVertical != 0)
        {
            Debug.Log(currentRotateHorizontal + rotateVertical);
        }

        if ( (currentRotateHorizontal - rotateVertical) >= 50 && rotateVertical < 0)
        {
            Debug.Log("stoping " + (-rotateVertical));
            rotateVertical = -(50 - currentRotateHorizontal);
            Debug.Log("adding " + (-rotateVertical));
        }
        else if ((currentRotateHorizontal - rotateVertical) <= -30 && rotateVertical > 0)
        {
            Debug.Log("stoping " + (-rotateVertical));
            rotateVertical = -(-30 - currentRotateHorizontal);
            Debug.Log("adding " + (-rotateVertical));

        }

        currentRotateHorizontal += -rotateVertical;


        
        transform.position = new Vector3 (Player.transform.position.x, Player.transform.position.y + neckhight,Player.transform.position.z);

        transform.RotateAround (Player.transform.position, transform.right, -rotateVertical);     
        Player.transform.RotateAround (Player.transform.position, -Vector3.up, -rotateHorizontal ); 
        transform.RotateAround (Player.transform.position, -Vector3.up, -rotateHorizontal );

    }

    // Update is called once per frame
    void FixedUpdate ()
     {

        
    }
}
