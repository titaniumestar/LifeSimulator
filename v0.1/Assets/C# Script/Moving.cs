using UnityEngine;

public class Moving : MonoBehaviour
{
    public Vector3 targetPosition;

    public float Rotation(Vector3 direction)
    {
        float targetAngle = (Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg) - 90;
        float currentAngle = transform.eulerAngles.z;
        float angleDifference = Mathf.Abs(Mathf.DeltaAngle(currentAngle, targetAngle));
        float newAngle = Mathf.MoveTowardsAngle(currentAngle, targetAngle, GameManager.Instance.cellRotationSpeed * Time.deltaTime);

        transform.rotation = Quaternion.Euler(0, 0, newAngle);

        return angleDifference;
    }

    public void Movement(Vector3 direction, float movementDistance)
    {
        transform.position += direction * movementDistance;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 distance = targetPosition - transform.position;
        float movementDistance = GameManager.Instance.cellMovementSpeed * Time.deltaTime;

        if (distance.magnitude < movementDistance)
        {
            transform.position = targetPosition;
            return;
        }

        Vector3 direction = distance.normalized;

        float angleDifference = Rotation(direction);

        if (angleDifference < 90)
        {
            Movement(direction, movementDistance);
        }
    }
}
