using UnityEngine;
using Zenject;

public class EventHandlerInstaller : MonoInstaller
{
    [SerializeField] private EventHandler _eventHandler;
    public override void InstallBindings()
    {
        Container.Bind<EventHandler>().FromInstance(_eventHandler).AsSingle().NonLazy();
    }
}