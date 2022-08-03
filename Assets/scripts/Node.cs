using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    public List<Node> connectedNodes;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("player"))
        {
            other.GetComponent<playerController>().myNode = this;
        }
        if (other.CompareTag("enemy"))
        {
            other.GetComponent<FollowPlayer>().MyNode = this;
        }
    } 


}
