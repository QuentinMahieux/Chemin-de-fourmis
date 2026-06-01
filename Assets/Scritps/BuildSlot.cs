using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BuildSlot : MonoBehaviour
{
    public BuildArticleData data;
    public Image image;
    public TMP_Text text;

    public void Set(BuildArticleData newData)
    {
        data = newData;
        image.sprite = data.sprite;
        text.text = data.name;
    }

    public void OnClick()
    {
        BuildInterface.instance.CreateNewObjetif(data.objetif);
    }
}
