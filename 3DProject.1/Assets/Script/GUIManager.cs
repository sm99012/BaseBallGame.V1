using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GUIManager : MonoBehaviour
{
    public GameManager m_gGameManager;
    public Ball Ball;

    public Image Strike1;
    public Image Strike2;
    public Image Strike3;

    public Image Ball1;
    public Image Ball2;
    public Image Ball3;
    public Image Ball4;

    public Image Out1;
    public Image Out2;
    public Image Out3;

    public Image Base1;
    public Image Base2;
    public Image Base3;

    public Text Count1;

    public GameObject Expression1_Panel;
    public Text Expression1_Text;

    void Start()
    {
        Expression1_Set("X", Color.blue);
    }

    
    void Update()
    {
        Expression1_Condition();
        SetBallCount();
    }

    public string Expression1_Set(string nstring, Color ncolor)
    {
        Expression1_Text.text = nstring;
        Expression1_Text.color = ncolor;

        return nstring;
    }

    void SetBallCount()
    {
        if (m_gGameManager.m_nStrike == 0)
        {
            Strike1.color = Color.white;
            Strike2.color = Color.white;
            Strike3.color = Color.white;
        }
        else if (m_gGameManager.m_nStrike == 1)
        {
            Strike1.color = Color.red;
        }
        else if (m_gGameManager.m_nStrike == 2)
        {
            Strike2.color = Color.red;
        }
        else if (m_gGameManager.m_nStrike == 3)
        {
            Strike3.color = Color.red;
        }

        if (m_gGameManager.m_nBall == 0)
        {
            Ball1.color = Color.white;
            Ball2.color = Color.white;
            Ball3.color = Color.white;
            Ball4.color = Color.white;
        }
        else if (m_gGameManager.m_nBall == 1)
        {
            Ball1.color = Color.green;
        }
        else if (m_gGameManager.m_nBall == 2)
        {
            Ball2.color = Color.green;
        }
        else if (m_gGameManager.m_nBall == 3)
        {
            Ball3.color = Color.green;
        }
        else if (m_gGameManager.m_nBall == 4)
        {
            Ball4.color = Color.green;
        }

        if (m_gGameManager.m_nOut == 1)
        {
            Out1.color = Color.red;
        }
        else if (m_gGameManager.m_nOut == 2)
        {
            Out2.color = Color.red;
        }
        else if (m_gGameManager.m_nOut == 3)
        {
            Out3.color = Color.red;
        }

        if (m_gGameManager.m_bBase_1 == true)
        {
            Base1.color = Color.yellow;
        }
        else Base1.color = Color.white;
        if (m_gGameManager.m_bBase_2 == true)
        {
            Base2.color = Color.yellow;
        }
        else Base2.color = Color.white;
        if (m_gGameManager.m_bBase_3 == true)
        {
            Base3.color = Color.yellow;
        }
        else Base3.color = Color.white;

        Count1.text = m_gGameManager.m_nCount.ToString();
    }

    void Expression1_Condition()
    {
        if (Ball.m_eHitState != BallManager.E_HIT_STATE.NULL)
        {
            if (Ball.m_eHitState == BallManager.E_HIT_STATE.OUT) { Expression1_Set("아웃", Color.red); }
            if (Ball.m_eHitState == BallManager.E_HIT_STATE.HIT) { Expression1_Set("안타", Color.blue); }
            if (Ball.m_eHitState == BallManager.E_HIT_STATE.FOUL) { Expression1_Set("파울", Color.red); }
            if (Ball.m_eHitState == BallManager.E_HIT_STATE.HOMERUN) { Expression1_Set("홈런", Color.blue); ; }
            //if (Ball.m_bOut == false && Ball.m_bAnta == false && Ball.m_bHomeRun == false && Ball.m_bFoul == false) { Expression1_Set("X", Color.blue); }
        }
        if (Expression1_Text.text == "X")
        {
            Expression1_Panel.gameObject.SetActive(false);
            //Debug.Log("SCREEN_NULL");
        }
        else
        {
            Expression1_Panel.gameObject.SetActive(true);
            //Debug.Log("SCREEN_NON_NULL");
        }
        //Debug.Log(Expression1_Text.text);
    }
}
