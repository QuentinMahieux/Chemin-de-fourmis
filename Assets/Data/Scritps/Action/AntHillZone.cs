using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AntHillZone", menuName = "Scriptable Objects/Action/AntHill Zone")]
public class AntHillZone : DefaultAction
{
    public AntData antData;
    
    public override void Action( Sommet sommet)
    {
        DefaultIA newAnt = UnityEngine.Object.Instantiate(antData.prefab, sommet.transform.position, sommet.transform.rotation, sommet.transform);
        
        LevelManager.instance.AddAnt(newAnt);
    }
}
