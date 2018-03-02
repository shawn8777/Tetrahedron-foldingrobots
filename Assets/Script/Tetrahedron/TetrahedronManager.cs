using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq.Expressions;
using System;

//using TetrahedronA.Unity.Script.sharedMesh;

namespace TetrahedronA.Unity.Script.TetrahedronManager
{
    public partial class TetrahedronManager : MonoBehaviour
    {

        [SerializeField] public GameObject CenterPrefab;
        [SerializeField] public GameObject APrefab;
        [SerializeField] public GameObject BPrefab;
        [SerializeField] public GameObject CPrefab;
        [SerializeField] public GameObject LegTestPrefab;
        [SerializeField] public GameObject EPrefab;
        //[SerializeField] private SharedMeshes _meshes;
        public Mesh _mesh0;
         public Mesh _mesh1;
         public Mesh _mesh2;
         public Mesh _mesh3;
         public Mesh _mesh4;
         public Mesh _mesh5;
       
        private Vector3[] CenterPoint = new Vector3[1];

        private Transform[] center = new Transform[1];
        private Transform[] A = new Transform[3];
        private Transform[] B = new Transform[3];
        private Transform[] C = new Transform[3];
        private Transform[] LegTest = new Transform[3];
        private Transform[] E = new Transform[3];


        FixedJoint[] FJ = new FixedJoint[12];
        HingeJoint[] HJ = new HingeJoint[6];

        public int springOO;
        public int damperOO;
        public int targetpositionOO;
        public int motorvelocityOO;
        public int angleOO;
        public int centermass;
        public int angulardrugOO;


     




        private void Awake()
        {
            SetPoints();
        }

        // Use this for initialization
        void Start()
        {
            
            SetCenterandABCD();
                
        }

        // Update is called once per frame
        void Update()
        {
             if (Input.GetKeyDown(KeyCode.S))
            {
                Standup();
            }

             if (Input.GetKeyDown(KeyCode.P))
            {
                Stopfolding();
            }


            if (Input.GetKeyDown(KeyCode.C))
            {
                Restart();
            }
                

            
        }

        void SetPoints()
        {
            CenterPoint[0] = new Vector3(0, 0, 0);

        }

        void SetCenterandABCD()
        {
            //set center 
            GameObject _Center = Instantiate(CenterPrefab, transform);
            _Center.transform.position = CenterPoint[0];
            center[0] = _Center.transform;
            _Center.AddComponent<Rigidbody>();
            //_Center.GetComponent<Rigidbody>().mass = centermass;
            //Center.GetComponent<Rigidbody>().useGravity = false;
            _Center.GetComponent<Rigidbody>().angularDrag = angulardrugOO;
            _Center.AddComponent<MeshCollider>().convex = true;
            _Center.GetComponent<MeshCollider>().sharedMesh = _mesh0;
            _Center.AddComponent<MeshFilter>().sharedMesh = _mesh0;
            

            for (int i = 0; i < A.Length; i++)
            {
                //set A
                GameObject _A = Instantiate(APrefab, transform);
                _A.transform.position = CenterPoint[0];
                _A.transform.localRotation = Quaternion.Euler(0, i * 120, 0);
                _A.AddComponent<Rigidbody>();
                _A.GetComponent<Rigidbody>().useGravity = false;
                _A.AddComponent<MeshCollider>();
                _A.GetComponent<MeshCollider>().sharedMesh = _mesh1;
                _A.AddComponent<MeshFilter>().sharedMesh = _mesh1;
                A[i] = _A.transform;
                FJ[i] = _A.AddComponent<FixedJoint>();
                FJ[i].connectedBody = _Center.GetComponent<Rigidbody>();


                //set B
                GameObject _B = Instantiate(BPrefab, transform);
                _B.transform.position = CenterPoint[0];
                _B.transform.localRotation = Quaternion.Euler(0, i * 120, 0);
                _B.AddComponent<MeshCollider>().convex = true;
                _B.GetComponent<MeshCollider>().sharedMesh = _mesh2;
                _B.AddComponent<MeshFilter>().sharedMesh = _mesh2;
                B[i] = _B.transform;
                _B.AddComponent<Rigidbody>();
                //FJ[i + 3] = _B.AddComponent<FixedJoint>();
                //FJ[i + 3].connectedBody = _A.GetComponent<Rigidbody>();
                
                HJ[i] = _B.AddComponent<HingeJoint>();
                HJ[i].connectedBody = _A.GetComponent<Rigidbody>();
                HJ[i].anchor = new Vector3(-2.55f,-0.7f, -0.15f);
                HJ[i].axis = new Vector3(0, 0, 1);
                HJ[i].autoConfigureConnectedAnchor = false;
                HJ[i].connectedAnchor = new Vector3(-2.6f, 0, -0.12f);
                HJ[i].enableCollision = true;
                HJ[i].useSpring = true;
                JointSpring spr = HJ[i].GetComponent<HingeJoint>().spring;
                spr.spring = 30;
                spr.damper = 30;
                spr.targetPosition = 0;
                HJ[i].GetComponent<HingeJoint>().spring = spr;
                


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

       void Standup()
        {
            for (int i = 0; i < A.Length; i++)
            {
                //every leg-D rotate
                //C[i].transform.localRotation = LegTest[i].transform.localRotation = E[i].transform.localRotation = Quaternion.Euler(angleOO * Time.deltaTime, i * 120, 0);
                C[i].transform.localRotation = LegTest[i].transform.localRotation = Quaternion.Euler(angleOO * Time.deltaTime, i * 120, 0);
                // center raising 
                center[0].transform.position = new Vector3(0, i * 10.0f, 0);
                //CenterPoint[0] = new Vector3(0, 5f * i, 0);
            }
            Debug.Log("stand up");
            Debug.Log(Time.deltaTime);

        }

        void Stopfolding()
        {
            for (int i = 0; i < A.Length; i ++)
            {
              A[i].transform.localRotation = B[i].transform.localRotation = C[i].transform.localRotation = LegTest[i].transform.localRotation = Quaternion.Euler(0, i * 120, 0);
              center[0].transform.position = A[i].transform.position = B[i].transform.position = C[i].transform.position = LegTest[i].transform.position  = new Vector3(0, 0, 0);
            }
        }

        void Restart()
        {
            //return  void SetCenterandABCD();
        }

    }
}
