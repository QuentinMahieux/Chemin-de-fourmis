using UnityEngine;

[CreateAssetMenu(fileName = "BuildArticleData", menuName = "Scriptable Objects/Article/BuildArticleData")]
public class BuildArticleData : ArticleData
{
    public ObjetifData objetif;
    public override void Buy()
    {
        BuildInterface.instance.objetifs.Add(this);
        BuildInterface.instance.Refresh();
    }
}
