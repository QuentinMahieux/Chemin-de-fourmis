using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;
    public Transform spawnPoint;
    public AntData antData;
    public int defaultNum = 0;
    private List<DefaultIA> ants = new List<DefaultIA>();

    void Awake()
    {
        if (!instance) instance = this;
        else Destroy(gameObject);
    }

    void Start()
    {
        for (int i = 0; i < defaultNum; i++)
        {
            DefaultIA newAnt = Instantiate(antData.prefab, spawnPoint.transform.position, spawnPoint.transform.rotation, spawnPoint.transform);
        
            AddAnt(newAnt);
        }
    }

    public void AddAnt(DefaultIA ant)
    {
        ants.Add(ant);
    }

    public DefaultIA ManipuleAnt(Sommet goal, Sommet home)
    {
        Debug.Log("Start Manipulation");
        foreach (DefaultIA ant in ants)
        {
            if (!ant.goal && !ant.home)
            {
                ant.goal = goal;
                ant.goalID = goal.defaultZone.objetif.id;
                ant.home = home;
                ant.homeID = home.defaultZone.objetif.id;

                ant.FindSommet(ant.currentSommet, ant.goal);

                return ant;
            }
        }
        return null;
    }

    public void RemoveAnt(DefaultIA ant)
    {
        ants.Remove(ant);
    }
}
