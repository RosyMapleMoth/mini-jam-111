using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bat : MonoBehaviour
{

    public Node MyNode;
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
    }

    public void Swing()
    {
        if (!myAnimationController.GetCurrentAnimatorStateInfo(0).IsName("batAttack"))
        {
            myAnimationController.SetTrigger("attack");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("enemy"))
        {
            other.transform.root.GetComponent<navmeshFollowPlayer>().Hit(batCore.transform.position,100f);
            /*other.GetComponent<navmeshFollowPlayer>().Die();
            //other.GetComponent<Rigidbody>().AddForce(test * 500f,ForceMode.Force);
            foreach (Rigidbody i in other.GetComponentsInChildren<Rigidbody>())
            {
                Vector3 ok = i.transform.position - batCore.transform.position;
                i.GetComponent<Rigidbody>().AddForce(ok * 100f,ForceMode.Force);
            }
            other.GetComponent<Rigidbody>().useGravity = true;
            */
        }
    } 
}
