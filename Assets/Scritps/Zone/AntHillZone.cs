using System;
using System.Collections.Generic;
using UnityEngine;

public class AntHillZone : DefaultZone
{
    [Header("Zone AntHill")]

    public int defaultAnt = 5;

    public Sommet antHill;
    public AntData antData;
    public List<DefaultIA> antWorks = new List<DefaultIA>();
    public List<DefaultIA> antSpeeds = new List<DefaultIA>();

    [Range(0,1)] public float rationWork = 0.6f;

    void Start()
    {
        for (int i = 0; i < defaultAnt; i++)
        {
            Work();
        }
    }
    
    //TODO POOLING SYSTEM
    public override float Work()
    {
        DefaultIA newAnt = Instantiate(antData.prefab, transform.position, transform.rotation, transform);
        

        newAnt.home = antHill;
        
        GoSleep(newAnt);
        
        return base.Work();
        
    }

    //Regarde si il y a le ration de travailleur ou non
    private bool LookRatioWork()
    {
        if(antWorks == null || antWorks.Count == 0) return false;
        
        if(antWorks.Count/100 >= rationWork) return true;
        
        return false;
    }

    private void GoSleep(DefaultIA ant)
    {
        ant.gameObject.SetActive(false);
        
        antWorks.Remove(ant);
        antSpeeds.Add(ant);
        
        if(!LookRatioWork()) GoWork(ant);
    }

    private void GoWork(DefaultIA ant)
    {
        ant.gameObject.SetActive(true);
        
        antWorks.Remove(ant);
        antSpeeds.Add(ant);
    }
}
