using UnityEngine;

public class Devour : MonoBehaviour
{
    private ObjectDetector objectDetector;
    private Cell cell;

    void OnTick()
    {
        if (objectDetector.foundObject && objectDetector.targetObject)
        {
            cell.nutrition += objectDetector.targetObject.GetComponent<Food>().nutrition;
            Destroy(objectDetector.targetObject);
            objectDetector.foundObject = false;
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        objectDetector = transform.Find("cellMembrane").GetComponent<ObjectDetector>();
        cell = gameObject.GetComponent<Cell>();
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
