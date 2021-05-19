using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerMovement : MonoBehaviour
{
    
    public Transform weapon; 
    public float attackRange = 5f;
    public LayerMask enemyLayers;

    public int attackDamage = 30;
    
    
// VAriables 
    [SerializeField] private float moveSpeed;
    [SerializeField] private float walkSpeed;
    [SerializeField] private float runSpeed;
    
    private Vector3 moveDirection;
    private Vector3 velocity;
    
    [SerializeField] private bool isGrounded;
    [SerializeField] private float groundCheckDistance;
    [SerializeField] private LayerMask groundMask;
    [SerializeField] private float gravity;

    [SerializeField] private float jumpHeight;
    
   
    
    
// references
    private CharacterController controller;
    private Animator anim;
    private static readonly int Speed = Animator.StringToHash("Speed");
    private static readonly int Attack1 = Animator.StringToHash("Attack");

    private void Start()
    {
        
        anim = GetComponentInChildren<Animator>();
        controller = GetComponent<CharacterController>();
      
    }

    private void Update()
    {
       Move();
       if (Input.GetKey(KeyCode.Mouse0))
       {
           if (isGrounded)
           {
               StartCoroutine(Attack());
               
           }
       }
       
    }
    

    private void Move()
    {
        isGrounded = Physics.CheckSphere(transform.position, groundCheckDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        float moveZ = Input.GetAxis("Vertical");

        moveDirection = new Vector3(0, 0, moveZ);
        moveDirection = transform.TransformDirection(moveDirection);
        if (isGrounded)
        {
            if (moveDirection != Vector3.zero && !Input.GetKey(KeyCode.LeftShift))
            {
                Walk();
            }
            else if (moveDirection != Vector3.zero && Input.GetKey(KeyCode.LeftShift))
            {
                Run(); 
            }
            else if (moveDirection == Vector3.zero)
            {
                Idle();
            }
            
            moveDirection *= moveSpeed;

           
        }
        if (Input.GetKey(KeyCode.Space))
        {
            Jump();
        }
        
        
        controller.Move(moveDirection * Time.deltaTime);

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    private void Walk()
    {
        moveSpeed = walkSpeed;
        anim.SetFloat(Speed,1.0f,0.1f,Time.deltaTime);
    }

    private void Run()
    {
        moveSpeed = runSpeed;
    }

    private void Idle()
    {
        anim.SetFloat(Speed,0);
    }

    private void Jump()
    {
        velocity.y = Mathf.Sqrt(jumpHeight * -2 * gravity);
    }

    private IEnumerator Attack()
    {
        anim.SetLayerWeight(anim.GetLayerIndex("Attack Layer"), 1);
        anim.SetTrigger(Attack1);
          
        yield return new WaitForSeconds(0.9f);
        anim.SetLayerWeight(anim.GetLayerIndex("Attack Layer"), 0);

    }


}





   



