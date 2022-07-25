using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bat : MonoBehaviour
{

    Animator myAnimationController;
    public GameObject player;
    public GameObject batCore;

    // Start is called before the first frame update
    void Awake()
    {
        myAnimationController = GetComponent<Animator>();
    }
    
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Swing();
        }
    }

    public void Swing()
    {
        if (!myAnimationController.GetCurrentAnimatorStateInfo(0).IsName("attack"))
        {
            myAnimationController.SetTrigger("attack");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("enemy"))
        {
            other.GetComponent<FollowPlayer>().Die();
            Vector3 test = other.transform.position - batCore.transform.position;
            other.GetComponent<Rigidbody>().AddForce(test * 500f,ForceMode.Force);
            other.GetComponent<Rigidbody>().useGravity = true;
        }
    } 
}
