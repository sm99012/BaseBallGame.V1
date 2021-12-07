using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallKind : MonoBehaviour
{
    public string m_sName = "null";

    // 구종별 옵션
    protected bool Move_Pitch = false;
    protected bool Move_Hit = false;

    protected Vector3 m_vBallProgress1;
    protected Vector3 m_vBallProgress2;
    protected Vector3 m_vBallProgress3;

    protected bool m_bBPS1 = false;
    protected bool m_bBPS2 = false;

    // BallProgress1 을 지날때 공과 포수의 방향
    public Vector3 SetVector1()
    {
        if (GameManager.Instance.Catcher)
        {
            m_vBallProgress1 = Vector3.Normalize(GameManager.Instance.Catcher.transform.position - Ball.BInstance.transform.position);
            //m_vBallProgress1 = Vector3.Normalize(GameManager.GM_Instance.Catcher.transform.position - m_bBall.transform.position);
            //m_vBallProgress1 = (m_vInitialProgressDir + new Vector3(1f, 0, 0));
        }
        
        return m_vBallProgress1;
    }
    // BallProgress2 을 지날때 공과 포수의 방향
    public Vector3 SetVector2()
    {
        m_vBallProgress2 = Vector3.Normalize(GameManager.Instance.Catcher.transform.position - Ball.BInstance.transform.position);
        return m_vBallProgress2;
    }

    public virtual void Move()
    {

    }

    public void Initialize()
    {
        Move_Pitch = false;
        Move_Hit = false;
        m_bBPS1 = false;
        m_bBPS2 = false;
    }
}
