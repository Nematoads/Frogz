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
        transform.Translate((targetedRoot.position - transform.position).normalized * Time.deltaTime * speed);
    }
    
    void Die()
    {
        targetedRoot.GetComponent<RootBase>().DecrementFrogsAround();
    }

}
