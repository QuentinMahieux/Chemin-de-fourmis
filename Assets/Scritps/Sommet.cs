using System;
using System.Collections.Generic;
using UnityEngine;

public class Sommet : MonoBehaviour
{
    private void Awake()
    {
        Debug.Log("Sommet created couco u");  
    }
    
    
    [HideInInspector] public int id;
    public Edge[] edges;
    public List<DefaultIA> currentAnts;
    public DefaultZone defaultZone;
    public bool isBloked;
    //public GameObject visualBloker;
    
    void OnDrawGizmos()
    {
        if (edges == null) return;
        foreach (var edge in edges)
        {
            if (MutualNeighbor(edge.neighbour)) Gizmos.color = Color.darkRed;
            else Gizmos.color = Color.darkBlue;
            
            if (!edge.neighbour) break;
            Gizmos.DrawLine(transform.position, edge.neighbour.transform.position);
        }
    }

    void Start()
    {
        foreach (Edge e in edges)
        {
            e.size = DistanceToNeighbour(e.neighbour.transform);
        }
    }
    
    float DistanceToNeighbour(Transform cible)
    {
        return Vector2.Distance(transform.position, cible.transform.position);
    }

    bool MutualNeighbor(Sommet neighbour)
    {
        if(neighbour == null) return false;
        if(neighbour.edges == null) return false;
        
        foreach (var edge in neighbour.edges)
        {
            if (edge.neighbour.name == this.name) return true;
        }
        return false;
    }
    
    
}



[System.Serializable]
public class Edge
{
    public Sommet neighbour;
    [HideInInspector]public float size = 1;
   
}

