using UnityEngine;
using Zenject;

public class AnimationPlayer : MonoBehaviour, IUpdateType
{
    private bool _isNegativeMoveDirection = false;
    [Inject] private EventHandler _eventHandler;
    public void UpdateMethod()
    {
        string animationType = "Idle";

        if(Input.GetAxis("Horizontal") < 0 || (_isNegativeMoveDirection == true && Input.GetAxis("Horizontal") == 0))
            _isNegativeMoveDirection = true;
        else
            _isNegativeMoveDirection = false;

        if(Input.GetButton("Horizontal") || Input.GetButton("Vertical"))
            animationType = "Walk";
        else if(Input.GetButton("Fire1"))
            animationType = "Punch";
        else if(Input.GetButton("Fire2"))
            animationType = "Kick";
        else if(Input.GetButton("Fire3"))
            animationType = "Jab";  

        _eventHandler.onChangeAnimation?.Invoke(animationType, _isNegativeMoveDirection);
    }
}
