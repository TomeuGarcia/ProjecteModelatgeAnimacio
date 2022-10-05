using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyQuaternion
{
    public float x, y, z, w;


    public MyQuaternion(float x, float y, float z, float w)
    {
        this.x = x;
        this.y = y;
        this.z = z;
        this.w = w;
        Normalize();
    }

    ~MyQuaternion()
    {
    }

    public static MyQuaternion operator* (MyQuaternion q1, MyQuaternion q2)
    {
        float w = (q1.w * q2.w) - (q1.x * q2.x) - (q1.y * q2.y) - (q1.z * q2.z);
        float x = (q1.w * q2.x) + (q1.x * q2.w) + (q1.y * q2.z) - (q1.z * q2.y);
        float y = (q1.w * q2.y) - (q1.x * q2.z) + (q1.y * q2.w) + (q1.z * q2.x);
        float z = (q1.w * q2.z) + (q1.x * q2.y) - (q1.y * q2.x) + (q1.z * q2.w);

        return new MyQuaternion(x, y, z, w).Normalize();
    }

    public static Quaternion ToUnityQuaternion (MyQuaternion myQ)
    {
        return new Quaternion(myQ.x, myQ.y, myQ.z, myQ.w);
    }

    public static MyQuaternion FromUnityQuaternion(Quaternion unityQ)
    {
        return new MyQuaternion(unityQ.x, unityQ.y, unityQ.z, unityQ.w);
    }

    public string ToString()
    {
        return "(" + x + ", " + y + ", " + z + ", " + w + ")";
    }


    public static MyQuaternion Identity()
    {
        return new MyQuaternion(0f, 0f, 0f, 1f);
    }

    public static MyQuaternion Inverse(MyQuaternion q)
    {
        return new MyQuaternion(-q.x, -q.y, -q.z, q.w);
    }


    public MyQuaternion Normalize()
    {
        float length = Mathf.Sqrt(x * x + y * y + z * z + w * w);
        x /= length;
        y /= length;
        z /= length;
        w /= length;
        return this;
    }


    public static MyQuaternion FromAxisAngle(Vector3 axis, float angle) ///// INCORRECT
    {
        float halfAngle = angle / 2f;
        float x = axis.x * Mathf.Sin(halfAngle);
        float y = axis.y * Mathf.Sin(halfAngle);
        float z = axis.z * Mathf.Sin(halfAngle);
        float w = Mathf.Cos(halfAngle);

        return new MyQuaternion(x, y, z, w).Normalize();
    }

    public void ToAxisAngle(out Vector3 axis, out float angle)
    {
        float v = Mathf.Sqrt(1f - (w * w));
        axis = new Vector3(x / v, x / v, x / v);
        angle = 2f * Mathf.Acos(w);
    }

}


