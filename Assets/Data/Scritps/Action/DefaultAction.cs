using UnityEngine;

[System.Serializable]
public abstract class DefaultAction : ScriptableObject
{
    public abstract void Action(Sommet sommet);
}
