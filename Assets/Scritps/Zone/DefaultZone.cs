using UnityEngine;

public class DefaultZone : MonoBehaviour
{
    public TypeZone typeZone;
    public float TimeWork = 5f;
    
    public  virtual TypeZone FindTypeZone()
    {
        return typeZone;
    }

    public virtual float Work()
    {
        return TimeWork;
    }
}

public enum TypeZone
{
    Void,
    Food,
    AntHill
}