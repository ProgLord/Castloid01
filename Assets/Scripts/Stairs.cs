using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stairs : MonoBehaviour 
{
    public Transform topStair;
    public Transform bottomStair;
    public bool isTopStair;
    public bool isBottomStair;
    
    void Start () 
	{
		
	}
	
	void Update () 
	{
		
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == ("Player"))
        {
            collision.gameObject.GetComponent<PlayerMove>().stairs = this;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == ("Player"))
        {
            PlayerMove pm = collision.gameObject.GetComponent<PlayerMove>();
            if (!pm.onStairs)
            {
                pm.stairs = null;
            }
        }
    }

    public void TraverseStair(PlayerMove player, float dir, float speed)
    {
        if( dir == 0)
        {
            return;
        }

        Transform nearestStair;
        Transform dest = null;
        if (Mathf.Abs(player.transform.position.y - bottomStair.position.y) > Mathf.Abs(player.transform.position.y - topStair.position.y))
        {
            nearestStair = topStair;
        }
        else
        {
            nearestStair = bottomStair;
        }

        //Move to bottom stair
        if (dir < 0)
        {
            if (player.transform.position == bottomStair.position)
            {
                player.onStairs = false;
            }
            else if(player.transform.position == topStair.position)
            {
                player.onStairs = true; 
            }

            if (!player.onStairs)
            {
                dest = nearestStair;
            }
            else
            {
                dest = bottomStair;
            }
                
                //player.transform.position = Vector3.MoveTowards(player.transform.position, bottomStair.position, speed * Time.fixedDeltaTime);

            
        }
        else if(dir > 0)
        {
            //player.transform.position = Vector3.MoveTowards(player.transform.position, topStair.position, speed * Time.fixedDeltaTime);

            if (player.transform.position == topStair.position)
            {
                player.onStairs = false;
            }
            else if (player.transform.position == bottomStair.position)
            {
                player.onStairs = true;
            }

            if (!player.onStairs)
            {
                dest = nearestStair;
            }
            else
            {
                dest = topStair;
            }
        }

        player.transform.position = Vector3.MoveTowards(player.transform.position, dest.position, speed * Time.fixedDeltaTime);
    }

}
