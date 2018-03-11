using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Colliderfly : MonoBehaviour
{
    private FixedJoint[] FJ = new FixedJoint[1];

    private GameObject TopBall;


    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            Reset();
        }
        if (Input.GetKeyDown(KeyCode.T))
        {
            Constructed();
        }


    }
    void OnTriggerEnter(Collider otherCollidersCollider)
    {
        FJ[0] = this.gameObject.AddComponent<FixedJoint>();
        FJ[0].connectedBody = otherCollidersCollider.GetComponent<Rigidbody>();
        print("Enter");
    }
    void OnTriggerExit(Collider otherCollider)
    {

        print("out");
    }

    void Reset()
    {
        var fj = FJ[0];
        Destroy(fj);
        this.gameObject.GetComponent<Rigidbody>().AddForce(this.gameObject.transform.right * 100);
        print("Reset");

    }

    void Constructed()
    {
        TopBall = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        TopBall.GetComponent<MeshRenderer>().material.color = Color.black;
        var b = new Vector3(0, 17.54f, 0);
        TopBall.transform.localPosition = b;
        TopBall.transform.localScale = new Vector3(2.5f, 2.5f, 2.5f);
        TopBall.AddComponent<Rigidbody>();
        TopBall.GetComponent<SphereCollider>().enabled = false;
        TopBall.GetComponent<Rigidbody>().useGravity = false;
        TopBall.AddComponent<FixedJoint>();
        TopBall.GetComponent<FixedJoint>().connectedBody = this.gameObject.GetComponent<Rigidbody>();
    }

}
