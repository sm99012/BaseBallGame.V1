using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : BallManager
{
    // Ball 스크립트 자체도 싱글톤으로 만들어 관리.
    private static Ball Ball_Instance = null;
    public static Ball BInstance
    {
        get
        {
            if (!Ball_Instance)
            {
                Ball_Instance = FindObjectOfType(typeof(Ball)) as Ball;
            }
            return Ball_Instance;
        }
    }


    public Gravity3 m_gGravity; // 물리

    public Vector3 m_vBallProgress; // 공의 이동방향 설정

    BallKind m_bCurrentBall;

    Vector3 PitcherPosition;

    protected override void UpdateBallState()
    {
        switch (m_eBallState)
        {
            case E_BALL_STATE.STAY: // 공 대기상태
                {
                    m_gGravity.m_bUse = false;
                    //m_gGravity.m_vCurrentForce = Vector3.zero; // 뭐가더 낫나?
                    //m_vBallProgress = Vector3.zero;
                    m_eHitState = E_HIT_STATE.NULL;
                }
                break;
            case E_BALL_STATE.THROW: // 투구
                {
                    m_eHitState = E_HIT_STATE.NULL;
                } 
                break;
            case E_BALL_STATE.HIT: // 타격
                {

                } 
                break;
            case E_BALL_STATE.CATCH: // 포구
                {
                    m_gGravity.m_bUse = false;
                } 
                break;
        }
    }

    protected override void UpdateHitState()
    {
        if (m_eBallState == E_BALL_STATE.HIT)
        {
            switch (m_eHitState)
            {
                case E_HIT_STATE.FOUL:
                    {
                        
                    }
                    break;
                case E_HIT_STATE.HIT:
                    {

                    }
                    break;
                case E_HIT_STATE.HOMERUN:
                    {

                    }
                    break;
                case E_HIT_STATE.OUT:
                    {
                        SetBallState(E_BALL_STATE.STAY);
                    }
                    break;
                case E_HIT_STATE.NULL:
                    {

                    }
                    break;
            }
        }
    }

    // GameManager > BallManager > Ball 상속관계
    // GamaManager 의 싱글톤이 필요가 없다!
    protected override void UpdateThrowState()
    {
        if (GameManager.Instance.StrikeZone && GameManager.Instance.Catcher)
        {
            // PPT에 넣을 내용.1 많이쓰는 변수는 따로 처음부터 불러와서 변수에 지정해놓는게 메모리관리에 유리
            float StrikeZone_Scale = GameManager.Instance.StrikeZone.transform.localScale.x;
            float StrikeZone_Position_x = GameManager.Instance.StrikeZone.transform.position.x;
            float StrikeZone_ax = StrikeZone_Position_x - StrikeZone_Scale / 2;
            float StrikeZone_bx = StrikeZone_Position_x + StrikeZone_Scale / 2;

            float Catcher_Scale = GameManager.Instance.Catcher.transform.localScale.x;
            float Catcher_Position_x = GameManager.Instance.Catcher.transform.position.x;
            if (Catcher_Position_x + Catcher_Scale / 2 >= StrikeZone_ax && Catcher_Position_x - Catcher_Scale / 2 <= StrikeZone_bx)
            {
                m_eThorwState = E_THROW_STATE.STRIKE;
            }
            else
            {
                m_eThorwState = E_THROW_STATE.BALL;
            }
        }
    }

    private void Awake()
    {
        if (!Ball_Instance)
        {
            Ball_Instance = this;
        }
        else if (Ball_Instance != this)
        {
            Destroy(gameObject);
        }

        //BallList = new List<FourSeam_FastBall2>();
        //m_eBallState = E_BALL_STATE.THROW;
        SetBallState(E_BALL_STATE.STAY);
        SetHitState(E_HIT_STATE.HIT);
        PitcherPosition = this.gameObject.transform.position;
        InitialSet_Ball();
    }

    private void Start()
    {

    }

    private void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            m_gGravity.m_vCurrentForce += Vector3.up;
        }
        if (m_gGravity.m_bUse == true)
        {
            //if (BallList[1] != null)
            //{
            //    BallList[1].Move();
            //}

            //Debug.Log(BallList[0].m_sName);
            //Debug.Log(BallList[1].m_sName);

            Debug.Log(m_bCurrentBall.m_sName);
            m_bCurrentBall.Move();
            // 구종 테스트
            //BallList[0].Move();
            //BallList[1].Move();
            //BallList[2].Move();
            //BallList[3].Move();
        }

        UpdateBallState();
        UpdateHitState();
        UpdateThrowState();

        BallCheck();

        if (m_eBallState == E_BALL_STATE.THROW || m_eBallState == E_BALL_STATE.HIT)
        {
        }
        //Debug.Log("m_gGravity.m_vCurrentForce: " + m_gGravity.m_vCurrentForce);
    }

    //public virtual void Move() { }
    //public virtual Vector3 SetVector1() { return Vector3.zero; }
    //public virtual Vector3 SetVector2() { return Vector3.zero; }

    // Ball의 Vector3 - Gravity의 Vector3 연동
    void Link()
    {
        m_gGravity.m_vCurrentForce = m_vBallProgress;
        //Debug.Log(m_gGravity.m_vCurrentForce);
    }

    public void Ball_InitialLize()
    {
        this.transform.position = PitcherPosition;
        m_gGravity.Gravity_InitialLize();
        m_bBatPower = false;
        SetBallState(E_BALL_STATE.STAY);
        SetHitState(E_HIT_STATE.NULL);
        m_bBallProgressSection1 = false;
        m_bBallProgressSection2 = false;
        Set_Ball();
        m_bSwing = false;
        for (int i = 0; i < BallList.Count; i++)
        {
            BallList[i].Initialize();
        }
    }

    // 초기 구종 설정
    private void InitialSet_Ball()
    {
        BallList = new List<BallKind>();
        BallKind B_FB1 = new BallKind_FastBall1();
        BallList.Add(B_FB1);
        BallKind B_S1 = new BallKind_Slider1();
        BallList.Add(B_S1);
        BallKind B_C1 = new BallKind_Curve1();
        BallList.Add(B_C1);
        BallKind B_M1 = new BallKind_Magu1();
        BallList.Add(B_M1);
        //Debug.Log(BallList.Count);
        //for (int i = 0; i < BallList.Count; i++)
        //{
        //    Debug.Log(i + ".BallList: " + BallList[i].m_sName);
        //}

        Set_Ball();
    }

    // 구종 설정
    int nNumber1;
    public void Set_Ball()
    {
        nNumber1 = Random1();
        m_bCurrentBall = BallList[nNumber1];
    }

    // 구종 설정을 위한 난수 생성
    int rand;
    public int Random1()
    {
        rand = Random.Range(0, BallList.Count);
        return rand;
    }

    // nDir: 좌우방향(HittingPosition에따른), nVector: 위아래방향(스윙 방법에따른)
    Vector3 Set_Progress(string nString, Vector3 nDir, Vector3 nVector)
    {
        SetBallState(E_BALL_STATE.HIT);
        m_bBatPower = true;
        Vector3 m_vRVector = -m_gGravity.m_vCurrentForce + nDir + nVector;
        Debug.Log(nString);
        m_gGravity.m_bBounce = true;

        m_vBallProgress = m_vRVector;
        return m_vRVector;
    }

    // 공의 각종 충돌체크
    int nLayer;
    public void BallCheck()
    {
        nLayer = 1 << LayerMask.NameToLayer("BPS");
        Collider[] BPS_C = Physics.OverlapSphere(this.transform.position, 0.15f, nLayer);
        if (BPS_C != null)
        {
            //Debug.Log(BPS_C.Length);
            for (int i = 0; i < BPS_C.Length; i++)
            {
                Debug.Log(BPS_C[i].gameObject.name);
                if (BPS_C[i].gameObject.name == "BallProgressSection1" && m_bBallProgressSection1 == false)
                {
                    if (m_eBallState == E_BALL_STATE.THROW)
                        m_bBallProgressSection1 = true;
                }
                if (BPS_C[i].gameObject.name == "BallProgressSection2" && m_bBallProgressSection2 == false)
                {
                    if (m_eBallState == E_BALL_STATE.THROW)
                        m_bBallProgressSection2 = true;
                }
                if (BPS_C[i].gameObject.name == "Wall")
                {
                    SetBallState(E_BALL_STATE.STAY);
                }

                if (BPS_C[i].gameObject.name == "HittingPosition1")
                {
                    //Debug.Log("HittingPosition1통과");
                    if (m_eThorwState == E_THROW_STATE.STRIKE)
                        if (m_bBatPower == false)
                        {
                            Vector3 m_vLeftRight = new Vector3(-0.2f, -0.1f, 0);
                            if (Input.GetKey(KeyCode.A)) // UpperSet_Progress
                            {
                                Vector3 m_vUpDown = new Vector3(0, 0.2f, 0.5f);
                                Set_Progress("HittingPosition1_A", m_vLeftRight, m_vUpDown);
                            }
                            else if (Input.GetKey(KeyCode.S)) // MiddleSet_Progress
                            {
                                Vector3 m_vUpDown = new Vector3(0, 0.1f, -0.4f);
                                Set_Progress("HittingPosition1_S", m_vLeftRight, m_vUpDown);
                            }
                            else if (Input.GetKey(KeyCode.D)) // MiddleSet_Progress
                            {
                                Vector3 m_vUpDown = new Vector3(0, 0.3f, -0.05f);
                                Set_Progress("HittingPosition1_D", m_vLeftRight, m_vUpDown);
                            }
                        }
                    if (m_eThorwState == E_THROW_STATE.BALL)
                    {
                        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
                            m_bSwing = true;
                    }
                }
                if (BPS_C[i].gameObject.name == "HittingPosition2")
                {
                    //Debug.Log("HittingPosition2통과");
                    if (m_eThorwState == E_THROW_STATE.STRIKE)
                        if (m_bBatPower == false)
                        {
                            Vector3 m_vLeftRight = new Vector3(-0.1f, 0, 0);
                            if (Input.GetKey(KeyCode.A)) // UpperSet_Progress
                            {
                                //Vector3 m_vUpDown = new Vector3(0, 0.3f, 0.3f);
                                Vector3 m_vUpDown = new Vector3(-0.1f, 0.3f, -0.8f);
                                Set_Progress("HittingPosition2_A", m_vLeftRight, m_vUpDown);
                            }
                            else if (Input.GetKey(KeyCode.S)) // MiddleSwing
                            {
                                Vector3 m_vUpDown = new Vector3(-0.1f, 0.3f, -0.8f);
                                Set_Progress("HittingPosition2_S", m_vLeftRight, m_vUpDown);
                            }
                            else if (Input.GetKey(KeyCode.D)) // MiddleSwing
                            {
                                //Vector3 m_vUpDown = new Vector3(-0.1f, 0.6f, -0.3f);
                                Vector3 m_vUpDown = new Vector3(-0.1f, 0.3f, -0.8f);
                                Set_Progress("HittingPosition2_D", m_vLeftRight, m_vUpDown);
                            }
                        }
                    if (m_eThorwState == E_THROW_STATE.BALL)
                    {
                        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
                            m_bSwing = true;
                    }
                }
                if (BPS_C[i].gameObject.name == "HittingPosition3")
                {
                    //Debug.Log("HittingPosition3통과");
                    if (m_eThorwState == E_THROW_STATE.STRIKE)
                        if (m_bBatPower == false)
                        {
                            Vector3 m_vLeftRight = new Vector3(0.3f, 0, 0);
                            if (Input.GetKey(KeyCode.A)) // UpperSwing
                            {
                                Vector3 m_vUpDown = new Vector3(-0.2f, 1f, -0.05f);
                                Set_Progress("HittingPosition3_A", m_vLeftRight, m_vUpDown);
                            }
                            else if (Input.GetKey(KeyCode.S)) // MiddleSwing
                            {
                                Vector3 m_vUpDown = new Vector3(0f, 0.7f, 0.1f);
                                Set_Progress("HittingPosition3_S", m_vLeftRight, m_vUpDown);
                            }
                            else if (Input.GetKey(KeyCode.D)) // MiddleSwing
                            {
                                Vector3 m_vUpDown = new Vector3(-0.1f, 0.5f, -0.1f);
                                Set_Progress("HittingPosition3_D", m_vLeftRight, m_vUpDown);
                            }
                        }
                    if (m_eThorwState == E_THROW_STATE.BALL)
                    {
                        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
                            m_bSwing = true;
                    }
                }
                if (BPS_C[i].gameObject.name == "HittingPosition4")
                {
                    //Debug.Log("HittingPosition4통과");
                    if (m_eThorwState == E_THROW_STATE.STRIKE)
                        if (m_bBatPower == false)
                        {
                            Vector3 m_vLeftRight = new Vector3(0.2f, 0, 0);
                            if (Input.GetKey(KeyCode.A)) // UpperSwing
                            {
                                Vector3 m_vUpDown = new Vector3(0, 0.3f, 0.4f);
                                Set_Progress("HittingPosition4_A", m_vLeftRight, m_vUpDown);
                            }
                            else if (Input.GetKey(KeyCode.S)) // MiddleSwing
                            {
                                Vector3 m_vUpDown = new Vector3(0.5f, 0.1f, 0.1f);
                                Set_Progress("HittingPosition4_S", m_vLeftRight, m_vUpDown);
                            }
                            else if (Input.GetKey(KeyCode.D)) // MiddleSwing
                            {
                                Vector3 m_vUpDown = new Vector3(0.1f, 0.3f, -0.1f);
                                Set_Progress("HittingPosition4_D", m_vLeftRight, m_vUpDown);
                            }
                        }
                    if (m_eThorwState == E_THROW_STATE.BALL)
                    {
                        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
                            m_bSwing = true;
                    }
                }

                if (BPS_C[i].gameObject.tag == "Deffenser") // 수비수가 공을 잡을때 Out, Hit
                {
                    if (m_eBallState == E_BALL_STATE.HIT)
                    if ( m_eHitState == E_HIT_STATE.NULL)
                    {
                        Debug.Log("플라이 아웃");
                        SetHitState(E_HIT_STATE.OUT);
                        //m_eBallState = E_BALL_STATE.CATCH;
                        GameManager.Instance.m_nOut++;
                        GameManager.Instance.ResetBallStrikeCount();
                        Debug.Log(GameManager.Instance.m_nOut);
                    }
                }

                if (BPS_C[i].gameObject.name == "Catcher") // 포수가 공을 잡을때 Strike, Ball
                {
                    Debug.Log("Ball in Catcher");
                    if(m_eBallState == E_BALL_STATE.THROW)
                    {
                        if (m_eThorwState == E_THROW_STATE.STRIKE)
                            GameManager.Instance.m_nStrike++;
                        else if (m_eThorwState == E_THROW_STATE.BALL)
                        {
                            if (m_bSwing == false)
                                GameManager.Instance.m_nBall++;
                            else
                                GameManager.Instance.m_nStrike++;
                        }
                    }
                    SetBallState(E_BALL_STATE.CATCH);
                }

                if (BPS_C[i].gameObject.name == "FoulArea")
                {
                    if (m_eBallState == E_BALL_STATE.HIT)
                    if (m_eHitState == E_HIT_STATE.NULL)
                    {
                        //m_eHitState = E_HIT_STATE.FOUL;
                        SetHitState(E_HIT_STATE.FOUL);
                        if (GameManager.Instance.m_nStrike < 2) GameManager.Instance.m_nStrike++;
                        Debug.Log("파울");
                    }
                }
                if (BPS_C[i].gameObject.name == "HomeRunArea")
                {
                    if (m_eBallState == E_BALL_STATE.HIT)
                    if (m_eHitState == E_HIT_STATE.NULL)
                    {
                        //m_eHitState = E_HIT_STATE.HOMERUN;
                        SetHitState(E_HIT_STATE.HOMERUN);
                        GameManager.Instance.BaseRun_HomeRun();
                        Debug.Log("홈런");
                    }
                }
                if (BPS_C[i].gameObject.name == "Ground")
                {
                    if (m_eBallState == E_BALL_STATE.HIT)
                    if (m_eHitState == E_HIT_STATE.NULL)
                    {
                        //m_eBallState = E_BALL_STATE.HIT;
                        //m_eHitState = E_HIT_STATE.HIT;
                        SetHitState(E_HIT_STATE.HIT);
                        GameManager.Instance.BaseRun();
                        Debug.Log("안타");
                        GameManager.Instance.ResetBallStrikeCount();
                    }
                }
            }
        }
    }
}
