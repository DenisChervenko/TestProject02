using UnityEngine;
public class AnimationEnemy : MonoBehaviour
{
    [SerializeField] private GameObject[] _interactAnimationBundle;
    
    public void OnAnimationChange(string animationType, bool isNegativeMoveDirection)
    {
        int index = 0;
        if(animationType == "Idle")
            index = 0;
        else if(animationType == "Walk")
            index = 1;
        else if(animationType == "Punch")
            index = 2;

        for(int i = 0; i < _interactAnimationBundle.Length; i++)
        {
            if(i == index)
            {
                _interactAnimationBundle[index].SetActive(true);
                continue;
            }

            _interactAnimationBundle[i].SetActive(false);
        }
    }
}
