using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerControler : MonoBehaviour
{

    private Rigidbody2D characterRigidbody;
    public static Animator characterAnimator;
    private float horizontalInput;
   [SerializeField]private float characterSpeed = 4.5f;
   [SerializeField]private float jumpForce = 8; 
   [SerializeField] private int healPoints = 5;
   private bool isAttacking;


    void Awake()
    {
        characterRigidbody = GetComponent<Rigidbody2D>();
        characterAnimator = GetComponent <Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        //characterRigidbody.AddForce(Vector2.up * jumpForce);
    }
    // Update is called once per frame
    void Update()
    {
       Movement();

        if(Input.GetButtonDown("Jump") && GroundSensor.isGrounded && !isAttacking)
        {
           Jump();
        }
       
       if(Input.GetButtonDown("Fire1") && GroundSensor.isGrounded && !isAttacking)
       {
         Attack();
       }
        
    }
     

    
    void FixedUpdate()
    {
        if(isAttacking)
        {
         characterRigidbody.velocity = new Vector2(0, characterRigidbody.velocity.y);

        }
        else
        {
          characterRigidbody.velocity = new Vector2(horizontalInput * characterSpeed, characterRigidbody.velocity.y);

        }
         
    }

    void Movement()
    {
         horizontalInput = Input.GetAxis("Horizontal");

        if(horizontalInput < 0)
        {
           transform.rotation = Quaternion.Euler(0, 180, 0); 
           characterAnimator.SetBool("isruning", true);
        }

        else if(horizontalInput > 0)
        {
           transform.rotation = Quaternion.Euler(0, 0, 0);
           characterAnimator.SetBool("isruning", true); 
        }
        else
        {
            characterAnimator.SetBool("isruning", false);
        }

    }

    void Jump()
    {
        characterRigidbody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse); 
        characterAnimator.SetBool("isjumping", true);
    }

    void Attack()
    {
        StartCoroutine(AttackAnimation());
        characterAnimator.SetTrigger("Attack");
    }

    IEnumerator AttackAnimation()
    {
        isAttacking = true;

        yield return new WaitForSeconds(0.5f);

        isAttacking = false;
    }


    

    void TakeDamage()
    {
        healPoints--;
        characterAnimator.SetTrigger("ishurt");

        if(healPoints <= 0)
        {
            Die();
        }
        else
        {
         characterAnimator.SetTrigger("ishurt");
        }


    }

    void Die()
    {
        characterAnimator.SetTrigger("isDeath");
        Destroy(gameObject, 0.6f);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.layer == 8)
        {
            //characterAnimator.SetTrigger("ishurt");
            //Destroy(gameObject, 0.3f);
            TakeDamage();
        }
    }
}
