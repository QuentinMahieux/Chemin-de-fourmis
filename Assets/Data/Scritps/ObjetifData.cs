using System;
using UnityEngine;

[CreateAssetMenu(fileName = "ObjetifData", menuName = "Scriptable Objects/ObjetifData")]
public class ObjetifData : ScriptableObject
{
    [field: SerializeField] public string id { get; private set; }
    [field: SerializeField] public Sprite elementSprite { get; private set; }
    [field: SerializeField] public Sprite finalElementSprite { get; private set; }

    [field: SerializeField] public Sprite targetSprite { get; private set; }

    
    [field: SerializeField] public int currenQuantity { get; private set; }
    [field: SerializeField] public int maxQuantity { get; private set; }
    [field: SerializeField] public FoodData collectible { get; private set; }


    [field: SerializeField] public ObjetifData home { get; private set; }
    [field: SerializeReference] public DefaultAction action { get; private set; }


    public void Reset()
    {
        id = Guid.NewGuid().ToString();
    }


    public virtual ObjetifDataInstance Instance()
    {
        return new ObjetifDataInstance(this);
    }
}
[Serializable]
public class ObjetifDataInstance
{
    public string id;
    public Sprite elementSprite;
    public Sprite finalElementSprite;
    public Sprite targetSprite;
    
    public int currenQuantity;
    public int maxQuantity;
    public FoodData collectible;
    public ObjetifData home;
    public DefaultAction action;
    

    public ObjetifDataInstance(ObjetifData data)
    {
        id =  data.id;
        elementSprite = data.elementSprite;
        finalElementSprite = data.finalElementSprite;
        targetSprite = data.targetSprite;
        currenQuantity = data.currenQuantity;
        maxQuantity = data.maxQuantity;
        collectible = data.collectible;
        home = data.home;
        action = data.action;
        
    }
}
