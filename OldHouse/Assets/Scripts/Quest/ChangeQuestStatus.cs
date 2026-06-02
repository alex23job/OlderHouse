using System;
using UnityEngine;

public class ChangeQuestStatus : MonoBehaviour
{
    [SerializeField] private QuestStatus[] _questStatuses;
    [SerializeField] private LevelControl _levelControl;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            foreach (var item in _questStatuses)
            {
                item.IsComplete = _levelControl.ChangeFazaQuest(item);
            }
        }
    }
}

[Serializable]
public class QuestStatus
{
    public int QuestID;
    public QuestFaza Faza;
    public bool IsComplete = false;

    public QuestStatus() { }
    public QuestStatus(int id, QuestFaza faza)
    {
        QuestID = id;
        Faza = faza;
    }
}
