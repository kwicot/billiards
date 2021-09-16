using UnityEngine;

public static class MathfUtilities
{
    public static Vector2 F_TriangleCenter(Vector2 v1, Vector2 v2, Vector2 v3)
    {
        float x1, x2, x3, y1, y2, y3;
        x1 = v1.x;
        x2 = v2.x;
        x3 = v3.x;
        y1 = v1.y;
        y2 = v2.y;
        y3 = v3.y;
        float x = (x1 + x2 + x3) / 3;
        float y = (y1 + y2 + y3) / 3;
        return new Vector2(x, y);
    }
    public static Vector3 F_TriangleCenter(Vector3 v1, Vector3 v2, Vector3 v3)
    {
        float x1, x2, x3, y1, y2, y3, z1, z2, z3;
        x1 = v1.x;
        x2 = v2.x;
        x3 = v3.x;
        y1 = v1.y;
        y2 = v2.y;
        y3 = v3.y;
        z1 = v1.z;
        z2 = v2.z;
        z3 = v3.z;
        float x = (x1 + x2 + x3) / 3;
        float y = (y1 + y2 + y3) / 3;
        float z = (z1 + z2 + z3) / 3;
        return new Vector3(x, y,z);
    }

    public static float Average(float min, float max, float current)
    {
        // var dis =  -(min) + max;
        // if (current > max) return max / current;
        // else return current / max;
        return  (current - min) / (max - min);

    }
}
