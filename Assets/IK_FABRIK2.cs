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

        // TODO (done)
        done = false;

        if (!done)
        {
            float targetRootDist = Vector3.Distance(copy[0], target.position);

            // Update joint positions
            if (targetRootDist > distances.Sum())
            {
                // The target is unreachable
                done = true;

                for (int i = 0; i < copy.Length-2; ++i)
                {
                    // Find the distance between the target and the joint
                    float targetToJointDist = Vector3.Distance(target.position, copy[i]);
                    float ratio = distances[i] / targetToJointDist;

                    // Find the new joint position
                    joints[i + 1].position = Vector3.Lerp(copy[i], target.position, ratio);
                }

            }
            else
            {
                // The target is reachable

                // Store root joint's initial position
                Vector3 rootJointInitialPos = copy[0];

                // TODO (done)
                // Check wether the distance between the end effector and the target is greater than tolerance
                float tolerance = 0.05f;
                float endEffectorToRootDistance = Vector3.Distance(copy[copy.Length - 1], target.position);

                while (endEffectorToRootDistance > tolerance)
                {
                    // STAGE 1: FORWARD REACHING
                    //TODO (done)

                    // Set end effector as target
                    copy[copy.Length - 1] = target.position;

                    for (int i = copy.Length-2; i >= 0; --i)
                    {
                        // Find the distance between the new joint position (i+1) and the current joint (i)
                        float distanceJoints = Vector3.Distance(copy[i], copy[i + 1]);
                        float ratio = distances[i] / distanceJoints;

                        // Find the new joint position
                        copy[i] = Vector3.Lerp(copy[i + 1], copy[i], ratio);
                    }


                    // STAGE 2: BACKWARD REACHING
                    //TODO (done)

                    // Set the root its initial position
                    copy[0] = rootJointInitialPos;

                    for (int i = 0; i < copy.Length -2; ++i)
                    {
                        // Find the distance between the new joint position (i+1) and the current joint (i)
                        float distanceJoints = Vector3.Distance(copy[i], copy[i + 1]);
                        float ratio = distances[i] / distanceJoints;

                        // Find the new joint position
                        copy[i] = Vector3.Lerp(copy[i], copy[i + 1], ratio);
                    }

                    endEffectorToRootDistance = Vector3.Distance(copy[copy.Length - 1], target.position); // Recompute
                }

                done = true;

            }

            // Update original joint rotations
            for (int i = 0; i <= joints.Length - 2; i++)
            {
                //TODO 
                Vector3 from = joints[i].forward;
                Vector3 to = copy[i] - joints[i].position;
                joints[i].rotation = Quaternion.FromToRotation(from, to);
            }          
        }
    }

}
