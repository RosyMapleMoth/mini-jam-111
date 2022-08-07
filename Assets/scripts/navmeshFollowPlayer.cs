using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class navmeshFollowPlayer : MonoBehaviour
{
    public GameObject Bones;
    public Rigidbody[] childbones;

    public float hitTimer = 0;
    public Animator myanimator;
    public NavMeshAgent ai;
    public GameObject Player;
    private float KillTimer = 0f;
    public bool KillPlayer = false;
    private float TimeToKill = 7f;
    public bool dead = false;
    Vector3 playerLastPostition;



    void Awake()
    {
    }

    // Start is called before the first frame update
    void Start()
    {
        childbones = GetComponentsInChildren<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (hitTimer > 0)
        {
            hitTimer -= Time.deltaTime;
            if (hitTimer < 0)
            {
                hitTimer = 0;
            }
        }
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
        GetComponent<Rigidbody>().useGravity = true;
        GetComponent<BoxCollider>().isTrigger = false;
        myanimator.enabled = false;
        Bones.SetActive(true);
        ai.enabled = false;
        //this.gameObject.SetActive(false);
        Debug.Log("IT HAPPEND");
    }

    private void GameOver()
    {

        
        ///SceneManager.LoadScene("player Test");
    }

    public void Hit(Vector3 hitLocation, float force)
    {
        if (hitTimer == 0)
        {
            hitTimer = 2;
            foreach (Rigidbody i in childbones)
            {
                Vector3 final_dir = i.transform.position - hitLocation;
                i.GetComponent<Rigidbody>().AddForce(final_dir * force, ForceMode.Force);
            }
            if (!dead)
            {
                Die();
            }
        }
    }
}
