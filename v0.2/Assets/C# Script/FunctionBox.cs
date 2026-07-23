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
    public static Vector3 ClampVector3(Vector3 vector3, Vector3 min, Vector3 max)
    {
        float x = Mathf.Clamp(vector3.x, min.x, max.x);
        float y = Mathf.Clamp(vector3.y, min.y, max.y);
        float z = Mathf.Clamp(vector3.z, min.z, max.z);

        Vector3 clampedVector3 = new Vector3(x,y,z);
        return clampedVector3;
    }
    public static Vector3 RandomAroundRangeVector3(Vector3 originalVector3, float rangeRadius, Vector3 worldMinRange, Vector3 worldMaxRange)
    {
        Vector3 vector3 = ClampVector3(
            originalVector3 + Random.insideUnitSphere * rangeRadius,
            worldMinRange,
            worldMaxRange
        );
        return vector3;
    }
}
