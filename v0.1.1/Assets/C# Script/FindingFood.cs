using UnityEngine;
using static FunctionBox;

public class FindingFood : MonoBehaviour
{
    public bool Wandering = true;
    public float WanderRangeRadius;

    [HideInInspector] public Vector3 WorldMinRange;
    [HideInInspector] public Vector3 WorldMaxRange;

    public float MinWaitingTime = 20;
    public float MaxWaitingTime = 48;
    public int WaitingTime = 20;

    private ObjectDetector objectDetector;
    private Moving moving;

    private int delay = 0;

    void OnTick()
    {
        if (delay <= 0) delay = Random.Range(0, 4);
        else { delay--; return; }

        if (WaitingTime > 0) WaitingTime--;

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

        if (Vector3.Distance(transform.position, moving.targetPosition) < GameManager.Instance.cellMovementSpeed * 0.25f && !Wandering)//When this gameObejct is arrives, and stop wandering.
        {
            Wandering = true;//Get Ready to random the next targetPosition (Start wandering).
            WaitingTime = Mathf.FloorToInt(Random.Range(MinWaitingTime / GameManager.Instance.cellWanderSpeed, MaxWaitingTime / GameManager.Instance.cellWanderSpeed));//Start waiting for the next targetPosition random time (WaitingTime).
        }

        if (Wandering && (WaitingTime == 0))//When is ready to wandering, and WaitingTime is done.
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

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        moving = gameObject.GetComponent<Moving>();

        WorldMinRange = GameManager.Instance.WorldMinRange;
        WorldMaxRange = GameManager.Instance.WorldMaxRange;

        objectDetector = transform.Find("scope").GetComponent<ObjectDetector>();
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
