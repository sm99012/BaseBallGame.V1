using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public Player m_pPlayer;
    public Ball m_bBall;

    private void FixedUpdate()
    {
        if (m_pPlayer)
        {

        }
    }
    private void Update()
    {
        Control();
    }

    void Control()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            Debug.Log("Start!");
            m_bBall.SetBallState(BallManager.E_BALL_STATE.THROW);
            m_bBall.SetHitState(Ball.E_HIT_STATE.NULL);
            m_bBall.m_gGravity.m_bUse = true;
        }
        if (Input.GetKeyDown(KeyCode.I))
        {
            GameManager.Instance.InitialLize();
            //m_bBall.Set_Ball();
        }
        //if (Input.GetKeyDown(KeyCode.U))
        //{
        //    m_bBall.m_gGravity.m_vCurrentForce += Vector3.up;
        //    Debug.Log("static_m_gGravity.m_vCurrentForce: " + m_bBall.m_gGravity.m_vCurrentForce);
        //}
    }
}
