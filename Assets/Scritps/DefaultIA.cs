using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DefaultIA : MonoBehaviour
{
    public AntData antData;
    public AntDataInstance antDataInstance;
    public Rigidbody2D rb;
    public SpriteRenderer spriteRenderer;

    [Header("Objetif")] 
    public Sommet lastSommet;
    public Sommet currentSommet;
    public Sommet home;
    public Sommet goal;

    [Header("Visual")] 
    public SpriteRenderer sheet;
    
    [Header("Task")]
    public List<Sommet> tasks;

    [Header("Timer")] 
    public float currentTime;
    public float maxTime;
    
    void OnEnable()
    {
        tasks = new List<Sommet>();
        sheet.gameObject.SetActive(false);
        antDataInstance = new AntDataInstance(antData);
        
        //Variation Génétique

        
        transform.localScale = new Vector3(1, 1, 1);
        
        antDataInstance.speed *= Random.Range(0.9f, 1.1f);
        
        float mutForce = Random.Range(0.7f, 1.3f);
        antDataInstance.force *= mutForce;
        transform.localScale *= mutForce;
        
        antDataInstance.work *= Random.Range(0.7f, 1.3f);
        
        antDataInstance.sociability  *=  Random.Range(0.5f, 5f);
    }

    void FixedUpdate()
    {
        currentTime += Time.deltaTime;
        if (currentTime <= (maxTime * antDataInstance.work))
        {
            return;
        }
        
        if(tasks.Count != 0) Move();
        else
        {
            if(currentSommet == null) return;

            Sommet random = currentSommet.edges[Random.Range(0, currentSommet.edges.Length)].neighbour;

            if (random.isBloked) return;
           
            tasks.Add(random);
        }
    }

    void Move()
    {
        if(tasks[0].isBloked) tasks[0] = lastSommet;
        
        Vector3 cible = tasks[0].transform.position;
        spriteRenderer.sortingOrder = tasks[0].defaultZone.spriteRenderer.sortingOrder + 2;
        sheet.sortingOrder =  spriteRenderer.sortingOrder - 1;
        if (transform.position == tasks[0].transform.position)
        {
            tasks.RemoveAt(0);
        }
        else
        {
            Vector2 direction = new Vector2();
            if (!sheet.gameObject.activeSelf) direction = Vector2.MoveTowards(rb.position, cible, antDataInstance.speed * Time.deltaTime) ;
            else direction = Vector2.MoveTowards(rb.position, cible, (antDataInstance.speed/1.5f) * Time.deltaTime) ;


            
            rb.MovePosition(direction);
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Sommet"))
        {
            if(currentSommet) lastSommet = currentSommet;
            currentSommet = col.GetComponent<Sommet>();
            Explore(currentSommet);
            
            currentSommet.currentAnts.Add(this);
            
            DefaultIA enemy = LocateEnemy(currentSommet.currentAnts);
            if (enemy) Attack(enemy);
            
            DefaultIA friend = LocateFriend(currentSommet.currentAnts);
            //if (friend && goal) Manipulation(goal, this);
            
            if(!lastSommet) lastSommet = currentSommet;
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.CompareTag("Sommet"))
        {
            currentSommet = col.GetComponent<Sommet>();
            
            currentSommet.currentAnts.Remove(this);
            
        }
    }

    void Explore(Sommet sommet)
    {
        if(sommet.defaultZone.objetif == null) return;

        if (goal)
        {
            if (sommet.id == goal.id)
            {
                goal = null;
                if(!sommet.defaultZone.ChangeObjetif(-1, sommet)) return;
                TakeEat(true, sommet);
                FindSommet(currentSommet, home);
            }
        }
        if (home)
        {
            if (sommet.id == home.id && goal == null)
            {
                home = null;
                if(!sommet.defaultZone.ChangeObjetif(1, sommet)) return;
                TakeEat(false, sommet);
            }
        }
       
    }

    void TakeEat(bool isTake, Sommet sommet)
    {
        if (isTake)
        {
            if (!sheet.gameObject.activeSelf)
            {
                maxTime = sommet.defaultZone.Work();
                sheet.gameObject.SetActive(true);
                sheet.sprite = sommet.defaultZone.objetif.collectible.sprite;
                currentTime = 0;
            }
        }
        else
        {
            if (sheet.gameObject.activeSelf)
            {
                maxTime = sommet.defaultZone.Work();
                sheet.gameObject.SetActive(false);
                currentTime = 0;
            }
        }
    }

    public void FindSommet(Sommet start, Sommet end)
    {
        List<Sommet> result = RechercheProfondeurGraph.instance.BestPath(start, end);
        if (result != null)
        {
            tasks.AddRange(result);
        }
    }

    DefaultIA LocateEnemy(List<DefaultIA> ants)
    {
        foreach (DefaultIA ant in ants)
        {
            if (ant.antDataInstance.id != antDataInstance.id)
            {
                return ant;
            }
        }
        return null;
    }
    DefaultIA LocateFriend(List<DefaultIA> ants)
    {
        foreach (DefaultIA ant in ants)
        {
            if (ant.antDataInstance.id == antDataInstance.id)
            {
                return ant;
            }
        }
        return null;
    }


    //Attaque une fourmis
    void Attack(DefaultIA ant)
    {
        maxTime = 1;
        currentTime = 0;
        
        ant.TakeDamage(antDataInstance.force, this);
    }
    
    //PRendre des dégat
    void TakeDamage(float damage,  DefaultIA enemy)
    {
        antDataInstance.life -= damage;
        if (antDataInstance.life <= 0)
        {
            Destroy(gameObject);
            return;
        }
        
        //Riposte
        Attack(enemy);
    }
    
    float DistanceToCible(Transform cible)
    {
        return Vector2.Distance(transform.position, cible.transform.position);
    }
}