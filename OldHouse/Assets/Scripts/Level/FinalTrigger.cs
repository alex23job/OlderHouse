using UnityEngine;

public class FinalTrigger : MonoBehaviour
{
    [SerializeField] private LevelUI _levelUI;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (GameManager.Instance.currentPlayer.inventory.CheckItemByID(7))
            {
                _levelUI.LoadFinalScene();
            }
            else
            {
                _levelUI.ViewHint("А как же древняя книга? Вы всё ещё не нашли её. Вернитесь в дом и попробуйте открыть сейф! Возможно, она там ...");
                Invoke("HideHint", 10f);
            }
        }
    }

    private void HideHint()
    {
        _levelUI.HideHint();
    }
}
