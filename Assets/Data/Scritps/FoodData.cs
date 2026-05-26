using System;
using UnityEngine;

[CreateAssetMenu(fileName = "FoodData", menuName = "Scriptable Objects/FoodData")]
public class FoodData : ScriptableObject
{
    [field: SerializeField] public int id { get; private set; }
    [field: SerializeField] public Sprite sprite { get; private set; }
    [field: SerializeField] public int maxFood { get; private set; }
    [field: SerializeField] public int currentFood { get; private set; }


    

    public virtual FoodDataInstance Instance()
    {
        return new FoodDataInstance(this);
    }
}
[Serializable]
public class FoodDataInstance
{
    public int id;
    public Sprite sprite;
    public int maxFood;
    public int currentFood;

    public FoodDataInstance(FoodData data)
    {
        id =  data.id;
        sprite = data.sprite;
        maxFood = data.maxFood;
        currentFood = data.maxFood;
    }
}
