using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bat : MonoBehaviour
{

    public Animator myAnimationController;
    public GameObject player;
    public GameObject batCore;

    BoxCollider hitbox;


    // Start is called before the first frame update
    void Awake()
    {
    }
    
    void Start()
    {
        hitbox = GetComponent<BoxCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Swing();
        }
        if (myAnimationController.GetCurrentAnimatorStateInfo(0).IsName("base"))
        {
            hitbox.enabled = false  ;
        }
    }

    public void Swing()
    {
        hitbox.enabled = true;
        if (!myAnimationController.GetCurrentAnimatorStateInfo(0).IsName("batAttack"))
        {
            hitbox.enabled = true;
            myAnimationController.SetTrigger("attack");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("enemy"))
        {
            other.GetComponent<navmeshFollowPlayer>().Die();
            Vector3 test = other.transform.position - batCore.transform.position;
            other.GetComponent<Rigidbody>().AddForce(test * 500f,ForceMode.Force);
            other.GetComponent<Rigidbody>().useGravity = true;
        }
    } 
}
