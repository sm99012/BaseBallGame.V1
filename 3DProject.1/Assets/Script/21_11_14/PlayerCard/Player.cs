using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private string m_sName;
    private int m_nCode;
    private Status m_psPlayerStatus;

    public Player(string name, int code, Status ps)
    {
        this.m_sName = name;
        this.m_nCode = code;
        this.m_psPlayerStatus = ps;
    }

    public string GetName()
    {
        return m_sName;
    }
    public int GetPower()
    {
        return m_psPlayerStatus.GetPower();
    }
    public int GetHP()
    {
        return m_psPlayerStatus.GetHP();
    }
    public int GetValue()
    {
        return m_psPlayerStatus.GetValue();
    }
}

