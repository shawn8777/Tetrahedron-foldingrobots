
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq.Expressions;
using System;

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

    //[SerializeField] public GameObject C1refab;
    //[SerializeField] public GameObject D1Prefab;
    //[SerializeField] public GameObject E1Prefab;
    //[SerializeField] private SharedMeshes _meshes;
    public Mesh _mesho;
    public Mesh _mesha;
    public Mesh _meshb;
    public Mesh _meshc;
    public Mesh _meshd;
    public Mesh _meshe;
    public Mesh _meshf;
    public Mesh _meshg;


    private Vector3[] OPoint = new Vector3[1];

    private Transform[] O =  new Transform[1];
    private Transform[] T1 = new Transform[3];
    private Transform[] T2 = new Transform[3];
    private Transform[] T3 = new Transform[3];
    private Transform[] T4 = new Transform[3];
    private Transform[] T5 = new Transform[3];
    private Transform[] T6 = new Transform[3];
    private Transform[] T7 = new Transform[3];
    //private Transform[] C1 = new Transform[3];
    //private Transform[] D1 = new Transform[3];


    FixedJoint[] FJt = new FixedJoint[20];
    HingeJoint[] HJt = new HingeJoint[20];
    ConfigurableJoint[] CJt = new ConfigurableJoint[20];

    public int limitspring;
    public int limitdamper;
    public int lowXAngular;
    public int highXangular;
    public int ZAngular;
    public int springOO;
    public int damperOO;
    public int springOOO;
    public int damperOOO;
    //public int targetpositionI;
    //public int motorvelocityI;
    //public int angleI;
    public int centermassI;
    public int angulardrugI;
    public int targetposition; 

        
    private void Awake()
    {
        SetOPoints();
    }

        // Use this for initialization
    private void Start()
        {

            SetBody();

        }

        

        void SetOPoints()
        {
            OPoint[0] = new Vector3(0, -5, 0);
            

        }

        void SetBody()
        {
            //set center 
            GameObject _O1 = Instantiate(OPrefab, transform);
            _O1.transform.position = OPoint[0];
            O[0] = _O1.transform;
            _O1.AddComponent<Rigidbody>();
            //_Center.GetComponent<Rigidbody>().mass = centermass;
            _O1.GetComponent<Rigidbody>().useGravity = false;
            _O1.GetComponent<Rigidbody>().angularDrag = angulardrugI;
            _O1.AddComponent<MeshCollider>().convex = true;
            _O1.GetComponent<MeshCollider>().sharedMesh = _mesho;
            _O1.AddComponent<MeshFilter>().sharedMesh = _mesho;
            
            for (int i = 0; i < 3; i++)
            {
                //set A
                GameObject _A = Instantiate(T1Prefab, transform);
                _A.transform.position = OPoint[0];
                _A.transform.localRotation = Quaternion.Euler(0, i * 120, 0);
                _A.AddComponent<Rigidbody>();
                _A.GetComponent<Rigidbody>().useGravity = false;
                _A.AddComponent<MeshCollider>();
                _A.GetComponent<MeshCollider>().sharedMesh = _mesha;
                _A.AddComponent<MeshFilter>().sharedMesh = _mesha;
                T1[i] = _A.transform;
                FJt[i] = _A.AddComponent<FixedJoint>();
                FJt[i].connectedBody = _O1.GetComponent<Rigidbody>();

                //set connection part between A and B
                GameObject _AB = Instantiate(T2Prefab, transform);
                _AB.transform.position = OPoint[0];
                _AB.transform.localRotation = Quaternion.Euler(0, i * 120, 0);
                _AB.AddComponent<Rigidbody>();
                _AB.GetComponent<Rigidbody>().useGravity = false;
                _AB.AddComponent<MeshCollider>();
                _AB.GetComponent<MeshCollider>().sharedMesh = _meshb;
                _AB.AddComponent<MeshFilter>().sharedMesh = _meshb;
                T2[i] = _AB.transform;
            
                HJt[i] = _AB.AddComponent<HingeJoint>();
                HJt[i].connectedBody = _A.GetComponent<Rigidbody>();
                HJt[i].anchor = new Vector3(4.45f, 7.3f, 0);
                HJt[i].axis = new Vector3(1, 0, 0);
                HJt[i].enableCollision = true;
                HJt[i].useSpring = true;
                JointSpring spr0 = HJt[i].GetComponent<HingeJoint>().spring;
                spr0.spring = springOO;
                spr0.damper = damperOO;
                spr0.targetPosition = targetposition;
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
            GameObject _B = Instantiate(T4Prefab, transform);
            _B.transform.position = OPoint[0];
            _B.transform.localRotation = Quaternion.Euler(0, i * 120, 0);
            _B.AddComponent<MeshCollider>().convex = true;
            _B.GetComponent<MeshCollider>().sharedMesh = _meshc;
            _B.AddComponent<MeshFilter>().sharedMesh = _meshc;
            T4[i] = _B.transform;
            _B.AddComponent<Rigidbody>();
            FJt[i + 6] = _B.AddComponent<FixedJoint>();
            FJt[i + 6].connectedBody = _AB.GetComponent<Rigidbody>();
            /*
                //set B(vertical)
                GameObject _E = Instantiate(T3Prefab, transform);
                _E.transform.position =new Vector3(20,2,20);
                _E.transform.localRotation = Quaternion.Euler(0, i * 120, 0);
                _E.AddComponent<MeshCollider>().convex = true;
                _E.GetComponent<MeshCollider>().sharedMesh = _meshd;
                _E.AddComponent<MeshFilter>().sharedMesh = _meshd;
                T3[i] = _E.transform;
                _E.AddComponent<Rigidbody>();
            */

            //set C
            GameObject _C = Instantiate(T5Prefab, transform);
            _C.transform.position = OPoint[0];
            _C.transform.localRotation = Quaternion.Euler(0, i * 120, 0);
            _C.AddComponent<MeshCollider>();
            _C.GetComponent<MeshCollider>().sharedMesh = _meshd;
            _C.AddComponent<MeshFilter>().sharedMesh = _meshd;
            T5[i] = _C.transform;
            _C.AddComponent<Rigidbody>();
            FJt[i + 9] = _C.AddComponent<FixedJoint>();
            FJt[i + 9].connectedBody = _B.GetComponent<Rigidbody>();
            //set D
            GameObject _D = Instantiate(T6Prefab, transform);
            _D.transform.position = OPoint[0];
            _D.transform.localRotation = Quaternion.Euler(0, i * 120, 0);
            _D.AddComponent<MeshCollider>();
            _D.GetComponent<MeshCollider>().sharedMesh = _meshe;
            _D.AddComponent<MeshFilter>().sharedMesh = _meshe;
            T6[i] = _D.transform;
            _D.AddComponent<Rigidbody>();
            //FJt[i + 12] = _D.AddComponent<FixedJoint>();
            //FJt[i + 12].connectedBody = _C.GetComponent<Rigidbody>();
            HJt[i + 3] = _D.AddComponent<HingeJoint>(); 
            HJt[i+3].connectedBody = _C.GetComponent<Rigidbody>();
            HJt[i+3].anchor = new Vector3(8.5f, 7.3f, 0);
            HJt[i + 3].axis = new Vector3(0, 1, 0);
            HJt[i + 3].enableCollision = true;
            HJt[i + 3].useSpring = true;
            JointSpring spr1 = HJt[i+3].GetComponent<HingeJoint>().spring;
            spr1.spring = springOOO;
            spr1.damper = damperOOO;S
            spr1.targetPosition = targetposition;
            HJt[i].GetComponent<HingeJoint>().spring = spr1;
            //set E
            GameObject _E = Instantiate(T7Prefab, transform);
            _E.transform.position = OPoint[0];
            _E.transform.localRotation = Quaternion.Euler(0, i * 120, 0);
            _E.AddComponent<MeshCollider>().convex = true;
            _E.GetComponent<MeshCollider>().sharedMesh = _meshf;
            _E.AddComponent<MeshFilter>().sharedMesh = _meshf;
            T7[i] = _E.transform;
            _E.AddComponent<Rigidbody>();
            FJt[i + 12] = _E.AddComponent<FixedJoint>();
            FJt[i + 12].connectedBody = _D.GetComponent<Rigidbody>();

            /*
                //set B(VERTICAL)
                GameObject _B1 = Instantiate(T2Prefab, transform);
                _B1.transform.position = OPoint[0];
                _B1.transform.localRotation = Quaternion.Euler(0, i * 120, 0);
                _B1.AddComponent<MeshCollider>().convex = true;
                _B1.GetComponent<MeshCollider>().sharedMesh = _meshb;
                _B1.AddComponent<MeshFilter>().sharedMesh = _meshb;
                T2[i] = _B1.transform;
                _B1.AddComponent<Rigidbody>();
                //FJ[i + 3] = _B.AddComponent<FixedJoint>();
                //FJ[i + 3].connectedBody = _A.GetComponent<Rigidbody>();

                HJt[i] = _B.AddComponent<HingeJoint>();
                HJt[i].connectedBody = _A.GetComponent<Rigidbody>();
                HJt[i].anchor = new Vector3(-2.55f, -0.7f, -0.15f);
                HJt[i].axis = new Vector3(0, 0, 1);
                HJt[i].autoConfigureConnectedAnchor = false;
                HJt[i].connectedAnchor = new Vector3(-2.6f, 0, -0.12f);
                HJt[i].enableCollision = true;
                HJt[i].useSpring = true;
                JointSpring spr0 = HJt[i].GetComponent<HingeJoint>().spring;
                spr0.spring = 30;
                spr0.damper = 30;
                spr0.targetPosition = 0;
                HJt[i].GetComponent<HingeJoint>().spring = spr0;

            */

            /*
            //set C
            GameObject _C = Instantiate(CPrefab, transform);
            _C.transform.position = CenterPoint[0];
            _C.transform.localRotation = Quaternion.Euler(0, i * 120, 0);
            _C.AddComponent<MeshCollider>().convex = true;
            _C.GetComponent<MeshCollider>().sharedMesh = _mesh3;
            _C.AddComponent<MeshFilter>().sharedMesh = _mesh3;
            C[i] = _C.transform;
            HJ[i + 3] = _C.AddComponent<HingeJoint>();
            HJ[i + 3].connectedBody = _B.GetComponent<Rigidbody>();
            _C.GetComponent<Rigidbody>().useGravity = false;
            HJ[i + 3].anchor = new Vector3(-4.91f, -0.03f, -0.73f);
            HJ[i + 3].axis = new Vector3(1, 0, 0);
            HJ[i + 3].autoConfigureConnectedAnchor = false;
            HJ[i + 3].connectedAnchor = new Vector3(-4.91f, -0.03f, -0.73f);
            HJ[i + 3].enableCollision = true;
            HJ[i + 3].useSpring = true;
            JointSpring spr2 = HJ[i + 3].GetComponent<HingeJoint>().spring;
            spr2.spring = springOO;
            spr2.damper = damperOO;
            spr2.targetPosition = targetpositionOO;
            HJ[i + 3].GetComponent<HingeJoint>().spring = spr2;
            HJ[i + 3].useMotor = true;
            JointMotor jmr2 = HJ[i + 3].GetComponent<HingeJoint>().motor;
            jmr2.targetVelocity = motorvelocityOO;
            HJ[i + 3].GetComponent<HingeJoint>().motor = jmr2;
            //FJ[3 + i] = _C.AddComponent<FixedJoint>();
            //FJ[3 + i].connectedBody = _B.GetComponent<Rigidbody>();

            //set D
            GameObject _Leg = Instantiate(LegTestPrefab, transform);
            _Leg.transform.position = CenterPoint[0];
            _Leg.transform.localRotation = Quaternion.Euler(0, i * 120, 0);
            _Leg.AddComponent<Rigidbody>();
            _Leg.AddComponent<MeshCollider>().convex = true;
            _Leg.GetComponent<MeshCollider>().sharedMesh = _mesh4;
            _Leg.AddComponent<MeshFilter>().sharedMesh = _mesh4;
            LegTest[i] = _Leg.transform;
            FJ[i + 6] = _Leg.AddComponent<FixedJoint>();
            FJ[i + 6].connectedBody = _C.GetComponent<Rigidbody>();

            /*
            HJ[i + 3] = _D.AddComponent<HingeJoint>();
            HJ[i + 3].connectedBody = _C.GetComponent<Rigidbody>();
            HJ[i+3].anchor = new Vector3(-6.88f, 0, -0.72f);
            HJ[i+3].axis = new Vector3(1, 0, 0);
            HJ[i+3].autoConfigureConnectedAnchor = false;
            HJ[i+3].connectedAnchor = new Vector3(-6.88f, 0, -0.75f);
            HJ[i+3].enableCollision = true;
            HJ[i+3].useSpring = true;
            JointSpring spr2 = HJ[i + 3].GetComponent<HingeJoint>().spring;
            spr2.spring = springOO;
            spr2.damper = damperOO;
            spr2.targetPosition = targetpositionOO;
            HJ[i+3].GetComponent<HingeJoint>().spring = spr2;
            HJ[i + 3].useMotor = true;
            JointMotor jmr2 = HJ[i + 3].GetComponent<HingeJoint>().motor;
            jmr2.targetVelocity = 50;
            HJ[i + 3].GetComponent<HingeJoint>().motor = jmr2;
            */

            /*
            //SET E
            GameObject _E = Instantiate(EPrefab, transform);
            _E.transform.position = CenterPoint[0];
            _E.transform.localRotation = Quaternion.Euler(0, i * 120, 0);
            _E.AddComponent<Rigidbody>();
            E[i] = _E.transform;
            FJ[i + 3] = _E.AddComponent<FixedJoint>();
            FJ[i + 3].connectedBody = _Leg.GetComponent<Rigidbody>();
            _E.AddComponent<MeshCollider>().convex = false;
            _E.GetComponent<MeshCollider>().sharedMesh = _mesh5;
            _E.AddComponent<MeshFilter>().sharedMesh = _mesh5;
            */

        }
    }
    // Update is called once per frame
    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Standup();
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            Stopfolding();
        }


        if (Input.GetKeyDown(KeyCode.D))
        {
            Standup1();
        }

        Updatabody();

    }
    void Updatabody()
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

            

            //set E 
            //var EO = T3[i].transform.position - new Vector3(0, 0, 0);
            //T3[i].localPosition = T4[i].transform.position;*/
        }

    }

        void Standup()
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
            //T4[i].GetComponent<Rigidbody>().AddTorque(T2[i].transform.forward*100000,ForceMode.Force);
            T2[i].GetComponent<Rigidbody>().AddTorque(T2[i].transform.right* 150, ForceMode.Force);
            //HJt[i].useSpring 
            T7[i].GetComponent<Rigidbody>().AddTorque(T7[i].transform.right * 500, ForceMode.Force);
        }
            Debug.Log("stand up");
            Debug.Log(Time.deltaTime);

        }

        void Standup1()
        {
            for (int i = 0; i < 3; i++)
            {
            T4[i].GetComponent<Rigidbody>().AddTorque(T4[i].transform.up * 150, ForceMode.VelocityChange);
            }
            Debug.Log("stand up1");
            Debug.Log(Time.deltaTime);
        }

        void Stopfolding()
        {
            for (int i = 0; i < 3; i++)
            {
                
                T1[i].transform.localRotation = T2[i].transform.localRotation = T4[i].transform.localRotation = Quaternion.Euler(0, i * 120, 0);
                T1[0].transform.position = T2[i].transform.position = T4[i].transform.position = new Vector3(0, 0, 0);
            }
        }

        void Restart()
        {
            //return  void SetCenterandABCD();
        }

       

    }

