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

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        HideHint();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ViewInventory()
    {
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
