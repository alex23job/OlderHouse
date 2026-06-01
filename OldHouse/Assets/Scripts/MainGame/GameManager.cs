using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Runtime.Serialization.Formatters.Binary;

public class GameManager : MonoBehaviour
{
    public PlayerInfo currentPlayer = new PlayerInfo();
    [SerializeField] private MainGameUI mm_control;

    public static GameManager Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            Invoke("LoadGame", 0.08f);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

public class PlayerInfo
{
}

[Serializable]
public class SaveData
{
    public int score;   //  exp
    public int gold;
    public int level = 1;
    public string scene = "";
    public string posAndRot = "";
    public string csvGarage = "";
    public int currentBonusDay = 0;
    public string bonusTime = "";
    public string csvInventory = "";
    public string csvQuest = "";

    public bool isFone;
    public bool isEffects;
    public bool isHints;
    public int volFone;
    public int volEffects;
    public override string ToString()
    {
        return "SaveData: score=" + score + " gold=" + gold + " level=" + level;
    }
}

