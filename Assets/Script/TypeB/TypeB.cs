using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq.Expressions;
using System;
using System.Runtime.InteropServices;

//using TetrahedronA.Unity.Script.sharedMesh;



public partial class TypeB : MonoBehaviour
{

    [SerializeField] public GameObject OPrefab;
    [SerializeField] public GameObject T1Prefab;
    [SerializeField] public GameObject T2Prefab;
    [SerializeField] public GameObject T3Prefab;
    [SerializeField] public GameObject V4Prefab;
    [SerializeField] public GameObject V5Prefab;
    [SerializeField] public GameObject V6Prefab;
    [SerializeField] public GameObject V7Prefab;

    //[SerializeField] public GameObject C1refab;
    //[SerializeField] public GameObject D1Prefab;
    //[SerializeField] public GameObject E1Prefab;
    //[SerializeField] private SharedMeshes _meshes;
    public Mesh Mesho;
    public Mesh Mesha;
    public Mesh Meshb;
    public Mesh Meshc;
    public Mesh Meshd;
    public Mesh Meshe;
    public Mesh Meshf;
    public Mesh Meshg;

    private Vector3[] OPoint = new Vector3[2];

    private Transform[] O = new Transform[2];
    private Transform[] T1 = new Transform[3];
    private Transform[] T2 = new Transform[3];
    private Transform[] T3 = new Transform[3];
    private Transform[] V4 = new Transform[3];
    private Transform[] V5 = new Transform[3];
    private Transform[] V6 = new Transform[3];
    private Transform[] V7 = new Transform[3];


    FixedJoint[] FJt = new FixedJoint[36];//21
    HingeJoint[] HJt = new HingeJoint[36];//20
    ConfigurableJoint[] CJt = new ConfigurableJoint[36];//20
    public AudioSource AudioS;


    public int Limitspring;
    public int Limitdamper;
    public int LowXAngular;
    public int HighXangular;
    public int YAngular;
    public int ZAngular;
    public int SpringOo;
    public int DamperOo;
    public int SpringOoo;
    public int DamperOoo;
    //public int targetpositionI;
    //public int motorvelocityI;
    //public int angleI;
    public int CentermassI;
    public int AngulardrugI;
    public int Targetposition;
    public int Targetposition2;
    private int dd;

    private void Awake()
    {
        SetOPoints();
    }

    // Use this for initialization
    private void Start()
    {

        SetBody();
        //SetBody2();

    }



    private void SetOPoints()
    {
        OPoint[0] = new Vector3(0, 0, 0);
        OPoint[1] = new Vector3(36, -5, 0);


    }

    private void SetBody()
    {
        //set center 
        var o = Instantiate(OPrefab, transform);
        o.transform.position = OPoint[0];
        O[0] = o.transform;
        o.AddComponent<Rigidbody>();
        //o.GetComponent<Rigidbody>().isKinematic = true;
       // o.GetComponent<Rigidbody>().mass = 10;
        o.GetComponent<Rigidbody>().angularDrag = AngulardrugI;
        o.GetComponent<Rigidbody>().collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;
        o.AddComponent<MeshCollider>().convex = true;
        o.GetComponent<MeshCollider>().sharedMesh = Mesho;
        o.AddComponent<MeshFilter>().sharedMesh = Mesho;
        for (int i = 0; i < 3; i++)
        {
            //set OA
            var oa = Instantiate(V7Prefab, transform);
            oa.transform.position = OPoint[0];
            oa.transform.localRotation = Quaternion.Euler(0, i * 120, 0);
            oa.AddComponent<Rigidbody>();
            oa.AddComponent<MeshCollider>();
            oa.GetComponent<MeshCollider>().sharedMesh = Meshg;
            V7[i] = oa.transform;
            oa.AddComponent<MeshFilter>().sharedMesh = Meshf;
            FJt[i] = oa.gameObject.AddComponent<FixedJoint>();
            FJt[i].connectedBody = o.GetComponent<Rigidbody>();

            //set A
            var a = Instantiate(T1Prefab, transform);
            T1[i] = a.transform;
            a.transform.position = OPoint[0];
            a.transform.localRotation = Quaternion.Euler(0, i * 120, 0);
            a.AddComponent<Rigidbody>();
            a.AddComponent<MeshCollider>();
            a.GetComponent<MeshCollider>().sharedMesh = Mesha;
            a.AddComponent<MeshFilter>().sharedMesh = Mesha;
            FJt[i + 3] = a.AddComponent<FixedJoint>();
            FJt[i + 3].connectedBody = oa.GetComponent<Rigidbody>();

            //set connection part between A and B
            var ab = Instantiate(T2Prefab, transform);
            T2[i] = ab.transform;
            ab.transform.position = OPoint[0];
            ab.transform.localRotation = Quaternion.Euler(0, i * 120, 0);
            ab.AddComponent<Rigidbody>();
            ab.AddComponent<MeshCollider>();
            ab.GetComponent<MeshCollider>().sharedMesh = Meshb;
            ab.AddComponent<MeshFilter>().sharedMesh = Meshb;
            // connection between ab and a
            CJt[i] = a.AddComponent<ConfigurableJoint>();
            CJt[i].connectedBody = ab.GetComponent<Rigidbody>();
            CJt[i].anchor = new Vector3(4.45f, 7.4f, 0);
            CJt[i].axis = new Vector3(0, 1, 0);
            CJt[i].xMotion = ConfigurableJointMotion.Locked;
            CJt[i].yMotion = ConfigurableJointMotion.Locked;
            CJt[i].zMotion = ConfigurableJointMotion.Locked;
            CJt[i].angularXMotion = ConfigurableJointMotion.Locked;
            CJt[i].angularYMotion = ConfigurableJointMotion.Locked;
            CJt[i].angularZMotion = ConfigurableJointMotion.Limited;
            SoftJointLimit Angz = CJt[i].GetComponent<ConfigurableJoint>().angularZLimit;
            Angz.limit = 45;
            CJt[i].GetComponent<ConfigurableJoint>().angularZLimit = Angz;

            //set B(HORIZONTAL)
            var b = Instantiate(V4Prefab, transform);
            b.transform.position = OPoint[0];
            b.transform.localRotation = Quaternion.Euler(0, i * 120, 0);
            b.AddComponent<MeshCollider>().convex = true;
            b.GetComponent<MeshCollider>().sharedMesh = Meshd;
            V4[i] = b.transform;
            b.AddComponent<Rigidbody>();
            // connection between ab and b
            CJt[i+3] = b.AddComponent<ConfigurableJoint>();
            CJt[i+3].connectedBody = ab.GetComponent<Rigidbody>();
            CJt[i+3].anchor = new Vector3(7f, 7.27f, 0);
            CJt[i+3].axis = new Vector3(1, 0, 200);
            CJt[i+3].xMotion = ConfigurableJointMotion.Locked;
            CJt[i + 3].yMotion = ConfigurableJointMotion.Locked;
            CJt[i + 3].zMotion = ConfigurableJointMotion.Locked;
            CJt[i + 3].angularXMotion = ConfigurableJointMotion.Locked;
            CJt[i + 3].angularYMotion = ConfigurableJointMotion.Limited;
            CJt[i + 3].angularZMotion = ConfigurableJointMotion.Locked;
            SoftJointLimitSpring spr = CJt[i + 3].GetComponent<ConfigurableJoint>().angularYZLimitSpring;
            spr.spring = 20;
            spr.damper = 90;
            CJt[i + 3].GetComponent<ConfigurableJoint>().angularYZLimitSpring = spr;
            //hinge joint 
            //HJt[i] = b.AddComponent<HingeJoint>();
            //HJt[i].connectedBody = ab.GetComponent<Rigidbody>();
            //HJt[i].anchor = new Vector3(7f, 7.3f, 0);
            //HJt[i].axis = new Vector3(0, 1, 0);
            //HJt[i].useLimits = true;
            //JointLimits hgj = HJt[i].GetComponent<HingeJoint>().limits;
            //hgj.min = -20;
            //hgj.max = 20;
            //HJt[i].limits = hgj;
           

            //set C
            var c = Instantiate(V5Prefab, transform);
            c.transform.position = OPoint[0];
            c.transform.localRotation = Quaternion.Euler(0, i * 120, 0);
            c.AddComponent<Rigidbody>();
            c.AddComponent<MeshCollider>();
            c.GetComponent<MeshCollider>().sharedMesh = Meshe;
            c.AddComponent<MeshFilter>().sharedMesh = Meshe;
            V5[i] = c.transform;
            CJt[i + 3 + 3] = b.AddComponent<ConfigurableJoint>();
            CJt[i + 3 + 3].connectedBody = c.GetComponent<Rigidbody>();
            CJt[i + 3 + 3].anchor = new Vector3(9.8f, 7.4f, 0);
            CJt[i + 3 + 3].axis = new Vector3(0, 1, 0);
            CJt[i + 3 + 3].xMotion = ConfigurableJointMotion.Locked;
            CJt[i + 3 + 3].yMotion = ConfigurableJointMotion.Locked;
            CJt[i + 3 + 3].zMotion = ConfigurableJointMotion.Locked;
            CJt[i + 3 + 3].angularXMotion = ConfigurableJointMotion.Locked;
            CJt[i + 3 + 3].angularYMotion = ConfigurableJointMotion.Locked;
            CJt[i + 3 + 3].angularZMotion = ConfigurableJointMotion.Limited;
            SoftJointLimit Angzz = CJt[i + 3+3].GetComponent<ConfigurableJoint>().angularZLimit;
            Angzz.limit = 20;
            CJt[i + 3+3].GetComponent<ConfigurableJoint>().angularZLimit = Angzz;
            
            //set d
            var d = Instantiate(T3Prefab, transform);
            d.transform.position = OPoint[0];
            d.transform.localRotation = Quaternion.Euler(0, i * 120, 0);
            d.AddComponent<Rigidbody>();
            d.AddComponent<MeshCollider>().convex = true;
            d.GetComponent<MeshCollider>().sharedMesh = Meshc;
            d.AddComponent<MeshFilter>().sharedMesh = Meshc;
            T3[i] = d.transform;
            FJt[i + 6] = d.AddComponent<FixedJoint>();
            FJt[i + 6].connectedBody = a.GetComponent<Rigidbody>();
          CJt[i + 6 + 3] = a.AddComponent<ConfigurableJoint>();
            CJt[i+  6 + 3].connectedBody = d.GetComponent<Rigidbody>();
            CJt[i + 6 + 3].anchor = new Vector3(4.45f, 7.4f, 0);
            CJt[i + 6 + 3].axis = new Vector3(0, 1, 0);
            CJt[i + 6 + 3].xMotion = ConfigurableJointMotion.Locked;
            CJt[i + 6 + 3].yMotion = ConfigurableJointMotion.Locked;
            CJt[i + 6 + 3].zMotion = ConfigurableJointMotion.Locked;
            CJt[i + 6 + 3].angularXMotion = ConfigurableJointMotion.Locked;
            CJt[i + 6 + 3].angularYMotion = ConfigurableJointMotion.Locked;
            CJt[i + 6 + 3].angularZMotion = ConfigurableJointMotion.Limited;
            SoftJointLimit Angzzz = CJt[i + 6+3].GetComponent<ConfigurableJoint>().angularZLimit;
            Angzzz.limit = 30;
            CJt[i + 6+3].GetComponent<ConfigurableJoint>().angularZLimit = Angzzz;
            //set e
            var e = Instantiate(V6Prefab, transform);
            e.transform.position = OPoint[0];
            e.transform.localRotation = Quaternion.Euler(0, i * 120, 0);
            e.AddComponent<Rigidbody>();
            e.AddComponent<CapsuleCollider>().radius = 0.55f;
            e.GetComponent<CapsuleCollider>().center = new Vector3(11.4f, 7.4f, 0.05f);
            e.GetComponent<CapsuleCollider>().height = 20;
            //e.AddComponent<MeshCollider>().convex = true;
            //e.GetComponent<MeshCollider>().sharedMesh = Meshf;
            e.AddComponent<MeshFilter>().sharedMesh = Meshf;
            V6[i] = e.transform;
            FJt[i + 6] = e.AddComponent<FixedJoint>();
            FJt[i + 6].connectedBody = c.GetComponent<Rigidbody>();
           
        }
    }
    
    // Update is called once per frame
    private void Update()
    {
        if (Input.GetButtonDown("4ction"))//(Input.GetKeyDown(KeyCode.S))
        {
            Standup();//all the legs stand
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
           AddrotateAxis();//rotate axis to move body by using one leg and two axis
        }
        if (Input.GetKey(KeyCode.H))
        {
            HoldupLeg();
        }
        if (Input.GetButton("Bction"))//rightarrow(Input.GetKeyDown(KeyCode.RightArrow))
        {
            Rotateforward();
        }
        if (Input.GetButton("Xction"))//leftarrow(Input.GetKeyDown(KeyCode.LeftArrow))
        {
            Rotateforward2();
        }
        if (Input.GetButton("5ction"))//downarrow(Input.GetKeyDown(KeyCode.DownArrow))
        {
            Impluseforward();
        }
    }
    private void Standup()//three legs support whole body
    {
        if (AudioS.isPlaying == false)
        {
            AudioS.Play();
        }
        for (int i =0; i <3; i++)
        {
           // V5[i].GetComponent<Rigidbody>().AddTorque(V5[i].transform.forward * -1 * 5000000000, ForceMode.Impulse);
            V5[i].GetComponent<Rigidbody>().AddForce(V5[i].transform.up*60, ForceMode.Acceleration);
        }
    }
    private void AddrotateAxis()
    {
        if (AudioS.isPlaying == false)
        {
            AudioS.Play();
        }
        //add axis angle for body
        SoftJointLimit Angy = CJt[0 + 3].GetComponent<ConfigurableJoint>().angularYLimit;
        Angy.limit = 20;
        CJt[0 + 3].GetComponent<ConfigurableJoint>().angularYLimit = Angy;
        SoftJointLimit Angyy = CJt[1 + 3].GetComponent<ConfigurableJoint>().angularYLimit;
        Angyy.limit = 20;
        CJt[1 + 3].GetComponent<ConfigurableJoint>().angularYLimit = Angyy;
    }
    private void HoldupLeg()
    {
        if (AudioS.isPlaying == false)
        {
            AudioS.Play();
        }
        for (int i = 0; i < 3; i++)
        {
            V5[i].GetComponent<Rigidbody>().AddTorque(V5[i].transform.forward * 5000000000, ForceMode.Acceleration);
        }
    }
    private void Rotateforward()//rotate axis to move body
    {
        if (AudioS.isPlaying == false)
        {
            AudioS.Play();
        }
        V4[0].GetComponent<Rigidbody>().AddTorque(V4[0].transform.up * 300, ForceMode.Impulse);
        V4[1].GetComponent<Rigidbody>().AddTorque(V4[1].transform.up * 300*-1, ForceMode.Impulse);
        
    }
    private void Rotateforward2()
    {
        if (AudioS.isPlaying == false)
        {
            AudioS.Play();
        }
        SoftJointLimit Angy = CJt[0 + 3].GetComponent<ConfigurableJoint>().angularYLimit;
        Angy.limit = 0;
        CJt[0 + 3].GetComponent<ConfigurableJoint>().angularYLimit = Angy;
        SoftJointLimit Angyy = CJt[1 + 3].GetComponent<ConfigurableJoint>().angularYLimit;
        Angyy.limit = 0;
        CJt[1 + 3].GetComponent<ConfigurableJoint>().angularYLimit = Angyy;
    }
    private void Impluseforward()
    {
        if (AudioS.isPlaying == false)
        {
            AudioS.Play();
        }
        //V5[2].GetComponent<Rigidbody>().AddForce(V5[2].transform.up * 60, ForceMode.Acceleration);
        V6[2].gameObject.GetComponent<Rigidbody>().AddRelativeForce(V6[2].transform.forward * 2, ForceMode.Impulse);
        //V5[2].gameObject.GetComponent<Rigidbody>().AddRelativeForce(V5[2].transform.forward * 1.2f, ForceMode.Impulse);
    }

    /*
    private void Standup()
    {
       
            if (AudioS.isPlaying == false)
            {
                AudioS.Play();
            }
            V5[0].GetComponent<Rigidbody>().AddForce(V5[0].transform.right * 0.4f, ForceMode.Impulse);
            V5[1].GetComponent<Rigidbody>().AddForce(V5[1].transform.right * 0.4f, ForceMode.Impulse);
            V5[2].GetComponent<Rigidbody>().AddForce(V5[2].transform.right * 0.4f, ForceMode.Impulse);
            //V5[0].gameObject.AddComponent<ConstantForce>().relativeForce = new Vector3(0, 2800 * Time.deltaTime, 0);
           // V5[1].gameObject.AddComponent<ConstantForce>().relativeForce = new Vector3(0, 2800 * Time.deltaTime, 0);
            //V5[2].gameObject.AddComponent<ConstantForce>().relativeForce = new Vector3(0, 2800 * Time.deltaTime, 0);

        
        Debug.Log("Stand");
        Debug.Log(Time.deltaTime);

    }
    private void Standup2()
    {
        if (AudioS.isPlaying == false)
        {
            AudioS.Play();
        }
        V4[0].gameObject.GetComponent<Rigidbody>().AddTorque(V4[0].transform.forward * -1*Mathf.Pow(10, 40), ForceMode.Force);
        V4[1].gameObject.GetComponent<Rigidbody>().AddTorque(V4[1].transform.forward* -1 * Mathf.Pow(10, 40), ForceMode.Force);
        V4[2].gameObject.GetComponent<Rigidbody>().AddTorque(V4[1].transform.forward * -1 * Mathf.Pow(10, 40), ForceMode.Force);
    }
    private void getdown()
    {
        if (AudioS.isPlaying == false)
        {
            AudioS.Play();
        }
        V5[0].GetComponent<Rigidbody>().AddTorque(V5[0].transform.forward * 50000, ForceMode.Acceleration);
        V5[1].GetComponent<Rigidbody>().AddTorque(V5[1].transform.forward * 50000, ForceMode.Acceleration);
        V5[2].GetComponent<Rigidbody>().AddTorque(V5[2].transform.forward * 50000, ForceMode.Acceleration);
        //V5[0].gameObject.AddComponent<ConstantForce>().relativeForce = new Vector3(0, -1 * 4000 * Time.deltaTime,0);
        //V5[1].gameObject.AddComponent<ConstantForce>().relativeForce = new Vector3(0, -1 * 4000 * Time.deltaTime, 0);
       // V5[2].gameObject.AddComponent<ConstantForce>().relativeForce = new Vector3(0, -1 * 4000 * Time.deltaTime, 0);
    }
    private void Rotateforward()
    {
        if (AudioS.isPlaying == false)
        {
          AudioS.Play();
        }

        V4[0].GetComponent<Rigidbody>().AddTorque(V4[0].transform.up * 2000 * -1, ForceMode.VelocityChange);
        V4[1].GetComponent<Rigidbody>().AddTorque(V4[1].transform.up * 2000, ForceMode.VelocityChange);
        V5[0].GetComponent<Rigidbody>().AddForce(V5[0].transform.right * 0.5f * -1, ForceMode.Impulse);
        V5[1].GetComponent<Rigidbody>().AddForce(V5[1].transform.right * 0.5f * -1, ForceMode.Impulse);
        V5[2].GetComponent<Rigidbody>().AddForce(V5[2].transform.right * 0.5f * -1, ForceMode.Impulse);
       
    }
    private void Rotateback()
    {
        if (AudioS.isPlaying == false)
        {
            AudioS.Play();
        }   
        for (int i = 0; i < 2; i++)
        {
            SoftJointLimit Angz = CJt[i + 3].GetComponent<ConfigurableJoint>().angularZLimit;
            Angz.limit = 0;
            CJt[i + 3].GetComponent<ConfigurableJoint>().angularZLimit = Angz;
        }
        V4[0].GetComponent<Rigidbody>().AddTorque(V4[0].transform.up * 2000, ForceMode.VelocityChange);
        V4[1].GetComponent<Rigidbody>().AddTorque(V4[1].transform.up * 2000*-1, ForceMode.VelocityChange);
        V5[0].GetComponent<Rigidbody>().AddForce(V5[0].transform.right * 0.5f * -1, ForceMode.Impulse);
        V5[1].GetComponent<Rigidbody>().AddForce(V5[1].transform.right * 0.5f * -1, ForceMode.Impulse);
        V5[2].GetComponent<Rigidbody>().AddForce(V5[2].transform.right * 0.5f * -1, ForceMode.Impulse);
        Debug.Log("ROTATING");
        Debug.Log(Time.deltaTime);
    }
    private void Forward()
    {
        if (AudioS.isPlaying == false)
        {
            AudioS.Play();
        }
        for (int i = 0; i < 3; i++)
        {
            //V5[i].GetComponent<Rigidbody>().AddTorque(V5[i].transform.forward * 80000*-1, ForceMode.Impulse);
        }
        V5[0].GetComponent<Rigidbody>().AddTorque(V5[0].transform.forward * 5000000000  * -1, ForceMode.Acceleration);
        V5[1].GetComponent<Rigidbody>().AddTorque(V5[1].transform.forward * 5000000000  * -1, ForceMode.Acceleration);
        //V5[2].GetComponent<Rigidbody>().AddTorque(V5[2].transform.forward * 500000000 * Time.deltaTime * -1, ForceMode.Acceleration);

    }
    private void Back()
    {
        if (AudioS.isPlaying == false)
        {
            AudioS.Play();
        }
        
    }

   
    private void LegB()
    {
        if (AudioS.isPlaying == false)
        {
            AudioS.Play();
        }
        V5[0].GetComponent<Rigidbody>().AddTorque(V5[0].transform.up * 500*-1, ForceMode.Impulse);
        T3[0].GetComponent<Rigidbody>().AddTorque(T3[0].transform.up * 500*-1, ForceMode.Impulse);
    }
    private void LegC()
    {
        if (AudioS.isPlaying == false)
        {
            AudioS.Play();
        }
        V5[1].GetComponent<Rigidbody>().AddTorque(V5[1].transform.up* 500 * -1, ForceMode.Impulse);
        T3[1].GetComponent<Rigidbody>().AddTorque(T3[1].transform.up * 500 * -1, ForceMode.Impulse);
    }

    private void LegA1()
    {
        if (AudioS.isPlaying == false)
        {
            AudioS.Play();
        }
        
        T3[0].GetComponent<Rigidbody>().AddTorque(T3[0].transform.forward * 500*Time.deltaTime*-1, ForceMode.Impulse);
    }
    private void LegB1()
    {
        if (AudioS.isPlaying == false)
        {
            AudioS.Play();
        }
        
        T3[1].GetComponent<Rigidbody>().AddTorque(T3[1].transform.forward * 500 * Time.deltaTime*-1, ForceMode.Impulse);
    }
    private void LegC1()
    {
        if (AudioS.isPlaying == false)
        {
            AudioS.Play();
        }
        
        V5[2].GetComponent<Rigidbody>().AddTorque(V5[2].transform.forward * 500000000 * Time.deltaTime*-1, ForceMode.Acceleration);
       
    }

    private void Disconnected()
    {
        if (AudioS.isPlaying == false)
        {
            AudioS.Play();
        }
        for (int i = 0; i < 3; i++)
        {
            var h = FJt[i + 12];
            var h1 = FJt[i + 27];
            Destroy(h);
            Destroy(h1);
        }
    }
    private void Restart()
    {
        //return  void SetCenterandABCD();
    }

    private void Leaving()
    {
        if (AudioS.isPlaying == false)
        {
            AudioS.Play();
        }
        for (int i = 0; i < 3; i++)
        {
          //  T4[i].GetComponent<Rigidbody>().AddTorque(T4[i].transform.right * 100000, ForceMode.Force);
        }

    }

    private void Stable()
    {
        if (AudioS.isPlaying == false)
        {
            AudioS.Play();
        }
        for (int i = 0; i < 3; i++)
        {
           
        }

    }
    */
}
