using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class navmeshFollowPlayer : MonoBehaviour
{

    public NavMeshAgent ai;
    public GameObject Player;
    private float KillTimer = 0f;
    public bool KillPlayer = false;
    private float TimeToKill = 7f;
    public bool dead = false;
    Vector3 playerLastPostition;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (dead)
        {

        }
        else if (KillPlayer)
        {

            KillTimer += Time.deltaTime;
            if (KillTimer > TimeToKill)
            {
                GameOver();
            }
            
            transform.position = transform.position + - playerLastPostition + Player.transform.position;
            transform.RotateAround(Player.transform.position, Vector3.up, 0.4f );

            Vector3 origRot = transform.eulerAngles;
            transform.LookAt(Player.transform.position);
            origRot.y = transform.eulerAngles.y;
            transform.eulerAngles = origRot;

            playerLastPostition = Player.transform.position;
        }
        else
        {
            ai.destination = Player.transform.position;
        }
    }

    public void Die()
    {
        dead = true;
        ai.enabled = false;
        //this.gameObject.SetActive(false);
        Debug.Log("IT HAPPEND");
    }

    private void GameOver()
    {

        
        ///SceneManager.LoadScene("player Test");
    }
}
