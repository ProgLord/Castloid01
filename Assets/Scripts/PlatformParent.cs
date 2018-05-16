using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformParent : MonoBehaviour 
{
    public bool showPlatformBoxes;
	void Start () 
	{
        SpriteRenderer[] platformSprites = GetComponentsInChildren<SpriteRenderer>();

        foreach (SpriteRenderer sprite in platformSprites)
        {
            sprite.enabled = showPlatformBoxes;
        }
	}
	
	void Update () 
	{
		
	}
}
