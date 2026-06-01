using Unity.VisualScripting;
using UnityEngine;

public class SetQuestObjects : MonoBehaviour
{
    [SerializeField] private GameObject[] _arrQuestObjects;

    public GameObject[] ArrQuestObjects => _arrQuestObjects;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public GameObject GetQuestObjectByID(int id)
    {
        foreach (GameObject obj in _arrQuestObjects)
        {
            QuestObject qo = obj.GetComponent<QuestObject>();
            if (qo != null)
            {
                if (qo.ID == id) return obj;
            }
        }
        return null;
    }
}
