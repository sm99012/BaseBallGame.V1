using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Move : MonoBehaviour
{
    public GameObject Bat;
    public Vector3 m_vInitial_Bat_Position;
    public GameObject Ball;

    public float m_fSpeed;

    public Quaternion m_qRotation_Start;

    public int m_nFrame;
    public int m_nFrame_Current;

    public bool m_bBat = true;
    public bool m_bASwing = false;
    public bool m_bSSwing = false;
    public bool m_bDSwing = false;

    // Start is called before the first frame update
    void Start()
    {
        m_qRotation_Start = this.transform.rotation;
        m_vInitial_Bat_Position = Bat.transform.position;
    }

    // Update is called once per frame
    private void Update()
    {
        Controller_Batting();
        Swing();
        //Debug.Log("N: " + n);
    }

    IEnumerator ProcessStopBat(float nTime)
    {
        yield return new WaitForSeconds(nTime);
        m_nFrame_Current = 0;
        n = 0;
        An = 0;
        m_bASwing = false;
        this.transform.rotation = m_qRotation_Start;
        m_bBat = true;
        Bat.transform.position = m_vInitial_Bat_Position;
    }
    void Bat_Initialize(float nTime)
    {
        n++;
        if (m_nFrame_Current >= m_nFrame)
        {
            StartCoroutine(ProcessStopBat(nTime)); // 경직
        }
    }

    void Controller_Batting()
    {
        if (m_bBat == true)
        {
            if (Input.GetKey(KeyCode.D))
            {
                transform.Rotate(Vector3.up * 3);
            }
            else if (Input.GetKeyDown(KeyCode.A)) // 어퍼스윙
            {
                m_bASwing = true;
                m_bBat = false;
            }
            else if (Input.GetKeyDown(KeyCode.S)) // 스윙
            {
                m_bSSwing = true;
                m_bBat = false;
            }
        }
    }
    void Swing()
    {
        if (m_bBat == false)
        {
            if (m_bASwing == true)
            {
                // 어퍼스윙일때 방망이시작점
                if (An == 0)
                {
                    Bat.transform.position += new Vector3(0, -0.5f, 0);
                }

                if (m_nFrame_Current <= m_nFrame)
                {
                    m_nFrame_Current += 1;
                    transform.Rotate(Vector3.down * 7.5f);
                    Bat.transform.position += Vector3.up * 0.06f;
                    //Debug.Log("Process_Hit: " + this.transform.rotation.y);
                }
                else
                {
                    if (n == 0)
                        Bat_Initialize(1);
                }
                An++;
            }

            else if (m_bSSwing == true)
            {
                if (m_nFrame_Current <= m_nFrame)
                {
                    m_nFrame_Current += 1;
                    transform.Rotate(Vector3.down * 7.5f);
                    Bat.transform.position += Vector3.up * 0.025f;
                    //Debug.Log("Process_Hit: " + this.transform.rotation.y);
                }
                else
                {
                    if (n == 0)
                        Bat_Initialize(1);
                }
            }
        }
    }
    int n = 0;
    int An = 0;
}
