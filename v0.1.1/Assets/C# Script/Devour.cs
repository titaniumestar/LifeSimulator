using UnityEngine;

public class Devour : MonoBehaviour
{
    private ObjectDetector objectDetector;

    public float devourSpeed = 2;//Tick

    void OnTick()
    {
        if (objectDetector.foundObject && objectDetector.targetObject)
        {
            Destroy(objectDetector.targetObject);
            objectDetector.foundObject = false;
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        objectDetector = transform.Find("cellMembrane").GetComponent<ObjectDetector>();
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
