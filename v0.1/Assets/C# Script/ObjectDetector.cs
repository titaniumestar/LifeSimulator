using UnityEngine;

public class ObjectDetector : MonoBehaviour 
{
    public bool foundObject;
    public GameObject targetObject;
    public string componentName;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (foundObject) return;

        if (other.GetComponent(componentName))
        {
            foundObject = true;
            targetObject = other.gameObject;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject == targetObject)
        {
            foundObject = false;
            targetObject = null;
        }
    }
}
