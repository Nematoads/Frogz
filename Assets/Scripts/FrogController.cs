using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrogController : MonoBehaviour
{
    
    public GameObject frogPrefab;
    public Transform frogsInHierarchy;
    [HideInInspector]
    public List<Transform> frogs;

    public List<Transform> roots;
    int rootCount = 0;

    //public int numberOfFrogPerRoot = 5;
    public float spawnPosOffset_upper = 5;
    public float spawnPosOffset_lower = 5;
    public float spawnTimeInterval = 2;

    float time = 0;

    int frogId = 0;

    int currentLayerForSpawn = 0;

    public float SpawnRateSpeed = 1;
    public float MinSpawnRate = 4;
    public float MaxSpawnRate = 0.2f;
    float timeIntervalTimeVariable = 0;

    private void Start()
    {
        rootCount = roots.Count;
        EventBroker.rootdied += OneRootIsDead;
    }

    private void Update()
    {
        time += Time.deltaTime;
        //if (time > spawnTimeInterval)
        timeIntervalTimeVariable += Time.deltaTime;
        float ft = Mathf.MoveTowards(MinSpawnRate, MaxSpawnRate, timeIntervalTimeVariable * SpawnRateSpeed);
        Debug.Log(ft);
        if (time > ft)
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
                f.gameObject.layer = currentLayerForSpawn;
                frogId++;
                frogs.Add(f);
            }
        }

    }

    void OneRootIsDead()
    {
        rootCount--;
        if(rootCount <= 0)
        {
            //gameOver
            EventBroker.CallGameOver();
        }
    }

    public void AddRayCastIgnoreLayer(Transform possessed)
    {
        currentLayerForSpawn = 2; //IgnoreRayCast
        for (int i = 0; i < frogs.Count; i++)
        {
            if (frogs[i] && frogs[i].name != possessed.name)
            {
                frogs[i].gameObject.layer = currentLayerForSpawn;
            }
        }
    }

    public void RemoveRayCastIgnoreLayer(Transform possessed)
    {
        currentLayerForSpawn = 0;//Default
        for (int i = 0; i < frogs.Count; i++)
        {
            if (frogs[i])
            {
                frogs[i].gameObject.layer = currentLayerForSpawn;
            }
        }
    }

    private void OnDestroy()
    {
        EventBroker.rootdied -= OneRootIsDead;
    }
}
