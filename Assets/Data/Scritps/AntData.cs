using System;
using UnityEngine;

[CreateAssetMenu(fileName = "AntData", menuName = "Scriptable Objects/AntData")]
public class AntData : ScriptableObject
{
    [field: SerializeField] public float life { get; private set; }
    [field: SerializeField] public float speed { get; private set; }
    [field: SerializeField] public float force { get; private set; }
    [field: SerializeField, Range(0,2)] public float work { get; private set; }
    [field: SerializeField] public float sociability { get; private set; }
    
    public GameObject prefab;

    public virtual AntDataInstance Instance()
    {
        return new AntDataInstance(this);
    }
}
[Serializable]
public class AntDataInstance
{
    public float life;
    public float speed;
    public float force;
    [Range(0,2)] public float work;
    public float sociability;

    public AntDataInstance(AntData data)
    {
        life = data.life;
        speed = data.speed;
        force = data.force;
        work = data.work;
        sociability = data.sociability;
    }
}