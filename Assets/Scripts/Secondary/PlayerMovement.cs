using System.Collections;
using System.Collections.Generic;
using System.Net.Mail;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //Serialize Field makes it editable in Unity
    [SerializeField] private float speed;
    [SerializeField] private float jumpForce;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask wallLayer;
    
    //You can add the unity components to the scripts to get them
    private Rigidbody2D body;
    private Animator animator;
    private BoxCollider2D boxCollider;
    
    // Start is called before the first frame update
    void Start()
    {
        //this gets the component from Unity and puts it into a private variable
        body = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //this variable holds what horizontal direction the player is inputing
        float horizontalInput = Input.GetAxis("Horizontal");
        
        //this code allows for the horizontal movement.
        body.velocity = new Vector2(horizontalInput * speed, body.velocity.y);

        //flips the player left or right depending on where they are going
        //this allows the mto face where they are going
        if(horizontalInput > 0.01f)
            //0.5f is the float for the size of the character
            transform.localScale = new Vector3(0.5f, 0.5f, 1);
        else if(horizontalInput < -0.01f)
            transform.localScale = new Vector3(-0.5f, 0.5f, 1);
        
        //jumps into the air when space is pressed by jumpForce
        if (Input.GetKey(KeyCode.Space) && isGrounded()) {
            Jump();
        }
        
        //sets the animators parameters so transitions to different animations.
        animator.SetBool("Run", horizontalInput != 0);
        animator.SetBool("grounded", isGrounded());
        
    }

    //Jump method is called everytime space is pressed. Also sets grounded to false to not allow another jump
    private void Jump()
    {
        body.velocity = new Vector2(body.velocity.x, jumpForce);
        animator.SetTrigger("jump");
    }

    private bool isGrounded()
    {
        /*raycasting creates a line from an orgiin into a certain point.
         if the line intersects a object with collider on it,
         it returns true.
        */        
        //boxcast has origin,size,angle,direction,distance, and layer mask
        //Layermask can sperate the different types of stuff like player,enemy,ground,wall
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0f, Vector2.down, 0.1f, groundLayer);
        return raycastHit.collider != null;
    }

    private bool onWall()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0f,new Vector2(transform.localScale.x, 0), 0.1f, wallLayer);
        return raycastHit.collider != null;
    }

    public bool canAttack()
    {
        return !onWall();
    }

}
