using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using UnityEngine;

public class FrogBase : MonoBehaviour
{
    [HideInInspector]
    public Transform targetedRoot;

    public float speed = 2.0f;

    [HideInInspector]
    public bool isPossessed = false;
    [HideInInspector]
    public Vector3 moveToPos;
    bool shouldMovetowardClickedPosition = false;


    LayerMask WhatCanBeClickedOn;
    // Update is called once per frame
    void Update()
    {
        if (!isPossessed)
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
        else
        {
            if (shouldMovetowardClickedPosition)
            {
                Vector3 dest = new Vector3(moveToPos.x, transform.position.y, moveToPos.z);
                if (transform.position != dest)
                {
                    transform.position = Vector3.MoveTowards(transform.position, dest, Time.deltaTime * speed);
                }
            }
        }
    }
    
    void Die()
    {
        if (targetedRoot)
        {
            targetedRoot.GetComponent<RootBase>().DecrementFrogsAround();
        }
    }

    private void OnMouseDown()
    {
        EventBroker.CallSetPossessedFrog(transform);
        Debug.Log("selecte: " + name);
    }

    public void MovePossessed(Vector3 destination)
    {
        shouldMovetowardClickedPosition = true;
        moveToPos = destination;
    }

}
