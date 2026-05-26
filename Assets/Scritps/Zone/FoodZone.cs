using System.Collections;
using UnityEngine;

public class FoodZone : DefaultZone
{
    [Header("Zone food")] 
    public SpriteRenderer sprite;
    public FoodData foodData;
    [SerializeField] private FoodDataInstance foodDataInstance;
    
    private TypeZone defaultZone;
    

    void Start()
    {
        foodDataInstance = foodData.Instance();
        defaultZone = typeZone;
    }
    
    public override float Work()
    {
        foodDataInstance.currentFood--;

        if (foodDataInstance.currentFood <= 0)
        {
            RemoveZone();
        }
        
        return base.Work();
    }

    void RemoveZone()
    {
        sprite.enabled = false;
        typeZone = TypeZone.Void;
        StartCoroutine(RestoreZone());
    }
    
    IEnumerator RestoreZone()
    {
        yield return new WaitForSeconds(25f);
        sprite.enabled = true;
        typeZone = defaultZone;
        foodDataInstance.currentFood = foodDataInstance.maxFood;
    }
}
