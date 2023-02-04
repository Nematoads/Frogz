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
    float possessedTime = 0;

    private ExplosionController explosionController;

    LayerMask WhatCanBeClickedOn;

    private void Start()
    {
        this.explosionController = GetComponent<ExplosionController>();
    }
    // Update is called once per frame
    void Update()
    {
        if (!isPossessed)
        {
            possessedTime = 0;
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
            possessedTime += Time.deltaTime;

            if (possessedTime >= 1)
            {
                Debug.Log("Destroy");
                //explosionController.Explode(this.transform);
                Destroy(this.gameObject);
            }
            
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
