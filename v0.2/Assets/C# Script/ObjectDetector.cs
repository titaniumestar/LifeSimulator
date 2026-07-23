using UnityEngine;

public class ObjectDetector : MonoBehaviour 
{
    public bool foundObject;
    public GameObject targetObject;
    public string componentName;

    public float centerOffset = 1;
    public float searchingRangeRadius;
    public float searchingRangeAngle;

    void RangeSearchingObject()
    {
        if (foundObject) return;

        float angle = transform.eulerAngles.z * Mathf.Deg2Rad;
        Vector3 searchingDirection = new Vector3(Mathf.Sin(angle), Mathf.Cos(angle), 0);
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position + searchingDirection * centerOffset, searchingRangeRadius);

        float minDistance = searchingRangeRadius * 2;
        GameObject minDistanceObject = null;

        foreach (Collider2D collider in colliders)
        {
            if (!collider.GetComponent(componentName)) continue;

            if (Vector3.Angle(searchingDirection, collider.transform.position - transform.position) < searchingRangeAngle / 2)
            {
                float distance = Vector3.Distance(transform.position, collider.transform.position);

                if (distance < minDistance)
                {
                    minDistance = distance;
                    minDistanceObject = collider.gameObject;
                }
            }
        }

        if (minDistanceObject)
        {
            foundObject = true;
            targetObject = minDistanceObject;
        }
    }

    void OnEnable()
    {
        Timer.OnTick += RangeSearchingObject;
    }

    void OnDisable()
    {
        Timer.OnTick -= RangeSearchingObject;
    }
}
