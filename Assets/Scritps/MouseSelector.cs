using UnityEngine;

public class MouseSelector : MonoBehaviour
{
    public bool isFull;
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
                    if (sommet.isBloked && !isFull)
                    {
                        sommet.TallClutter();
                        isFull = true;
                    }
                    else if (!sommet.isBloked && isFull)
                    {
                        sommet.TallClutter();
                        isFull = false;
                    }
                }
            }
        }
    }
}