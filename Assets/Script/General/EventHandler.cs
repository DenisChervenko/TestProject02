using UnityEngine;
using UnityEngine.Events;

public class EventHandler : MonoBehaviour
{
    public delegate void OnChangeAnimation(string animationType, bool isNegativeMoveDirection);
    public OnChangeAnimation onChangeAnimation;

    public delegate void OnAddUpdateComponent(IUpdateType updateType);
    public OnAddUpdateComponent onAddUpdateComponent;
    public OnAddUpdateComponent onRemoveUpdateComponent;

    public UnityAction onDisablePlayer;

    public UnityAction onPlayerAttack;
    public UnityAction onPlayerGetDamage;
    public UnityAction onPlayerRevertDamage;

    public UnityAction onEnemyDie;
    public UnityAction onAddEnemy;

    public UnityAction onShowDieScreen;

}
