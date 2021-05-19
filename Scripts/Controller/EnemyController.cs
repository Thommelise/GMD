using System;
using UnityEngine;
using UnityEngine.AI;
using System.Collections;
using TMPro;
using UnityEngine.SceneManagement;

public class EnemyController : MonoBehaviour {
    
    public TextMeshProUGUI killCounter;
    private static int killCount = 0;
    public float lookRadius = 20f;
    public int maxHealth = 100;
    private int currentHealth;
    public Transform target;
    public NavMeshAgent agent;
    private Animator anim;
    private static readonly int Speed = Animator.StringToHash("Speed");
    private Vector3 velocityCheck;
    private static readonly int Attack1 = Animator.StringToHash("Attack");
    public int damage;
  
    void Start()
    {
        currentHealth = maxHealth;
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponentInChildren<Animator>();
    }

    void Update ()
    {
      
        if (killCount == 10)
        {
            SceneManager.LoadScene(3);
        }
       

        // Get the distance to the player
        var distance = Vector3.Distance(target.position, transform.position);

        var velocity = agent.velocity.magnitude;
        
        if (velocity > 0.5f) {
            Walk();
        } else if (velocity < 0.5f){
            Idle();
        }

        if (distance <= 2)
        {
            
            StartCoroutine(Attack());
        }

        // If inside the radius
        if (distance <= lookRadius)
        {
            // Move towards the player
            agent.SetDestination(target.position);
           
        }
    }
    
    
    private void Idle()
    {
        anim.SetFloat(Speed,0,0.1f,Time.deltaTime);
    }
    private void Walk()
    {
        anim.SetFloat(Speed,1.0f,0.1f,Time.deltaTime);
    }
    
    private IEnumerator Attack()
    {
        anim.SetLayerWeight(anim.GetLayerIndex("Attack Layer"), 1);
        anim.SetTrigger(Attack1);
       
        yield return new WaitForSeconds(0.8f);
        anim.SetLayerWeight(anim.GetLayerIndex("Attack Layer"), 0);
        
    }

    // Point towards the player
    private void FaceTarget ()
    {
        var direction = (target.position - transform.position).normalized;
        var lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }

    void OnDrawGizmosSelected ()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
    }
    
   

   
    
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("tæt på ");
        if (other.gameObject.CompareTag("Player"))
        {
            currentHealth -= damage;
            if (currentHealth <= 0)
            {
                
                Debug.Log("død");
                Die();
                
            }
            
        }
        
    }

     void Die()
    {
        killCount += 1;
        killCounter.text = killCount.ToString();
        Destroy(gameObject);
        
    }

}