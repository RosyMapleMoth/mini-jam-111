using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FollowPlayer : MonoBehaviour
{
    public GameObject Player;

    public MovementGraph graph;

    public Node MyNode;

    public List<Cell> open;
    public List<Cell> closed;
    public Node target;

    public bool Moving;

    Stack<GameObject> pathing;

    public float MosterSpeed = 4f;
    public float MosterRotateSpeed = 20f;
    private float KillTimer = 0f;
    public bool KillPlayer = false;
    private float TimeToKill = 7f;
    public bool dead = false;
    Vector3 playerLastPostition;

    public class Cell
    {
        
        public Cell(Node n, Cell p, float g, float h)
        {
            this.parentCell = p;
            this.node = n;
            this.f = g+h;
            this.g = g;
            this.h = h;
        }
        public Node node;
        public Cell parentCell;
        
        public float f,g,h;
    }


    // Start is called before the first frame update
    void Awake()
    {
        pathing = new Stack<GameObject>();
        open = new List<Cell>();
        closed = new List<Cell>();
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
            if (Moving && pathing.Count > 0 )
            {
                //
                transform.position = Vector3.MoveTowards(transform.position, pathing.Peek().transform.position, MosterSpeed * Time.deltaTime);

                // look at thing
                Vector3 origRot = transform.eulerAngles;
                transform.LookAt(pathing.Peek().transform.position);
                origRot.y = transform.eulerAngles.y;
                transform.eulerAngles = origRot;



                if (Vector3.Distance(transform.position, pathing.Peek().transform.position) < 0.2f)
                {
                    GameObject temp = pathing.Pop();
                    temp.GetComponent<MeshRenderer>().enabled = false;
                }
            }

            if (MyNode == Player.GetComponent<playerController>().myNode)
            {
                playerLastPostition = Player.transform.position;
                KillTimer = 0f;
                KillPlayer = true;
                //gameObject.transform.SetParent(Player.transform);
            }
        }
    }

    private void GameOver()
    {

        
        ///SceneManager.LoadScene("player Test");
    }

    public void AStar()
    {   


        open.Clear();
        closed.Clear();
        pathing.Clear();
        float myNodeDist = float.PositiveInfinity;
        foreach (GameObject i in graph.nodes)
        {
            float dist = Vector3.Distance(transform.position,i.transform.position);
            if (dist < myNodeDist)
            {
                myNodeDist = dist;
                MyNode = i.GetComponent<Node>();
            }
            i.GetComponent<MeshRenderer>().enabled = false;   
        } 
        open.Add(new Cell(MyNode,null,0,0));
        Cell ourCell = AStarPermutation();
        highlightPath(ourCell);
        Moving = true;
    }


    public Cell AStarPermutation()
    {
        Debug.Log("Astar: Starting ");
        while (open.Count > 0)
        {
            float lowestF = float.PositiveInfinity;
            Cell q = null;
            for (int i = 0; i < open.Count; i++)
            {
                if (open[i].f < lowestF)
                {
                    Debug.Log("Astar: Guesing Q should be " + open[i].node.gameObject.name + " has f of : " + open[i].f + " \n vs \n " + q + " has f of " + lowestF);
                    q = open[i];
                    lowestF = open[i].f;
                }
            }
            Debug.Log("Astar: Q is " + q.node.gameObject.name + " with f of " + lowestF);
            open.Remove(q);

            foreach(Node i in q.node.connectedNodes)
            {
                if (i == target)
                {
                    return new Cell(i,q,getG(i,q),getH(i));
                }
                else
                {
                    Cell temp = new Cell(i,q,getG(i,q),getH(i));
                    Debug.Log("Astar: Checking " + temp.node.gameObject.name);
                    if (!isAlreayOnOpen(temp) && !isAlreayOnClosed(temp))
                    {
                        open.Add(temp);
                        Debug.Log("Astar: Adding " + temp.node.gameObject.name);
                    }
                }
            }
            closed.Add(q);
        }
        return null;
    }

    private bool isAlreayOnClosed(Cell temp)
    {
        foreach (Cell i in closed)
        {
            if (temp.node == i.node && temp.f >= i.f)
            {
                return true;
            } 
        }
        return false;
    }

    private bool isAlreayOnOpen(Cell temp)
    {
        foreach (Cell i in open)
        {
            if (temp.node == i.node && temp.f >= i.f)
            {
                return true;
            } 
        }
        return false;
    }

    private void grabPath(Cell cell)
    {
        throw new NotImplementedException();
    }


    public float getH(Node start)
    {
        return (Vector3.Distance(start.transform.position, target.transform.position));
    }
    public float getG(Node start, Cell Parent)
    {
        return (Vector3.Distance(start.transform.position, Parent.node.transform.position));
    }


    public void highlightPath(Cell end)
    {   
        Cell Cur = end;
        while (Cur.parentCell != null)
        {
            pathing.Push(Cur.node.gameObject);
            //Cur.node.gameObject.GetComponent<MeshRenderer>().enabled = true;
            Cur = Cur.parentCell;
        }
            pathing.Push(Cur.node.gameObject);
            //Cur.node.gameObject.GetComponent<MeshRenderer>().enabled = true;
    }

    public void Die()
    {
        dead = true;
        //this.gameObject.SetActive(false);
        Debug.Log("IT HAPPEND");
    }
}
