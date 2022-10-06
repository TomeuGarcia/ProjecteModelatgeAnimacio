using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mirrorMovement : MonoBehaviour
{
    public GameObject original;
	
	void Update ()
    {
        transform.localRotation = original.transform.localRotation;
    }
}
