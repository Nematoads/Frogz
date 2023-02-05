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

    public float MinSpeed = 1.0f;
    public float MaxSpeed = 7.0f;
    float speedTimeVariable = 0;
    public float SpeedIncreaseRate = 0.2f;

    public float explosionTimeLimit = 10.0f;
    public float blowUpRadius = 10.0f;

    [HideInInspector]
    public bool isPossessed = false;
    [HideInInspector]
    public Vector3 moveToPos;
    bool shouldMovetowardClickedPosition = false;
    float possessedTime = 0;

    public LayerMask frogsLayer;

    private SpriteRenderer spriteRenderer;
    public GameObject puddle;
    private Animator animator;

    private ExplosionController explosionController;

    [HideInInspector]
    public Collider[] nearbyFrogs;

    PlayerController pc;



    private void Start()
    {
        pc = GameObject.FindObjectOfType<PlayerController>();
        this.explosionController = GetComponent<ExplosionController>();
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        this.animator = GetComponent<Animator>();
    }
    // Update is called once per frame
    void Update()
    {
        speedTimeVariable += Time.deltaTime;
        movementSpeed = Mathf.MoveTowards(MinSpeed, MaxSpeed, speedTimeVariable * SpeedIncreaseRate);

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
        if (!isPossessed && this.animator.GetBool("isPossessed"))
        {
            this.animator.SetBool("isPossessed", false);

        } else if (isPossessed && !this.animator.GetBool("isPossessed"))
        {
            this.animator.SetBool("isPossessed", true);
        }

    }
    void RotateToCamera()
    {
        transform.LookAt(Camera.main.transform);
        //transform.localRotation = Quaternion.Euler(0f, transform.rotation.eulerAngles.y, 0);
    }

    void TurnToPuddle()
    {
        //this.animator.SetBool("isPuddle", true);
        //
        //Destroy(this.gameObject);
    }

    private void DestroyContainer()
    {
        Destroy(this.gameObject);
    }

    void Die()
    {
        //Only possessd ones come to this function
        EventBroker.CallGoOnCoolDown();
        this.animator.SetBool("IsExploding", true);

        //Instantiate(this.puddle, this.transform);
        Invoke("DestroyContainer", 1);

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
    }

    public void Kill()
    {
        if (!isPossessed && targetedRoot)
        {
            targetedRoot.GetComponent<RootBase>().DecrementFrogsAround();
        }
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
        if (animator.GetBool("isPuddle") || animator.GetBool("IsExploding"))
        {
            return;
        }

        shouldMovetowardClickedPosition = true;
        moveToPos = destination;
    }

}
