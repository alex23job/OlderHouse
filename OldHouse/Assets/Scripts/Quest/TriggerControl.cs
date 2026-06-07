using Assets.Scripts.Level;
using UnityEngine;

public class TriggerControl : MonoBehaviour
{
    [SerializeField] private bool _checkOneEntering = true;
    [SerializeField] private GameObject _linkObject;
    [SerializeField] private string _checkTag = "Player";

    private bool _isEnter = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(_checkTag))
        {
            if ((_checkOneEntering && _isEnter == false) || (_checkOneEntering == false))
            {
                IMyCommand myCommand = _linkObject.GetComponent<IMyCommand>();
                if (myCommand != null)
                {
                    myCommand.Execute();
                }
            }
            _isEnter = true;
        }
    }
}
