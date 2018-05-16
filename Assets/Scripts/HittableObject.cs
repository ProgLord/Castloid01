using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HittableObject : MonoBehaviour 
{
    public int hP = 1;
    public int damage = 2;

    public GameObject onDestroyEffect;

    void Start () 
	{
		
	}
	
	void Update () 
	{
		
	}

    public virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "weapon")
        {
            TakeDamage(collision.gameObject.GetComponent<Weapon>().damage);
        }
    }

    public void TakeDamage(int incomingDamage)
    {
        hP -= incomingDamage;

        Debug.Log(gameObject.name + " was struck for " + incomingDamage + " damage");

        if(hP <= 0)
        {
            OnDestruction();
        }
    }

    private void OnDestruction()
    {
        if (onDestroyEffect != null)
        {
            GameObject destroyEffect = Instantiate(onDestroyEffect);
            destroyEffect.transform.position = new Vector3(transform.position.x, transform.position.y + 0.5f, 0);
        }

        Destroy(gameObject);
    }
}
