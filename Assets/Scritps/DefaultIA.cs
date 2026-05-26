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
        
    }

    void FixedUpdate()
    {
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
            
            if(!lastSommet) lastSommet = currentSommet;
        }
    }

    void Explore(Sommet sommet)
    {
        TypeZone typeZone = sommet.defaultZone.FindTypeZone();

        if (typeZone == TypeZone.Void)
        {
            
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
            sheet.SetActive(true);

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
                sommet.defaultZone.Work();
            }
            
            sheet.SetActive(false);

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
}
public enum AntState
{
    None,
    Lost,
    GoHome,
    GoGoal
}