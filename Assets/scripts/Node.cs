using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    public List<Node> connectedNodes;

    public FollowPlayer enemy;

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
            enemy.target = this;
            if (!enemy.KillPlayer)
            {
                enemy.target = this;  
                enemy.AStar();
            }
            other.GetComponent<playerController>().myNode = this;
        }
        if (other.CompareTag("enemy"))
        {
            other.GetComponent<FollowPlayer>().MyNode = this;
        }
    } 


}
