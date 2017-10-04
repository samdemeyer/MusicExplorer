using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
    public float movementSpeed = 5.0f;
    private Animator playerAnimator;
    private bool facingRight;
    private Vector3 characterScale;
    public float jumpForce;
    public bool grounded;
    public LayerMask whatIsGrounded;

    private Rigidbody2D myRigidbody;
    private Collider2D myCollider;

    // Use this for initialization
    void Start () {
        myRigidbody = GetComponent<Rigidbody2D>();
        myCollider = GetComponent<Collider2D>();
        playerAnimator = GetComponent<Animator>();
        facingRight = true;
	}
    private void Update()
    {

        grounded = Physics2D.IsTouchingLayers(myCollider, whatIsGrounded);
        Movement();   
    }

    private void Movement()
    {
        bool isMoving = false;
        bool isJumping = false;

        if (Input.GetKey(KeyCode.Q))
        {
            isMoving = true;
            gameObject.transform.Translate(new Vector2(-movementSpeed * Time.deltaTime,0));
            //Debug.Log("Moving left");
            Flip(false);

        }
        else if (Input.GetKey(KeyCode.D))
        {
            isMoving = true;
            //Debug.Log("Moving right");
            Flip(true);
            gameObject.transform.Translate(new Vector2(movementSpeed * Time.deltaTime, 0));

        }
        if (Input.GetKey(KeyCode.Space))
        {
            isJumping = true;
            if (grounded)
            {
                myRigidbody.velocity = new Vector2(myRigidbody.velocity.x, jumpForce);
            }
            gameObject.transform.Translate(new Vector2(0, movementSpeed * Time.deltaTime));
        }

        playerAnimator.SetBool("isJumping", isJumping);
        playerAnimator.SetBool("isMoving", isMoving);

    }
    private void Flip(bool right)
    {
        if (right && !facingRight || !right && facingRight)
        {
            facingRight = !facingRight;

            characterScale = transform.localScale;
            characterScale.x *= -1;
            transform.localScale = characterScale;
        }
    }
}
