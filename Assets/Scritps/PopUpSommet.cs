using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PopUpSommet : MonoBehaviour
{
    public GameObject popup;
    public Vector2 decallage =  Vector2.one;
    public TMP_Text name;
    public TMP_Text ressource;
    public Image image;
    public Image ressourceImage;

    public void Affiche(bool isAfficher, ObjetifDataInstance data = null)
    {
        if (isAfficher)
        {
            popup.SetActive(true);
            name.text = data.name;
            image.sprite = data.elementSprite;

            if (data.collectible)
            {
                ressource.enabled = true;
                ressourceImage.enabled = true;
                ressource.text = data.currenQuantity + "/" + data.maxQuantity;
                ressourceImage.sprite = data.collectible.sprite;
            }
            else
            {
                ressource.enabled = false;
                ressourceImage.enabled = false;
            }
            
            
            Vector2 mousePos = Input.mousePosition;
            mousePos = Camera.main.ScreenToWorldPoint(mousePos);
            transform.position = new Vector3(mousePos.x + decallage.x, mousePos.y + decallage.y, 0);
        }
        else 
        {
            popup.SetActive(false);
        }
    }
}
