using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class LevelUI : MonoBehaviour
{
    [SerializeField] private GameObject _hintPanel;
    [SerializeField] private Text _hintText;

    [SerializeField] private SetSprite _setSprite;
    [SerializeField] private GameObject _inventPanel;
    [SerializeField] private GameObject[] _viewItems;
    [SerializeField] private Button _prev;
    [SerializeField] private Button _next;
    private int _firstItem = 0;
    private InventoryTail[] _items;

    [SerializeField] private GameObject _pagePanel;
    [SerializeField] private Text _title;
    [SerializeField] private Button _pagePrev;
    [SerializeField] private Button _pageNext;
    [SerializeField] private Image _pageImage;
    private int _currentPage = 0;
    private Sprite[] _pages;

    [SerializeField] private PlaySounds _playEffects;
    [SerializeField] private PlaySounds _playSounds;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        HideHint();
        if (_playSounds != null)
        {
            _playSounds.SetVolume(GameManager.Instance.currentPlayer.volumeFone);
            if (GameManager.Instance.currentPlayer.isSoundFone == false) _playSounds.PauseSounds();
            else _playSounds.PlayClip(0);
        }
        if (_playEffects != null)
        {
            _playEffects.SetVolume(GameManager.Instance.currentPlayer.volumeEffects);
            if (GameManager.Instance.currentPlayer.isSoundEffects == false) _playEffects.PauseSounds();
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ViewInventory()
    {
        if (_inventPanel.activeInHierarchy)
        {
            _inventPanel.SetActive(false);
            return;
        }
        _inventPanel.SetActive(true);
        _items = GameManager.Instance.currentPlayer.inventory.GetItems();
        if (_items.Length > 0)
        {
            for(int i = 0; i < _items.Length; i++)
            {
                SimpleSprite spr = _setSprite.GetSpriteByID(_items[i].NumSprite);
                if (spr != null) _items[i].SetSprite(spr._sprite);
            }
        }
        UpdateInventoryPanel();
    }

    private void UpdateInventoryPanel()
    {
        int i;
        for (i = 0; i < _viewItems.Length; i++)
        {
            if (i + _firstItem < _items.Length)
            {
                _viewItems[i].SetActive(true);
                Image img = _viewItems[i].transform.GetChild(0).gameObject.GetComponentInChildren<Image>();
                if (img != null) img.sprite = _items[i + _firstItem].Sprite;
                Text txt = _viewItems[i].GetComponentInChildren<Text>();
                if (txt != null) txt.text = _items[i + _firstItem].Name;
            }
            else
            {
                _viewItems[i].SetActive(false);
            }
        }
        _prev.interactable = _firstItem > 0;
        _next.interactable = _firstItem + _viewItems.Length < _items.Length;
    }

    public void OnPrevBtnClick()
    {
        if (_firstItem > 0)
        {
            _firstItem--;
            UpdateInventoryPanel();
        }
    }

    public void OnNextBtnClick()
    {
        if (_firstItem + _viewItems.Length < _items.Length)
        {
            _firstItem++;
            UpdateInventoryPanel();
        }
    }

    public void ViewPagePanel(string title, Sprite[] pages)
    {
        _pagePanel.SetActive(true);
        _title.text = title;
        _pages = pages;
        _currentPage = 0;
        UpdatePagePanel();
    }
    private void UpdatePagePanel()
    {
        _pageImage.sprite = _pages[_currentPage];
        _pagePrev.interactable = _currentPage > 0;
        _pageNext.interactable = _currentPage + 1 < _pages.Length;
    }
    public void OnPrevPageBtnClick()
    {
        if (_currentPage > 0)
        {
            _currentPage--;
            UpdatePagePanel();
        }
    }

    public void OnNextPageBtnClick()
    {
        if (_currentPage + 1 < _pages.Length)
        {
            _currentPage++;
            UpdatePagePanel();
        }
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

    public void LoadFinalScene()
    {
        _playSounds.PauseSounds();
        SceneManager.LoadScene("FinalScene");
    }

    public void LoadMainScene()
    {
        _playSounds.PauseSounds();
        SceneManager.LoadScene("MainScene");
    }

    public void LoadMiniGameScene(string nameScene)
    {
        _playSounds.PauseSounds();
        SceneManager.LoadScene(nameScene);
    }

    public void PlayEffect(int numClip)
    {
        _playEffects.PlayClip(numClip);
    }
}
