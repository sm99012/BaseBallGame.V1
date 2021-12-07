using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class GUI_PlayerHitter : MonoBehaviour
    , IPointerEnterHandler
    , IPointerExitHandler
{
    public GameObject m_gDetailDescription;
    private Vector2 m_vMousePosition;

    public Text m_tPower;
    public Text m_tHP;
    public Text m_tValue;

    public void FixedUpdate()
    {
        m_vMousePosition = Input.mousePosition;
        m_gDetailDescription.transform.position = new Vector2(m_vMousePosition.x - 5, m_vMousePosition.y - 5);
        SetDetailDescription();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        //Debug.Log(name + ": Enter");
        if (m_gDetailDescription)
            m_gDetailDescription.SetActive(true);
        //Debug.Log(m_gDetailDescription.gameObject.name);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Debug.Log(name + "Exit");
        if (m_gDetailDescription)
            m_gDetailDescription.SetActive(false);
    }

    public void SetDetailDescription()
    {
        m_tPower.text = "파워: " + MouseClick.MCInstance.m_pPlayer.GetPower();
        m_tHP.text = "체력: " + MouseClick.MCInstance.m_pPlayer.GetHP();
        m_tValue.text = "가치: " + MouseClick.MCInstance.m_pPlayer.GetValue();
    }
}
