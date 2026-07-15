using UnityEngine;
using static FunctionBox;

public class Food : MonoBehaviour
{
    public bool DecideRadiusByObjectSize;
    
    public float Radius;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        CircleCollider2D collider = gameObject.AddComponent<CircleCollider2D>();
        collider.isTrigger = false;
        if (DecideRadiusByObjectSize) collider.radius = GetSpriteWorldSize(GetSprite(gameObject)) / 4;
        else collider.radius = Radius;

        Rigidbody2D RB = gameObject.AddComponent<Rigidbody2D>();
        RB.bodyType = RigidbodyType2D.Kinematic;
    }
}
