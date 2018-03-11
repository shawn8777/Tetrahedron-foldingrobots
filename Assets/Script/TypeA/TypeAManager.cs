using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq.Expressions;
using System;
using System.Runtime.InteropServices;
using UnityEditor;


//using TetrahedronA.Unity.Script.sharedMesh;



public partial class TypeAManager : MonoBehaviour
{

    [SerializeField] public GameObject OPrefab;
    [SerializeField] public GameObject T1Prefab;
    [SerializeField] public GameObject T2Prefab;
    [SerializeField] public GameObject T3Prefab;
    [SerializeField] public GameObject T4Prefab;
    [SerializeField] public GameObject T5Prefab;
    [SerializeField] public GameObject T6Prefab;
    [SerializeField] public GameObject T7Prefab;
    [SerializeField] public GameObject T8Prefab;
    [SerializeField] public GameObject T88Prefab;
    [SerializeField] public GameObject T9Prefab;
    [SerializeField] public GameObject T99Prefab;

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
    public Mesh Meshh;
    public Mesh Meshi;
    public Mesh Meshj;


    private Vector3[] OPoint = new Vector3[2];

    private Transform[] O = new Transform[2];
    private Transform[] T1 = new Transform[6];
    private Transform[] T2 = new Transform[6];
    private Transform[] T3 = new Transform[6];
    private Transform[] T4 = new Transform[6];
    private Transform[] T5 = new Transform[6];
    private Transform[] T6 = new Transform[6];
    private Transform[] T7 = new Transform[6];
    private Transform[] T8 = new Transform[6];
    private Transform[] T88 = new Transform[6];
    private Transform[] T9 = new Transform[6];
    private Transform[] T99 = new Transform[3];
    //private Transform[] C1 = new Transform[3];
    //private Transform[] D1 = new Transform[3];


    FixedJoint[] FJt = new FixedJoint[42];//21
    HingeJoint[] HJt = new HingeJoint[36];//20
    ConfigurableJoint[] CJt = new ConfigurableJoint[36];//20
    public AudioSource AudioS;


    public int Limitspring;
    public int Limitdamper;
    public int LowXAngular;
    public int HighXangular;
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
        OPoint[0] = new Vector3(0, -5, 0);
        OPoint[1] = new Vector3(36, -5, 0);


    }

    private void SetBody()
    {
        //set center 
        var o = Instantiate(OPrefab, transform);
        o.transform.position = OPoint[0];
        O[0] = o.transform;
        o.AddComponent<Rigidbody>();
        //_Center.GetComponent<Rigidbody>().mass = centermass;
        o.GetComponent<Rigidbody>().useGravity = false;
        o.GetComponent<Rigidbody>().angularDrag = AngulardrugI;
        o.GetComponent<Rigidbody>().collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;
        o.AddComponent<MeshCollider>().convex = true;
        o.GetComponent<MeshCollider>().sharedMesh = Mesho;
        o.AddComponent<MeshFilter>().sharedMesh = Mesho;

        //seto1
        var o1 = Instantiate(OPrefab, transform);
        o1.transform.position = OPoint[1];
        O[1] = o.transform;
        o1.AddComponent<Rigidbody>();
        //_Center.GetComponent<Rigidbody>().mass = centermass;
        o1.GetComponent<Rigidbody>().useGravity = false;
        o1.GetComponent<Rigidbody>().angularDrag = AngulardrugI;
        o1.GetComponent<Rigidbody>().collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;
        o1.AddComponent<MeshCollider>().convex = true;
        o1.GetComponent<MeshCollider>().sharedMesh = Mesho;
        o1.AddComponent<MeshFilter>().sharedMesh = Mesho;


        for (int i = 0; i < 3; i++)
        {
            //set A
            var a = Instantiate(T1Prefab, transform);
            a.transform.position = OPoint[0];
            a.transform.localRotation = Quaternion.Euler(0, i * 120, 0);
            a.AddComponent<Rigidbody>();
            a.GetComponent<Rigidbody>().useGravity = false;
            a.AddComponent<MeshCollider>();
            a.GetComponent<MeshCollider>().sharedMesh = Mesha;
            //a.AddComponent<SphereCollider>().center = new Vector3(3.52f, 7.4f, 0);
            //a.GetComponent<SphereCollider>().radius = 0.3f;
            a.AddComponent<MeshFilter>().sharedMesh = Mesha;
            T1[i] = a.transform;
            FJt[i] = a.AddComponent<FixedJoint>();
            FJt[i].connectedBody = o.GetComponent<Rigidbody>();

            //set connection part between A and B
            var ab = Instantiate(T2Prefab, transform);
            ab.transform.position = OPoint[0];
            ab.transform.localRotation = Quaternion.Euler(0, i * 120, 0);
            ab.AddComponent<Rigidbody>();
            ab.GetComponent<Rigidbody>().useGravity = false;
            ab.AddComponent<MeshCollider>();
            ab.GetComponent<MeshCollider>().sharedMesh = Meshb;
            ab.AddComponent<MeshFilter>().sharedMesh = Meshb;
            T2[i] = ab.transform;

            HJt[i] = ab.AddComponent<HingeJoint>();
            HJt[i].connectedBody = a.GetComponent<Rigidbody>();
            HJt[i].anchor = new Vector3(4.45f, 7.3f, 0);
            HJt[i].axis = new Vector3(1, 0, 0);
            HJt[i].enableCollision = true;
            HJt[i].useSpring = true;
            JointSpring spr0 = HJt[i].GetComponent<HingeJoint>().spring;
            spr0.spring = SpringOo;
            spr0.damper = DamperOo;
            spr0.targetPosition = Targetposition;
            HJt[i].GetComponent<HingeJoint>().spring = spr0;
            /*
                 CJt[i] = _AB.AddComponent<ConfigurableJoint>();
                 CJt[i].connectedBody = _A.GetComponent<Rigidbody>();
                 CJt[i].anchor = new Vector3(4.45f, 7.3f, 0);
                 CJt[i].axis = new Vector3(1, 0, 0);
                 CJt[i].secondaryAxis = new Vector3(0, 1, 0);
                 CJt[i].xMotion = ConfigurableJointMotion.Locked;
                 CJt[i].yMotion = ConfigurableJointMotion.Locked;
                 CJt[i].zMotion = ConfigurableJointMotion.Locked;
                 CJt[i].angularXMotion = ConfigurableJointMotion.Limited;
                 CJt[i].angularYMotion = ConfigurableJointMotion.Locked;
                 CJt[i].angularZMotion = ConfigurableJointMotion.Limited;
                 CJt[i].targetRotation = Quaternion.Euler(0,0,90);
                 SoftJointLimitSpring spr = CJt[i].GetComponent<ConfigurableJoint>().angularXLimitSpring;
                 spr.spring = limitspring;
                 spr.damper = limitdamper;
                 CJt[i].GetComponent<ConfigurableJoint>().angularXLimitSpring = spr;
                 SoftJointLimitSpring spr2 = CJt[i].GetComponent<ConfigurableJoint>().angularYZLimitSpring;
                 spr2.spring = limitspring;
                 spr2.damper = limitdamper;
                 CJt[i].GetComponent<ConfigurableJoint>().angularYZLimitSpring = spr2;
                 SoftJointLimit lowX = CJt[i].GetComponent<ConfigurableJoint>().lowAngularXLimit;
                 lowX.limit = lowXAngular;
                 lowX.bounciness = 1;
                 CJt[i].GetComponent<ConfigurableJoint>().lowAngularXLimit = lowX;
                 SoftJointLimit highX = CJt[i].GetComponent<ConfigurableJoint>().highAngularXLimit;
                 highX.limit = highXangular;
                 highX.bounciness = 1;
                 CJt[i].GetComponent<ConfigurableJoint>().highAngularXLimit = highX;
                 SoftJointLimit lowZ = CJt[i].GetComponent<ConfigurableJoint>().angularZLimit;
                 lowZ.limit = ZAngular;
                 lowZ.bounciness = 1;
                 CJt[i].GetComponent<ConfigurableJoint>().angularZLimit = lowZ;
             */

            //set B(HORIZONTAL)
            var b = Instantiate(T4Prefab, transform);
            b.transform.position = OPoint[0];
            b.transform.localRotation = Quaternion.Euler(0, i * 120, 0);
            b.AddComponent<MeshCollider>().convex = true;
            b.GetComponent<MeshCollider>().sharedMesh = Meshc;
            b.AddComponent<MeshFilter>().sharedMesh = Meshc;
            T4[i] = b.transform;
            b.AddComponent<Rigidbody>();

            FJt[i + 6] = b.AddComponent<FixedJoint>();
            FJt[i + 6].connectedBody = ab.GetComponent<Rigidbody>();
            /*
                //set B(vertical)
                GameObject _E = Instantiate(T3Prefab, transform);
                _E.transform.position =new Vector3(20,2,20);
                _E.transform.localRotation = Quaternion.Euler(0, i * 120, 0);
                _E.AddComponent<MeshCollider>().convex = true;
                _E.GetComponent<MeshCollider>().sharedMesh = Meshd;
                _E.AddComponent<MeshFilter>().sharedMesh = Meshd;
                T3[i] = _E.transform;
                _E.AddComponent<Rigidbody>();
            */

            //set C
            var c = Instantiate(T5Prefab, transform);
            c.transform.position = OPoint[0];
            c.transform.localRotation = Quaternion.Euler(0, i * 120, 0);
            //c.AddComponent<MeshCollider>();
            //c.GetComponent<MeshCollider>().sharedMesh = Meshd;
            c.AddComponent<BoxCollider>().center = new Vector3(7.3f, 7.3f, 0.06f);
            c.GetComponent<BoxCollider>().size = new Vector3(0.7f, 1.6f, 1.3f);
            c.AddComponent<MeshFilter>().sharedMesh = Meshd;
            T5[i] = c.transform;
            c.AddComponent<Rigidbody>();
            FJt[i + 9] = c.AddComponent<FixedJoint>();
            FJt[i + 9].connectedBody = b.GetComponent<Rigidbody>();
            //set D
            var d = Instantiate(T6Prefab, transform);
            d.transform.position = OPoint[0];
            d.transform.localRotation = Quaternion.Euler(0, i * 120, 0);
            d.AddComponent<MeshCollider>();
            d.GetComponent<MeshCollider>().sharedMesh = Meshe;
            //d.AddComponent<BoxCollider>().center = new Vector3(8.6f, 7.3f, 0);
            //d.GetComponent<BoxCollider>().size = new Vector3(1.9f, 1.9f, 2.1f);
            d.AddComponent<MeshFilter>().sharedMesh = Meshe;
            T6[i] = d.transform;
            d.AddComponent<Rigidbody>();
            //FJt[i + 12] = _D.AddComponent<FixedJoint>();
            //FJt[i + 12].connectedBody = _C.GetComponent<Rigidbody>();
            HJt[i + 3] = d.AddComponent<HingeJoint>();
            HJt[i + 3].connectedBody = c.GetComponent<Rigidbody>();
            HJt[i + 3].anchor = new Vector3(8.5f, 7.3f, 0);
            HJt[i + 3].axis = new Vector3(0, 1, 0);
            HJt[i + 3].enableCollision = true;
            HJt[i + 3].useSpring = true;
            var spr1 = HJt[i + 3].GetComponent<HingeJoint>().spring;
            spr1.spring = SpringOoo;
            spr1.damper = DamperOoo;
            spr1.targetPosition = Targetposition2;
            HJt[i + 3].GetComponent<HingeJoint>().spring = spr1;
            //set E
            var e = Instantiate(T7Prefab, transform);
            e.transform.position = OPoint[0];
            e.transform.localRotation = Quaternion.Euler(0, i * 120, 0);
            e.AddComponent<MeshCollider>().convex = true;
            e.GetComponent<MeshCollider>().sharedMesh = Meshf;
            e.AddComponent<MeshFilter>().sharedMesh = Meshf;
            T7[i] = e.transform;
            e.AddComponent<Rigidbody>();
            FJt[i + 12] = e.AddComponent<FixedJoint>();
            FJt[i + 12].connectedBody = d.GetComponent<Rigidbody>();
            // set F
            var f = Instantiate(T99Prefab, transform);
            f.transform.position = OPoint[0];
            f.transform.localRotation = Quaternion.Euler(0, i * 120, 0);
            T99[i] = f.transform;
            f.AddComponent<Rigidbody>();
            f.GetComponent<Rigidbody>().useGravity = false;
            f.AddComponent<SphereCollider>().center = new Vector3(10.18f, 7.3f, -12.5f);
            f.GetComponent<SphereCollider>().radius = 0.45f;
            f.GetComponent<SphereCollider>().isTrigger = true;
            FJt[i + 15] = f.AddComponent<FixedJoint>();
            FJt[i + 15].connectedBody = e.GetComponent<Rigidbody>();

            //setA1
            var a1 = Instantiate(T1Prefab, transform);
            a1.transform.position = OPoint[1];
            a1.transform.localRotation = Quaternion.Euler(0, i * 120, 0);
            a1.AddComponent<Rigidbody>();
            a1.GetComponent<Rigidbody>().useGravity = false;
            a1.AddComponent<MeshCollider>();
            a1.GetComponent<MeshCollider>().sharedMesh = Mesha;
            a1.AddComponent<MeshFilter>().sharedMesh = Mesha;
            T1[i+3] = a1.transform;
            FJt[i+18] = a1.AddComponent<FixedJoint>();
            FJt[i+18].connectedBody = o1.GetComponent<Rigidbody>();
            //setab1
            var ab1 = Instantiate(T2Prefab, transform);
            ab1.transform.position = OPoint[1];
            ab1.transform.localRotation = Quaternion.Euler(0, i * 120, 0);
            ab1.AddComponent<Rigidbody>();
            ab1.GetComponent<Rigidbody>().useGravity = false;
            ab1.AddComponent<MeshCollider>();
            ab1.GetComponent<MeshCollider>().sharedMesh = Meshb;
            ab1.AddComponent<MeshFilter>().sharedMesh = Meshb;
            T2[i+3] = ab1.transform;
            HJt[i+6] = ab1.AddComponent<HingeJoint>();
            HJt[i + 6].connectedBody = a1.GetComponent<Rigidbody>();
            HJt[i + 6].anchor = new Vector3(4.45f, 7.3f, 0);
            HJt[i + 6].axis = new Vector3(1, 0, 0);
            HJt[i + 6].enableCollision = true;
            HJt[i + 6].useSpring = true;
            JointSpring spr01 = HJt[i].GetComponent<HingeJoint>().spring;
            spr01.spring = SpringOo;
            spr01.damper = DamperOo;
            spr01.targetPosition = Targetposition;
            HJt[i + 6].GetComponent<HingeJoint>().spring = spr01;
            //setB
            var b1 = Instantiate(T4Prefab, transform);
            b1.transform.position = OPoint[1];
            b1.transform.localRotation = Quaternion.Euler(0, i * 120, 0);
            b1.AddComponent<MeshCollider>().convex = true;
            b1.GetComponent<MeshCollider>().sharedMesh = Meshc;
            b1.AddComponent<MeshFilter>().sharedMesh = Meshc;
            T4[i+3] = b1.transform;
            b1.AddComponent<Rigidbody>();
            FJt[i + 21] = b1.AddComponent<FixedJoint>();
            FJt[i + 21].connectedBody = ab1.GetComponent<Rigidbody>();
            //setC
            var c1 = Instantiate(T5Prefab, transform);
            c1.transform.position = OPoint[1];
            c1.transform.localRotation = Quaternion.Euler(0, i * 120, 0);
            c1.AddComponent<BoxCollider>().center = new Vector3(7.3f, 7.3f, 0.06f);
            c1.GetComponent<BoxCollider>().size = new Vector3(0.7f, 1.6f, 1.3f);
            c1.AddComponent<MeshFilter>().sharedMesh = Meshd;
            T5[i+3] = c1.transform;
            c1.AddComponent<Rigidbody>();
            FJt[i + 24] = c1.AddComponent<FixedJoint>();
            FJt[i + 24].connectedBody = b1.GetComponent<Rigidbody>();
            //setD
            var d1 = Instantiate(T6Prefab, transform);
            d1.transform.position = OPoint[1];
            d1.transform.localRotation = Quaternion.Euler(0, i * 120, 0);
            d1.AddComponent<MeshCollider>();
            d1.GetComponent<MeshCollider>().sharedMesh = Meshe;
            d1.AddComponent<MeshFilter>().sharedMesh = Meshe;
            T6[i+3] = d1.transform;
            d1.AddComponent<Rigidbody>();
            HJt[i + 9] = d1.AddComponent<HingeJoint>();
            HJt[i + 9].connectedBody = c1.GetComponent<Rigidbody>();
            HJt[i + 9].anchor = new Vector3(8.5f, 7.3f, 0);
            HJt[i + 9].axis = new Vector3(0, 1, 0);
            HJt[i + 9].enableCollision = true;
            HJt[i + 9].useSpring = true;
            var spr11 = HJt[i + 9].GetComponent<HingeJoint>().spring;
            spr11.spring = SpringOoo;
            spr11.damper = DamperOoo;
            spr11.targetPosition = Targetposition2;
            HJt[i + 9].GetComponent<HingeJoint>().spring = spr11;
            //setE
            var e1 = Instantiate(T7Prefab, transform);
            e1.transform.position = OPoint[1];
            e1.transform.localRotation = Quaternion.Euler(0, i * 120, 0);
            e1.AddComponent<MeshCollider>().convex = true;
            e1.GetComponent<MeshCollider>().sharedMesh = Meshf;
            e1.AddComponent<MeshFilter>().sharedMesh = Meshf;
            T7[i+3] = e1.transform;
            e1.AddComponent<Rigidbody>();
            FJt[i + 27] = e1.AddComponent<FixedJoint>();
            FJt[i + 27].connectedBody = d1.GetComponent<Rigidbody>();
            //setF
            var f1 = Instantiate(T99Prefab, transform);
            f1.transform.position = OPoint[1];
            f1.transform.localRotation = Quaternion.Euler(0, i * 120, 0);
            T99[i] = f1.transform;
            f1.AddComponent<Rigidbody>();
            f1.GetComponent<Rigidbody>().useGravity = false;
            f1.AddComponent<SphereCollider>().center = new Vector3(10.18f, 7.3f, -12.5f);
            f1.GetComponent<SphereCollider>().radius = 0.45f;
            f1.GetComponent<SphereCollider>().isTrigger = true;
            FJt[i + 30] = f1.AddComponent<FixedJoint>();
            FJt[i + 30].connectedBody = e1.GetComponent<Rigidbody>();
        }

       


    }
    /*
    private void SetBody2()
    {
        //set center 
        var o1 = Instantiate(OPrefab, transform);
        o1.transform.position = OPoint[1];
        O[1] = o1.transform;
        o1.AddComponent<Rigidbody>();
        //_Center.GetComponent<Rigidbody>().mass = centermass;
        o1.GetComponent<Rigidbody>().useGravity = false;
        o1.GetComponent<Rigidbody>().angularDrag = AngulardrugI;
        o1.GetComponent<Rigidbody>().collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;
        o1.AddComponent<MeshCollider>().convex = true;
        o1.GetComponent<MeshCollider>().sharedMesh = Mesho;
        o1.AddComponent<MeshFilter>().sharedMesh = Mesho;
        for (int i = 0; i < 3; i++)
        {
            //set A
            var a1 = Instantiate(T1Prefab, transform);
            a1.transform.position = OPoint[1];
            a1.transform.localRotation = Quaternion.Euler(0, i * 120, 0);
            a1.AddComponent<Rigidbody>();
            a1.GetComponent<Rigidbody>().useGravity = false;
            a1.AddComponent<MeshCollider>();
            a1.GetComponent<MeshCollider>().sharedMesh = Mesha;
            a1.AddComponent<MeshFilter>().sharedMesh = Mesha;
            T1[i] = a1.transform;
            FJt[i] = a1.AddComponent<FixedJoint>();
            FJt[i].connectedBody = o1.GetComponent<Rigidbody>();

            //set connection part between A and B
            var ab1 = Instantiate(T2Prefab, transform);
            ab1.transform.position = OPoint[1];
            ab1.transform.localRotation = Quaternion.Euler(0, i * 120, 0);
            ab1.AddComponent<Rigidbody>();
            ab1.GetComponent<Rigidbody>().useGravity = false;
            ab1.AddComponent<MeshCollider>();
            ab1.GetComponent<MeshCollider>().sharedMesh = Meshb;
            ab1.AddComponent<MeshFilter>().sharedMesh = Meshb;
            T2[i] = ab1.transform;
            HJt[i] = ab1.AddComponent<HingeJoint>();
            HJt[i].connectedBody = a1.GetComponent<Rigidbody>();
            HJt[i].anchor = new Vector3(4.45f, 7.3f, 0);
            HJt[i].axis = new Vector3(1, 0, 0);
            HJt[i].enableCollision = true;
            HJt[i].useSpring = true;
            JointSpring spr01 = HJt[i].GetComponent<HingeJoint>().spring;
            spr01.spring = SpringOo;
            spr01.damper = DamperOo;
            spr01.targetPosition = Targetposition;
            HJt[i].GetComponent<HingeJoint>().spring = spr01;

            //set B(HORIZONTAL)
            var b1 = Instantiate(T4Prefab, transform);
            b1.transform.position = OPoint[1];
            b1.transform.localRotation = Quaternion.Euler(0, i * 120, 0);
            b1.AddComponent<MeshCollider>().convex = true;
            b1.GetComponent<MeshCollider>().sharedMesh = Meshc;
            b1.AddComponent<MeshFilter>().sharedMesh = Meshc;
            T4[i] = b1.transform;
            b1.AddComponent<Rigidbody>();
            FJt[i + 6] = b1.AddComponent<FixedJoint>();
            FJt[i + 6].connectedBody = ab1.GetComponent<Rigidbody>();
     
            //set C
            var c1 = Instantiate(T5Prefab, transform);
            c1.transform.position = OPoint[1];
            c1.transform.localRotation = Quaternion.Euler(0, i * 120, 0);
            c1.AddComponent<BoxCollider>().center = new Vector3(7.3f, 7.3f, 0.06f);
            c1.GetComponent<BoxCollider>().size = new Vector3(0.7f, 1.6f, 1.3f);
            c1.AddComponent<MeshFilter>().sharedMesh = Meshd;
            T5[i] = c1.transform;
            c1.AddComponent<Rigidbody>();
            FJt[i + 9] = c1.AddComponent<FixedJoint>();
            FJt[i + 9].connectedBody = b1.GetComponent<Rigidbody>();
            //set D
            var d1 = Instantiate(T6Prefab, transform);
            d1.transform.position = OPoint[1];
            d1.transform.localRotation = Quaternion.Euler(0, i * 120, 0);
            d1.AddComponent<MeshCollider>();
            d1.GetComponent<MeshCollider>().sharedMesh = Meshe;
            d1.AddComponent<MeshFilter>().sharedMesh = Meshe;
            T6[i] = d1.transform;
            d1.AddComponent<Rigidbody>();
            HJt[i + 3] = d1.AddComponent<HingeJoint>();
            HJt[i + 3].connectedBody = c1.GetComponent<Rigidbody>();
            HJt[i + 3].anchor = new Vector3(8.5f, 7.3f, 0);
            HJt[i + 3].axis = new Vector3(0, 1, 0);
            HJt[i + 3].enableCollision = true;
            HJt[i + 3].useSpring = true;
            var spr11 = HJt[i + 3].GetComponent<HingeJoint>().spring;
            spr11.spring = SpringOoo;
            spr11.damper = DamperOoo;
            spr11.targetPosition = Targetposition2;
            HJt[i + 3].GetComponent<HingeJoint>().spring = spr11;
            //set E
            var e1 = Instantiate(T7Prefab, transform);
            e1.transform.position = OPoint[1];
            e1.transform.localRotation = Quaternion.Euler(0, i * 120, 0);
            e1.AddComponent<MeshCollider>().convex = true;
            e1.GetComponent<MeshCollider>().sharedMesh = Meshf;
            e1.AddComponent<MeshFilter>().sharedMesh = Meshf;
            T7[i] = e1.transform;
            e1.AddComponent<Rigidbody>();
            FJt[i + 12] = e1.AddComponent<FixedJoint>();
            FJt[i + 12].connectedBody = d1.GetComponent<Rigidbody>();
            // set F
            var f1 = Instantiate(T99Prefab, transform);
            f1.transform.position = OPoint[1];
            f1.transform.localRotation = Quaternion.Euler(0, i * 120, 0);
            T99[i] = f1.transform;
            f1.AddComponent<Rigidbody>();
            f1.GetComponent<Rigidbody>().useGravity = false;
            f1.AddComponent<SphereCollider>().center = new Vector3(10.18f, 7.3f, -12.5f);
            f1.GetComponent<SphereCollider>().radius = 0.45f;
            f1.GetComponent<SphereCollider>().isTrigger = true;
            FJt[i + 15] = f1.AddComponent<FixedJoint>();
            FJt[i + 15].connectedBody = e1.GetComponent<Rigidbody>();
        }
    }*/
        // Update is called once per frame
        private void Update()
    {
        if (Input.GetButton("Action"))
        {
            Standup();
        }
        //if (Input.GetAxis("RightHorizontal"))
        //{
        //    Standup();
        //}

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            Stopfolding();
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            Rotate();
        }

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            LegA();
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            LegB();
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            LegC();
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            LegA1();
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            LegB1();
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            LegC1();
        }
        if (Input.GetKeyDown(KeyCode.F))
        {
            Disconnected();
        }
        if (Input.GetKeyDown(KeyCode.G))
        {
            Leaving();
        }
        if (Input.GetKeyDown(KeyCode.H))
        {
            Stable();
        }
        if (Input.GetKeyDown(KeyCode.J))
        {
            Topballconnect();
        }

        Updatabody();

    }

    private static float NewMethod()
    {
        return Input.GetAxis(axisName: "RightHorizontal");
    }

    private void Topballconnect()
    {
        for (int i = 0; i < 3; i++)
        {
            FJt[i + 18] = T99[i].gameObject.AddComponent<FixedJoint>();
            FJt[i + 18].connectedBody = T99[i + 1].gameObject.GetComponent<Rigidbody>();
        }

    }
    private void Updatabody()
    {
        //T2[0].localRotation = T2[1].localRotation = T2[2].localRotation;
        for (int i = 0; i < 3; i++)
        {
            /*
            // set A
            var AO = T1[i].transform.position - O[0].transform.position;
            Vector3 fwd = Vector3.Cross(Vector3.up, AO);
            
            //T1[i].localRotation = Quaternion.LookRotation(fwd, AO);
            // set ABO
            var ABO = T2[i].transform.position - T1[i].transform.position;
           
            //T2[i].localRotation = Quaternion.LookRotation(fwd, ABO);
            // set B
            var BO = T4[i].transform.position - O[0].transform.position;
            //T1[i].localPosition = BO;
           // T2[i].localPosition = BO;
           // T4[i].localPosition = BO;
             //T3[i].localRotation = Quaternion.LookRotation(fwd, BO);
            
    */

        }

    }

    private void Standup()
    {
        for (int i = 0; i < 3; i++)
        {

            //every leg-D rotate
            //C[i].transform.localRotation = LegTest[i].transform.localRotation = E[i].transform.localRotation = Quaternion.Euler(angleOO * Time.deltaTime, i * 120, 0);
            //A1[i].transform.localRotation = B1[i].transform.localRotation = B2[i].transform.localRotation = Quaternion.Euler(angleI * Time.deltaTime, i * 120, 0);
            // center raising 
            //O1[0].transform.position = new Vector3(0, i * 10.0f, 0);
            //CenterPoint[0] = new Vector3(0, 5f * i, 0);
            // T1[i].transform.localRotation = T2[i].transform.localRotation = T4[i].transform.localRotation = Quaternion.Euler(10*Time.deltaTime, i * 120, 0);
            T2[i].GetComponent<Rigidbody>().AddTorque(T2[i].transform.right * 150, ForceMode.Force);
            //HJt[i].useSpring 
            T7[i].GetComponent<Rigidbody>().AddTorque(T7[i].transform.right * 500, ForceMode.Force);

            T2[i+3].GetComponent<Rigidbody>().AddTorque(T2[i+3].transform.right * 150, ForceMode.Force);
            //HJt[i].useSpring 
            T7[i+3].GetComponent<Rigidbody>().AddTorque(T7[i+3].transform.right * 500, ForceMode.Force);

            if (AudioS.isPlaying == false)
            {
                AudioS.Play();
            }
        }
        Debug.Log("FOLDING");
        Debug.Log(Time.deltaTime);

    }

    private void Rotate()
    {
        if (AudioS.isPlaying == false)
        {
            AudioS.Play();
        }
        for (int i = 0; i < 3; i++)
        {
            T4[i].GetComponent<Rigidbody>().AddTorque(T4[i].transform.up * 500, ForceMode.VelocityChange);
            T7[i].GetComponent<Rigidbody>().AddTorque(T7[i].transform.up * 300, ForceMode.Force);
        }
        Debug.Log("ROTATING");
        Debug.Log(Time.deltaTime);
    }

    private void Stopfolding()
    {
        if (AudioS.isPlaying == false)
        {
            AudioS.Play();
        }
        for (int i = 0; i < 3; i++)
        {
            T4[i].GetComponent<Rigidbody>().AddTorque(T4[i].transform.right * 500 * -1, ForceMode.VelocityChange);
            //T7[i].transform.localRotation = T4[i].transform.localRotation = Quaternion.Euler(0, i * 120, 0);
            //T1[i].transform.localRotation = T2[i].transform.localRotation = T4[i].transform.localRotation = Quaternion.Euler(0, i * 120, 0);
            //T1[0].transform.position = T2[i].transform.position = T4[i].transform.position = new Vector3(0, 0, 0);
        }
    }

    private void LegA()
    {
        if (AudioS.isPlaying == false)
        {
            AudioS.Play();
        }
        T4[0].GetComponent<Rigidbody>().AddTorque(T4[0].transform.right * 200 * -1, ForceMode.VelocityChange);
    }
    private void LegB()
    {
        if (AudioS.isPlaying == false)
        {
            AudioS.Play();
        }
        T4[1].GetComponent<Rigidbody>().AddTorque(T4[1].transform.right * 200 * -1, ForceMode.VelocityChange);
    }
    private void LegC()
    {
        if (AudioS.isPlaying == false)
        {
            AudioS.Play();
        }
        T4[2].GetComponent<Rigidbody>().AddTorque(T4[2].transform.right * 200 * -1, ForceMode.VelocityChange);
    }

    private void LegA1()
    {
        if (AudioS.isPlaying == false)
        {
            AudioS.Play();
        }
        T4[3].GetComponent<Rigidbody>().AddTorque(T4[0].transform.right * 200 * -1, ForceMode.VelocityChange);
    }
    private void LegB1()
    {
        if (AudioS.isPlaying == false)
        {
            AudioS.Play();
        }
        T4[4].GetComponent<Rigidbody>().AddTorque(T4[1].transform.right * 200 * -1, ForceMode.VelocityChange);
    }
    private void LegC1()
    {
        if (AudioS.isPlaying == false)
        {
            AudioS.Play();
        }
        T4[5].GetComponent<Rigidbody>().AddTorque(T4[2].transform.right * 200 * -1, ForceMode.VelocityChange);
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
            T4[i].GetComponent<Rigidbody>().AddTorque(T4[i].transform.right * 100000, ForceMode.Force);
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
            T7[i].GetComponent<Rigidbody>().isKinematic = true;
            T99[i].GetComponent<Rigidbody>().isKinematic = true;
        }

    }
}
