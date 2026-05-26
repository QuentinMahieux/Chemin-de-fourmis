using UnityEngine;

public class DefaultZone : MonoBehaviour
{
    public TypeZone typeZone;
    
    public  virtual TypeZone FindTypeZone()
    {
        return typeZone;
    }

    public virtual void Work(){}
}

public enum TypeZone
{
    Void,
    Food,
    AntHill
}