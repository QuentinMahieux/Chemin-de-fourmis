using UnityEngine;

[CreateAssetMenu(fileName = "CreateCollectibleAction", menuName = "Scriptable Objects/Action/CreateCollectibleAction")]
public class CreateCollectibleAction : CollecteAction
{
    public override void PassifAction(Sommet sommet)
    {
        if (sommet.defaultZone.objetif.currenQuantity < sommet.defaultZone.objetif.maxQuantity)
        {
            sommet.defaultZone.objetif.currenQuantity++;
        }

        if (sommet.defaultZone.objetif.currenQuantity >= sommet.defaultZone.objetif.maxQuantity)
        {
            sommet.defaultZone.elementSpriteRenderer.sprite = sommet.defaultZone.objetif.elementSprite;
        }
    }
}
