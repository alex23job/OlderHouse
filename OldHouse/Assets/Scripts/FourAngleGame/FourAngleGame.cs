using UnityEngine;

public class FourAngleGame : MonoBehaviour
{
    [SerializeField] private FourAngleUI _fourAngleUI;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetResult(int value)
    {
        QuestResult result = new QuestResult(2, QuestFaza.NotAvailable);
        switch(value)
        {
            case 0: result.Faza = QuestFaza.NotAvailable; break;
            case 1: result.Faza = QuestFaza.Available; break;
            case 2: result.Faza = QuestFaza.Processing; break;
            case 3: result.Faza = QuestFaza.Completed; break;
        }
        _fourAngleUI.ViewHint($"QuestID = {result.QuestID}    Faza = {result.Faza}");
        GameManager.Instance.currentPlayer.questResult = result;
    }
}
