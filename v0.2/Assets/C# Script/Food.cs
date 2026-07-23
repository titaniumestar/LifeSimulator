using UnityEngine;
using static FunctionBox;

public class Food : MonoBehaviour
{
    public bool DecideRadiusByObjectSize;
    
    public float Radius;

    public float nutrition = 1;

    public float desityRatioNutrition = 1; //desity : nutrition

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        CircleCollider2D collider = gameObject.AddComponent<CircleCollider2D>();
        collider.isTrigger = false;
        if (DecideRadiusByObjectSize) collider.radius = GetSpriteWorldSize(GetSprite(gameObject)) / 4;
        else collider.radius = Radius;

        Rigidbody2D RB = gameObject.AddComponent<Rigidbody2D>();
        RB.bodyType = RigidbodyType2D.Kinematic;

        gameObject.GetComponent<SpriteRenderer>().color += new Color(0, 0, 0, nutrition / desityRatioNutrition - 1);
    }
}
