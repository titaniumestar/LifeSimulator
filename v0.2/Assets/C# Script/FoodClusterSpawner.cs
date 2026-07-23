using UnityEngine;
using static FunctionBox;

public class FoodClusterSpawner : MonoBehaviour
{
    public float spawnRangeRadius;
    public float nutrition;
    public float density;

    [HideInInspector] public Vector3 WorldMinRange;
    [HideInInspector] public Vector3 WorldMaxRange;

    public GameObject[] ObjectTypeArray;

    void AverageSpawnFood(
        GameObject[] objectTypeArray,
        Vector3 currentPosition,
        float spawnRangeRadius,
        float density,
        float nutrition,
        Vector3 worldMinRange,
        Vector3 worldMaxRange
        )
    {
        float typeDensity = objectTypeArray.Length * density;

        float eachObjectNutrition = nutrition / (Mathf.PI * spawnRangeRadius * spawnRangeRadius) * density * density;

        foreach (GameObject objectType in objectTypeArray)
        {
            int index = System.Array.IndexOf(objectTypeArray, objectType);
            Vector3 objectPosition = currentPosition + new Vector3(index * density, spawnRangeRadius - index * density, 0);
            for (
                Vector3 originPosition = objectPosition;
                Vector3.Distance(originPosition, currentPosition) <= spawnRangeRadius;
                originPosition -= new Vector3(0, typeDensity, 0)
                )
            {
                for (int i = -1; i < 2; i += 2)
                {
                    for (
                        objectPosition = originPosition;
                        Vector3.Distance(objectPosition, currentPosition) <= spawnRangeRadius;
                        objectPosition += new Vector3(typeDensity * i, 0, 0)
                        )
                    {
                        GameObject newObject = Instantiate(objectType, ClampVector3(objectPosition, worldMinRange, worldMaxRange), Quaternion.identity);
                        newObject.GetComponent<Food>().nutrition = eachObjectNutrition;
                    }
                }
            }
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        WorldMinRange = GameManager.Instance.WorldMinRange;
        WorldMaxRange = GameManager.Instance.WorldMaxRange;

        AverageSpawnFood(
            ObjectTypeArray,
            transform.position,
            spawnRangeRadius,
            density,
            nutrition,
            WorldMinRange,
            WorldMaxRange
            );

        Destroy(gameObject);
    }
}