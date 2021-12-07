using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallManager : MonoBehaviour
{
    protected bool m_bSet_Ball = false; // 구종 설정(랜덤)

    public bool m_bBallProgressSection1 = false; // 공의 움직임을 위한 변수.
    public bool m_bBallProgressSection2 = false;

    protected List<BallKind> BallList = null; // 구종 리스트

    // public -> protected
    public enum E_BALL_STATE { STAY, THROW, HIT, CATCH } // 공의상태 fsm
    public E_BALL_STATE m_eBallState;
    public void SetBallState(E_BALL_STATE ebs)
    {
        switch (ebs)
        {
            case E_BALL_STATE.STAY:
                {

                }
                break;
            case E_BALL_STATE.THROW:
                {

                }
                break;
            case E_BALL_STATE.HIT:
                {

                }
                break;
            case E_BALL_STATE.CATCH:
                {

                }
                break;
        }
        m_eBallState = ebs;
    }
    protected virtual void UpdateBallState() { }

    public enum E_HIT_STATE { FOUL, HIT, HOMERUN, OUT , NULL } // 타격시 공의상태 fsm
    public E_HIT_STATE m_eHitState;
    public void SetHitState(E_HIT_STATE ehs)
    {
        if (m_eBallState == E_BALL_STATE.HIT)
        {
            switch (ehs)
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
                        //Debug.Log("OUT!");
                    }
                    break;
                case E_HIT_STATE.NULL:
                    {

                    }
                    break;
            }
            m_eHitState = ehs;
        }
    }
    protected virtual void UpdateHitState() { }

    public enum E_THROW_STATE { BALL, STRIKE } // 투구시 스트라이크, 볼 판단 fsm
    public E_THROW_STATE m_eThorwState;
    public void SetThrowState(E_THROW_STATE ets)
    {
        if (m_eBallState == E_BALL_STATE.THROW)
        {
            switch (ets)
            {
                case E_THROW_STATE.BALL:
                    {
                        //Debug.Log("BALL!");
                    }
                    break;
                case E_THROW_STATE.STRIKE:
                    {
                        //Debug.Log("STRIKE!");
                    }
                    break;
            }
            m_eThorwState = ets;
        }
    }
    protected virtual void UpdateThrowState() { }

    protected bool m_bBatPower = false; // 타격시 힘을 한번만 받게하기위한 불 변수
    protected bool m_bSwing = false; // 타자의 스윙 여부를 판단하는 변수 - 스트라이크, 볼 판정에 사용
}
