using UnityEngine;
using Zenject;

public class DialogueHandler : MonoBehaviour
{
    [SerializeField] private CanvasGroup _dialogueScreen;
    [SerializeField] private CanvasGroup _winScreen;

    [SerializeField] private GameObject[] _playerWord;
    [SerializeField] private GameObject[] _npcWord;

    private int _currentIndexWord = 0;

    [Inject] private EventHandler _eventHandler;

    public void NextDialogue()
    {
        _currentIndexWord++;

        if(_currentIndexWord == _playerWord.Length)
        {
            _winScreen.alpha = 1;
            _winScreen.interactable = false;
            _dialogueScreen.alpha = 0;
            return;
        }

        for(int i = 0; i < _playerWord.Length; i++)
        {
            _playerWord[i].SetActive(false);
            _npcWord[i].SetActive(false);

            _playerWord[_currentIndexWord].SetActive(true);
            _npcWord[_currentIndexWord].SetActive(true);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            _dialogueScreen.alpha = 1;
            _dialogueScreen.blocksRaycasts = true;
            _dialogueScreen.interactable = true;  
            _eventHandler.onDisablePlayer?.Invoke();
        }
    }
}
