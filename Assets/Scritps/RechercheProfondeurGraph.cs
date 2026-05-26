using System;
using System.Collections.Generic;
using UnityEngine;
using Debug = UnityEngine.Debug;


public class RechercheProfondeurGraph : MonoBehaviour
{
    public static RechercheProfondeurGraph instance;
    
    public GameObject parentSommet;
    public List<Sommet> graph =  new List<Sommet>();
    //public List<PathFind> path;
    
    

    void Awake()
    {
        if (!instance) instance = this;
        else Destroy(gameObject);
        
        foreach (Sommet s in parentSommet.GetComponentsInChildren<Sommet>())
        {
            graph.Add(s);
        }
        
        int id = 0;
        foreach (Sommet sommet in graph)
        {
            sommet.id = id;
            id++;
        }
    }
    
    void Start()
    {
        
    }
    
    void PathFinding(Sommet s, Sommet end, List<Sommet> dejavue, List<PathFind> path)
    {
        if (s == null) return;
    
        if (dejavue.Contains(s)) return;
        
    
        dejavue.Add(s);
    
        if (s.id == end.id)
        {
            path.Add(new PathFind());
            path[^1].path.AddRange(dejavue);
            Debug.Log("Chemin trouvé !");
            return;
        }
    
        foreach (Edge edge in s.edges)
        {
            PathFinding(edge.neighbour,end, new List<Sommet>(dejavue), path);
        }
    }
    
    public List<Sommet> BestPath(Sommet start, Sommet end)
    {
        List<PathFind> path = new List<PathFind>();
        
        PathFinding(start,end, new List<Sommet>(), path);
        
        PathFind best = null;
        float bestCost = float.MaxValue;

        foreach (PathFind p in path)
        {
            float cost = 0;
            bool isBloked = false;

            for (int i = 0; i < p.path.Count - 1; i++)
            {
                foreach (Edge e in p.path[i].edges)
                {
                    if (e.neighbour == p.path[i + 1])
                    {
                        if (e.neighbour.isBloked)
                        {
                            Debug.Log(e.neighbour.name + "is bloked");
                            isBloked = true;
                        }
                        else
                        {
                            cost += e.size;
                        }
                    }
                }
            }

            if(isBloked) break;
            
            if (cost < bestCost)
            {
                bestCost = cost;
                best = p;
            }
        }
        
        if(best == null) return null;

        return best.path;
    }
}
    
[Serializable]
public class PathFind
{
    public List<Sommet> path = new List<Sommet>();
}

