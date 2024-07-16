using UnityEngine;
using System.Collections.Generic;

public class PlayerUpdate : MonoBehaviour
{
    private  List<IUpdateType> _updateType;

    private void Awake() => _updateType = new List<IUpdateType>(GetComponents<IUpdateType>());

    private void Update()
    {
        foreach(var updateType in _updateType)
            updateType.UpdateMethod();
    }
}
