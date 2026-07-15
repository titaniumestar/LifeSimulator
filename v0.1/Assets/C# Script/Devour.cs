using UnityEngine;
using static FunctionBox;

public class Devour : MonoBehaviour
{
    private ObjectDetector objectDetector;
    void SpawnCellMembrane(GameObject selfObject)
    {
        GameObject cellMembrane = new GameObject("cellMembrane");
        cellMembrane.transform.SetParent(selfObject.transform);
        cellMembrane.transform.localPosition = new Vector3(0, 0, 0);
        cellMembrane.transform.localRotation = Quaternion.identity;
        cellMembrane.transform.localScale = new Vector3(0.8f, 0.8f, 1);

        CapsuleCollider2D collider = cellMembrane.AddComponent<CapsuleCollider2D>();
        collider.isTrigger = true;
        collider.size = GetSpriteWorldSizeV2(GetSprite(selfObject));

        objectDetector = cellMembrane.AddComponent<ObjectDetector>();
        objectDetector.componentName = "Food";

        Rigidbody2D RB = cellMembrane.AddComponent<Rigidbody2D>();
        RB.bodyType = RigidbodyType2D.Kinematic;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SpawnCellMembrane(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if (objectDetector.foundObject)
        {
            Destroy(objectDetector.targetObject);
        }
    }
}
