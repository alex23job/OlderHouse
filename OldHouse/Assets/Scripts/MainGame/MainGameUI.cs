using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainGameUI : MonoBehaviour
{
    [SerializeField] private Text _txtDebug;

    [SerializeField] private PlaySounds _playSounds;

    [SerializeField] private GameObject _settingsPanel;
    [SerializeField] private Slider _sliderFone;
    [SerializeField] private Slider _sliderEffects;
    [SerializeField] private Toggle _togleFone;
    [SerializeField] private Toggle _togleEffects;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (_playSounds != null)
        {
            _playSounds.SetVolume(GameManager.Instance.currentPlayer.volumeFone);
            if (GameManager.Instance.currentPlayer.isSoundFone == false) _playSounds.PauseSounds();
        }
        _playSounds.PlayClip(0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadHouseScene()
    {
        SceneManager.LoadScene("HouseScene");
    }

    public void FirstGame()
    {
        GameManager.Instance.currentPlayer = PlayerInfo.FirstGame();
        LoadHouseScene();
    }

    public void ViewSettingPanel()
    {
        _settingsPanel.SetActive(true);
        _togleFone.isOn = GameManager.Instance.currentPlayer.isSoundFone;
        _togleEffects.isOn = GameManager.Instance.currentPlayer.isSoundEffects;
        _sliderFone.value = GameManager.Instance.currentPlayer.volumeFone / 100f;
        _sliderEffects.value = GameManager.Instance.currentPlayer.volumeEffects / 100f;
        _sliderFone.interactable = _togleFone.isOn;
        _sliderEffects.interactable = _togleEffects.isOn;
    }
    public void ChangeSoundFone()
    {
        GameManager.Instance.currentPlayer.isSoundEffects = _togleFone.isOn;
        _sliderFone.interactable = _togleFone.isOn;
        if (_togleFone.isOn) _playSounds.PlayClip(0);
        else _playSounds.PauseSounds();
    }

    public void ChangeSoundEffects()
    {
        GameManager.Instance.currentPlayer.isSoundEffects = _togleEffects.isOn;
        _sliderEffects.interactable = _togleEffects.isOn;
    }

    public void ChangeVolumeFone()
    {
        GameManager.Instance.currentPlayer.volumeFone = (int)(_sliderFone.value * 100);
        _playSounds.SetVolume(GameManager.Instance.currentPlayer.volumeFone);
    }

    public void ChangeVolumeEffects()
    {
        GameManager.Instance.currentPlayer.volumeEffects = (int)(_sliderEffects.value * 100);
    }

    public void CloseSettings()
    {
        GameManager.Instance.SaveGame();
        _settingsPanel.SetActive(false);
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
