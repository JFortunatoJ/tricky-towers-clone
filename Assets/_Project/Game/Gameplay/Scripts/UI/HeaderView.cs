using System;
using System.Collections.Generic;
using MiniclipTest.Game.HUD;
using UnityEngine;
using UnityEngine.UI;

public class HeaderView : MonoBehaviour
{
    [SerializeField] private Button _pauseButtton;
    [Space] [SerializeField] private PlayerHeart _heartPrefab;
    [SerializeField] private Transform _heartsParent;

    private int _maxHearts;
    private List<PlayerHeart> _heartsList;

    public Action onPauseButtonClick;

    public int MaxHearts
    {
        private get => _maxHearts;
        set
        {
            _maxHearts = value;
            _heartsList ??= new List<PlayerHeart>();

            for (int i = 0; i < _heartsList.Count; i++)
            {
                Destroy(_heartsList[i].gameObject);
            }

            for (int i = 0; i < _maxHearts; i++)
            {
                _heartsList.Add(Instantiate(_heartPrefab, _heartsParent));
            }
        }
    }

    public int Hearts
    {
        set
        {
            for (int i = 0; i < _maxHearts; i++)
            {
                _heartsList[i].IsFull = i >= _maxHearts - value;
            }
        }
    }


    private void Start()
    {
        _pauseButtton.onClick.AddListener(() => onPauseButtonClick?.Invoke());
    }

    public void Init(int maxHearts)
    {
        MaxHearts = maxHearts;
        Hearts = MaxHearts;
    }
}