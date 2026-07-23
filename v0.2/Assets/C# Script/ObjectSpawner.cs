using System.Collections.Generic;
using UnityEngine;
using static FunctionBox;

public class ObjectSpawner : MonoBehaviour
{
    public float spawnRangeRadius;

    [HideInInspector] public Vector3 WorldMinRange;
    [HideInInspector] public Vector3 WorldMaxRange;

    public float MinWaitingTime;
    public float MaxWaitingTime;
    public float WaitingTime = 1;
    public float WaitingTimeMultiplier = 1;

    public GameObject[] ObjectTypeArray;
    public List<GameObject> WorldObjectList = new List<GameObject>();

    public int MaxSpawnQuantity;

    void RandomSpawnObject(GameObject[] objectTypeArray, List<GameObject> worldObjectList, Vector3 currentPosition, float spawnRangeRadius, Vector3 worldMinRange, Vector3 worldMaxRange)
    {
        int randomIndex = Random.Range(0, objectTypeArray.Length);
        GameObject randomObject = objectTypeArray[randomIndex];

        Vector3 randomPosition = RandomAroundRangeVector3(
            currentPosition,
            spawnRangeRadius,
            worldMinRange,
            worldMaxRange
        );

        GameObject newObject = Instantiate(randomObject, randomPosition, Quaternion.identity);
        worldObjectList.Add(newObject);
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        WorldMinRange = GameManager.Instance.WorldMinRange;
        WorldMaxRange = GameManager.Instance.WorldMaxRange;
    }

    // Update is called once per frame
    void Update()
    {
        WaitingTime -= Time.deltaTime;

        if (WaitingTime <= 0)
        {
            WorldObjectList.RemoveAll(item => item == null);
            if (WorldObjectList.Count < MaxSpawnQuantity) RandomSpawnObject
            (
                ObjectTypeArray,
                WorldObjectList,
                transform.position,
                spawnRangeRadius,
                WorldMinRange,
                WorldMaxRange
            );

            WaitingTime = Random.Range(MinWaitingTime / WaitingTimeMultiplier, MaxWaitingTime / WaitingTimeMultiplier);
        }
    }
}