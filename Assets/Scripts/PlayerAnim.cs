using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnim : MonoBehaviour 
{
    PlayerMove playerMoveScript;
    Animator animScript;
    Whip whip;

	void Start () 
	{
        playerMoveScript = GetComponent<PlayerMove>();
        animScript = GetComponent<Animator>();
        whip = GetComponentInChildren<Whip>();
        
	}
	
	void Update () 
	{
        animScript.SetFloat("x", Mathf.Abs(playerMoveScript.x));

        //Crouch
        if (playerMoveScript.crouched)
        {
            animScript.SetBool("crouched", true);   
        }
        else
        {
            animScript.SetBool("crouched", false);
        }

        //Jumping
        if (playerMoveScript.jumping && !playerMoveScript.decendingJumpArc && !playerMoveScript.attacking)
        {
            animScript.SetTrigger("jumping");
        }
        else if(playerMoveScript.jumping && playerMoveScript.decendingJumpArc)
        {
            animScript.SetTrigger("decendingJump");
        }

        animScript.SetBool("grounded", playerMoveScript.isGrounded);

        //attacking
        if(playerMoveScript.attacking)
        {
            animScript.SetBool("attacking", true);
        }

        //knockback
        if(playerMoveScript.knockback)
        {
            animScript.SetBool("knockback", true);
        }
        else
        {
            animScript.SetBool("knockback", false);
        }

        //dead
        if(playerMoveScript.dead)
        {
            animScript.SetBool("dead", true);
        }


    }

    public void WhipEnable()
    {
        whip.sprite.enabled = true;
    }

    public void WhipHurtboxEnabler()
    {
        whip.WhipHurtBoxEnable();
    }

    public void WhipHurtboxDisabler()
    {
        whip.WhipHurtBoxDisable();
        animScript.SetBool("attacking", false);
    }
}
