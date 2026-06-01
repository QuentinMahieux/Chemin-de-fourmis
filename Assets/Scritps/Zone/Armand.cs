using System.Collections.Generic;
using UnityEngine;

public class Armand : MonoBehaviour
{
    public Sommet currentSommet;
    public bool isOpen;
    public GameObject shop;
    public List<ArticleData> articles;
    public List<ArticleSlot> slots;

    void Start()
    {
        Refresh();
        OpenShop();
    }
    
    public void OpenShop()
    {
        isOpen = !isOpen;
        shop.SetActive(isOpen);
    }

    public void Buy(ArticleData article)
    {
        if (article.price <= currentSommet.defaultZone.objetif.currenQuantity)
        {
            currentSommet.defaultZone.objetif.currenQuantity -= article.price;
            article.Buy();
            articles.Remove(article);
            Refresh();
        }
    }

    void Refresh()
    {
        int index = 0;
        foreach (ArticleSlot slot in shop.GetComponentsInChildren<ArticleSlot>())
        {
            if(index >= articles.Count)
            {
                slot.gameObject.SetActive(false);
            }
            else
            {
                slot.gameObject.SetActive(true);
                slot.Set(articles[index],this);
                index++;
            }
            
        }
    }
}
