using Assets.Scripts.Level;
using UnityEngine;

public class OpenBedTableDoor : MonoBehaviour, IMyCommand
{
    [SerializeField] private int _id;
    [SerializeField] private bool _isKeyClose = false;

    private QuestObject _questObject;
    private Animator _anim;
    private bool _isOpen = false;

    public int ID => _id;
    private void Awake()
    {
        _anim = transform.parent.parent.gameObject.GetComponent<Animator>();
        _questObject = GetComponent<QuestObject>();
    }

    public void Execute()
    {
        if (_isOpen) return;
        if (_isKeyClose)
        {
            if (GameManager.Instance.currentPlayer.inventory.CheckItemByID(ID) == false) return;
        }
        _isOpen = true;
        _anim.SetBool("IsOpen", true);
        Invoke("CloseDoor", 10f);
    }

    public void CloseDoor()
    {
        _isOpen = false;
        _anim.SetBool("IsOpen", false);
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if ((_questObject != null) && (_isKeyClose == false))
        {
            _questObject.SetQuestFaza(QuestFaza.Available);
        }
    }
}
