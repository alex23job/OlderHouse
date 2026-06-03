using Assets.Scripts.Interact;
using UnityEngine;

public class PushBtn : MonoBehaviour, IInteract
{
    [SerializeField] private int _id;
    [SerializeField] private string _descr;
    [SerializeField] private SafeBoxControl _boxControl;
    [SerializeField] private LevelUI _levelUI;

    private MyOutLine _outLine;

    public int ID => _id;

    private void Awake()
    {
        _outLine = GetComponent<MyOutLine>();
    }

    public string GetHint()
    {
        return _descr;
    }

    private void OnMouseUp()
    {
        if (Input.GetMouseButtonUp(0))
        {
            _boxControl.PushButton(ID);
        }
    }
    private void OnMouseEnter()
    {
        if (_outLine != null)
        {
            _outLine.ViewOutLine();
        }
        if (_levelUI != null)
        {
            _levelUI.ViewHint(_descr);
        }

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
    }
}
