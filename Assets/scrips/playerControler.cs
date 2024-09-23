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

        if(Input.GetButtonDown("Jump") && GroundSensor.isGrounded)
        {
           characterRigidbody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse); 
           characterAnimator.SetBool("isjumping", true);
        }
       
        
    }
     

    
    void FixedUpdate()
    {
        characterRigidbody.velocity = new Vector2(horizontalInput * characterSpeed, characterRigidbody.velocity.y); 
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.layer == 8)
        {
            characterAnimator.SetTrigger("ishurt");
            //Destroy(gameObject, 0.3f);
        }
    }
}
