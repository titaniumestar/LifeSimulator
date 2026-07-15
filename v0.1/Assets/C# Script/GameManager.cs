using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public Vector3 WorldMinRange;
    public Vector3 WorldMaxRange;

    public float cellMovementSpeed = 0.6f;
    public float cellRotationSpeed = 100;
    public float cellWanderSpeed = 1;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        if (Instance  == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else Destroy(gameObject);

        Application.targetFrameRate = 60;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
