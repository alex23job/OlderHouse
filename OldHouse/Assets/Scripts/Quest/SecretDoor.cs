using UnityEngine;

public class SecretDoor : MonoBehaviour
{
    [SerializeField] private GameObject[] _figure;
    [SerializeField] private LevelUI _levelUI;

    private SecretDoor _secretDoor;
    private Animator _anim;
    private int _code;

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
            foreach (var figure in _figure)
            {
                ElefantControl ef = figure.GetComponentInChildren<ElefantControl>();
                if (ef != null)
                {
                    ef.SetParams(_levelUI, _secretDoor);
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SendFigureState(int id, int state)
    {
        if (id >= 0 && id < 7)
        {
            int[] mask = new int[7] { 0xfe, 0xfd, 0xfb, 0xf7, 0xef, 0xdf, 0xbf};
            _code = (_code & mask[id]) + (state << id);
        }
        if (_code == 0x7f)
        {
            _anim.SetBool("IsOpen", true);
        }
    }
}
