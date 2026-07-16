using UnityEngine;

public static class FunctionBox
{
    public static Sprite GetSprite(GameObject gameObject)
    {
        Sprite sprite = gameObject.GetComponent<SpriteRenderer>().sprite;
        return sprite;
    }
    public static float GetSpriteWorldSize(Sprite sprite)
    {
        float pxWidth = sprite.rect.width;
        float pxHeight = sprite.rect.height;
        float pxPerUnit = sprite.pixelsPerUnit;
        float size = (pxWidth / pxPerUnit) + (pxHeight / pxPerUnit);
        return size;
    }
    public static Vector2 GetSpriteWorldSizeV2(Sprite sprite)
    {
        float pxWidth = sprite.rect.width;
        float pxHeight = sprite.rect.height;
        float pxPerUnit = sprite.pixelsPerUnit;
        Vector2 vector2Size = new Vector2((pxWidth / pxPerUnit),(pxHeight / pxPerUnit));
        return vector2Size;
    }
    public static Vector3 Vector2ToVector3(Vector2 vector2,float z)
    {
        float x = vector2.x;
        float y = vector2.y;

        Vector3 vector3 = new Vector3(x,y,z);
        return vector3;
    }
    public static Vector3 ClampVector3(Vector3 vector3, Vector3 min, Vector3 max)
    {
        float x = Mathf.Clamp(vector3.x, min.x, max.x);
        float y = Mathf.Clamp(vector3.y, min.y, max.y);
        float z = Mathf.Clamp(vector3.z, min.z, max.z);

        Vector3 clampedVector3 = new Vector3(x,y,z);
        return clampedVector3;
    }
    public static Vector3 RandomAroundRangeVector3(Vector3 originalVector3, float rangeRadius, float z, Vector3 worldMinRange, Vector3 worldMaxRange)
    {
        Vector3 vector3 = ClampVector3(
            originalVector3 + Vector2ToVector3(Random.insideUnitCircle * rangeRadius, z),
            worldMinRange,
            worldMaxRange
        );
        return vector3;
    }
}
