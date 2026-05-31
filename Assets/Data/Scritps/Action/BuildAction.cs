using UnityEngine;

[CreateAssetMenu(fileName = "BuildAction", menuName = "Scriptable Objects/Action/BuildAction")]
public class BuildAction : DefaultAction
{
    public ObjetifData build;
    public override void Action(Sommet sommet)
    {
        if (sommet.defaultZone.objetif.currenQuantity >= sommet.defaultZone.objetif.maxQuantity)
        {
            sommet.defaultZone.SetObjetif(build.Instance(), ObjetifBuild.Force);
        }
    }
}
