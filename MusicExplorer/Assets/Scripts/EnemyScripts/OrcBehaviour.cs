using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrcBehaviour : MonoBehaviour {

    public GameObject AttackRangeLeft, AttackRangeRight;
    public GameObject weakness;
    public Vector3 targetPosition;//where to go
    private Vector3 characterScale;
    private bool facingRight;
    private int maxForce = 4;
    private float mass = 200;
    private float gravity = 9.81f;
    private float fallSpeed;
    private Animator playerAnimator;
    public Vector2 velocity;
    private Vector2 steerForce;
    public Vector2 acceleration;
    private float minShadowDistance = 5f;
    private float maxShadowDistance = 2f;
    private float arriveDamping = 6;
    public bool closeEnoughToAttack = false;
    CharacterController controller;//this GO's CharacterController
    public bool isOnGround;
    // Use this for initialization
    void Start () {
		controller = GetComponent<CharacterController>();//this GO's CharacterController
        playerAnimator = GetComponent<Animator>();
        
    }
	
	// Update is called once per frame
	void Update () {
        //Debug.Log("Looking left is " + facingRight);
        fall();
        isGrounded();
        targetPosition = GameObject.FindWithTag("Player").transform.position;
        if (decideToAttack())
        {
            if (transform.position.x-targetPosition.x > 2.3f || transform.position.x - targetPosition.x <-2.3f)
            {
                GameObject.FindWithTag("Player").GetComponent<PlayerBattle>().firstHit = false;
               
            }
            //SendMessage("SetTarget", GameObject.FindWithTag("Player").transform.position);//just use the player

            steerForce = Shadow(targetPosition);

            Truncate(ref steerForce, maxForce);// not > max
            acceleration = steerForce / mass;
            velocity += acceleration;//velocity = transform.TransformDirection(velocity);
            
            
            controller.Move(velocity * Time.deltaTime);//move
            animationOrc();
        }
    }

    private void Truncate(ref Vector2 myVector, int myMax)//not above max
    {
        if (myVector.magnitude > myMax)
        {
            myVector.Normalize();// Vector3.normalized returns this vector with a magnitude of 1
            myVector *= myMax;//scale to max
        }
    }
    private void SetTarget(Vector3 newTargetPosition)
    {
        targetPosition = newTargetPosition;
    }
    void isGrounded()
    {
        isOnGround = (Physics2D.Raycast(transform.position, -transform.up, controller.height/controller.slopeLimit));
    }
    void fall()
    {
        if (!isOnGround)
        {
            fallSpeed += gravity * Time.deltaTime;
        }
        else
        {
            if (fallSpeed > 0)
            {
                fallSpeed = 0;
            }
            
        }
        controller.Move(new Vector2(0,-fallSpeed)*Time.deltaTime);
    }
    public Vector2 Seek(Vector2 seekPosition)
    {
        
        Vector2 targetPos = new Vector2(transform.position.x, transform.position.y);
        Vector2 mySteeringForce = (seekPosition - targetPos).normalized * maxForce;//look at target direction, normalized and scaled
        Debug.DrawLine(transform.position, seekPosition, Color.red);
        
        return mySteeringForce;
    }
    public Vector2 Shadow(Vector2 shadowPosition)
    {
        //hold everything
        Vector2 mySteeringForce = Vector3.zero;

        //to far => Seek
        if (Vector2.Distance(transform.position, shadowPosition) > maxShadowDistance)
        {
            mySteeringForce = Seek(shadowPosition);
        }
        else //slow down
        {
            velocity = Vector2.Lerp(velocity, Vector2.zero, Time.deltaTime * arriveDamping);//go to zero in some time
        }
        Debug.DrawLine(transform.position, shadowPosition, Color.magenta);
        return mySteeringForce;
    }
    private void Flip(bool right)
    {
        if (right && !facingRight || !right && facingRight)
        {
            Vector3 weaknessPosition;
            facingRight = !facingRight;
            characterScale = transform.localScale;
            characterScale.x *= -1;
            transform.localScale = characterScale;

            weaknessPosition = weakness.gameObject.transform.localScale;
            weaknessPosition.x = -weaknessPosition.x;
            weakness.transform.localScale = weaknessPosition;

            weaknessPosition = weakness.gameObject.transform.localPosition;
            weaknessPosition.x = -weaknessPosition.x;
            weakness.transform.localPosition = weaknessPosition;
        }
    }
    private bool decideToAttack()
    {
        if (!facingRight)
        {
            if (targetPosition.x > AttackRangeLeft.transform.position.x && targetPosition.x < AttackRangeRight.transform.position.x)
            {
                closeEnoughToAttack = true;
                //Debug.Log("Close enough");
            }
        }
        else
        {
            if (targetPosition.x < AttackRangeLeft.transform.position.x && targetPosition.x > AttackRangeRight.transform.position.x)
            {
                closeEnoughToAttack = true;
                //Debug.Log("Close enough");
            }
        }
        return closeEnoughToAttack;
    }
    private void animationOrc()
    {
        if (acceleration.x != 0)
        {
            playerAnimator.SetBool("isMoving", true);
        }
        else
        {
            playerAnimator.SetBool("isMoving", false);
        }
        if (targetPosition.x - transform.position.x < 0)
        {
            Flip(true);
        }
        else
        {
            Flip(false);
        }
        if (targetPosition.x - transform.position.x < 2 && targetPosition.x - transform.position.x > -2)
        {
            playerAnimator.SetBool("closeEnough", true);
        }
        else
        {
            playerAnimator.SetBool("closeEnough", false);
        }
    }
}
