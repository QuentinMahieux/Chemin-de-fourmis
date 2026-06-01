using UnityEngine;

//[CreateAssetMenu(fileName = "ArticleData", menuName = "Scriptable Objects/Article/")]
public abstract class ArticleData : ScriptableObject
{
    public int price;
    public Sprite sprite;
    public abstract void Buy();
}
