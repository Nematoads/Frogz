using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{
    [HideInInspector]
    public Transform possessed;
    public LayerMask WhatCanBeClickedOn;

    [HideInInspector]
    public bool isOnCoolDown = false;
    public float coolDownTime = 3;
    FrogController fc;

    // Start is called before the first frame update
    void Start()
    {
        EventBroker.setPossessed += setPossessed;
        EventBroker.goOnCoolDown += GoOnCoolDown;
        fc = FindObjectOfType<FrogController>();
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

            fc.AddRayCastIgnoreLayer(possessed);
           // possessed.GetComponent<Renderer>().material.color = Color.cyan;
        }
    }

    void GoOnCoolDown()
    {
        possessed = null;
        Debug.Log("GoOnCoolDown");
        isOnCoolDown = true;
        fc.RemoveRayCastIgnoreLayer(possessed);
        StartCoroutine("CountCoolDownTime");
    }

    IEnumerator CountCoolDownTime()
    {
        Debug.Log("GO IN");
        yield return new WaitForSeconds(coolDownTime);
        Debug.Log("DONE");
        isOnCoolDown = false;
        yield return null;
    }

    private void OnDestroy()
    {
        EventBroker.setPossessed -= setPossessed;
        EventBroker.goOnCoolDown -= GoOnCoolDown;
    }


}
