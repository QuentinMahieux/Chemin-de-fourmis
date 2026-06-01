using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ArticleSlot : MonoBehaviour
{
    public ArticleData data;
    public TMP_Text priceText;
    public Image image;
    public Armand dealer;

    public void Set(ArticleData newData, Armand newDealer)
    {
        dealer = newDealer;
        data = newData;
        priceText.text = data.price + "$";
        image.sprite = data.sprite;
    }

    public void OnClick()
    {
        dealer.Buy(data);
    }
}
