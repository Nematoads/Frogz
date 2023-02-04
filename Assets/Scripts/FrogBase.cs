using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using UnityEngine;

public class FrogBase : MonoBehaviour
{
    [HideInInspector]
    public Transform targetedRoot;

    public float movementSpeed = 2.0f;
    public float explosionTimeLimit = 10.0f;

    [HideInInspector]
    public bool isPossessed = false;
    [HideInInspector]
    public Vector3 moveToPos;
    bool shouldMovetowardClickedPosition = false;
    float possessedTime = 0;

    private ExplosionController explosionController;

    private void Start()
    {
        this.explosionController = GetComponent<ExplosionController>();
    }
    // Update is called once per frame
    void Update()
    {
        RotateToCamera();

        if (!isPossessed)
        {
            possessedTime = 0;
            if (targetedRoot)
            {
                float distance = (transform.position - targetedRoot.position).magnitude;
                if (distance > 1)
                {
                    transform.position = Vector3.MoveTowards(transform.position, targetedRoot.position, Time.deltaTime * movementSpeed);
                }
            }
        }
        else
        {

            possessedTime += Time.deltaTime;

            if (possessedTime >= explosionTimeLimit)
            {
                //explode
                Die();
                Debug.Log("Destroy");
                explosionController.Explode(this.transform);
                Destroy(this.gameObject);
            }
            
            if (shouldMovetowardClickedPosition)
            {
                Vector3 dest = new Vector3(moveToPos.x, transform.position.y, moveToPos.z);
                if (transform.position != dest)
                {
                    transform.position = Vector3.MoveTowards(transform.position, dest, Time.deltaTime * movementSpeed);
                }
            }
        }
    }

    void RotateToCamera()
    {
        transform.LookAt(Camera.main.transform);
        transform.localRotation = Quaternion.Euler(0f, transform.rotation.eulerAngles.y, 0);
    }
    
    void Die()
    {
        if (!isPossessed && targetedRoot)
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
