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
       // o.GetComponent<Rigidbody>().isKinematic = true;
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
            a.transform.position = OPoint[0];
            a.transform.localRotation = Quaternion.Euler(0, i * 120, 0);
            a.AddComponent<Rigidbody>();
            a.AddComponent<MeshCollider>();
            a.GetComponent<MeshCollider>().sharedMesh = Mesha;
            a.AddComponent<MeshFilter>().sharedMesh = Mesha;
            T1[i] = a.transform;
            FJt[i+3 ] = a.gameObject.AddComponent<FixedJoint>();
            FJt[i+3 ].connectedBody = oa.GetComponent<Rigidbody>();
            //rotate T1
            CJt[i] = oa.AddComponent<ConfigurableJoint>();
            CJt[i].connectedBody = a.GetComponent<Rigidbody>();
            CJt[i].anchor = new Vector3(2.45f, 7.4f, 0);
            CJt[i].axis = new Vector3(-0.11f, 0, 1);
            CJt[i].xMotion = ConfigurableJointMotion.Locked;
            CJt[i].yMotion = ConfigurableJointMotion.Locked;
            CJt[i].zMotion = ConfigurableJointMotion.Locked;
            CJt[i].angularXMotion = ConfigurableJointMotion.Locked;
            CJt[i].angularYMotion = ConfigurableJointMotion.Limited;
            CJt[i].angularZMotion = ConfigurableJointMotion.Locked;
            SoftJointLimit Angy = CJt[i].GetComponent<ConfigurableJoint>().angularYLimit;
            Angy.limit = 30;
            CJt[i].GetComponent<ConfigurableJoint>().angularYLimit = Angy;

            //set connection part between A and B
            var ab = Instantiate(T2Prefab, transform);
            ab.transform.position = OPoint[0];
            ab.transform.localRotation = Quaternion.Euler(0, i * 120, 0);
            ab.AddComponent<Rigidbody>();
            ab.AddComponent<MeshCollider>();
            ab.GetComponent<MeshCollider>().sharedMesh = Meshb;
            ab.AddComponent<MeshFilter>().sharedMesh = Meshb;
            T2[i] = ab.transform;
            FJt[i+3 + 3] = ab.gameObject.AddComponent<FixedJoint>();
            FJt[i+3 + 3].connectedBody = a.GetComponent<Rigidbody>();
            

            //set B(HORIZONTAL)
            var b = Instantiate(V4Prefab, transform);
            b.transform.position = OPoint[0];
            b.transform.localRotation = Quaternion.Euler(0, i * 120, 0);
            b.AddComponent<MeshCollider>();
            b.GetComponent<MeshCollider>().sharedMesh = Meshd;
            V4[i] = b.transform;
            b.AddComponent<Rigidbody>();
            // ab
            CJt[i+3] = ab.AddComponent<ConfigurableJoint>();
            CJt[i+3].connectedBody = b.GetComponent<Rigidbody>();
            CJt[i+3].anchor = new Vector3(5.4f, 7.4f, 0);
            CJt[i+3].axis = new Vector3(0, 1, 0);
            CJt[i+3].xMotion = ConfigurableJointMotion.Locked;
            CJt[i + 3].yMotion = ConfigurableJointMotion.Locked;
            CJt[i + 3].zMotion = ConfigurableJointMotion.Locked;
            CJt[i + 3].angularXMotion = ConfigurableJointMotion.Locked;
            CJt[i + 3].angularYMotion = ConfigurableJointMotion.Locked;
            CJt[i + 3].angularZMotion = ConfigurableJointMotion.Limited;
            SoftJointLimit Angz = CJt[i+3].GetComponent<ConfigurableJoint>().angularZLimit;
            Angz.limit = 45;
            CJt[i+3].GetComponent<ConfigurableJoint>().angularZLimit = Angz;

            //set C
            var c = Instantiate(V5Prefab, transform);
            c.transform.position = OPoint[0];
            c.transform.localRotation = Quaternion.Euler(0, i * 120, 0);
            c.AddComponent<Rigidbody>();
            c.AddComponent<MeshCollider>();
            c.GetComponent<MeshCollider>().sharedMesh = Meshe;
            c.AddComponent<MeshFilter>().sharedMesh = Meshe;
            V5[i] = c.transform;
            CJt[i + 3 + 3] = c.AddComponent<ConfigurableJoint>();
            CJt[i + 3 + 3].connectedBody = b.GetComponent<Rigidbody>();
            CJt[i + 3 + 3].anchor = new Vector3(10.79f, 7.4f, 0);
            CJt[i + 3 + 3].axis = new Vector3(0.08f, 1, 0);
            CJt[i + 3 + 3].xMotion = ConfigurableJointMotion.Locked;
            CJt[i + 3 + 3].yMotion = ConfigurableJointMotion.Locked;
            CJt[i + 3 + 3].zMotion = ConfigurableJointMotion.Locked;
            CJt[i + 3 + 3].angularXMotion = ConfigurableJointMotion.Locked;
            CJt[i + 3 + 3].angularYMotion = ConfigurableJointMotion.Locked;
            CJt[i + 3 + 3].angularZMotion = ConfigurableJointMotion.Limited;
            SoftJointLimit Angzz = CJt[i + 3].GetComponent<ConfigurableJoint>().angularZLimit;
            Angzz.limit = 45;
            CJt[i + 3].GetComponent<ConfigurableJoint>().angularZLimit = Angzz;
           
            //set d
            var d = Instantiate(T3Prefab, transform);
            d.transform.position = OPoint[0];
            d.transform.localRotation = Quaternion.Euler(0, i * 120, 0);
            d.AddComponent<Rigidbody>();
            d.AddComponent<MeshCollider>().convex = true;
            d.GetComponent<MeshCollider>().sharedMesh = Meshc;
            d.AddComponent<MeshFilter>().sharedMesh = Meshc;
            T3[i] = d.transform;
            CJt[i + 6 + 3] = a.AddComponent<ConfigurableJoint>();
            CJt[i+ 6 + 3].connectedBody = d.GetComponent<Rigidbody>();
            CJt[i + 6 + 3].anchor = new Vector3(4.45f, 7.4f, 0);
            CJt[i + 6 + 3].axis = new Vector3(0.5f, 1, 0);
            CJt[i + 6 + 3].xMotion = ConfigurableJointMotion.Locked;
            CJt[i + 6 + 3].yMotion = ConfigurableJointMotion.Locked;
            CJt[i + 6 + 3].zMotion = ConfigurableJointMotion.Locked;
            CJt[i + 6 + 3].angularXMotion = ConfigurableJointMotion.Locked;
            CJt[i + 6 + 3].angularYMotion = ConfigurableJointMotion.Locked;
            CJt[i + 6 + 3].angularZMotion = ConfigurableJointMotion.Limited;
            SoftJointLimit Angyy = CJt[i + 6+3].GetComponent<ConfigurableJoint>().angularZLimit;
            Angyy.limit = 0;
            CJt[i + 6+3].GetComponent<ConfigurableJoint>().angularZLimit = Angyy;
            //set e
            var e = Instantiate(V6Prefab, transform);
            e.transform.position = OPoint[0];
            e.transform.localRotation = Quaternion.Euler(0, i * 120, 0);
            e.AddComponent<Rigidbody>();
            e.AddComponent<CapsuleCollider>().radius = 0.63f;
            e.GetComponent<CapsuleCollider>().center = new Vector3(12.4f, 7.4f, 0);
            e.GetComponent<CapsuleCollider>().height = 22;
            e.AddComponent<MeshFilter>().sharedMesh = Meshf;
            V6[i] = e.transform;
            FJt[i + 3 + 3] = e.AddComponent<FixedJoint>();
            FJt[i + 3 + 3].connectedBody = c.GetComponent<Rigidbody>();
           
        }
    }
    
    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKey(KeyCode.A))
        {
            Standup();
        }
        if (Input.GetKey(KeyCode.UpArrow))
        {
           Standup2();
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            getdown();
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            Rotateforward();
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            Rotateback();
        }
        if (Input.GetKey(KeyCode.D))
        {
            LegC();
        }
        if (Input.GetKey(KeyCode.Q))
        {
            LegA1();
        }
        if (Input.GetKey(KeyCode.W))
        {
            LegB1();
        }
        if (Input.GetKey(KeyCode.E))
        {
            LegC1();
        }
        if (Input.GetKey(KeyCode.Z))
        {
           
        }
        if (Input.GetKey(KeyCode.X))
        {
           
        }
        if (Input.GetKey(KeyCode.C))
        {
            
        }
        if (Input.GetKey(KeyCode.F))
        {
            Disconnected();
        }
        if (Input.GetKey(KeyCode.G))
        {
            Leaving();
        }
        if (Input.GetKey(KeyCode.H))
        {
            Stable();
        }
        if (Input.GetKey(KeyCode.J))
        {
            Topballconnect();
        }

        Updatabody();

    }

    private void Topballconnect()
    {
        for (int i = 0; i < 3; i++)
        {
            //FJt[i + 18] = T99[i].gameObject.AddComponent<FixedJoint>();
            //FJt[i + 18].connectedBody = T99[i + 1].gameObject.GetComponent<Rigidbody>();
        }

    }
    private void Updatabody()
    {
        
        for (int i = 0; i < 3; i++)
        {
          
        }

    }

    private void Standup()
    {
        for (int i = 0; i < 3; i++)
        {
            if (AudioS.isPlaying == false)
            {
                AudioS.Play();
            }
            //V5[0].GetComponent<Rigidbody>().AddTorque(V5[0].transform.forward * 500 * -1, ForceMode.Impulse);
            //V5[1].GetComponent<Rigidbody>().AddTorque(V5[1].transform.forward * 500 * -1, ForceMode.Impulse);
            //V5[2].GetComponent<Rigidbody>().AddTorque(V5[0].transform.forward * 500 * -1, ForceMode.Impulse);
            V5[0].gameObject.AddComponent<ConstantForce>().relativeForce = new Vector3(0, 2000 * Time.deltaTime, 0);
            V5[1].gameObject.AddComponent<ConstantForce>().relativeForce = new Vector3(0, 2000 * Time.deltaTime, 0);
            V5[2].gameObject.AddComponent<ConstantForce>().relativeForce = new Vector3(0, 2000 * Time.deltaTime, 0);

        }
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
        V5[0].GetComponent<Rigidbody>().AddTorque(V5[0].transform.forward * 500, ForceMode.Acceleration);
        V5[1].GetComponent<Rigidbody>().AddTorque(V5[1].transform.forward * 500, ForceMode.Acceleration);
        V5[2].GetComponent<Rigidbody>().AddTorque(V5[2].transform.forward * 500, ForceMode.Acceleration);
        V5[0].gameObject.AddComponent<ConstantForce>().relativeForce = new Vector3(0, 0, -1 * 4000 * Time.deltaTime);
        V5[1].gameObject.AddComponent<ConstantForce>().relativeForce = new Vector3(0, 0, -1 * 4000 * Time.deltaTime);
        V5[2].gameObject.AddComponent<ConstantForce>().relativeForce = new Vector3(0, 0, -1 * 4000 * Time.deltaTime);
    }
    private void Rotateforward()
    {
        if (AudioS.isPlaying == false)
        {
          AudioS.Play();
        }
        T1[0].GetComponent<Rigidbody>().AddTorque(T1[0].transform.up * 20000 * -1, ForceMode.VelocityChange);
        T1[1].GetComponent<Rigidbody>().AddTorque(T1[0].transform.up * 20000, ForceMode.VelocityChange);
        
    }
    private void Rotateback()
    {
        if (AudioS.isPlaying == false)
        {
            AudioS.Play();
        }
        T1[0].GetComponent<Rigidbody>().AddTorque(T1[0].transform.up * 20000 , ForceMode.VelocityChange);
        T1[1].GetComponent<Rigidbody>().AddTorque(T1[1].transform.up * 20000 * -1, ForceMode.VelocityChange);
        Debug.Log("ROTATING");
        Debug.Log(Time.deltaTime);
    }
    private void Forward()
    {
        if (AudioS.isPlaying == false)
        {
            AudioS.Play();
        }  
    }
    private void Back()
    {
        if (AudioS.isPlaying == false)
        {
            AudioS.Play();
        }
        for (int i = 0; i < 3; i++)
        {
            
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
        
        T3[0].GetComponent<Rigidbody>().AddTorque(T3[0].transform.forward * 500*Time.deltaTime, ForceMode.Impulse);
    }
    private void LegB1()
    {
        if (AudioS.isPlaying == false)
        {
            AudioS.Play();
        }
        
        T3[1].GetComponent<Rigidbody>().AddTorque(T3[1].transform.forward * 500 * Time.deltaTime, ForceMode.Impulse);
    }
    private void LegC1()
    {
        if (AudioS.isPlaying == false)
        {
            AudioS.Play();
        }
       
        T3[2].GetComponent<Rigidbody>().AddTorque(T3[2].transform.forward * 500 * Time.deltaTime, ForceMode.Impulse);
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
}
