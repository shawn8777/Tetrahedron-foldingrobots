/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestA : MonoBehaviour
{
    public GameObject joint;
    private Rigidbody RB;
    private ConfigurableJoint T2;

    public Vector3 Vec1;
    private Quaternion QVec1;

	// Use this for initialization
	void Start () {
        T2 = joint.GetComponent("ConfigurableJoint") as ConfigurableJoint;
        RB = joint.GetComponent("Rigidbody") as Rigidbody;
		
	}
	
	// Update is called once per frame
	void Update () {
		if (RB.IsSleeping())
        {
            RB.WakeUp();
        }
        if (Input.GetMouseButton(0))
        {
            float h = Input.GetAxis("Mouse X");
            float v = Input.GetAxis("Mouse Y");

            Vec1[0] = Vec1[0] + h;
            Vec1[1] = Vec1[1] + v;

        }
        QVec1 = Quaternion.Euler(Vec1);
        T2.targetRotation = QVec1;
	}
}
*/
