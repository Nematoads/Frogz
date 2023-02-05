using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RootBase : MonoBehaviour
{
    public float health = 2.0f;
    public float dmgPerFrog = 1.0f;
    public float dmgInterval = 1.0f;
    int frogAround = 0;
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("ReduceHealth");

        EventBroker.CallSetRaizHealth((int) health);

        this.animator = GetComponent<Animator>();
        EventBroker.setPossessed += Attack;
    }

    private void Update()
    {
        RotateToCamera();
    }
    void RotateToCamera()
    {
        transform.LookAt(Camera.main.transform);
        //transform.localRotation = Quaternion.Euler(0f, transform.rotation.eulerAngles.y, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("frog"))
        {
            frogAround++;
        }
    }
    //private void OnTriggerExit(Collider other)
    //{
    //    if (other.CompareTag("frog"))
    //    {
    //        frogAround--;
    //        if(frogAround < 0)
    //        {
    //            frogAround = 0;
    //        }
    //    }
    //}



    public void Attack(Transform p)
    {
        this.animator.SetTrigger("sporeAttack");
    }

    IEnumerator ReduceHealth()
    {
        while (true)
        {
            if(health < 0)
            {
                EventBroker.CallRootdied();
                Destroy(gameObject);
                break;
            }

            if(frogAround > 0)
            {
                EventBroker.CallSetRaizHealth((int)health);

                health -= dmgPerFrog;

                this.animator.SetBool("isTakingDamage", true);
                //this.animator.SetBool("", false);

            }
            if (health >= 0)
            {
                yield return new WaitForSeconds(dmgInterval);
            }
            else
            {
                yield return null;
            }
        }
        yield return null;
    }
    public void DecrementFrogsAround()
    {
        frogAround--;
        if(frogAround < 0)
        {
            frogAround = 0;
        }
    }

    private void OnDestroy()
    {
        EventBroker.setPossessed -= Attack;
    }

}


