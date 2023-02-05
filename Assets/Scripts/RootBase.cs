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
        this.animator = GetComponent<Animator>();
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
                health -= dmgPerFrog;
            }
            yield return new WaitForSeconds(dmgInterval);
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

}


