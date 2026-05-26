using System;
using UnityEngine;

[CreateAssetMenu(fileName = "ScriptableObjectInstance", menuName = "ScriptableObjects")]
public class ScriptableObjectInstance : ScriptableObject
{
    [field: SerializeField] public string  exemple { get; private set; }

    public virtual ScriptableObjectInstanceInstance Instance()
    {
        return new ScriptableObjectInstanceInstance(this);
    }
}

[Serializable]
public class ScriptableObjectInstanceInstance
{
    public string exemple;

    public ScriptableObjectInstanceInstance(ScriptableObjectInstance data)
    {
        exemple = data.exemple;
    }
}