using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class LevelUI : MonoBehaviour
{
    [SerializeField] private GameObject _hintPanel;
    [SerializeField] private Text _hintText;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        HideHint();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ViewHint(string hint)
    {
        _hintPanel.SetActive(true);
        _hintText.text = hint;
    }

    public void HideHint()
    {
        _hintPanel.SetActive(false);
    }

    public void LoadMainScene()
    {
        SceneManager.LoadScene("MainScene");
    }

    public void LoadMiniGameScene(string nameScene)
    {
        SceneManager.LoadScene(nameScene);
    }
}
