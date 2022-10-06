using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class angleConstraints : MonoBehaviour
{
    public bool active;

    [Range(0.0f, 180.0f)]
    public float maxAngle;

    [Range(0.0f, 180.0f)]
    public float minAngle;

    public Transform parent;
    public Transform child;

    Vector3 twistAxis;
    float v;

    void Start()
    {
        twistAxis =Vector3.up;
    }

    void LateUpdate()
    {
        if (active)
        {
            //solve your exercise here
            ClampRotation();
        }
    }

    //add auxiliary functions, if needed, below

    private void ClampRotation()
    {
        Quaternion qTwist = new Quaternion(twistAxis.x* transform.localRotation.x, 
                                           twistAxis.y * transform.localRotation.y, 
                                           twistAxis.z * transform.localRotation.z, 
                                           transform.localRotation.w).normalized;

        Quaternion qSwing = Quaternion.Inverse(qTwist) * transform.localRotation;


        float angle;
        Vector3 axis;
        qSwing.ToAngleAxis(out angle, out axis);

        angle = Mathf.Clamp(angle, minAngle, maxAngle);

        transform.localRotation = Quaternion.AngleAxis(angle, axis);
    }


}
