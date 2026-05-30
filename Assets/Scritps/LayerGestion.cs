using UnityEngine;

public class LayerGestion : MonoBehaviour
{
    public int levelLayer = 10;

    [Header("Render")] 
    public SpriteRenderer zone;
    public SpriteRenderer element;
    public SpriteRenderer target;
    public Canvas canvas;

    void Start()
    {
        ChangeOrder(levelLayer);
    }
    
    void ChangeOrder(int order)
    {
        zone.sortingOrder = order;
        element.sortingOrder = order + 1;
        target.sortingOrder = order + 2;
        canvas.sortingOrder = order + 3;
    }
}
