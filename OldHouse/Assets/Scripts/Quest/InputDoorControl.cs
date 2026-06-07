using Assets.Scripts.Level;
using UnityEngine;

public class InputDoorControl : MonoBehaviour, IMyCommand
{
    [SerializeField] private int _id;
    [SerializeField] private LevelUI _levelUI;
    private Animator _anim;
    private Vector3 _startRotation;

    public int ID => _id;

    public void Execute()
    {
        transform.rotation = Quaternion.Euler(_startRotation);
        _anim.SetBool("IsClosing", true);
        if ((GameManager.Instance.currentPlayer.totalGold & 1) == 0)
        {
            GameManager.Instance.currentPlayer.totalGold |= 1;  //  установили флаг, что дверь закрыта
            _levelUI.PlayEffect(0);
        }
    }

    private void Awake()
    {
        _anim = GetComponent<Animator>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _startRotation = transform.rotation.eulerAngles;

        if ((GameManager.Instance.currentPlayer.totalGold & 1) > 0) _anim.SetBool("IsClosing", true);      
    }
}
