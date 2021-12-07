using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bat : MonoBehaviour
{
    public GameObject Controller;

    public GameObject m_gBattingPosition_1;
    public GameObject m_gBattingPosition_2;
    public GameObject m_gBattingPosition_3;
    public GameObject m_gBattingPosition_4;
    public Vector3 m_vBattingPosition_1;
    public Vector3 m_vBattingPosition_2;
    public Vector3 m_vBattingPosition_3;
    public Vector3 m_vBattingPosition_4;

    public GameObject m_gBall;
    // Start is called before the first frame update
    void Start()
    {
        m_vBattingPosition_1 = m_gBattingPosition_1.transform.position;
        m_vBattingPosition_2 = m_gBattingPosition_2.transform.position;
        m_vBattingPosition_3 = m_gBattingPosition_3.transform.position;
        m_vBattingPosition_4 = m_gBattingPosition_4.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(Vector3.Normalize(m_gBattingPosition_1.transform.position - Controller.transform.position));
    }

    private void OnDrawGizmos()
    {
        //Gizmos.color = Color.red;
        //Gizmos.DrawLine(m_gBall.gameObject.transform.position, m_vBattingPosition_1);
        //Gizmos.DrawLine(m_gBall.gameObject.transform.position, m_vBattingPosition_2);
        //Gizmos.DrawLine(m_gBall.gameObject.transform.position, m_vBattingPosition_3);
        //Gizmos.DrawLine(m_gBall.gameObject.transform.position, m_vBattingPosition_4);

        //Gizmos.color = Color.red;
        //Gizmos.DrawLine(Controller.transform.position, m_gBattingPosition_1.transform.position);
    }
}
