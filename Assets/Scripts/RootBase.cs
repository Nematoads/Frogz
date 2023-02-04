using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RootBase : MonoBehaviour
{
    public float health = 2.0f;
    public float dmgPerFrog = 1.0f;
    int frogAround = 0;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("ReduceHealth");
        StartCoroutine("die");
    }

    private void Update()
    {
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("frog"))
        {
            frogAround++;
        }
    }

    IEnumerator ReduceHealth()
    {
        while (true)
        {
            if(health < 0)
            {
                Debug.Log("dead");
                //Destroy(gameObject);
                yield return null;
            }

            if(frogAround > 0)
            {
                Debug.Log(health);
                health -= dmgPerFrog;
            }
            yield return new WaitForSeconds(1);
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

