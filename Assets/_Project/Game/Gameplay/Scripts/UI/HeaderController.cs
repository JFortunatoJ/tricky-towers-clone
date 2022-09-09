using Blazewing;
using MiniclipTrick.Game;
using MiniclipTrick.Game.Events;
using MiniclipTrick.Game.Pause;
using UnityEngine;
using Zenject;

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
    
    [Inject]
    public void Construct(LevelSettings settings)
    {
        _view = GetComponent<HeaderView>();
        _view.onPauseButtonClick += OnPauseButtonClick;
        _settings = settings;
        
        if (!_settings.againstCPU)
        {
            Hearts = _settings.piecesLostToGameOver;
            _view.Init(Hearts);
        }
    }

    private void OnPieceLost(OnPieceLostEvent eventData)
    {
        Hearts--;
    }

    private void OnPauseButtonClick()
    {
        DataEvent.Notify(new OnPauseEvent(true));
        PauseController.Show();
    }
    
    private LevelSettings _settings;
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
