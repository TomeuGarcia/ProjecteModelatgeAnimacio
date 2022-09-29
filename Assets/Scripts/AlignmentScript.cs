using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlignmentScript : MonoBehaviour
{

    public Transform target1;
    public Transform target2;
    public Transform targetCam;
    public Transform targetHead;
    public int exercise = 1;

    // Exercise 2
    private float offsetAngle;
    private Vector3 offsetAxis;
    private float angleTemp = 0f;
    private Quaternion startRotation;
    private float rotationSpeed = 0.1f;
    Quaternion offsetRotation;
    Quaternion offsetHeadToCam;

    void Start ()
    {
        // Exercise 2
        //Quaternion offsetRotation = transform.rotation * Quaternion.Inverse(target1.rotation);
        //offsetRotation.ToAngleAxis(out offsetAngle, out offsetAxis);
        //startRotation = target1.rotation;

        // Exercise 3
        //offsetRotation = target1.rotation * Quaternion.Inverse(transform.rotation);
        offsetRotation = targetCam.rotation * Quaternion.Inverse(transform.rotation);
        offsetHeadToCam = targetCam.rotation * Quaternion.Inverse(targetHead.rotation);

        switch (exercise)
        {
            case 4:
                {

                }
                break;
        }
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
                    Exercise3();
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


        offsetRotation = transform.rotation * Quaternion.Inverse(target1.rotation);
        offsetRotation.ToAngleAxis(out offsetAngle, out offsetAxis);
        startRotation = target1.rotation;

        if (Mathf.Abs(offsetAngle) > 0.01f)
        {
            angleTemp = Mathf.Clamp(offsetAngle, -rotationSpeed, rotationSpeed);
            target1.rotation = Quaternion.AngleAxis(angleTemp, offsetAxis) * startRotation; // Option 1
            // target1.Rotate(offsetAxis, angleTemp, Space.World); // Option 2
        }
        
    }

    private void Exercise3()
    {
        // 1. Make target1 follow tracker local rotations, if tracker rotates on the X axis, target should rotate
        //    on its own X axis
        //target1.rotation = offsetRotation * transform.rotation;

        // 2.Imagine “tracker” is an HMD tracker, and rotate the camera while keeping the offset.
        targetCam.rotation = offsetRotation * transform.rotation;

        // 3. Now we want to apply it also to the robot’s head. But be careful!
        //    The robot’s head axis doesn’t match camera axis, we need to rotate the head on the camera axis, not it’s own. 
        //targetHead.rotation = offsetRotation * transform.rotation * offsetHeadToCam;
        targetHead.rotation = offsetRotation * transform.rotation * offsetHeadToCam;
    }

    private void Exercise4()
    {
        // Make target2 follow the transformations of target1, but in such a way that it is aligned with the tracker
        // How can you find the right offset?


    }

}
