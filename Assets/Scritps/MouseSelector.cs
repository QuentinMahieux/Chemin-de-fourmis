using UnityEngine;

public class MouseSelector : MonoBehaviour
{
    public ObjetifData objetifTarget;
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
                    sommet.defaultZone.SetObjetif(objetifTarget.Instance());
                }
            }
        }
    }
}