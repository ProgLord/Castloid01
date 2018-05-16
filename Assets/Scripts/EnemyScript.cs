using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : HittableObject 
{
    public float moveSpeed;
    public int moveDirection;

	void Start () 
	{
		
	}
	
	void Update () 
	{
		
	}

    private void FixedUpdate()
    {
        transform.position += new Vector3(moveDirection * (moveSpeed * Time.fixedDeltaTime), 0, 0);
    }

    public override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);

        if(collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<PlayerMove>().TakeDamage(damage, transform.position);
        }
    }


}
