using UnityEngine;
using UnityEngine.UI;

public class SafeBoxControl : MonoBehaviour
{
    [SerializeField] private Text _code;
    [SerializeField] private LevelControl _levelControl;

    private Animator _anim;
    private int _numCode = 0;
    private int _oldZn = 0;

    private void Awake()
    {
        _anim = GetComponent<Animator>();
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ViewCode();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void ViewCode()
    {
        _code.text = $"{_numCode:D05}";
    }

    public void PushButton(int zn)
    {
        _oldZn = zn;
        float delay = 0.02f;
        switch(zn)
        {
            case 0:
                if (_numCode == 16758)
                {
                    _anim.SetBool("IsOpen", true);
                    _levelControl.ChangeFazaQuest(new QuestStatus(3, QuestFaza.Processing));
                    //Invoke("ClearButton", 0.24f);
                }
                _anim.SetBool("IsError", true);
                Invoke("ClearButton", delay);
                break;
            case 1:
                _numCode = (_numCode + 10000) % 100000;
                _anim.SetBool("Push_1", true);
                Invoke("ClearButton", delay);
                break;
            case 2:
                _numCode = 10000 * (_numCode / 10000) + (_numCode % 10000 + 1000) % 10000;
                _anim.SetBool("Push_2", true);
                Invoke("ClearButton", delay);
                break;
            case 3:
                _numCode = 1000 * (_numCode / 1000) + (_numCode % 1000 + 100) % 1000;
                _anim.SetBool("Push_3", true);
                Invoke("ClearButton", delay);
                break;
            case 4:
                _numCode = 100 * (_numCode / 100) + (_numCode % 100 + 10) % 100;
                _anim.SetBool("Push_4", true);
                Invoke("ClearButton", delay);
                break;
            case 5:
                _numCode =  10 * (_numCode / 10) + (_numCode % 10 + 1) % 10;
                _anim.SetBool("Push_5", true);
                Invoke("ClearButton", delay);
                break;
        }

    }

    private void ClearButton()
    {
        string nameParam = $"Push_{_oldZn}";
        if (_oldZn == 0) nameParam = "IsError";
        else ViewCode();
        _anim.SetBool(nameParam, false);
        //print($"numCode={_numCode}   oldZn={_oldZn}  nameParam={nameParam}");
    }
}
