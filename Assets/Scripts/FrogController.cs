using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class FrogController : MonoBehaviour
{
    
    public GameObject frogPrefab;
    public Transform frogsInHierarchy;
    [HideInInspector]
    public List<Transform> frogs;

    public List<Transform> roots;


    float startPosOffset = 5;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < roots.Capacity; i++)
        {
            for (int j = 0; j < 2; j++)
            {
                Transform f = Instantiate(frogPrefab, frogsInHierarchy.transform).transform;
                float r1 = Random.value * 2 - 1;
                float r2 = Random.value * 2 - 1;

                f.position = roots[i].GetChild(0).position + new Vector3(r1 * startPosOffset, 0, r2 * startPosOffset);
                f.GetComponent<FrogBase>().targetedRoot = roots[i];
                f.name = "frog" + (i * j + j).ToString();
                frogs.Add(f);
            }     
        }   
    }
}
