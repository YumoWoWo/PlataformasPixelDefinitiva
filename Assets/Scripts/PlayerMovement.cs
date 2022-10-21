using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class PlayerMovement : MonoBehaviour
{
    
    public float speed = 5.5f;
    public float jumpForce = 5.5f;

    private float horizontal;

    private Transform playerTransform;
    private Rigidbody2D rb;
    private Animator animator;

    public PlayableDirector director;

    //Detector del suelo
    public static bool isGrounded;
    public Transform groundSensor;
    public LayerMask ground;
    public float sensorRadius = 0.1f;
       
    

    void Awake()
    {

        playerTransform = GetComponent<Transform>();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

    }

    //
    void FixedUpdate()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        //El GetAxis hace que el movimiento sea un poco Smooth, para quitarlo deberiamos hacer GetAxisRaw, entonces es mas seco
        playerTransform.position += new Vector3(horizontal * speed * Time.deltaTime, 0, 0);
        

        if(horizontal == 0)
        {
            animator.SetBool("Correr", false);
        }
        else if (horizontal == 1)
        {
            animator.SetBool("Correr", true);
            playerTransform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else if(horizontal == -1)
        {
            animator.SetBool("Correr", true);
            playerTransform.rotation = Quaternion.Euler(0, -180, 0);
        }
    }

    void Update ()
    {
        
       
        Jump();
        

    }
    
    
    void Jump()
    {
        isGrounded = Physics2D.OverlapCircle(groundSensor.position, sensorRadius, ground);
        if(Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.AddForce(playerTransform.up * jumpForce);
            
        }
    
        if(isGrounded)
        {
            animator.SetBool("Jump", false);
        }
        else{
            animator.SetBool("Jump", true);
        }

    }

    void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.gameObject.tag == "Cinematica")
        {
            director.Play();
        }
        
    }
}
