using UnityEngine;
using Zenject;
public class NPCactivator : MonoBehaviour
{
    [SerializeField] private GameObject _npc;
    private int _enemyCountOnLevel;
    [Inject] private EventHandler _eventHandler;

    private void CountEnemyUpdate()
    {
        _enemyCountOnLevel--;

        if(_enemyCountOnLevel == 0)
            _npc.SetActive(true);
    }

    private void SetCount() => _enemyCountOnLevel++;

    private void OnEnable() 
    {
        _eventHandler.onAddEnemy += SetCount;
        _eventHandler.onEnemyDie += CountEnemyUpdate;
    }
    private void OnDisable()
    {
        _eventHandler.onAddEnemy -= SetCount;
        _eventHandler.onEnemyDie -= CountEnemyUpdate;
    }
}
