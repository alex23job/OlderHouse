using Assets.Scripts.Level;
using UnityEngine;

public class OutDoorControl : MonoBehaviour, IMyCommand
{
    [SerializeField] private int _id;
    [SerializeField] private GameObject _linkObject;
    public int ID => _id;

    private Animator _anim;
    private QuestObject _questObject;

    public void Execute()
    {
        if (_questObject != null)
        {
            if (_questObject.Faza >= QuestFaza.Available)
            {
                if (TestCompleted())
                {
                    _anim.SetBool("IsOpen", true);
                    if (_linkObject != null)
                    {
                        IMyCommand myCommand = _linkObject.GetComponent<IMyCommand>();
                        if (myCommand != null) myCommand.Execute();
                    }
                }
            }
        }
        else
        {
            _anim.SetBool("IsOpen", true);
        }
    }
    private void Awake()
    {
        _anim = GetComponent<Animator>();
        _questObject = GetComponent<QuestObject>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if ((GameManager.Instance.currentPlayer.totalGold & 8) > 0) _anim.SetBool("IsOpen", true);
    }

    private bool TestCompleted()
    {
        if (_questObject != null)
        {
            GameObject[] qos = _questObject.GetArrObjects();
            if (qos != null && qos.Length > 0)
            {
                int count = 0;
                foreach (GameObject obj in qos)
                {
                    QuestObject qo = obj.GetComponent<QuestObject>();
                    if (qo != null)
                    {
                        if ((qo.Faza >= QuestFaza.Processing) && (qo.ID >= 9 && qo.ID <= 12)) count++;
                        print($"TestCompleted ID={qo.ID}  Faza={qo.Faza}");
                    }
                }
                if (count == 4)
                {

                    return true;
                }
            }
        }
        return false;
    }
}
