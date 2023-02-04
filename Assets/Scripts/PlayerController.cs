using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{
    
    public Transform possessed;
    public LayerMask WhatCanBeClickedOn;

    // Start is called before the first frame update
    void Start()
    {
        EventBroker.setPossessed += setPossessed;
    }

    // Update is called once per frame
    void Update()
    {
        if (possessed && Input.GetMouseButton(0))
        {
            Ray r = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if(Physics.Raycast(r, out hit, 100, WhatCanBeClickedOn))
            {
                possessed.GetComponent<FrogBase>().MovePossessed(hit.point);
            }
        }
    }

    private void setPossessed(Transform p)
    {
        if (!possessed)
        {
            possessed = p;
            possessed.GetComponent<FrogBase>().isPossessed = true;
            Transform tr = possessed.GetComponent<FrogBase>().targetedRoot;
            if (tr) { tr.GetComponent<RootBase>().DecrementFrogsAround(); }

            possessed.GetComponent<Renderer>().material.color = Color.cyan;
        }
    }

    private void OnDestroy()
    {
        EventBroker.setPossessed -= setPossessed;
    }


}
