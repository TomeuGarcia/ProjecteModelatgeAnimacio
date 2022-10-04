using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Maths
{
    public class Quaternion
    {
        public float x, y, z, w;


        public Quaternion(float x, float y, float z, float w)
        {
            this.x = x;
            this.y = y;
            this.z = z;
            this.w = w;
            Normalize();
        }

        ~Quaternion()
        {
        }

        public static Quaternion operator* (Quaternion q1, Quaternion q2)
        {
            float w = (q1.w * q2.w) - (q1.x * q2.x) - (q1.y * q2.y) - (q1.z * q2.z);
            float x = (q1.w * q2.x) + (q1.x * q2.w) + (q1.y * q2.z) - (q1.z * q2.y);
            float y = (q1.w * q2.y) - (q1.x * q2.z) + (q1.y * q2.w) + (q1.z * q2.x);
            float z = (q1.w * q2.z) + (q1.x * q2.y) - (q1.y * q2.x) + (q1.z * q2.w);

            return new Quaternion(x, y, z, w).Normalize();
        }

        public static Quaternion Identity()
        {
            return new Quaternion(0f, 0f, 0f, 1f);
        }

        public Quaternion Inverse()
        {
            return new Quaternion(-x, -y, -z, w);
        }


        public Quaternion Normalize()
        {
            float length = Mathf.Sqrt(x * x + y * y + z * z + w * w);
            x /= length;
            y /= length;
            z /= length;
            w /= length;
            return this;
        }


        static public Quaternion FromAxisAngle(Vector3 axis, float angle)
        {
            float halfAngle = angle / 2f;
            float x = axis.x * Mathf.Sin(halfAngle);
            float y = axis.y * Mathf.Sin(halfAngle);
            float z = axis.z * Mathf.Sin(halfAngle);
            float w = Mathf.Cos(halfAngle);

            return new Quaternion(x, y, z, w).Normalize();
        }

        public void ToAxisAngle(out Vector3 axis, out float angle)
        {
            float v = Mathf.Sqrt(1f - (w * w));
            axis = new Vector3(x / v, x / v, x / v);
            angle = 2f * Mathf.Acos(w);
        }

    }

}
