using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainGameUI : MonoBehaviour
{
    [SerializeField] private Text _txtDebug;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadHouseScene()
    {
        SceneManager.LoadScene("HouseScene");
    }

    public void ExitGame()
    {
        GameManager.Instance.SaveGame();
        Application.Quit();
    }
    public void ViewDebug(string str)
    {
        if (_txtDebug != null) _txtDebug.text = str;
    }
}
