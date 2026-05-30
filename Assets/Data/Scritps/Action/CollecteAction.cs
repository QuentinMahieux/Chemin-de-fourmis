using UnityEngine;

[CreateAssetMenu(fileName = "CollecteAction", menuName = "Scriptable Objects/Action/CollecteAction")]

public class CollecteAction : DefaultAction
{
    public override void Action(Sommet sommet)
    {
        if(sommet.defaultZone.objetif.currenQuantity > 0) return;

        sommet.defaultZone.elementSpriteRenderer.sprite = sommet.defaultZone.objetif.finalElementSprite;
    }
}
