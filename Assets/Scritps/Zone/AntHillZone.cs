using System;
using System.Collections.Generic;
using UnityEngine;

public class AntHillZone : DefaultZone
{
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
    
    public override void Work()
    {
        GameObject newAnt = Instantiate(antData.prefab, transform.position, transform.rotation, transform);
        
        
        DefaultIA newIA = newAnt.GetComponent<DefaultIA>();

        newIA.home = antHill;
        
        GoSleep(newIA);
        
    }

    //Regarde si il y a le ration de travailleur ou non
    bool LookRatioWork()
    {
        if(antWorks == null || antWorks.Count == 0) return false;
        if(antWorks.Count/100 >= rationWork) return true;
        return false;
    }

    public void GoSleep(DefaultIA ant)
    {
        ant.gameObject.SetActive(false);
        
        antWorks.Remove(ant);
        antSpeeds.Add(ant);
        
        if(!LookRatioWork()) GoWork(ant);
    }

    public void GoWork(DefaultIA ant)
    {
        ant.gameObject.SetActive(true);
        
        antWorks.Remove(ant);
        antSpeeds.Add(ant);
    }
}
