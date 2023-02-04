using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionController : MonoBehaviour
{
    public ParticleSystem explosion;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Explode(Transform transform)
    {
        var exp = Instantiate<ParticleSystem>(explosion);
        exp.transform.position = transform.position;
        exp.Play();
    }
}
