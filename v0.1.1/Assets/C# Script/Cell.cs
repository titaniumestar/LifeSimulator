using UnityEngine;

[RequireComponent(typeof(Moving))]
[RequireComponent(typeof(FindingFood))]
[RequireComponent(typeof(Devour))]

public class Cell : MonoBehaviour
{
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
        SpawnSubObject
        (
            "cellMembrane",
            new Vector3(0, 0, 0),
            Quaternion.identity,
            new Vector3(1, 1, 1),
            "Food",
            0,
            0.4f,
            360
        );

        SpawnSubObject
        (
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
}
