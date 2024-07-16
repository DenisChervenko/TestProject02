using UnityEngine;
using Zenject;

public class AnimationHandler : MonoBehaviour
{
    // index 0 - idle | index 1 - walk | index 2 - punch | index 3 - kick | index 4 - jab
    [SerializeField] private GameObject[] _interactAnimationBundle;
    [SerializeField] private GameObject _hurtAnimation;
    [Inject] private EventHandler _eventHandler;

    private bool _takeHit = false;
    private void OnAnimationChange(string animationType, bool isNegativeMoveDirection)
    {
        if(_takeHit)
            return;

        int index = 0;
        if(animationType == "Idle")
            index = 0;
        else if(animationType == "Walk")
            index = 1;
        else if(animationType == "Punch")
            index = 2;
        else if(animationType == "Kick")
            index = 3;
        else if(animationType == "Jab")
            index = 4;

        for(int i = 0; i < _interactAnimationBundle.Length; i++)
        {
            if(i == index)
            {
                _interactAnimationBundle[index].SetActive(true);

                if(isNegativeMoveDirection)
                    gameObject.transform.localScale = new Vector2(-1, 1);
                else
                    gameObject.transform.localScale = new Vector2(1, 1);

                continue;
            }

            _interactAnimationBundle[i].SetActive(false);
        }
    }

    private void TakeHit()
    {
        _takeHit = true;
        for(int i = 0; i < _interactAnimationBundle.Length; i++)
            _interactAnimationBundle[i].SetActive(false);
        _hurtAnimation.SetActive(!_hurtAnimation.activeSelf);
    }

    public void RevertHurtAnimation()
    {
        _takeHit = false;
        _hurtAnimation.SetActive(!_hurtAnimation.activeSelf);
        for(int i = 0; i < _interactAnimationBundle.Length; i++)
            _interactAnimationBundle[i].SetActive(true);
    }
    private void OnEnable() 
    {
        _eventHandler.onChangeAnimation += OnAnimationChange;
        _eventHandler.onPlayerGetDamage += TakeHit; 
        _eventHandler.onPlayerRevertDamage += RevertHurtAnimation;
    }
    private void OnDisable(){
        _eventHandler.onChangeAnimation -= OnAnimationChange;
        _eventHandler.onPlayerGetDamage -= TakeHit; 
        _eventHandler.onPlayerRevertDamage -= RevertHurtAnimation;
    }
}
