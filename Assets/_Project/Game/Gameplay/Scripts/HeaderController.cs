using Blazewing.DataEvent;
using MiniclipTrick.Game.Events;
using MiniclipTrick.Game.Pause;
using UnityEngine;

[RequireComponent(typeof(HeaderView))]
public class HeaderController : MonoBehaviour
{
    private HeaderView _view;

    private void Awake()
    {
        _view = GetComponent<HeaderView>();
    }

    private void Start()
    {
        _view.onPauseButtonClick += OnPauseButtonClick;
    }

    private void OnPauseButtonClick()
    {
        DataEvent.Notify(new OnPauseEvent(true));
        PauseController.Show();
    }
}
