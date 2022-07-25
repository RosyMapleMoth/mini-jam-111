using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(MovementGraph))]
public class GraphEditor : Editor {

    MovementGraph Graph;

    void OnEnable()
    {
        Graph = (MovementGraph)target;
    }

    public override void OnInspectorGUI() {
        base.OnInspectorGUI();

    }

    void OnSceneGUI() {


        for (int i = 0; i < Graph.nodes.Count; i++)
        {
            Handles.Label(Graph.nodes[i].transform.position, Graph.nodes[i].gameObject.name);

        }

        for (int i = 0; i < Graph.edges.Length; i++)
        {
            Handles.Label(Vector3.Lerp(Graph.edges[i].edge1.transform.position,Graph.edges[i].edge2.transform.position,0.5f), Vector3.Distance(Graph.edges[i].edge1.transform.position,Graph.edges[i].edge2.transform.position).ToString());
            Handles.DrawLine(Graph.edges[i].edge1.transform.position, Graph.edges[i].edge2.transform.position,3f);
        }

    }
}
