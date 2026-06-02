using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class MyOutLine : MonoBehaviour
{
    [SerializeField] private Material outlineMaterial;

    private MeshRenderer _mr;

    private void Awake()
    {
        _mr = GetComponent<MeshRenderer>();
        //print($"_mr={_mr}");
    }

    public void ViewOutLine()
    {
        //print($"OnMouseEnter");
        if (_mr != null)
        {
            Material[] mats = _mr.materials;
            if (mats.Contains(outlineMaterial) == false)
            {
                Material[] updMats = new Material[mats.Length + 1];
                for (int i = 0; i < mats.Length; i++) updMats[i] = mats[i];
                updMats[mats.Length] = outlineMaterial;
                _mr.materials = updMats;
            }
        }
    }

    public void HideOutLine()
    {
        //print($"OnMouseExit");
        if (_mr != null)
        {
            Material[] mats = _mr.materials;
            if (mats.Length > 1)
            {
                //Debug.Log($"ClearHighlight outlineMaterial=true ({mats[mats.Length - 1]})");
                Material[] updMats = new Material[mats.Length - 1];
                for (int i = 0; i < mats.Length - 1; i++)
                {
                    //if (mats[i] != outlineMaterial)
                    updMats[i] = mats[i];
                }
                _mr.materials = updMats;
            }
        }
    }
}
