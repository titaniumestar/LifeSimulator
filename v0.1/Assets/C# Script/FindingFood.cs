using UnityEngine;
using static FunctionBox;

public class FindingFood : MonoBehaviour
{
    public bool Wandering = true;
    public float WanderRangeRadius;

    [HideInInspector] public Vector3 WorldMinRange;
    [HideInInspector] public Vector3 WorldMaxRange;

    public float MinWaitingTime;
    public float MaxWaitingTime;
    public float WaitingTime = 1;

    private ObjectDetector objectDetector;
    private Moving moving;

    void SpawnScope(GameObject selfObject)
    {
        GameObject scope = new GameObject("scope");
        scope.transform.SetParent(selfObject.transform);
        scope.transform.localPosition = new Vector3(0, 0, 0);
        scope.transform.localRotation = Quaternion.identity;

        PolygonCollider2D collider = scope.AddComponent<PolygonCollider2D>();
        collider.isTrigger = true;
        collider.points = new Vector2[] {
            new Vector2(0.5f,0),
            new Vector2(-0.5f,0),
            new Vector2(-2,7),
            new Vector2(2,7),
        };

        objectDetector = scope.AddComponent<ObjectDetector>();
        objectDetector.componentName = "Food";

        Rigidbody2D RB = scope.AddComponent<Rigidbody2D>();
        RB.bodyType = RigidbodyType2D.Kinematic;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        moving = gameObject.GetComponent<Moving>();

        SpawnScope(gameObject);

        WorldMinRange = GameManager.Instance.WorldMinRange;
        WorldMaxRange = GameManager.Instance.WorldMaxRange;
    }

    // Update is called once per frame
    void Update()
    {
        if (WaitingTime > 0) WaitingTime -= Time.deltaTime;

        if (objectDetector.foundObject)//When scope's objectDetector found object.
        {
            if (objectDetector.targetObject == null) objectDetector.foundObject = false;//If the target object is null, then set the foundObject to false.
            else
            {
                moving.targetPosition = objectDetector.targetObject.transform.position;//Else set the targetPosition to targetObject's position (Start track).
                Wandering = false;//Stop Wandering.
                return;
            }
        }

        if (Vector3.Distance(transform.position, moving.targetPosition) < GameManager.Instance.cellMovementSpeed * Time.deltaTime && !Wandering)//When this gameObejct is arrives, and stop wandering.
        {
            Wandering = true;//Get Ready to random the next targetPosition (Start wandering).
            WaitingTime = Random.Range(MinWaitingTime / GameManager.Instance.cellWanderSpeed, MaxWaitingTime / GameManager.Instance.cellWanderSpeed);//Start waiting for the next targetPosition random time (WaitingTime).
        }

        if (Wandering && (WaitingTime <= 0))//When is ready to wandering, and WaitingTime is done.
        {
            moving.targetPosition = RandomAroundRangeVector3//(Start wandering).
            (
                transform.position,
                WanderRangeRadius,
                0,
                WorldMinRange,
                WorldMaxRange
            );
            Wandering = false;//Stop Wandering.
        }
    }
}
