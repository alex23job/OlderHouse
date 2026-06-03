using Assets.Scripts.Level;
using UnityEngine;

public class InventoryItem : MonoBehaviour, IMyCommand
{
    [SerializeField] private int _id;
    [SerializeField] private string _name;
    [SerializeField] private string _descr;
    [SerializeField] private int _numSprite;

    private QuestObject _questObject;
    public int ID => _id;

    private void Awake()
    {
        _questObject = GetComponent<QuestObject>();
    }

    public void Execute()
    {
        if (_questObject != null && _questObject.Faza >= QuestFaza.Available)
        {
            //transform.parent.gameObject.SetActive(false);
            gameObject.SetActive(false);
            if (_questObject != null)
            {
                _questObject.SetQuestFaza(QuestFaza.Completed);
                _questObject.TestMainQuest();
            }
            GameManager.Instance.currentPlayer.inventory.AddTail(_id, _name, _descr, _numSprite);
        }
    }
}
