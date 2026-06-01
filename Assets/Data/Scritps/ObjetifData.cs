using System;
using UnityEngine;

[CreateAssetMenu(fileName = "ObjetifData", menuName = "Scriptable Objects/ObjetifData")]
public class ObjetifData : ScriptableObject
{
    [field: SerializeField] public string id { get; private set; }
    [field: SerializeField] public string name { get; private set; }

    [field: SerializeField] public Sprite elementSprite { get; private set; }
    [field: SerializeField] public Sprite targetSprite { get; private set; }

    
    [field: SerializeField] public int currenQuantity { get; private set; }
    [field: SerializeField] public int maxQuantity { get; private set; }
    [field: SerializeField] public FoodData collectible { get; private set; }

    [field: SerializeField] public bool isGoal { get; private set; }
    [field: SerializeField] public ObjetifData home { get; private set; }
    [field: SerializeReference] public DefaultAction action { get; private set; }
    [field: SerializeReference] public float actionTimer { get; private set; }
    [field: SerializeReference] public int level { get; private set; }





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
    public string name;
    public Sprite elementSprite;
    public Sprite targetSprite;
    
    public int currenQuantity;
    public int maxQuantity;
    public FoodData collectible;
    public ObjetifData home;
    public bool isGoal;
    public DefaultAction action;
    public float actionTimer;
    public int level;
    

    public ObjetifDataInstance(ObjetifData data)
    {
        id =  data.id;
        name =  data.name;
        elementSprite = data.elementSprite;
        targetSprite = data.targetSprite;
        currenQuantity = data.currenQuantity;
        maxQuantity = data.maxQuantity;
        collectible = data.collectible;
        home = data.home;
        isGoal = data.isGoal;
        action = data.action;
        actionTimer = data.actionTimer;
        level = data.level;
    }
}
