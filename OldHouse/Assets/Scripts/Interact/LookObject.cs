using UnityEngine;
using Assets.Scripts.Interact;

public class LookObject : MonoBehaviour, IInteract
{
    [SerializeField] private int _lookID;
    [SerializeField] private string _hint;
    [SerializeField] private LevelUI _levelUI;

    private MyOutLine _outLine;

    public int ID => _lookID;

    public string GetHint()
    {
        return _hint;
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
            _levelUI.ViewHint(_hint);
        }
        
    }

    private void OnMouseExit()
    {
        if (_outLine != null)
        {
            _outLine.HideOutLine();
        }
        if ( _levelUI != null)
        {
            _levelUI.HideHint();
        }
    }
}
