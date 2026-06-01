using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class LevelControl : MonoBehaviour
{
    [SerializeField] private LevelUI _levelUI;
    [SerializeField] private SetQuestObjects _setQuestObjects;

    private List<MyQuest> _questList = new List<MyQuest>();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        CreateMyQuestList();
        Invoke("BuildQuestList", 0.5f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void CreateMyQuestList()
    {
        _questList.Clear();
        _questList.Add(new MyQuest(1, "Странный замок", "На этот замок закрыта дверь из старого дома. Нужно найти 4 ключа, вставить их в замок и повернуть ключ, чтобы разблокировать выходную дверь", QuestFaza.NotAvailable, new int[] { 1, 2, 3, 4, 5}));
        _questList.Add(new MyQuest(2, "Шкатулка с секретом", "Возможно в шкатулке лежит что-то важное. Сыграйте в игру, чтобы открыть шкатулку и взять её содержимое.", QuestFaza.NotAvailable, new int[] { 6, 1 }));
        _questList.Add(new MyQuest(3, "Сейф с кодовым замком", "В сейфе однозначно есть что-то интересное. Введите код, откройте сейф и заберите то, что внутри", QuestFaza.NotAvailable, new int[] { 8, 7, 9 }));
    }

    private void BuildQuestList()
    {
        if (_setQuestObjects != null)
        {

        }
    }
}
