using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyVector3
{
    public float x, y, z;

    private static MyVector3 right = new MyVector3(1f, 0f, 0f);


    public MyVector3(float x, float y, float z)
    {
        this.x = x;
        this.y = y;
        this.z = z;
    }

    public MyVector3(MyVector3 v)
    {
        this.x = v.x;
        this.y = v.y;
        this.z = v.z;
    }

    public MyVector3(Vector3 v)
    {
        this.x = v.x;
        this.y = v.y;
        this.z = v.z;
    }

    public MyVector3 Normalize()
    {
        float length = Mathf.Sqrt((x * x) + (y * y) + (z * z));

        x /= length;
        y /= length;
        z /= length;

        return this;
    }

    public static MyVector3 Cross(MyVector3 v1, MyVector3 v2)
    {
        return new MyVector3((v1.y * v2.z) - (v1.z * v2.y),
                             (v1.z * v2.x) - (v1.x * v2.z),
                             (v1.x * v2.y) - (v1.y * v2.x));
    }

    public static float Dot(MyVector3 v1, MyVector3 v2)
    {
        return (v1.x * v2.x) + (v1.y * v2.y) + (v1.z * v2.z);
    }


    public string Tostring()
    {
        return "(" + x + ", " + y + ", " + z + ")";
    }
}
