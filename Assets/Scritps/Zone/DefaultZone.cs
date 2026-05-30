using TMPro;
using UnityEngine;

public class DefaultZone : MonoBehaviour
{
    public float TimeWork = 5f;
    public SpriteRenderer spriteRenderer;
    public SpriteRenderer elementSpriteRenderer;
    public SpriteRenderer targetSpriteRenderer;
    private Sommet currentSommet;

    [Header("Objetfif")] 
    public int nbrAnt;
    public TMP_Text nbrText;

    [SerializeField] ObjetifData defaultObjetif;
    public ObjetifDataInstance objetif;
    protected virtual void Start()
    {
        targetSpriteRenderer.gameObject.SetActive(false);
        currentSommet = gameObject.GetComponent<Sommet>();
        if (defaultObjetif)
        {
            SetDefaultObjetif(defaultObjetif.Instance());
        }
    }
    

    public virtual float Work()
    {
        return TimeWork;
    }

    void SetDefaultObjetif(ObjetifDataInstance newObjetif)
    {
        objetif = newObjetif;
        elementSpriteRenderer.sprite = objetif.elementSprite;
    }

    public void SetObjetif(ObjetifDataInstance newObjetif)
    {
        if (objetif.type == ObjetifType.None)
        {
            if (newObjetif.type == ObjetifType.Build)
            {
                objetif = newObjetif;
                
                targetSpriteRenderer.gameObject.SetActive(true);
                targetSpriteRenderer.sprite = objetif.targetSprite;
            }
        }
        if (objetif.type == ObjetifType.Collect)
        {
            if(!ChangeObjetif(0, currentSommet)) return;

            Sommet home = RechercheProfondeurGraph.instance.FindSommet(objetif.home.id);
            if(!home) return;
            
            if(!LevelManager.instance.ManipuleAnt(currentSommet, home)) return;
            
            nbrAnt++;
            nbrText.text = nbrAnt.ToString();
            
            targetSpriteRenderer.gameObject.SetActive(true);
            targetSpriteRenderer.sprite = objetif.targetSprite;
        }
        
        
        
    }

    public bool ChangeObjetif(int number, Sommet sommet)
    {
        if (objetif == null) return false;
        
        objetif.currenQuantity += number;
        nbrAnt += number;
        nbrText.text = nbrAnt.ToString();

        if(objetif.action) objetif.action.Action(sommet);
        
        if (objetif.type == ObjetifType.Collect && objetif.currenQuantity > 0)
        {
            return true;
        }
        else if (objetif.type == ObjetifType.Build && objetif.currenQuantity < objetif.maxQuantity)
        {
            return true;
        }
        
        targetSpriteRenderer.gameObject.SetActive(false);
        return false;
    }
}