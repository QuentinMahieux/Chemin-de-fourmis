using System.Collections.Generic;
using UnityEngine;

public class BuildInterface : MonoBehaviour
{
    public static BuildInterface instance;
    
    public MouseSelector mouseSelector;
    public List<BuildArticleData> objetifs;
    public GameObject buildMenu;
    public Sommet sommet;

    void Awake()
    {
        if (!instance) instance = this;
        else Destroy(this);
    }
    
    void Start()
    {
        gameObject.SetActive(false);
    }

    public void CreateNewObjetif(ObjetifData objetifData)
    {
        sommet.defaultZone.SetObjetif(objetifData.Instance(), ObjetifBuild.Create);
        mouseSelector.OpenInterface(false);
    }

    public void Refresh()
    {
        int index = 0;
        foreach (BuildSlot slot in buildMenu.GetComponentsInChildren<BuildSlot>(true))
        {
            if(index >= objetifs.Count)
            {
                slot.gameObject.SetActive(false);
            }
            else
            {
                slot.gameObject.SetActive(true);
                slot.Set(objetifs[index]);
                index++;
            }
        }
    }
}
