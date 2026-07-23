using UnityEngine;

[RequireComponent(typeof(Moving))]
[RequireComponent(typeof(FindingFood))]
[RequireComponent(typeof(Devour))]
[RequireComponent(typeof(Gene))]

public class Cell : MonoBehaviour
{
    private Gene gene;

    public float nutrition = 5;
    public float nutritionSaturation = 8;

    public float health = 15;

    public float nutritionConsumptionRate;

    public float baseConsumptionCoefficient = 0.003f;
    public float movementConsumptionCoefficient = 0.004f;
    public float rotationConsumptionCoefficient = 0.003f;

    public void nutritionConsumption()
    {
        nutrition -= nutritionConsumptionRate;

        if (nutrition < 0) health += nutrition;//subtruct
    }

    void SpawnSubObject
    (
        string name,
        Vector3 position,
        Quaternion rotation,
        Vector3 scale,
        string componentName,
        float centerOffset,
        float searchingRangeRadius,
        float searchingRangeAngle
    )
    {
        GameObject subObject = new GameObject(name);
        subObject.transform.SetParent(gameObject.transform);
        subObject.transform.localPosition = position;
        subObject.transform.localRotation = rotation;
        subObject.transform.localScale = scale;

        ObjectDetector objectDetector = subObject.AddComponent<ObjectDetector>();
        objectDetector.componentName = componentName;
        objectDetector.centerOffset = centerOffset;
        objectDetector.searchingRangeRadius = searchingRangeRadius;
        objectDetector.searchingRangeAngle = searchingRangeAngle;
    }

    void Awake()
    {
        if (!transform.Find("cellMembrane"))
        {
            SpawnSubObject(
                "cellMembrane",
                new Vector3(0, 0, 0),
                Quaternion.identity,
                new Vector3(1, 1, 1),
                "Food",
                0,
                0.4f,
                360
                );
        }

        if (!transform.Find("scope"))
        {
            SpawnSubObject(
                "scope",
                new Vector3(0, 0, 0),
                Quaternion.identity,
                new Vector3(1, 1, 1),
                "Food",
                3,
                3,
                90
                );
        }

        gene = gameObject.GetComponent<Gene>();

        nutritionConsumptionRate = baseConsumptionCoefficient
            + gene.movementSpeed / 1 * movementConsumptionCoefficient 
            + gene.rotationSpeed / 100 * rotationConsumptionCoefficient;
    }
    
    void OnTick()
    {
        if (nutrition > nutritionSaturation)
        {
            nutrition -= nutritionSaturation / 2;
            gene.cellDivision(gameObject);
        }
        nutritionConsumption();
        if (health <= 0) Destroy(gameObject);
    }

    void OnEnable()
    {
        Timer.OnTick += OnTick;
    }

    void OnDisable()
    {
        Timer.OnTick -= OnTick;
    }
}
