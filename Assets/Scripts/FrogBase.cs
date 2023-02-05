using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using UnityEngine;

public enum StatesEnum {
    Idle,
    Possessed
}
public class FrogBase : MonoBehaviour
{
    [HideInInspector]
    public Transform targetedRoot;

    public float movementSpeed = 2.0f;
    public float explosionTimeLimit = 10.0f;
    public float blowUpRadius = 10.0f;

    [HideInInspector]
    public bool isPossessed = false;
    [HideInInspector]
    public Vector3 moveToPos;
    bool shouldMovetowardClickedPosition = false;
    float possessedTime = 0;

    public LayerMask frogsLayer;

    public SpriteRenderer spriteRenderer;
    public Sprite idleFrog;
    public Sprite possessedFrog;


    private ExplosionController explosionController;

    public Collider[] nearbyFrogs;

    PlayerController pc;

    private void Start()
    {
        pc = GameObject.FindObjectOfType<PlayerController>();
        this.explosionController = GetComponent<ExplosionController>();
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }
    // Update is called once per frame
    void Update()
    {
        RotateToCamera();
        handleSwitchSprites(isPossessed);

        if (!isPossessed)
        {
            possessedTime = 0;
            if (targetedRoot)
            {
                float distance = (transform.position - targetedRoot.position).magnitude;
                if (distance > 1)
                {
                    Vector3 dest = new Vector3(targetedRoot.position.x, transform.position.y, targetedRoot.position.z);
                    transform.position = Vector3.MoveTowards(transform.position, dest, Time.deltaTime * movementSpeed);
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

    void handleSwitchSprites(bool isPossessed)
    {
        if (!isPossessed && spriteRenderer.sprite != idleFrog)
        {
            spriteRenderer.sprite = idleFrog;
        } else if (isPossessed && spriteRenderer.sprite != possessedFrog)
        {
            //Debug.Log("switch To possessed");
            spriteRenderer.sprite = possessedFrog;
        }

    }
    void RotateToCamera()
    {
        transform.LookAt(Camera.main.transform);
        //transform.localRotation = Quaternion.Euler(0f, transform.rotation.eulerAngles.y, 0);
    }
    
    void Die()
    {
        //Only possessd ones come to this function
        EventBroker.CallGoOnCoolDown();
        nearbyFrogs = Physics.OverlapSphere(transform.position, blowUpRadius);
        for (int i = 0; i < nearbyFrogs.Length; i++)
        {
            if (nearbyFrogs[i].CompareTag("frog") && nearbyFrogs[i].name != this.name)
            {
                nearbyFrogs[i].GetComponent<FrogBase>().Kill();
            }
        }
        //Physics.OverlapSphere(transform.position, 10, frogsLayer, QueryTriggerInteraction.Ignore);
        //Debug.Log(nearbyFrogs.Length);
        //Debug.Log("DIE");
        if (!isPossessed && targetedRoot)
        {
            targetedRoot.GetComponent<RootBase>().DecrementFrogsAround();
        }
        
        explosionController.Explode(this.transform);
        Destroy(this.gameObject);
    }

    public void Kill()
    {
        if (!isPossessed && targetedRoot)
        {
            targetedRoot.GetComponent<RootBase>().DecrementFrogsAround();
        }
        //Debug.Log("Destroy");
        Destroy(this.gameObject);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, 10);
        
    }

    private void OnMouseDown()
    {
        if (!pc.isOnCoolDown && !isPossessed) {
            Debug.Log("OnMouseDown");
            EventBroker.CallSetPossessedFrog(transform);
        }
        else if (isPossessed)
        {
            Die();
        }
        //if (!isPossessed)
        //{
        //    EventBroker.CallSetPossessedFrog(transform);
        //    //Debug.Log("selecte: " + name);
        //}
        //else
        //{
        //    Die();
        //}
    }

    public void MovePossessed(Vector3 destination)
    {
        shouldMovetowardClickedPosition = true;
        moveToPos = destination;
    }

}
