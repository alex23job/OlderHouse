using Assets.Scripts.Level;
using System;
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
                //_faza = result;
                UpdateFaza(result);
            }
        }
    }

    public string SaveProcessingToCsv(char sep = '=')
    {
        StringBuilder sb = new StringBuilder($"{_id}{sep}{_faza}{sep}");

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
                if (qo != null) { qo.SetQuestFaza(_faza); }
                _questObjects[i] = obj;
                //Debug.Log($"i={i}  name={obj.name} qo=<{qo}>");
            }
        }
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
                    Debug.Log($"name={go.name} qo=<{qo}> faza={_faza}");
                    if (qo.Faza >= QuestFaza.Processing)
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
        Debug.Log($"QUEST {_name} : nums={_arrIdObjects.Length}  go={_questObjects.Length} notNull={count}");
    }

    public void TestCompleted()
    {

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
