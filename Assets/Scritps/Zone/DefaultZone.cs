using System.Collections.Generic;
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
    public int antsInTransit = 0;
    public TMP_Text nbrText;
    public List<DefaultIA> currentAnts;


    [SerializeField] ObjetifData defaultObjetif;
    public ObjetifDataInstance objetif;

    [Header("Passif Action")] 
    private float actionTime;

    [Header("Animation")] 
    public Animator animator;
    
    protected virtual void Start()
    {
        targetSpriteRenderer.gameObject.SetActive(false);
        currentSommet = gameObject.GetComponent<Sommet>();
        if (defaultObjetif)
        {
            SetDefaultObjetif(defaultObjetif.Instance());
        }
    }

    void Update()
    {
        if ( objetif == null) return;
        if(!objetif.action || objetif.actionTimer == 0) return;
        
        actionTime += Time.deltaTime;
        if (actionTime > objetif.actionTimer)
        {
            actionTime = 0;
            objetif.action.PassifAction(currentSommet);
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

    public void SetObjetif(ObjetifDataInstance newObjetif, ObjetifBuild objetifBuild)
    {
        if ((objetif.id == "0" && objetifBuild == ObjetifBuild.Create) || objetifBuild == ObjetifBuild.Force)
        {
            objetif = newObjetif;
            elementSpriteRenderer.sprite = objetif.elementSprite;
        }
        
     
        if(!objetif.home) return;
        if(!ChangeObjetif(0, currentSommet,false)) return;

        
        Sommet home = RechercheProfondeurGraph.instance.FindSommet(objetif.home.id);
            
        if(!home) return;
        if (currentAnts.Count >= objetif.currenQuantity && objetif.isGoal) return;
        if (currentAnts.Count >= objetif.maxQuantity - objetif.currenQuantity && !objetif.isGoal) return;


        DefaultIA ant = null;

        
        if (objetif.isGoal)
        {
            ant = LevelManager.instance.ManipuleAnt(currentSommet, home);
        }
        else
        {
             ant = LevelManager.instance.ManipuleAnt(home, currentSommet);
        }
        
        if(ant) AntAdd(ant);
            
            
        targetSpriteRenderer.gameObject.SetActive(true);
        targetSpriteRenderer.sprite = objetif.targetSprite;
    }
    
    public bool CanChangeObjetif(int number)
    {
        if (objetif == null) return false;
        int simulated = objetif.currenQuantity + number;
        return simulated >= 0 && simulated <= objetif.maxQuantity;
    }

    public bool ChangeObjetif(int number, Sommet sommet, bool isAnt)
    {
        if (objetif == null) return false;
        
        objetif.currenQuantity += number;

        if(objetif.action && isAnt) objetif.action.Action(sommet);
        
        
        if (objetif.currenQuantity >= 0 && objetif.currenQuantity <= objetif.maxQuantity)
        {
            return true;
        }

        if (objetif.currenQuantity > objetif.maxQuantity)
        {
            objetif.currenQuantity = objetif.maxQuantity;
        }

        if (objetif.currenQuantity < 0)
        {
            objetif.currenQuantity = 0;
        }
        
        targetSpriteRenderer.gameObject.SetActive(false);
        return false;
    }
    
    public void AntAdd(DefaultIA  ant)
    {
        currentAnts.Add(ant);
        nbrText.text = currentAnts.Count.ToString();
    }

    public void AntRemver(DefaultIA ant)
    {
        currentAnts.Remove(ant);
        nbrText.text = currentAnts.Count.ToString();
    }
}

public enum ObjetifBuild
{
    Force,
    Create,
    None
}