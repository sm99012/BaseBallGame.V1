using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MouseClick : MonoBehaviour
{
    private static MouseClick MC_Instance = null;
    public static MouseClick MCInstance
    {
        get
        {
            if (!MC_Instance)
            {
                MC_Instance = FindObjectOfType(typeof(MouseClick)) as MouseClick;
            }
            return MC_Instance;
        }
    }

    public Text m_tName;
    public Image m_iPower;
    public Image m_iPower_Current;
    RectTransform m_rPower;

    public Image m_iHP;
    public Image m_iHP_Current;
    RectTransform m_rHP;

    public Player m_pPlayer;

    public void Player_Change()
    {
        if (i < LoadPlayer.PlayerList.Count - 1)
        {
            i++;
        }
        else 
            i = 0;
        SetPlayer();
    }

    Vector2 m_vPowerSize;
    float m_fPower_CurrentSize;
    Vector2 m_vHPSize;
    float m_fHP_CurrentSize;
    private void Awake()
    {
        if (!MC_Instance)
        {
            MC_Instance = this;
        }
        else if (MC_Instance != this)
        {
            Destroy(gameObject);
        }

        m_vPowerSize = m_iPower.GetComponent<RectTransform>().sizeDelta;
        m_rPower = m_iPower_Current.GetComponent<RectTransform>();
        m_fPower_CurrentSize = 0;

        m_vHPSize = m_iHP.GetComponent<RectTransform>().sizeDelta;
        m_rHP = m_iHP_Current.GetComponent<RectTransform>();
        m_fHP_CurrentSize = 0;

        SetPlayer();
    }

    int i = 0;
    public void SetPlayer()
    {
        m_pPlayer = LoadPlayer.PlayerList[i];

        m_tName.text = m_pPlayer.GetName();
        Vector2 rp = new Vector2(m_pPlayer.GetPower(), 30);
        m_rPower.sizeDelta = rp;
        Vector2 rh = new Vector2(m_pPlayer.GetHP(), 30);
        m_rHP.sizeDelta = rh;
    }
}
