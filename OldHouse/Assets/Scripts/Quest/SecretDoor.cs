using UnityEngine;

public class SecretDoor : MonoBehaviour
{
    [SerializeField] private GameObject[] _figure;
    [SerializeField] private LevelUI _levelUI;
    [SerializeField] private int _doorID;

    private SecretDoor _secretDoor;
    private Animator _anim;
    private int _code;
    private int _codeEtalon = 0x7f;

    private void Awake()
    {
        _anim = GetComponent<Animator>();
        _secretDoor = GetComponent<SecretDoor>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (_figure != null && _figure.Length > 0)
        {
            int startPos = Random.Range(0, 127);
            foreach (var figure in _figure)
            {
                ElefantControl ef = figure.GetComponentInChildren<ElefantControl>();
                if (ef != null)
                {
                    ef.SetParams(_levelUI, _secretDoor);
                    if ((startPos & (1 << ef.ID)) > 0) ef.TurnElefant();
                }
            }
            _code = startPos;
            int rnd = Random.Range(0, 2);
            if (rnd > 0) _codeEtalon = 0;
        }
        if ((GameManager.Instance.currentPlayer.totalGold & _doorID) > 0) _anim.SetBool("IsOpen", true);
    }

    public void SendFigureState(int id, int state)
    {
        if (id >= 0 && id < 7)
        {
            int[] mask = new int[7] { 0xfe, 0xfd, 0xfb, 0xf7, 0xef, 0xdf, 0xbf};
            _code = (_code & mask[id]) + (state << id);
        }
        if (_code == _codeEtalon)
        {
            _anim.SetBool("IsOpen", true);
            GameManager.Instance.currentPlayer.totalGold |= _doorID;
            GameManager.Instance.SaveGame();
            _levelUI.PlayEffect(1);
        }
    }
}
