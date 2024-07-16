using UnityEngine;
using Zenject;
using UnityEngine.SceneManagement;
public class DieScreen : MonoBehaviour
{
    [SerializeField] private CanvasGroup _canvasGroup;
    [Inject] private EventHandler _eventHandler;

    private void ShowDieScreen()
    {
        _canvasGroup.alpha = 1;
        _canvasGroup.blocksRaycasts = true;
        _canvasGroup.interactable = true;

        Time.timeScale = 0;
    }

    public void OnRestartButton()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }

    private void OnEnable() => _eventHandler.onShowDieScreen += ShowDieScreen;
    private void OnDisable() => _eventHandler.onShowDieScreen -= ShowDieScreen;
}
