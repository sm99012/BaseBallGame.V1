using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager GM_Instance = null;
    public static GameManager Instance
    {
        get
        {
            if (!GM_Instance)
            {
                GM_Instance = FindObjectOfType(typeof(GameManager)) as GameManager;
            }
            return GM_Instance;
        }
    }

    private void Awake()
    {
        if (!GM_Instance)
        {
            GM_Instance = this;
        }
        else if (GM_Instance != this)
        {
            Destroy(gameObject);
        }
    }

    public Camera MainCamera;
    private Quaternion MainCamera_InitialAngle;
    private Vector3 MainCamera_InitialPosiiton;
    public Quaternion MainCamera_HittingAngle;

    public GameObject StrikeZone;
    public GameObject Catcher;

    public GameObject Ball;
    //private Move_Ball m_mBall;
    private Ball m_bBall;

    public Vector3 m_vDir;
    public bool m_bCameraShow0;
    public bool m_bCameraShow1;

    public int m_nPitchBall = 0;
    public int m_nStrike = 0;
    public int m_nBall = 0;
    public int m_nOut = 0;

    public int m_nCount = 0;

    public bool m_bBase_1 = false;
    public bool m_bBase_2 = false;
    public bool m_bBase_3 = false;

    void BaseBallRule()
    {
        DecisionBallCount();
    }

    public void BaseRun()
    {
        if (m_bBase_1 == false && m_bBase_2 == false && m_bBase_3 == false) m_bBase_1 = true;
        else if (m_bBase_1 == true && m_bBase_2 == false && m_bBase_3 == false) m_bBase_2 = true;
        else if (m_bBase_1 == true && m_bBase_2 == true && m_bBase_3 == false) m_bBase_3 = true;
        else if (m_bBase_1 == true && m_bBase_2 == true && m_bBase_3 == true) m_nCount++;
    }

    public void BaseRun_HomeRun()
    {
        int Count = 1;
        if (m_bBase_1 == true) { Count++; m_bBase_1 = false; }
        if (m_bBase_2 == true) { Count++; m_bBase_2 = false; }
        if (m_bBase_3 == true) { Count++; m_bBase_3 = false; }
        m_nCount += Count;
        ResetBallStrikeCount();
    }

    IEnumerator ProcessResetBallStrikeCount(float nTime)
    {
        yield return new WaitForSeconds(nTime);
        if (m_nStrike == 3)
        {
            if (m_nOut < 2)
            {
                m_nOut++;
            }
            else Debug.Log("End_Attack");
        }
        else if (m_nBall == 4)
        {
            BaseRun();
        }
        m_nStrike = 0;
        m_nBall = 0;
    }

    public void ResetBallStrikeCount()
    {
        m_nStrike = 0;
        m_nBall = 0;
    }

    void DecisionBallCount()
    {
        if (m_nBall == 4 || m_nStrike == 3)
        {
            StartCoroutine(ProcessResetBallStrikeCount(1));
        }
    }

    void Start()
    {
        m_bBall = Ball.GetComponent<Ball>();
        m_vDir = Vector3.Normalize(Catcher.transform.position - Ball.transform.position);
        m_bCameraShow0 = true;
        m_bCameraShow1 = false;

        MainCamera_InitialPosiiton = MainCamera.transform.position;
        MainCamera_InitialAngle = MainCamera.transform.rotation;
        MainCamera_HittingAngle = MainCamera.transform.rotation;

        Ro = MainCamera_InitialAngle;
        Ro0 = Ro;
    }

    // Update is called once per frame
    void Update()
    {
        BaseBallRule();
        //if (Input.GetKeyDown(KeyCode.I))
        //{
        //    InitialLize();
        //}
        //if (Input.GetKeyDown(KeyCode.O))
        //{
        //    m_nPitchBall++; // 던진공 수
        //    Ball.gameObject.GetComponent<Move_Ball>().m_gGravity3.m_bUse = true;
        //    Ball.gameObject.GetComponent<Move_Ball>().m_bThrow = true;
        //}

        if (Input.GetKeyDown(KeyCode.C))
        {
            Catcher.transform.position += new Vector3(0.1f, 0, 0);
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            Catcher.transform.position += new Vector3(-0.1f, 0, 0);
        }
        //MainCamera_HittingAngle = MainCamera.transform.rotation;
        //Debug.Log("MainCamera.Rotation: " + MainCamera.transform.localRotation.x + ", " + MainCamera.transform.localRotation.y + ", " + MainCamera.transform.localRotation.z);
    }

    Coroutine PCS1;

    IEnumerator ProcessCameraShow1(float nTime)
    {
        yield return new WaitForSeconds(nTime);
        if (m_bCameraShow1 == false)
        {
            m_bCameraShow0 = false;
            m_bCameraShow1 = true;
        }
    }

    Vector3 m_vBallPos;
    Vector3 m_vMainCameraPos;
    Vector3 m_vnDir;
    float m_fDir;
    Quaternion Ro;
    Quaternion Ro0;

    private void FixedUpdate()
    {
        if (m_bBall.m_eBallState == BallManager.E_BALL_STATE.HIT)
        {
            m_vBallPos = Ball.gameObject.transform.position;
            m_vMainCameraPos = MainCamera.gameObject.transform.position;
            m_vnDir = Vector3.Normalize(m_vBallPos - m_vMainCameraPos);

            m_fDir = Vector3.Distance(m_vBallPos, m_vMainCameraPos);

            PCS1 = StartCoroutine(ProcessCameraShow1(2));
        }
        else m_vnDir = Vector3.zero;

        if (m_bCameraShow0 == true)
        {
            Ro0.w = m_vnDir.x / 2;
            Ro0.z = m_vnDir.y / 2;
            MainCamera.transform.rotation = Ro0;
        }
        else if (m_bCameraShow1 == true)
        {
            //if (m_fDir > 0.05f)
            {
                Vector3 m_vCamPos = MainCamera.transform.position;
                m_vCamPos.y = Ball.transform.position.y + 40;
                MainCamera.transform.position = m_vCamPos;
                Vector3 m_vnDir = Vector3.down;

                MainCamera.transform.position = new Vector3(Ball.transform.position.x, Ball.transform.position.y + 30, Ball.transform.position.z);
                MainCamera.transform.rotation = Quaternion.AngleAxis(90, new Vector3(1, 0, 0));
            }
            Ro0 = Ro;
        }
    }

    // 초기화(카메라, 공, 배트)
    public void InitialLize()
    {
        StopAllCoroutines();
        //StopCoroutine(PCS1);
        m_bBall.Ball_InitialLize();
        m_vDir = Vector3.zero;

        MainCamera.transform.position = MainCamera_InitialPosiiton;
        MainCamera.transform.rotation = MainCamera_InitialAngle;

        m_bCameraShow0 = true;
        m_bCameraShow1 = false;
    }
}