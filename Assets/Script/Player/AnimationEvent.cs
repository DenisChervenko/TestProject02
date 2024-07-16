using UnityEngine;
using Zenject;

public class AnimationEvent : MonoBehaviour
{
    [Inject] private EventHandler _eventHandler;

    public void OnRevertPlayerHurtAnimation() => _eventHandler.onPlayerRevertDamage?.Invoke();
    public void AttackPlayer() => _eventHandler.onPlayerAttack?.Invoke();
}
