using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class EnemyUpdate : MonoBehaviour
{
    private List<IUpdateType> _enemy;
    [Inject] private EventHandler _eventHandler;

    private void AddEnemy(IUpdateType updateType) => _enemy.Add(updateType);
    private void RemoveEnemy(IUpdateType updateType) => _enemy.Remove(updateType);

    private void Awake()
    {
        _enemy = new List<IUpdateType>();
        _eventHandler.onRemoveUpdateComponent += RemoveEnemy;
        _eventHandler.onAddUpdateComponent += AddEnemy;
    } 
    private void Update()
    {
        foreach(var enemy in _enemy)
            enemy.UpdateMethod();
    }

    private void OnDisable()
    {
        _eventHandler.onRemoveUpdateComponent -= RemoveEnemy;
        _eventHandler.onAddUpdateComponent -= AddEnemy;
    } 
}
