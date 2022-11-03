using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class IK_FABRIK2 : MonoBehaviour
{
    public Transform[] joints;
    public Transform target;

    private Vector3[] copy;
    private float[] distances;
    private bool done;

    void Start()
    {
        distances = new float[joints.Length - 1];
        for (int i = 0; i < distances.Length; i++)
        {
            distances[i] = Vector3.Distance(joints[i].position, joints[i + 1].position); 
        }


        copy = new Vector3[joints.Length];
    }

    void Update()
    {
        // Copy the joints positions to work with
        //TODO (done)
        for (int i = 0; i < joints.Length; ++i)
        {
            copy[i] = joints[i].position;
        }

        //done = TODO
        //done = 

        if (!done)
        {
            float targetRootDist = Vector3.Distance(copy[0], target.position);

            // Update joint positions
            if (targetRootDist > distances.Sum())
            {
                // The target is unreachable
                done = true;

                for (int i = 0; i < copy.Length-1; ++i)
                {
                    // Find the distance between the target and the joint
                    float targetToJointDist = Vector3.Distance(target.position, copy[i]);
                    float ratio = distances[i] / targetToJointDist;

                    // Find the new joint positions
                    joints[i + 1].position = ((1f - ratio) * copy[i]) + (ratio * copy[i + 1]);

                }

            }
            else
            {
                // The target is reachable

                Vector3 initialJoint0Pos = copy[0];

                //while (TODO)
                float endEffectorTargetDist = Vector3.Distance(copy[copy.Length-1], target.position);
                float tolerance = 0.1f;

                while (endEffectorTargetDist > tolerance)
                {
                    // STAGE 1: FORWARD REACHING
                    //TODO
                    // Set end effector as target
                    copy[copy.Length - 1] = target.position;
                    for (int i = copy.Length-2; i >= 0; --i)
                    {
                        // Find the distance between the new joint position and the current joint
                        Vector3 jointDir = copy[i] - copy[i + 1];
                        float distanceJoints = Vector3.Distance(copy[i], copy[i + 1]);

                        float t = distances[i] / distanceJoints;
                    }


                    // STAGE 2: BACKWARD REACHING
                    //TODO
                }
            }

            // Update original joint rotations
            for (int i = 0; i <= joints.Length - 2; i++)
            {
               //TODO 
            }          
        }
    }

}
