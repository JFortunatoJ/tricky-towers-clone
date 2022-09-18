using Blazewing;
using MiniclipTest.Game;
using MiniclipTest.Game.Events;
using MiniclipTest.Game.Pause;
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
    
    private void Start()
    {
        _view = GetComponent<HeaderView>();
        _view.onPauseButtonClick += OnPauseButtonClick;
        
        if (!GameManager.Instance.IsAgainstCPU)
        {
            Hearts = MiniclipTest.Game.GameSettings.Instance.PiecesLostToGameOver;
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
        BaseController.Show(PauseController.SCENE_NAME);
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
