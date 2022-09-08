using Blazewing.DataEvent;
using MiniclipTrick.Game.Events;
using MiniclipTrick.Game.Pause;
using UnityEngine;

[RequireComponent(typeof(HeaderView))]
public class HeaderController : MonoBehaviour
{ 
    private void OnEnable()
    {
        DataEvent.Register<OnPieceLostEvent>(OnPieceLost);
    }

    private void OnDisable()
    {
        DataEvent.Unregister<OnPieceLostEvent>(OnPieceLost);
    }

    private void OnPieceLost(OnPieceLostEvent eventData)
    {
        Hearts--;
    }

    private void Awake()
    {
        _view = GetComponent<HeaderView>();
    }

    private void Start()
    {
        _view.onPauseButtonClick += OnPauseButtonClick;
        Hearts = 5;
        
        _view.Init(5);
    }

    private void OnPauseButtonClick()
    {
        DataEvent.Notify(new OnPauseEvent(true));
        PauseController.Show();
    }
    
    private HeaderView _view;
    private int _hearts;
    
    public int Hearts
    {
        get => _hearts;
        set
        {
            _hearts = value;
            _view.Hearts = _hearts;
        }
    }
}
