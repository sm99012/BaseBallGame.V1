using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallKind_Curve1 : BallKind
{
    public BallKind_Curve1()
    {
        m_sName = "Curve1";
    }

    public override void Move()
    {
        SetVector1();
        SetVector2();
        if (Ball.BInstance.m_eBallState == BallManager.E_BALL_STATE.THROW)
        {
            if (Move_Pitch == false)
            {
                Move_Pitch = true;
                Ball.BInstance.m_gGravity.m_vCurrentForce = m_vBallProgress1 * 0.5f;
                Ball.BInstance.m_gGravity.m_vCurrentForce += new Vector3(-0.03f, 0.1f, -0.1f);
                Debug.Log("m_vBallProgress1: " + m_vBallProgress1);
            }
            if (Ball.BInstance.m_bBallProgressSection1 == true && m_bBPS1 == false)
            {
                m_bBPS1 = true;
                Ball.BInstance.m_gGravity.m_vCurrentForce += new Vector3(0.02f, -0.02f, -0.1f);
            }
            if (Ball.BInstance.m_bBallProgressSection2 == true && m_bBPS2 == false)
            {
                m_bBPS2 = true;
                Ball.BInstance.m_gGravity.m_vCurrentForce = m_vBallProgress2 * 0.5f;
                Ball.BInstance.m_gGravity.m_vCurrentForce += new Vector3(0, -0.05f, 0);
            }
        }
        else
        {
            if (Move_Hit == false)
            {
                Move_Hit = true;
                Ball.BInstance.m_gGravity.m_vCurrentForce = Ball.BInstance.m_vBallProgress; // 배팅되었을때의 공의힘
            }
        }
    }
}
