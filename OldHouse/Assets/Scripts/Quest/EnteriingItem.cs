using Assets.Scripts.Level;
using UnityEngine;

public class EnteriingItem : MonoBehaviour, IMyCommand
{
    [SerializeField] private GameObject _go;
    [SerializeField] private int _id;
    [SerializeField] private int _inventoryID;
    public int ID => _id;
    private QuestObject _questObject;
    public void Execute()
    {
        if (_questObject != null)
        {
            if (_questObject.Faza == QuestFaza.Available || _questObject.Faza == QuestFaza.Processing)
            {
                if (GameManager.Instance.currentPlayer.inventory.CheckItemByID(_inventoryID))
                {
                    _go.SetActive(true);
                    _questObject.SetQuestFaza(QuestFaza.Completed);
                    _questObject.TestMainQuest();
                }
            }
        }
    }
    private void Awake()
    {
        _questObject = GetComponent<QuestObject>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (_questObject != null)
        {
            if (_questObject.Faza != QuestFaza.Completed) _go.SetActive(false);
            else _go.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
