using Assets.Scripts.Interact;
using UnityEngine;

public class ElefantControl : MonoBehaviour, IInteract
{
    [SerializeField] private int _id;
    private LevelUI _levelUI;
    private SecretDoor _secretDoor;
    private MyOutLine _outLine;
    private string _hint = "Кликните по слонику и он развернётся";

    public int ID => _id;
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

    public void SetParams(LevelUI lui, SecretDoor dc)
    {
        _levelUI = lui;
        _secretDoor = dc;
    }

    public string GetHint()
    {
        return _hint;
    }

    private void OnMouseUp()
    {
        transform.Rotate(new Vector3(0, 180f, 0), Space.World);
        _secretDoor.SendFigureState(ID, (Mathf.RoundToInt(transform.rotation.eulerAngles.y) % 360) / 180);
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
        if (_levelUI != null)
        {
            _levelUI.HideHint();
        }
    }
}
