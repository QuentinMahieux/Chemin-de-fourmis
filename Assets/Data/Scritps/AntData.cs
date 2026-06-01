using System;
using UnityEngine;

[CreateAssetMenu(fileName = "AntData", menuName = "Scriptable Objects/AntData")]
public class AntData : ScriptableObject
{
    [field: SerializeField] public int id { get; private set; }
    [field: SerializeField] public float life { get; private set; }
    [field: SerializeField] public float speed { get; private set; }
    [field: SerializeField] public float force { get; private set; }
    [field: SerializeField, Range(0,2)] public float work { get; private set; }
    [field: SerializeField] public int level { get; private set; }
    
    public DefaultIA prefab;

    public virtual AntDataInstance Instance()
    {
        return new AntDataInstance(this);
    }
}
[Serializable]
public class AntDataInstance
{
    public int id;
    public float life;
    public float speed;
    public float force;
    [Range(0,2)] public float work;
    public float level;

    public AntDataInstance(AntData data)
    {
        id =  data.id;
        life = data.life;
        speed = data.speed;
        force = data.force;
        work = data.work;
        level = data.level;
    }
}