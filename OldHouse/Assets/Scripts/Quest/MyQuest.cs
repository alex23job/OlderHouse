using Assets.Scripts.Level;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public enum QuestFaza
{
    NotAvailable,
    Available,
    Processing,
    Completed
}

public class MyQuest
{
    private int _id;
    private string _name;
    private string _description;
    private QuestFaza _faza;
    private int[] _arrIdObjects;
    private GameObject[] _questObjects;
    private int _questObjectsFaza = 0;

    //private MyQuest _myQuest = this;

    public int Id { get => _id; }
    public string Name { get => _name; }
    public string Description { get => _description; }
    public QuestFaza Faza { get => _faza; set => _faza = value; }

    public MyQuest() { }
    public MyQuest(int id, string name, string description, QuestFaza faza, int[] arrIdObjects)
    {
        _id = id;
        _name = name;
        _description = description;
        _faza = faza;
        _arrIdObjects = arrIdObjects;
    }

    public void LoadProcessing(string csv, char sep = '=')
    {
        string[] ar = csv.Split(sep, StringSplitOptions.RemoveEmptyEntries);
        if (ar.Length >= 2)
        {
            if (int.TryParse(ar[0], out int cid))
            {
                if (_id != cid) return;
            }
            if (Enum.TryParse(ar[1], out QuestFaza result))
            {
                _faza = result;
                //UpdateFaza(result);
            }
            if (ar.Length >= 3)
            {
                if (int.TryParse(ar[2], out int qaf))
                {
                    _questObjectsFaza = qaf;
                }
            }
        }
    }

    public string SaveProcessingToCsv(char sep = '=')
    {
        int i, zn, tmp = 0;
        for (i = 0; i < _questObjects.Length && i < 15; i++)
        {
            QuestObject qo = _questObjects[i].GetComponent<QuestObject>();
            if (qo != null)
            {
                zn = 0;
                switch (qo.Faza)
                {
                    case QuestFaza.NotAvailable: zn = 0; break;
                    case QuestFaza.Available: zn = 1; break;
                    case QuestFaza.Processing: zn = 2; break;
                    case QuestFaza.Completed: zn = 3; break;
                }
                tmp |= zn << (2 * i);
            }
        }
        _questObjectsFaza = tmp;
        StringBuilder sb = new StringBuilder($"{_id}{sep}{_faza}{sep}{_questObjectsFaza}{sep}");

        return sb.ToString();
    }

    public void SetQuestObjects(SetQuestObjects questObjects)
    {
        _questObjects = new GameObject[_arrIdObjects.Length];
        for(int i = 0; i < _arrIdObjects.Length; i++)
        {
            GameObject obj = questObjects.GetQuestObjectByID(_arrIdObjects[i]);
            if (obj != null)
            {
                QuestObject qo = obj.GetComponent<QuestObject>();
                if (qo != null) 
                {
                    //qo.SetQuestFaza(_faza);
                    int zn = (_questObjectsFaza >> (2 * i)) & 0x3;
                    QuestFaza faza = QuestFaza.NotAvailable;
                    if (zn == 1) faza = QuestFaza.Available;
                    if (zn == 2) faza = QuestFaza.Processing;
                    if (zn == 3) faza = QuestFaza.Completed;
                    qo.SetQuestFaza(faza);
                    qo.SetMyQuest(this);
                }
                _questObjects[i] = obj;
                //Debug.Log($"i={i}  name={obj.name} qo=<{qo}>");
            }
        }
    }
    public GameObject[] GetArrObjects()
    {
        return _questObjects;
    }

    public void UpdateFaza(QuestFaza questFaza)
    {
        int count = 0;
        _faza = questFaza;
        foreach(GameObject go in _questObjects)
        {
            if (go != null)
            {
                count++;
                QuestObject qo = go.GetComponent<QuestObject>();
                if (qo != null) 
                { 
                    qo.SetQuestFaza(_faza);
                    //Debug.Log($"name={go.name} qo=<{qo}> faza={_faza}");
                    if (qo.Faza > QuestFaza.Processing)
                    {
                        IMyCommand myCommand = go.GetComponent<IMyCommand>();
                        if (myCommand != null)
                        {
                            myCommand.Execute();
                        }
                    }
                }
            }
        }
        //Debug.Log($"QUEST {_name} : nums={_arrIdObjects.Length}  go={_questObjects.Length} notNull={count}");
    }

    public void TestCompleted()
    {
        int i, count = 0; 
        if (_arrIdObjects.Length > 1)
        {
            for (i = 1; i < _arrIdObjects.Length; i++)
            {
                if (_questObjects[i] != null)
                {
                    QuestObject qo = _questObjects[i].GetComponent<QuestObject>();
                    if (qo != null)
                    {
                        if (qo.Faza >= QuestFaza.Processing)
                        {
                            count++;
                        }
                    }
                }
            }
            if (count + 1 == _arrIdObjects.Length)
            {
                QuestObject qo = _questObjects[0].GetComponent<QuestObject>();
                if (qo != null && qo.Faza < QuestFaza.Processing)
                {
                    qo.SetQuestFaza(QuestFaza.Processing);
                    GameManager.Instance.SaveGame();
                }
            }
        }
    }
}

[Serializable]
public class QuestResult
{
    public int QuestID;
    public QuestFaza Faza;

    public QuestResult() { }
    public QuestResult(int questID, QuestFaza faza)
    {
        QuestID = questID;
        Faza = faza;
    }
}
