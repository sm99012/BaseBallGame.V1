﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Four_Seam_FastBall : Move_Ball
{
    public override void CreateTest()
    {
        Debug.Log("Create Test Instance Four_Seam_FastBall");
    }

    public override Vector3 SetVector1()
    {
        if (Catcher)
        {
            m_vInitialProgressDir = Vector3.Normalize(Catcher.transform.position - this.transform.position);
            m_vBallProgress1 = (m_vInitialProgressDir);
            //m_vBallProgress1 = (m_vInitialProgressDir + new Vector3(1f, 0, 0));
        }
        return m_vBallProgress1;
    }

    public override Vector3 SetVector2()
    {
        m_vBallProgress2 = Vector3.Normalize(Catcher.transform.position - this.transform.position);
        return m_vBallProgress2;
    }

    public override void Move()
    {
        if (!m_bHit)
        {
            if (m_bPitch == false)
            {
                //m_gGravity3.m_vCurrentForce = new Vector3(0, 0.01f, 0.01f);
                m_gGravity3.m_vCurrentForce = (m_vInitialProgressDir + new Vector3(0, 0.07f, 0)) * 0.5f;
                m_bPitch = true;
            }
            if (m_bBallProgressSection1 == true)
            {
                //m_gGravity3.m_vCurrentForce += new Vector3(-0.015f, -0.01f, -0.01f);
                m_gGravity3.m_vCurrentForce = m_vBallProgress2 * 0.5f;
            }
        }
        else
        {
            if (i == 0)
            {
                //m_vBallProgress3 = new Vector3(m_vBallProgress3.x, 0.5f, m_vBallProgress3.z);
                m_gGravity3.m_vCurrentForce = m_vBallProgress3; // 배팅되었을때의 공의힘
                //m_gGravity3.m_vCurrentForce = new Vector3(0, 0.8f, -.5f);
                //Debug.Log("타격!!@ + " + m_vBallProgress3);
            }
            i++;
        }
    }
}
