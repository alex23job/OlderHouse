using UnityEngine;

public class FinalControl : MonoBehaviour
{
    [SerializeField] private GameObject _forest;
    [SerializeField] private GameObject _house;
    [SerializeField] private FinalUI _finalUI;

    private bool _isMove = false;

    private Vector3 _startForest;
    private Vector3 _startHouse;
    private float _offsetY = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _startForest = _forest.transform.position;
        _startHouse = _house.transform.position;
        Invoke("StartMove", 5f);
    }

    // Update is called once per frame
    void Update()
    {
        if (_isMove)
        {
            _offsetY += Time.deltaTime;
            Vector3 pos = _startForest;
            pos.y += _offsetY;
            _forest.transform.position = pos;
            if (pos.y >= 0)
            {
                _isMove = false;
                Invoke("ViewHint", 3f);
            }
            pos = _startHouse;
            pos.y -= _offsetY;
            _house.transform.position = pos;
        }
    }

    private void StartMove()
    {
        _isMove = true;
    }

    private void ViewHint()
    {
        _finalUI.ViewHint();
    }
}
