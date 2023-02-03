using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrogController : MonoBehaviour
{

    public GameObject frogPrefab;
    public Transform frogsInHierarchy;
    public List<Transform> frogs = new List<Transform>();
    public Transform target;
    public Transform start;

    // Start is called before the first frame update
    void Start()
    {
        Transform f = Instantiate(frogPrefab, frogsInHierarchy.transform).transform;
        f.position = start.position;
        f.GetComponent<FrogBase>().target = target;
        frogs.Add(f);     
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
