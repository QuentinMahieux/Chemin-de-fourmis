using UnityEngine;

public class BuildInterface : MonoBehaviour
{
    public MouseSelector mouseSelector;
    public Sommet sommet;

    void Start()
    {
        gameObject.SetActive(false);
    }

    public void CreateNewObjetif(ObjetifData objetifData)
    {
        sommet.defaultZone.SetObjetif(objetifData.Instance(), ObjetifBuild.Create);
        mouseSelector.OpenInterface(false);
    }
}
