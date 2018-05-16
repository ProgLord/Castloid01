using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour 
{
    public float fallSpeed, moveSpeed, jumpHeight, x, y;
    public float jumpUpRate, jumpX, jumpY;
    public float knockbackHeight, knockbackDuration, knockbackX;
    public float iFrames, iFramesRemaining;
    
    float jumpingX;
    public bool isFalling, isGrounded, canMove, canMoveLeft, canMoveRight, movingLeft, movingRight, 
        crouched, decendingJumpArc, jumping, attacking, knockback, dead;
    public bool onStairs;
    public Stairs stairs;

    Vector3 knockbackEnemyPos;

    void Start () 
	{
        //groundCheck = GetComponentInChildren<GroundCheck>();
	}
	
	void Update () 
	{
        //Ground Check
        //isGrounded = groundCheck.grounded;
        
        //Controls

        //Knockback debug controls
        if(Input.GetKeyDown(KeyCode.K))
        {
            if (knockback == false)
            {
                Vector3 facing;
                if(transform.localScale.x > 0)
                {
                    facing = transform.position + Vector3.one;
                }
                else
                {
                    facing = transform.position - Vector3.one;
                }
                StartCoroutine(Knockback(facing));
            }
        }

        //Jump
        if (Input.GetButtonDown("Jump"))
        {
            if (isGrounded && !attacking)
            {
                //Debug.Log("Jumped pressed");
                StartCoroutine( Jump());
            }
        }
        
        //for Crouching and subweapon
        y = Input.GetAxisRaw("Vertical");

        

        //attacking

        if (Input.GetButtonDown("Fire1"))
        {
            x = 0;
            if (!attacking && canMove)
            {
                attacking = true;
                canMove = false;
                x = 0;
            }

            if (!attacking && jumping)
            {
                Debug.Log("Jump attack");
                attacking = true;
                canMove = false;
                x = 0;
            }
        }

        //Debug.Log("Attacking is " + attacking);

        //Crouch
        if (isGrounded)
        {
            if (y < 0)
            {
                canMove = false;
                crouched = true;
                x = 0;
            }
            else if (!attacking && !knockback)
            {
                canMove = true;
                crouched = false;
            }
        }

        //horizontal movement
        if (canMove && isGrounded && !attacking)
        {
            //Horizontal movement
            x = Input.GetAxisRaw("Horizontal");    
        }
        
        

        //Invincibility Frames
        iFramesRemaining -= Time.deltaTime;
	}

    private void FixedUpdate()
    {
        //Ground Movement
        if (x > 0 && canMoveRight && !onStairs)
        {
            transform.position += new Vector3(x * moveSpeed * Time.fixedDeltaTime, 0, 0);
            transform.localScale = new Vector3(1, 1, 1);
        }

        if (x < 0 && canMoveLeft && !onStairs)
        {
            transform.position += new Vector3(x * moveSpeed * Time.fixedDeltaTime, 0, 0);
            transform.localScale = new Vector3(-1, 1, 1);
        }

        //falling
        if (isFalling && !onStairs)
        {
            transform.position -= new Vector3(0, fallSpeed, 0);
        }

        //Stair movement
        if(stairs != null && y != 0)
        {
            //StairMovement(y);
            stairs.TraverseStair(this, y, moveSpeed);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Collision below
        if(transform.position.y - collision.transform.position.y > 0)
        {
            isFalling = false;
            isGrounded = true;
            if (!knockback)
            {
                canMove = true;
            }
        }

        //Collision from side
        //Right side
        if (transform.position.x - collision.transform.position.x < 0 && collision.gameObject.tag == "platform") 
        {
            jumpingX = 0;
            knockbackX = 0;
        }
        //Left Side
        if (transform.position.x - collision.transform.position.x > 0 && collision.gameObject.tag == "platform")
        {
            jumpingX = 0;
            knockbackX = 0;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        //Collision below
        if (transform.position.y - collision.transform.position.y > 0)
        {
            isFalling = true;
            isGrounded = false;
        }

        //Continue with jumping movement if player clears platform
        //Collision from side
        //Right side
        if (transform.position.x - collision.transform.position.x < 0 && collision.gameObject.tag == "platform")
        {
            jumpingX = x;
            //Debug.Log("Jumping x set by OnColExit");
        }
        //Left Side
        if (transform.position.x - collision.transform.position.x > 0 && collision.gameObject.tag == "platform")
        {
            jumpingX = x;
            //Debug.Log("Jumping x set by OnColExit");
        }
    }

    
    private void OnCollisionStay2D(Collision2D collision)
    {
        //Collision from side
        //Prevent player from jumping through platforms
        //Right side
        if (transform.position.x - collision.transform.position.x < 0 && collision.gameObject.tag == "platform")
        {
            jumpingX = 0;
        }
        //Left Side
        if (transform.position.x - collision.transform.position.x > 0 && collision.gameObject.tag == "platform")
        {
            jumpingX = 0;
        }
    }

    //Jump Code
    private IEnumerator Jump()
    {
        Debug.Log("Starting Jump Cooroutine");
        jumping = true;

        Vector3 startPos = transform.position;
        float startX = jumpX;
        
        jumpingX = 0;

        if(x != 0)
        {
            jumpingX = x;
            //Debug.Log(jumpingX);
        }

        canMove = false;
        isGrounded = false;

        float lastJumpY = 0;
        decendingJumpArc = false;

        while (!isGrounded && !knockback)
        {
            //parabolic jump
            //jumpY = -jumpA * Mathf.Pow((startX - jumpH), 2) + jumpK;
            jumpY = -1 * Mathf.Pow(startX, 2) + jumpHeight * startX;
            //check if in descending arc of jump
            if (jumpY > lastJumpY)
            {
                lastJumpY = jumpY;
            }
            else if(jumpY < lastJumpY)
            {
                decendingJumpArc = true;
            }
            
            //check for horizontal obstacles
            if(jumpingX != 0)
            {
                if(jumpingX > 0 && !canMoveRight)
                {
                    jumpingX = 0;
                }
                else if (jumpingX < 0 && !canMoveLeft)
                {
                    jumpingX = 0;
                }
            }
            startPos += new Vector3(jumpingX * moveSpeed * Time.fixedDeltaTime, 0, 0);

            transform.position = startPos + new Vector3(jumpingX * moveSpeed * Time.fixedDeltaTime, jumpY, 0);
           
            startX += jumpUpRate * Time.deltaTime;
            
            yield return null;
        }

        Debug.Log("Jump Finished");
        jumping = false;
        yield return null;
    }


    //Knockback Code
    private IEnumerator Knockback(Vector3 enemyPos)
    {
        //determine which way the enemy is and flip the player backward
        if (transform.position.x <= enemyPos.x)
        {
            FlipSprite(1);
        }
        else
        {
            FlipSprite(-1);
        }

        StopCoroutine(Jump());
        x = 0;
        isFalling = false;

        Debug.Log("Starting Knockback Cooroutine");
        knockback = true;

        Vector3 startPos = transform.position;
        float startX = jumpX;

        knockbackX = -1 * transform.localScale.x;

        canMove = false;
        isGrounded = false;

        float lastJumpY = 0;
        decendingJumpArc = false;

        while (!isGrounded)
        {
            //parabolic jump
            jumpY = -1 * Mathf.Pow(startX, 2) + knockbackHeight * startX;
            //check if in descending arc of jump
            if (jumpY > lastJumpY)
            {
                lastJumpY = jumpY;
            }
            else if (jumpY < lastJumpY)
            {
                decendingJumpArc = true;
            }

            //check for horizontal obstacles
            if (knockbackX != 0)
            {
                if (knockbackX > 0 && !canMoveRight)
                {
                    knockbackX = 0;
                }
                else if (knockbackX < 0 && !canMoveLeft)
                {
                    knockbackX = 0;
                }
            }
            Vector3 xMoveModification = new Vector3(knockbackX * moveSpeed * Time.fixedDeltaTime, 0, 0);
            startPos += xMoveModification;
            transform.position = startPos + new Vector3(
                knockbackX * moveSpeed * Time.fixedDeltaTime
                , jumpY, 0);

            startX += jumpUpRate * Time.fixedDeltaTime;
            //Debug.Log("Jumping X is " + xMoveModification);
            yield return null;
        }

        yield return new WaitForSeconds(knockbackDuration);

        Debug.Log("Knockback Finished");
        knockback = false;
        yield return null;
    }


    public void FinishAttack()
    {
        Debug.Log("Done attacking");
        attacking = false;
        canMove = true;
    } 

    public void TakeDamage(int damage, Vector3 enemyPos)
    {
        if (iFramesRemaining <= 0)
        {
            StartCoroutine(Knockback(enemyPos));
            iFramesRemaining = iFrames;
        }

        VitalStats.playerHP -= damage;
        Debug.Log("Player hit for " + damage + ", " + VitalStats.playerHP + " remaining.");
    }

    public void FlipSprite(int dir)
    {   
            transform.localScale = new Vector3(dir, 1, 1);
    }

    public void StairMovement(float yMove)
    {
        //Vector3.MoveTowards(transform.position, )
        transform.position += new Vector3(x * moveSpeed * Time.fixedDeltaTime, 0, 0);
    }
}
