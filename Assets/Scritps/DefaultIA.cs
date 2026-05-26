using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DefaultIA : MonoBehaviour
{
    public AntData antData;
    public AntDataInstance antDataInstance;
    public Rigidbody2D rb;

    [Header("Objetif")] 
    public Sommet lastSommet;
    public Sommet currentSommet;
    public Sommet home;
    public Sommet goal;

    [Header("Visual")] 
    public GameObject sheet;
    
    
    [Header("Task")]
    public List<Sommet> tasks;

    [Header("Timer")] 
    public float currentTime;
    public float maxTime;
    
    [Header("Information")]
    public AntState currentState = AntState.None;
    void OnEnable()
    {
        tasks = new List<Sommet>();
        sheet.SetActive(false);
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
        if (transform.position == tasks[0].transform.position)
        {
            tasks.RemoveAt(0);
        }
        else
        {
            currentState = AntState.Lost;
            Vector2 direction = new Vector2();
            if (!sheet.activeSelf) direction = Vector2.MoveTowards(rb.position, cible, antDataInstance.speed * Time.deltaTime) ;
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
            if (friend && goal) Manipulation(goal, this);
            
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
        TypeZone typeZone = sommet.defaultZone.FindTypeZone();

        if (typeZone == TypeZone.Void && goal)
        {
            if( sommet.id == goal.id) goal = null;
            
        }
        else if (typeZone == TypeZone.Food)
        {
            TakeEat(true, sommet);
            goal = sommet;
            
        }
        else if (typeZone == TypeZone.AntHill)
        {
            TakeEat(false, sommet);
        }
    }

    void TakeEat(bool isTake, Sommet sommet)
    {
        if (isTake)
        {

            if (!sheet.activeSelf)
            {
                maxTime = sommet.defaultZone.Work();
                sheet.SetActive(true);
                Debug.Log(maxTime);
                currentTime = 0;
            }

            if (home)
            {
                
                List<Sommet> result = RechercheProfondeurGraph.instance.BestPath(currentSommet, home);
                if (result != null)
                {
                    tasks.AddRange(result);
                    currentState = AntState.GoHome;
                }
                
            }
        }
        else
        {
            if (sheet.activeSelf)
            {
                maxTime = sommet.defaultZone.Work();
                sheet.SetActive(false);
                currentTime = 0;
            }
            

            if (goal)
            {
                List<Sommet> result = RechercheProfondeurGraph.instance.BestPath(currentSommet, goal);
                if (result != null)
                {
                    tasks.AddRange(result);
                    currentState = AntState.GoGoal;
                }
            }
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

    void Manipulation(Sommet target, DefaultIA friend)
    {
        if (friend.antDataInstance.sociability >= antDataInstance.sociability)
        {
            Debug.Log("Manipulation");
            goal = target;
        }
    }
    
    float DistanceToCible(Transform cible)
    {
        return Vector2.Distance(transform.position, cible.transform.position);
    }
}
public enum AntState
{
    None,
    Lost,
    GoHome,
    GoGoal
}