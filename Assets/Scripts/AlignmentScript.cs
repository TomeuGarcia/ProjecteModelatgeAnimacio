using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlignmentScript : MonoBehaviour
{

    public Transform target1;
    public Transform target2;
	public int exercise = 1;

    private float offsetAngle;
    private Vector3 offsetAxis;
    private float angleTemp = 0f;
    //private float startAngle = 0f;

    // Use this for initialization
    void Start ()
    {
        Quaternion offsetRotation = transform.rotation* Quaternion.Inverse(target1.rotation);
        offsetRotation.ToAngleAxis(out offsetAngle, out offsetAxis);

        Vector3 temp;
        target1.rotation.ToAngleAxis(out angleTemp, out temp);

    }

    void Update()
    {
        switch(exercise)
        {
            case 1:
            {
                Exercise1();
            }
            break;

            case 2:
            {
                Exercise2();
            }
            break;

            case 3:
            {
            
            }
            break;

            case 4:
            {

            }
            break;
        }
    }



    private void Exercise1()
    {
        // Find the offset angles between target1 and tracker.
        //  - Use an “angle axis approach” to find explicitly the angle offsets, then rotate using Rotate method. 

        Vector3 axisY = Vector3.Cross(transform.up, target1.up).normalized;
        float angleY = -Mathf.Acos(Vector3.Dot(transform.up, target1.up)) * Mathf.Rad2Deg;
        if (Mathf.Abs(angleY) > 0.01f)
            target1.transform.Rotate(axisY, angleY, Space.World);

        Vector3 axisZ = Vector3.Cross(transform.forward, target1.forward).normalized;
        float angleZ = -Mathf.Acos(Vector3.Dot(transform.forward, target1.forward)) * Mathf.Rad2Deg;
        if (Mathf.Abs(angleZ) > 0.01f)
            target1.transform.Rotate(axisZ, angleZ, Space.World);
    }

    private void Exercise2()
    {
        // Make target1 align with tracker.
        //  - Use one line of code (use the quaternion that corresponds to the offset rotation)
        // Then, make it align with tracker, but slowly in time.
        //  - Use method Quaternion.AngleAxis
        //  - Use method Transform.Rotate


        if (angleTemp < offsetAngle)
            angleTemp += 0.1f;
        target1.rotation = Quaternion.AngleAxis(angleTemp, offsetAxis);

    }

}
