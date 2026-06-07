using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FinalUI : MonoBehaviour
{
    [SerializeField] private GameObject _hint;

    [SerializeField] private PlaySounds _playSounds;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (_playSounds != null)
        {
            _playSounds.SetVolume(GameManager.Instance.currentPlayer.volumeFone);
            if (GameManager.Instance.currentPlayer.isSoundFone == false) _playSounds.PauseSounds();
            else _playSounds.PlayClip(0);
        }
        //_playSounds.PlayClip(0);
    }

    public void ViewHint()
    {
        _hint.SetActive(true);
    }
    public void LoadMainScene()
    {
        _playSounds.PauseSounds();
        SceneManager.LoadScene("MainScene");
    }
}
