using Assets.Scripts.Interact;
using Assets.Scripts.Level;
using UnityEngine;

public class LookBook : MonoBehaviour, IInteract, IMyCommand
{
    [SerializeField] private int _id;
    [SerializeField] private string _hint;
    [SerializeField] private string _title;
    [SerializeField] private Sprite[] _pages;
    [SerializeField] private LevelUI _levelUI;

    private MyOutLine _outLine;
    public int ID => _id;

    public void Execute()
    {
        if (_levelUI != null) _levelUI.ViewPagePanel(_title, _pages);
    }

    public string GetHint()
    {
        return _hint;
    }

    private void Awake()
    {
        _outLine = GetComponent<MyOutLine>();
    }

    private void OnMouseEnter()
    {
        if (_outLine != null)
        {
            _outLine.ViewOutLine();
        }
        if (_levelUI != null)
        {
            _levelUI.ViewHint(_hint);
        }
        IMyCommand imc = GetComponent<IMyCommand>();
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
}
