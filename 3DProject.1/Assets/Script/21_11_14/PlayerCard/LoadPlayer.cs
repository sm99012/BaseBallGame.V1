using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

public class LoadPlayer : MonoBehaviour
{
    public static Dictionary<int, Player> PlayerList;
    private void Awake()
    {
        PlayerList = new Dictionary<int, Player>();
        Load();
    }

    private void FixedUpdate()
    {
        // 파일로드 테스트
        //if (Input.GetKeyDown(KeyCode.L))
        //{
        //    Debug.Log(PlayerList["배종원"].GetName());
        //    Debug.Log(PlayerList["배종투"].GetName());
        //    Debug.Log(PlayerList["배종삼"].GetName());
        //}
    }

    private void Load()
    {
        string FileName = "Player.csv";
        StreamReader sr = new StreamReader(FileName);
        string line;
        string[] temp;
        while (!sr.EndOfStream)
        {
            line = sr.ReadLine();
            temp = line.Split(',');
            Status status = new Status();
            status.SetStatus(Int32.Parse(temp[2]), Int32.Parse(temp[3]), Int32.Parse(temp[4]));
            Player player = new Player(temp[0], Int32.Parse(temp[1]), status);
            PlayerList.Add(Int32.Parse(temp[1]), player);
        }
        sr.Close();
    }
}
public struct Status
{
    // 파워
    private int m_nPower;
    // 체력
    private int m_nHP;
    // 가치
    private int m_nValue;

    public void SetStatus(int power, int hp, int value)
    {
        this.m_nPower = power;
        this.m_nHP = hp;
        this.m_nValue = value;
    }

    public int GetPower()
    {
        return m_nPower;
    }
    public int GetHP()
    {
        return m_nHP;
    }
    public int GetValue()
    {
        return m_nValue;
    }
}