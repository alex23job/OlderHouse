using Assets.Scripts.Level;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CasketControl : MonoBehaviour, IMyCommand
{
    [SerializeField] private int _id;
    [SerializeField] private string _sceneMiniGame;
    public int ID => _id;

    private Animator _animator;
    private QuestObject _questObject;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _questObject = GetComponent<QuestObject>();
    }

    public void Execute()
    {
        if (_questObject.Faza == QuestFaza.Available)
        {
            SceneManager.LoadScene(_sceneMiniGame);
        }
        if (_questObject.Faza >= QuestFaza.Processing)
        {
            _animator.SetBool("IsOpen", true);
            _questObject.SetQuestFaza(QuestFaza.Completed);
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
