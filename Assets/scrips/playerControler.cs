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
   [SerializeField] private int _maxHealth = 5;
   [SerializeField] private int _currentHealth;
   private bool isAttacking;
   [SerializeField] private Transform attackHitBox;
   [SerializeField] private float attackRadius;
   


    void Awake()
    {
        characterRigidbody = GetComponent<Rigidbody2D>();
        characterAnimator = GetComponent <Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        _currentHealth = _maxHealth; 

        GameManager.instance.SetHealthBar(_maxHealth);
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
         //Attack();
         StartAttack();
       }

       if(Input.GetKeyDown(KeyCode.P))
       {
         GameManager.instance.Pause();
       }
        
    }
     

    
    void FixedUpdate()
    {
        characterRigidbody.velocity = new Vector2(horizontalInput * characterSpeed, characterRigidbody.velocity.y);

    }

    void Movement()
    {
        if(isAttacking && horizontalInput == 0)
        {
            horizontalInput = 0;
        }
        else
        {
            horizontalInput = Input.GetAxis("Horizontal");
        }
        
        

        if(horizontalInput < 0)
        {
           
            if(!isAttacking)
            {
                transform.rotation = Quaternion.Euler(0, 180, 0);
            }
            
            characterAnimator.SetBool("isruning", true);

        }
           

        else if(horizontalInput > 0)
        {
            if(!isAttacking)
           {
                transform.rotation = Quaternion.Euler(0, 0, 0);
           }
           
           characterAnimator.SetBool("isruning", true); 
        }
        else
        {
            characterAnimator.SetBool("isruning", false);
        }
        

    }

    void Jump()
    {
        SoundManager.instance.PlaySFX(SoundManager.instance._audioSource, SoundManager.instance.jumpAudio);
        characterRigidbody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse); 
        characterAnimator.SetBool("isjumping", true);
    }

    /*void Attack()
    {
        StartCoroutine(AttackAnimation());
        SoundManager.instance.PlaySFX(SoundManager.instance._audioSource, SoundManager.instance.attackAudio);
        characterAnimator.SetTrigger("Attack");
    }
    

    IEnumerator AttackAnimation()
    {
        isAttacking = true;

        yield return new WaitForSeconds(0.2f);

        Collider2D[] collider = Physics2D.OverlapCircleAll(attackHitBox.position, attackRadius);

        foreach(Collider2D enemy in collider)
        {
            if(enemy.gameObject.CompareTag("Mimic"))
            {
                //Destroy(enemy.gameObject);
                Rigidbody2D enemyRigidBody = enemy.GetComponent<Rigidbody2D>();
                enemyRigidBody.AddForce(transform.right + transform.up  * 2, ForceMode2D.Impulse);

                Enemy enemyScript = enemy.GetComponent<Enemy>();
                enemyScript.TakeDamage();
            }
        }

        yield return new WaitForSeconds(0.5f);

        isAttacking = false;
    }*/


    void StartAttack()
    {
        isAttacking = true;
        characterAnimator.SetTrigger("Attack");
    }

    void Attack()
    {
        Collider2D[] collider = Physics2D.OverlapCircleAll(attackHitBox.position, attackRadius);

        foreach(Collider2D enemy in collider)
        {
            if(enemy.gameObject.CompareTag("Mimic"))
            {
                //Destroy(enemy.gameObject);
                Rigidbody2D enemyRigidBody = enemy.GetComponent<Rigidbody2D>();
                enemyRigidBody.AddForce(transform.right + transform.up  * 2, ForceMode2D.Impulse);

                Enemy enemyScript = enemy.GetComponent<Enemy>();
                enemyScript.TakeDamage();
            }
        }   
    }

    void EndAttack()
    {
        isAttacking = false;
    }


    

    void TakeDamage(int damage)
    {
        _currentHealth -= damage;
        GameManager.instance.UpdateHealthBar(_currentHealth);
        SoundManager.instance.PlaySFX(SoundManager.instance._audioSource, SoundManager.instance.hurtAudio);
        characterAnimator.SetTrigger("ishurt");

        if(_currentHealth <= 0)
        {
            Die();
        }
        else
        {
         characterAnimator.SetTrigger("ishurt");
        }


    }

    public void AddHealth()
    {
        _currentHealth++;
        GameManager.instance.UpdateHealthBar(_currentHealth);
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
            TakeDamage(1);
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackHitBox.position, attackRadius);
    }
}
