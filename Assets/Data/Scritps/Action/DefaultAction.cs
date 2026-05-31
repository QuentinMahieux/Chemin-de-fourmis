using UnityEngine;

[System.Serializable]
//[CreateAssetMenu(fileName = "CollecteAction", menuName = "Scriptable Objects/Action/CollecteAction")]
public abstract class DefaultAction : ScriptableObject
{
    public abstract void Action(Sommet sommet);

    public virtual void PassifAction(Sommet sommet) { }
}
