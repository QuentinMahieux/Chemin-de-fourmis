using UnityEngine;

public class MouseSelector : MonoBehaviour
{
    public ObjetifData objetifTarget;
    public BuildInterface buildInterface;
    public PopUpSommet popUpSommet;
    
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 worldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            RaycastHit2D hit = Physics2D.Raycast(worldPos, Vector2.zero);

            if (hit.collider != null && hit.collider.CompareTag("Sommet"))
            {
                Sommet sommet = hit.collider.GetComponent<Sommet>();

                if (sommet != null)
                {
                    sommet.defaultZone.SetObjetif(objetifTarget.Instance(), ObjetifBuild.Create);
                }
            }
        }
        if (Input.GetMouseButtonDown(1))
        {
            Vector2 worldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            RaycastHit2D hit = Physics2D.Raycast(worldPos, Vector2.zero);

            if (hit.collider != null && hit.collider.CompareTag("Sommet"))
            {
                Sommet sommet = hit.collider.GetComponent<Sommet>();

                if (sommet != null)
                {
                    OpenInterface(true, sommet);
                }
            }
            else
            {
                OpenInterface(false);
            }
            
            if (hit.collider != null && hit.collider.CompareTag("Armand"))
            {
                Armand armand = hit.collider.GetComponent<Armand>();

                if (armand != null)
                {
                    Debug.unityLogger.Log("Hit");
                    armand.OpenShop();
                }
            }
        }
        
        Vector2 _worldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        RaycastHit2D _hit = Physics2D.Raycast(_worldPos, Vector2.zero);

        if (_hit.collider != null && _hit.collider.CompareTag("Sommet"))
        {
            Sommet sommet = _hit.collider.GetComponent<Sommet>();

            if (sommet != null &&  sommet.defaultZone.objetif.id != "0")
            {
                popUpSommet.Affiche(true, sommet.defaultZone.objetif);
            }
            else popUpSommet.Affiche(false);
        }
        else
        {
            popUpSommet.Affiche(false);
        }
    }
    
    public void OpenInterface(bool isOpen, Sommet sommet = null)
    {
        if (isOpen)
        {
            Vector2 mousePos = Input.mousePosition;
            mousePos = Camera.main.ScreenToWorldPoint(mousePos);
            buildInterface.transform.position = new Vector3(mousePos.x, mousePos.y, 0);
            
            buildInterface.gameObject.SetActive(true);
            
            buildInterface.sommet = sommet;

            buildInterface.Refresh();
        }
        else
        {
            buildInterface.gameObject.SetActive(false);
        }
    }
}