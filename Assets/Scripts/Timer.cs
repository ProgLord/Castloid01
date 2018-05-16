using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Timer : MonoBehaviour 
{
    public float runEvery = 1.0f;
    public bool runOnAwake = false;
    public bool loop = true;
    public UnityEvent onTimerEnd;


	void OnEnable () 
	{
        if(runOnAwake)
        {
            Invoke("Execute", 0);
        }

        if (loop)
        {
            InvokeRepeating("Execute", runEvery, runEvery);
        }
        else
        {
            Invoke("Execute", runEvery);
        }
         
	}
	
    void Execute()
    {
        onTimerEnd.Invoke();
    }
}
