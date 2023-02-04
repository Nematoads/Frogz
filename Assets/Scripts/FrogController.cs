using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.Port;
using static UnityEngine.GraphicsBuffer;

public class FrogController : MonoBehaviour
{
    
    public GameObject frogPrefab;
    public Transform frogsInHierarchy;
    [HideInInspector]
    public List<Transform> frogs;

    public List<Transform> roots;

    //public int numberOfFrogPerRoot = 5;
    public float spawnPosOffset_upper = 5;
    public float spawnPosOffset_lower = 5;
    public float spawnTimeInterval = 2;

    float time = 0;

    int frogId = 0;
    // Start is called before the first frame update
    void Start()
    {
        //for (int i = 0; i < roots.Capacity; i++)
        //{
        //    for (int j = 0; j < numberOfFrogPerRoot; j++)
        //    {
        //        Transform f = Instantiate(frogPrefab, frogsInHierarchy.transform).transform;
        //        float r1 = Random.value * 2 - 1;
        //        float r2 = Random.value * 2 - 1;

        //        f.position = roots[i].GetChild(0).position + new Vector3(r1 * spawnPosOffset, 0, r2 * spawnPosOffset);
        //        f.GetComponent<FrogBase>().targetedRoot = roots[i];
        //        f.name = "frog" + (i * j + j).ToString();
        //        frogs.Add(f);
        //    }     
        //}   
    }

    private void Update()
    {
        time += Time.deltaTime;
        if (time > spawnTimeInterval)
        {
            time = 0;
            int rootIndex = Random.Range(0, roots.Capacity);
            Transform f = Instantiate(frogPrefab, frogsInHierarchy.transform).transform;
            float r1 = (Random.value) * (spawnPosOffset_upper - spawnPosOffset_lower) + spawnPosOffset_lower;
            float r2 = (Random.value) * Mathf.PI * 2;
            if (roots[rootIndex])
            {
                f.position = roots[rootIndex].GetChild(0).position + new Vector3(Mathf.Cos(r2)*r1, 0, Mathf.Sin(r2) * r1);
                f.GetComponent<FrogBase>().targetedRoot = roots[rootIndex];
                f.name = "frog" + (rootIndex * frogId + frogId).ToString();
                frogId++;
                frogs.Add(f);
            }
        }
    }
}
