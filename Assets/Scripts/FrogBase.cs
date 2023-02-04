using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrogBase : MonoBehaviour
{
    [HideInInspector]
    public Transform targetedRoot;

    public float speed = 2.0f;

    // Update is called once per frame
    void Update()
    {
        if (targetedRoot)
        {
            float distance = (transform.position - targetedRoot.position).magnitude;
            if (distance > 1)
            {
                transform.Translate((targetedRoot.position - transform.position).normalized * Time.deltaTime * speed);
            }
        } 
    }
    
    void Die()
    {
        targetedRoot.GetComponent<RootBase>().DecrementFrogsAround();
    }

    private void OnMouseDown()
    {
        EventBroker.CallSetPossessedFrog(transform);
        //Debug.Log("selecte: " + name);
    }
}
