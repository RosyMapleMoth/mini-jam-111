using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementGraph : MonoBehaviour
{

    [SerializeField] public SubClass[] edges;
	public void SetValue (int index, SubClass subClass) {
	
		// Perform any validation checks here.
		edges[index] = subClass;
	}
	public SubClass GetValue (int index) {
		// Perform any validation checks here.
		return edges[index];
	}

    [System.Serializable]
    public class SubClass {
        public GameObject edge1;
        public GameObject edge2;
    }

    public List<GameObject> nodes;

    ///GameObject[] edges;

 
    // Start is called before the first frame update
    void Start()
    {
        foreach (SubClass i in edges)
        {
            //i.edge1.GetComponent<Node>().connectedNodes.Add(i.edge2.GetComponent<Node>());
            //i.edge2.GetComponent<Node>().connectedNodes.Add(i.edge1.GetComponent<Node>());
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
