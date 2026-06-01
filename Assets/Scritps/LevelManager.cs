using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;
    public Transform spawnPoint;
    public AntData antData;
    public int defaultNum = 0;
    public List<DefaultIA> ants = new List<DefaultIA>();
    
    [Header("Level Information")]
    public TMP_Text antNumberText;

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
        foreach (DefaultIA ant in ants)
        {
            if (!ant.goal && !ant.home && ant.antDataInstance.level >= goal.defaultZone.objetif.level)
            {
                ant.goal = goal;
                ant.goalID = goal.defaultZone.objetif.id;
                ant.home = home;
                ant.homeID = home.defaultZone.objetif.id;

                ant.FindSommet(ant.currentSommet, ant.goal);
                FreeAnt();
                return ant;
            }
        }
        return null;
    }

    public void FreeAnt()
    {
        int num = 0;
        foreach (DefaultIA ant in ants)
        {
            if (!ant.goal && !ant.home) num++;
        }
        
        antNumberText.text = num.ToString() + "/" + ants.Count.ToString();
    }

    public void RemoveAnt(DefaultIA ant)
    {
        ants.Remove(ant);
    }
}
