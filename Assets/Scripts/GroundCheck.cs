using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour 
{
    public bool grounded;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "platform")
        {
            grounded = true;
        }
        else if (collision.gameObject.tag != "platform")
        {
            grounded = false;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "platform")
        {
            grounded = false;
        }
    }
}
