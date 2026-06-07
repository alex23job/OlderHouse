using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class Inventory
{
    private List<InventoryTail> items = new List<InventoryTail>();

    public Inventory() { }
    public Inventory(string csv, char sep = '#')
    {
        if (string.IsNullOrEmpty(csv)) return;
        string[] ar = csv.Split(sep, StringSplitOptions.RemoveEmptyEntries);
        foreach (string s in ar)
        {
            items.Add(new InventoryTail(s));
        }
    }
    public void AddTail(int id, string nm, string descr, int numSprite)
    {
        foreach (InventoryTail item in items)
        {
            if (item.Id == id) return;
        }
        items.Add(new InventoryTail(id, nm, descr, numSprite));
        Debug.Log($"items = {items.Count}");
    }

    public InventoryTail[] GetItems()
    {
        return items.ToArray();
    }

    public bool CheckItemByID(int id)
    {
        foreach (InventoryTail item in items)
        {
            if (item.Id == id) return true;
        }
        return false;
    }

    public string ToCsvString(char sep = '#')
    {
        StringBuilder sb = new StringBuilder();
        foreach (InventoryTail item in items)
        {
            sb.Append($"{item.ToCsvString()}{sep}");
        }
        return sb.ToString();
    }
}

[Serializable]
public class InventoryTail
{
    private int _id;
    private string _name;
    private string _description;
    private int _numSprite;
    private Sprite _sprite;

    public int Id => _id;
    public string Name => _name;
    public string Description => _description;
    public int NumSprite => _numSprite;
    public Sprite Sprite => _sprite;

    public InventoryTail() { }
    public InventoryTail(int id, string name, string description, int numSprite, Sprite sprite = null)
    {
        _id = id;
        _name = name;
        _description = description;
        _numSprite = numSprite;
        _sprite = sprite;
    }

    public InventoryTail(string csv, char sep = '=')
    {
        string[] ar = csv.Split(sep, StringSplitOptions.RemoveEmptyEntries);
        if (ar.Length >= 4)
        {
            if (int.TryParse(ar[0], out int id))
            {
                _id = id;
            }
            _name = ar[1];
            _description = ar[2];
            if (int.TryParse(ar[3], out int num))
            {
                _numSprite = num;
            }
        }
    }

    public string ToCsvString(char sep = '=')
    {
        return $"{_id}{sep}{_name}{sep}{_description}{sep}{_numSprite}{sep}";
    }

    public void SetSprite(Sprite sprite)
    {
        _sprite = sprite;
    }
}
