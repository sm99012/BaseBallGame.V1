using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gravity2 : MonoBehaviour
{
    public Vector3 m_vGravity = Vector3.down;
    public Vector3 m_vVelocity = Vector3.zero;
    public float m_fGravity = 9.8f;
    public bool m_bGround = false;

    void Start()
    {
        
    }

    private void FixedUpdate()
    {
        Vector3 vPos = transform.position + new Vector3(0, 0, 0);
        Vector3 vGravity = m_vGravity.normalized;
        Ray ray = new Ray(vPos, m_vGravity.normalized);
        Debug.DrawLine(vPos, vPos + vGravity, Color.red);

        RaycastHit racastHit;
        if (Physics.Raycast(ray, out racastHit, m_fGravity, 1 << LayerMask.NameToLayer("Ground")))
        {
            //if (racastHit.transform.gameObject.name != gameObject.name) 
            //{
            //    m_bGround = false;
            //}
            //else
            //{
            //    m_bGround = true;
            //}
            m_bGround = false;
        }
        else
        {
            m_bGround = true;
        }
    }

    void Update()
    {
        m_vVelocity = m_vVelocity * Time.deltaTime;
        if (m_bGround == false)
            m_vVelocity += m_vGravity * m_fGravity * Time.deltaTime;
        transform.position += m_vVelocity;
    }
}
