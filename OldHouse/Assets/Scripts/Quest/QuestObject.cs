using UnityEngine;
using Assets.Scripts.Interact;
using Assets.Scripts.Level;

public class QuestObject : MonoBehaviour, IInteract
{
    [SerializeField] private int _id;
    [SerializeField] private string[] _hint;
    [SerializeField] private LevelUI _levelUI;

    private QuestFaza _questFaza;
    private MyOutLine _outLine;
    private MyQuest _mainQuest;

    public QuestFaza Faza => _questFaza;

    public int ID => _id;

    public string GetHint()
    {
        string res = _hint[0];
        switch(_questFaza)
        {
            case QuestFaza.NotAvailable: res = _hint[0]; break;
            case QuestFaza.Available: if (_hint.Length > 1) res = _hint[1]; break;
            case QuestFaza.Processing: if (_hint.Length > 2) res = _hint[2]; break;
            case QuestFaza.Completed: if (_hint.Length > 3) res = _hint[3]; break;
        }
        return res;
    }

    private void Awake()
    {
        _outLine = GetComponent<MyOutLine>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseEnter()
    {
        if (_outLine != null)
        {
            _outLine.ViewOutLine();
        }
        if (_levelUI != null)
        {
            _levelUI.ViewHint(GetHint());
        }
        IMyCommand imc = GetComponent<IMyCommand>();
        print($"IMyCommand = <{imc}>");
        GameManager.Instance.currentPlayer.myCommand = GetComponent<IMyCommand>();
    }

    private void OnMouseExit()
    {
        if (_outLine != null)
        {
            _outLine.HideOutLine();
        }
        if (_levelUI != null)
        {
            _levelUI.HideHint();
        }
        GameManager.Instance.currentPlayer.myCommand = null;
    }

    public void SetQuestFaza(QuestFaza questFaza)
    {
        _questFaza = questFaza;
    }

    public void SetMyQuest(MyQuest quest)
    {
        _mainQuest = quest;
    }

    public void TestMainQuest()
    {
        if (_mainQuest != null) _mainQuest.TestCompleted();
    }

    public GameObject[] GetArrObjects()
    {
        return _mainQuest.GetArrObjects();
    }

    public void PlayEffect(int numClip)
    {
        _levelUI.PlayEffect(numClip);
    }
}
