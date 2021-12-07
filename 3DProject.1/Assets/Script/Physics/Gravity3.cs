using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gravity3 : MonoBehaviour
{
    public float m_fGravity = 9.8f;

    public Vector3 nDir = new Vector3(0, -1f, 0);
    public Vector3 m_vGravityForce;
    public Vector3 m_vCurrentForce;
    public Vector3 m_vReverseForce; // 바닥에닿고 튕길때

    public bool m_bUse = true;
    public bool m_bGround = false;
    public bool m_bGravity = true;
    public bool m_bBounce = false;

    public bool m_bUp = false;

    public float m_fHigh;
    public float m_fHigh_Current;

    public int m_nFrame_Down;
    public int m_nFrame_Up;
    public int m_nFrame_BounceLimit;

    public int m_nFrictionalForce;
    public int m_nBounceCount;
    public int m_nBounceCount_Current;
    void Start()
    {
        m_bUse = false;
        //m_vGravityForce = m_fGravity * nDir;
        m_fHigh = this.transform.position.y;
        m_fHigh_Current = m_fHigh;
        m_nBounceCount_Current = m_nBounceCount;
    }

    void DecisionGround()
    {
        if (this.transform.position.y > 0.15f)
        {
            m_bGround = false;
        }
        else
        {
            if (m_bGround == false)
            {
                m_vReverseForce = m_vCurrentForce / m_nFrictionalForce; //땅의 마찰계수; // 반발력(탄성력)
                m_nBounceCount_Current--;
                m_bBounce = false;
                if (m_nBounceCount_Current > 0 && m_bBounce == false)
                {
                    m_bBounce = true;
                    m_vCurrentForce = new Vector3(m_vReverseForce.x, -m_vReverseForce.y, m_vReverseForce.z);
                }
            }
            m_bGround = true;
        }
    }

    void CurrentGraivity()
    {
        if (m_bUp == true)
        {
            m_bUp = false;
            this.transform.position += m_vCurrentForce;
        }
        else
        {
            if (m_bGround == false)
            {
                m_vGravityForce += m_fGravity * nDir * 0.000015f;
                m_vCurrentForce += m_vGravityForce;
                this.transform.position += m_vCurrentForce;
                //Debug.Log("공중체공");
                if (m_nBounceCount_Current == 0)
                {
                    m_nBounceCount_Current = m_nBounceCount;
                }
            }
            else
            {
                m_vGravityForce = m_fGravity * nDir * 0.00001f;
                if (m_bBounce == false)
                {
                    m_vCurrentForce = Vector3.zero; // = m_vGravityForce;
                    this.transform.position = new Vector3(this.transform.position.x, 0.15f, this.transform.position.z);
                }
                this.transform.position += m_vCurrentForce;
                //Debug.Log("땅");
            }
        }
    }

    public void Gravity_InitialLize()
    {
        m_bUse = false;
        m_vCurrentForce = Vector3.zero;
        m_vGravityForce = Vector3.zero;
        m_bGround = false;
        m_bBounce = false;
    }

    private void FixedUpdate()
    {
        if (m_bUse == true)
        {
            DecisionGround(); // 높이에따른 공의 위치.
            CurrentGraivity(); // 공의 위치에따른 공에 가해지는 힘(Vectir3).
        }
        else
        {
            m_vCurrentForce = Vector3.zero;
        }
    }
}
