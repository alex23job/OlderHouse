using System;
using UnityEngine;

public class SetSprite : MonoBehaviour
{
    [SerializeField] private SimpleSprite[] _sprits;

    public SimpleSprite GetSpriteByID(int id)
    {
        foreach(var spr in _sprits)
        {
            if (spr._spriteID == id) return spr;
        }
        return null;
    }
}

[Serializable]
public class SimpleSprite
{
    public int _spriteID;
    public Sprite _sprite;
}
