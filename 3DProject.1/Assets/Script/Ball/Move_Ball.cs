using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move_Ball : MonoBehaviour
{
    public GameManager Gm;

    public GameObject Catcher;
    public GameObject StrikeZone;
    public GameObject BPS1;
    public GameObject BPS2;
    public GameObject PitcherPosition;

    public Gravity3 m_gGravity3;
    public float m_fBall_Speed;

    public bool m_bPitch = false;
    public bool m_bThrow = false;
    public bool m_bSwing = false;

    public bool m_bBallProgressSection1 = false;
    public bool m_bBallProgressSection2 = false;

    public bool m_bBattingPosition1 = false;
    public bool m_bBattingPosition2 = false;
    public bool m_bBattingPosition3 = false;
    public bool m_bBattingPosition4 = false;

    public bool m_bHit = false;//
    public bool m_bStrike = false;//
    public bool m_bFoul = false;//
    public bool m_bAnta = false;//
    public bool m_bOut = false;//
    public bool m_bHomeRun = false;//
    public bool m_bBatPower = false;

    public bool m_bCountball = false;

    public Vector3 m_vInitialProgressDir;
    public Vector3 m_vFinalBallProgressDir;
    public Vector3 m_tBatPosition;

    public Vector3 m_vBallProgress1;
    public Vector3 m_vBallProgress2;
    public Vector3 m_vBallProgress3;

    public virtual void Move() { }

    protected virtual void Awake() { }

    public virtual Vector3 SetVector1() { return Vector3.zero; }
    public virtual Vector3 SetVector2() { return Vector3.zero; }

    public virtual void CreateTest() { }

    private void Start()
    {
        m_bThrow = false;
    }

    void ThinkBall()
    {
        if (StrikeZone && Catcher)
        {
            float StrikeZone_Scale = StrikeZone.transform.localScale.x;
            float StrikeZone_Position_x = StrikeZone.transform.position.x;
            float StrikeZone_ax = StrikeZone_Position_x - StrikeZone_Scale / 2;
            float StrikeZone_bx = StrikeZone_Position_x + StrikeZone_Scale / 2;

            float Catcher_Scale = Catcher.transform.localScale.x;
            float Catcher_Position_x = Catcher.transform.position.x;
            if (Catcher_Position_x + Catcher_Scale / 2 >= StrikeZone_ax && Catcher_Position_x - Catcher_Scale / 2 <= StrikeZone_bx)
            {
                m_bStrike = true; //Debug.Log("Strike " + m_bStrike);
            }
            else
            {
                m_bStrike = false; //Debug.Log("Ball " + m_bStrike);
            }
        }
    }

    protected int i = 0;
    public void Ball_InitialLize()
    {
        this.transform.position = PitcherPosition.transform.position;
        m_gGravity3.Gravity_InitialLize();
        m_bBatPower = false;
        m_bHit = false;
        m_bThrow = false;
        m_bPitch = false;
        m_bBattingPosition1 = false;
        m_bBattingPosition2 = false;
        m_bBattingPosition3 = false;
        m_bBattingPosition4 = false;
        m_vBallProgress2 = Vector3.zero;
        m_vBallProgress3 = Vector3.zero;
        m_bBallProgressSection1 = false;
        m_bBallProgressSection2 = false;
        m_bCountball = false;
        m_bSwing = false;
        m_bFoul = false;
        m_bAnta = false;
        m_bOut = false;
        m_bHomeRun = false;
        i = 0;
    }

    private void FixedUpdate()
    {
        SetVector1();
        ThinkBall();
        if (m_bThrow == true)
        {
            Move();
            BallProgressSection();
            if (m_bBallProgressSection1 == true)
            {
                SetVector2();
            }
        }
        //if (m_bHit == true)
        //{
        //    if (m_gGravity3.m_nBounceCount == m_gGravity3.m_nBounceCount_Current)
        //    {
        //        if (this.transform.position.y < 0.15f) Debug.Log("안타");
        //    }
        //}
    }

    // nDir: 좌우방향(HittingPosition에따른), nVector: 위아래방향(스윙 방법에따른)
    void Swing(string nString, Vector3 nDir, Vector3 nVector)
    {
        m_bHit = true;
        m_bBatPower = true;
        Vector3 m_vRVector = -m_gGravity3.m_vCurrentForce + nDir + nVector;
        m_vBallProgress3 = m_vRVector;
        Debug.Log(nString);
        m_gGravity3.m_bBounce = true;
    }

    void BallProgressSection()
    {
        int nLayer = 1 << LayerMask.NameToLayer("BPS");
        Collider[] BPS_C = Physics.OverlapSphere(this.transform.position, 0.15f, nLayer);
        //Collider[] BPS_C = Physics.OverlapBox(BPS.transform.position, BPS.transform.localScale, BPS.transform.rotation, nLayer);
        if (BPS_C != null)
        {
            //Debug.Log(BPS_C.Length);
            for (int i = 0; i < BPS_C.Length; i++)
            {
                //Debug.Log(BPS_C[i].gameObject.name);
                if (BPS_C[i].gameObject.name == "BallProgressSection1" && m_bBallProgressSection1 == false)
                {
                    Debug.Log("Ball in BPS1");
                    m_bBallProgressSection1 = true;
                }
                if (BPS_C[i].gameObject.name == "BallProgressSection2" && m_bBallProgressSection2 == false)
                {
                    Debug.Log("Ball in BPS2");
                    m_bBallProgressSection2 = true;
                }
                //if (BPS_C[i].gameObject.tag == "Bat")
                //{
                //    if (m_bHit == false)
                //    {
                //        Debug.Log("타격");
                //        m_bHit = true;
                //        //Debug.Log("Ball.Position: " + this.transform.position);
                //        //Debug.Log("Bat.Position: " + BPS_C[i].gameObject.transform.position);
                //        Vector3 m_vRVector = -m_gGravity3.m_vCurrentForce;
                //        //Debug.Log("m_vRVector: " + m_vRVector);
                //        m_vBallProgress3 = BPS_C[i].gameObject.transform.position - this.transform.position;
                //        m_vBallProgress3 += m_vRVector;
                //        //Debug.Log("BallProgress: " + m_vBallProgress3);
                //        //Debug.Log("Ball in " + BPS_C[i].gameObject.name);
                //        m_gGravity3.m_bBounce = true;
                //        //StartCoroutine(ProcessHitDelay(1));
                //    }
                //}
                if (BPS_C[i].gameObject.name == "Wall")
                {
                    m_gGravity3.m_vCurrentForce = Vector3.zero;
                }

                if (BPS_C[i].gameObject.name == "HittingPosition1")
                {
                    //Debug.Log("HittingPosition1통과");
                    if (m_bStrike == true)
                    if (m_bBatPower == false)
                    {
                        Vector3 m_vLeftRight = new Vector3(-0.2f, -0.1f, 0);
                        if (Input.GetKey(KeyCode.A)) // UpperSwing
                        {
                            Vector3 m_vUpDown = new Vector3(0, 0.2f, 0.5f);
                            Swing("HittingPosition1_A", m_vLeftRight, m_vUpDown);
                        }
                        else if (Input.GetKey(KeyCode.S)) // MiddleSwing
                        {
                            Vector3 m_vUpDown = new Vector3(0, 0.1f, -0.4f);
                            Swing("HittingPosition1_S", m_vLeftRight, m_vUpDown);
                        }
                        else if (Input.GetKey(KeyCode.D)) // MiddleSwing
                        {
                            Vector3 m_vUpDown = new Vector3(0, 0.3f, -0.05f);
                            Swing("HittingPosition1_D", m_vLeftRight, m_vUpDown);
                        }
                    }
                    if (m_bStrike == false)
                    {
                        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
                            m_bSwing = true;
                    }
                }
                if (BPS_C[i].gameObject.name == "HittingPosition2")
                {
                    //Debug.Log("HittingPosition2통과");
                    if (m_bStrike == true)
                    if (m_bBatPower == false)
                    {
                        Vector3 m_vLeftRight = new Vector3(-0.1f, 0, 0);
                        if (Input.GetKey(KeyCode.A)) // UpperSwing
                        {
                            Vector3 m_vUpDown = new Vector3(0, 0.3f, 0.3f);
                            Swing("HittingPosition2_A", m_vLeftRight, m_vUpDown);
                        }
                        else if (Input.GetKey(KeyCode.S)) // MiddleSwing
                        {
                            Vector3 m_vUpDown = new Vector3(-0.1f, 0.1f, -0.4f);
                            Swing("HittingPosition2_S", m_vLeftRight, m_vUpDown);
                        }
                        else if (Input.GetKey(KeyCode.D)) // MiddleSwing
                        {
                            Vector3 m_vUpDown = new Vector3(-0.1f, 0.6f, -0.3f);
                            Swing("HittingPosition2_D", m_vLeftRight, m_vUpDown);
                        }
                    }
                    if (m_bStrike == false)
                    {
                        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
                            m_bSwing = true;
                    }
                }
                if (BPS_C[i].gameObject.name == "HittingPosition3")
                {
                    //Debug.Log("HittingPosition3통과");
                    if (m_bStrike == true)
                    if (m_bBatPower == false)
                    {
                        Vector3 m_vLeftRight = new Vector3(0.3f, 0, 0);
                        if (Input.GetKey(KeyCode.A)) // UpperSwing
                        {
                            Vector3 m_vUpDown = new Vector3(-0.2f, 1f, -0.05f);
                            Swing("HittingPosition3_A", m_vLeftRight, m_vUpDown);
                        }
                        else if (Input.GetKey(KeyCode.S)) // MiddleSwing
                        {
                            Vector3 m_vUpDown = new Vector3(0f, 0.7f, 0.1f);
                            Swing("HittingPosition3_S", m_vLeftRight, m_vUpDown);
                        }
                        else if (Input.GetKey(KeyCode.D)) // MiddleSwing
                        {
                            Vector3 m_vUpDown = new Vector3(-0.1f, 0.5f, -0.1f);
                            Swing("HittingPosition3_D", m_vLeftRight, m_vUpDown);
                        }
                    }
                    if (m_bStrike == false)
                    {
                        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
                            m_bSwing = true;
                    }
                }
                if (BPS_C[i].gameObject.name == "HittingPosition4")
                {
                    //Debug.Log("HittingPosition4통과");
                    if (m_bStrike == true)
                        if (m_bBatPower == false)
                        {
                            Vector3 m_vLeftRight = new Vector3(0.2f, 0, 0);
                            if (Input.GetKey(KeyCode.A)) // UpperSwing
                            {
                                Vector3 m_vUpDown = new Vector3(0, 0.3f, 0.4f);
                                Swing("HittingPosition4_A", m_vLeftRight, m_vUpDown);
                            }
                            else if (Input.GetKey(KeyCode.S)) // MiddleSwing
                            {
                                Vector3 m_vUpDown = new Vector3(0.5f, 0.1f, 0.1f);
                                Swing("HittingPosition4_S", m_vLeftRight, m_vUpDown);
                            }
                            else if (Input.GetKey(KeyCode.D)) // MiddleSwing
                            {
                                Vector3 m_vUpDown = new Vector3(0.1f, 0.3f, -0.1f);
                                Swing("HittingPosition4_D", m_vLeftRight, m_vUpDown);
                            }
                        }
                    if (m_bStrike == false)
                    {
                        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
                            m_bSwing = true;
                    }
                }

                if (BPS_C[i].gameObject.tag == "Deffenser")
                {
                    m_gGravity3.Gravity_InitialLize();
                    Debug.Log("Catching Ball");
                    if (m_bOut == false)
                    {
                        m_bOut = true;
                        Debug.Log("플라이 아웃");
                        Gm.m_nOut++;
                        Gm.ResetBallStrikeCount();
                    }
                }

                if (BPS_C[i].gameObject.name == "Catcher")
                {
                    Debug.Log("Ball in Catcher");
                    m_gGravity3.m_bUse = false;
                    if (m_bCountball == false)
                    {
                        m_bCountball = true;
                        if (m_bStrike == true) Gm.m_nStrike++;
                        else
                        {
                            if (m_bSwing == false)
                                Gm.m_nBall++;
                            else Gm.m_nStrike++;
                        }
                    }
                }

                if (BPS_C[i].gameObject.name == "FoulArea")
                {
                    //if (m_gGravity3.m_nBounceCount == m_gGravity3.m_nBounceCount_Current)
                    {
                        if (m_bFoul == false)
                        {
                            m_bFoul = true;
                            if (Gm.m_nStrike < 2) Gm.m_nStrike++;
                            Debug.Log("파울");
                        }
                    }
                }
                if (BPS_C[i].gameObject.name == "HomeRunArea")
                {
                    //if (m_gGravity3.m_nBounceCount == m_gGravity3.m_nBounceCount_Current)
                    {
                        if (m_bHomeRun == false)
                        {
                            m_bHomeRun = true;
                            Gm.BaseRun_HomeRun();
                            Debug.Log("홈런");
                        }
                    }
                }
                if (BPS_C[i].gameObject.name == "Ground")
                {
                    //if (m_gGravity3.m_nBounceCount == m_gGravity3.m_nBounceCount_Current)
                    {
                        if (m_bAnta == false && m_bFoul == false && m_bHomeRun == false)
                        {
                            m_bAnta = true;
                            Gm.BaseRun();
                            Debug.Log("안타");
                            Gm.ResetBallStrikeCount();
                        }
                    }
                }
            }
        }
    }

    IEnumerator ProcessHitDelay(float nTime)
    {
        yield return new WaitForSeconds(nTime);
        m_bHit = true;
        m_gGravity3.m_bUse = true;
    }

    private void OnDrawGizmos()
    {
        //Gizmos.color = Color.white;
        //Gizmos.DrawCube(BPS1.transform.position, BPS1.transform.localScale);
        //Gizmos.DrawCube(BPS2.transform.position, BPS2.transform.localScale);
        Gizmos.color = Color.red;
        Gizmos.DrawLine(this.transform.position, Catcher.transform.position);
    }
}
