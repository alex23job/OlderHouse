using NUnit.Framework;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class LevelControl : MonoBehaviour
{
    [SerializeField] private LevelUI _levelUI;
    [SerializeField] private SetQuestObjects _setQuestObjects;
    [SerializeField] private GameObject _player;

    private List<MyQuest> _questList = new List<MyQuest>();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        CreateMyQuestList();
        Invoke("LoadPlayerPosition", 0.8f);
        Invoke("BuildQuestList", 0.5f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void LoadPlayerPosition()
    {
        Vector3 delta = Vector3.zero - GameManager.Instance.currentPlayer.oldPosition;
        if (delta.magnitude < 0.1f)
        {
            _levelUI.ViewHint("Наконец-то я добрался до этого странного дома. Всё моё расследование указывает, что КНИГА древних рецептов снадобий спрятана в нём. Захожу в дом и заБираю её!!! Делов то ... ");
            return;
        }
        _player.transform.position = GameManager.Instance.currentPlayer.oldPosition;
        _player.transform.rotation = Quaternion.Euler(GameManager.Instance.currentPlayer.oldRotation);
    }

    private void CreateMyQuestList()
    {
        _questList.Clear();
        _questList.Add(new MyQuest(1, "Странный замок", "На этот замок закрыта дверь из старого дома. Нужно найти 4 ключа, вставить их в замок и повернуть ключ, чтобы разблокировать выходную дверь", QuestFaza.NotAvailable, new int[] { 1, 3, 4, 5, 9, 10, 11, 12}));
        _questList.Add(new MyQuest(2, "Шкатулка с секретом", "Возможно в шкатулке лежит что-то важное. Сыграйте в игру, чтобы открыть шкатулку и взять её содержимое.", QuestFaza.NotAvailable, new int[] { 6, 2 }));
        _questList.Add(new MyQuest(3, "Сейф с кодовым замком", "В сейфе однозначно есть что-то интересное. Введите код, откройте сейф и заберите то, что внутри", QuestFaza.NotAvailable, new int[] { 8, 7 }));
        _questList.Add(new MyQuest(4, "Закрытая тумбочка", "Возможно, в тумбочке лежит что-то интересное. Иначе зачемеё запирать. Найдите ключик, откройте тумбочку чтобы посмотреть то, что внутри", QuestFaza.NotAvailable, new int[] { 32, 31 }));
    }

    private void BuildQuestList()
    {
        if (_setQuestObjects != null)
        {
            if (_questList.Count > 0)
            {
                if (GameManager.Instance.currentPlayer.questStatus != "")
                {
                    string[] ar = GameManager.Instance.currentPlayer.questStatus.Split('#', System.StringSplitOptions.RemoveEmptyEntries);
                    if (ar.Length > 0)
                    {
                        for (int i = 0; i < ar.Length; i++)
                        {
                            _questList[i].LoadProcessing(ar[i]);
                            //_questList[i].SetQuestObjects(_setQuestObjects);
                        }
                    }
                }
                foreach(var item in _questList)
                {
                    item.SetQuestObjects(_setQuestObjects);
                }
            }
        }
        if (_questList.Count > 0 && GameManager.Instance.currentPlayer.questResult != null)
        {
            QuestResult qr = GameManager.Instance.currentPlayer.questResult;
            foreach (var item in _questList)
            {
                if (item.Id == qr.QuestID)
                {
                    item.UpdateFaza(qr.Faza);
                    GameManager.Instance.currentPlayer.questStatus = ListQuestsToCsv();
                    break;
                }
            }

        }
    }

    public void LevelExit()
    {
        GameManager.Instance.currentPlayer.oldPosition = _player.transform.position;
        GameManager.Instance.currentPlayer.oldRotation = _player.transform.rotation.eulerAngles;
        GameManager.Instance.currentPlayer.questStatus = ListQuestsToCsv();
        GameManager.Instance.SaveGame();
        _levelUI.LoadMainScene();
    }

    private string ListQuestsToCsv(char sep = '#')
    {
        StringBuilder sb = new StringBuilder();
        if (_questList.Count > 0)
        {
            foreach (var quest in _questList)
            {
                sb.Append($"{quest.SaveProcessingToCsv()}{sep}");
            }
        }
        return sb.ToString();
    }

    public bool ChangeFazaQuest(QuestStatus qs)
    {
        foreach(var quest in _questList)
        {
            if (quest.Id == qs.QuestID)
            {
                if (quest.Faza < qs.Faza)
                {
                    quest.UpdateFaza(qs.Faza);
                    GameManager.Instance.currentPlayer.questStatus = ListQuestsToCsv();
                    GameManager.Instance.SaveGame();
                    return true;
                }
            }
        }
        return false;
    }
}
