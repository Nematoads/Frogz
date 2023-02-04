using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    
    public Transform possessed;

    // Start is called before the first frame update
    void Start()
    {
        EventBroker.setPossessed += setPossessed;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void setPossessed(Transform p)
    {
        if (!possessed)
        {
            possessed = p;
        }
    }

    private void OnDestroy()
    {
        EventBroker.setPossessed -= setPossessed;
    }


}
