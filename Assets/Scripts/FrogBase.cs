using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrogBase : MonoBehaviour
{
    public Transform target;
    public float speed;

    // Update is called once per frame
    void Update()
    {
        transform.Translate((target.position - transform.position).normalized * Time.deltaTime * speed);
    }
}
