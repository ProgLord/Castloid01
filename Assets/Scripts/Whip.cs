using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Whip : MonoBehaviour 
{
    BoxCollider2D col;
    public SpriteRenderer sprite;
    public GameObject whipHitEffect;
    

	void Start () 
	{
        col = GetComponent<BoxCollider2D>();
        sprite = GetComponent<SpriteRenderer>();
	}
	
	void Update () 
	{
		
	}

    public void WhipHurtBoxEnable()
    {
        col.enabled = true;
    }

    public void WhipHurtBoxDisable()
    {
        col.enabled = false;
        sprite.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.GetComponent<HittableObject>() != null)
        {
            int whichSide = 1;

            if(transform.position.x - collision.transform.position.x > 0)
            {
                whichSide = 1;
            }

            else if (transform.position.x - collision.transform.position.x < 0)
            {
                whichSide = -1;
            }

            Instantiate(whipHitEffect, new Vector3(collision.transform.position.x + (0.2f * whichSide) , collision.transform.position.y + 0.5f, 0), Quaternion.identity);
        }
    }
}
