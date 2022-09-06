using System;
using UnityEngine;
using UnityEngine.UI;

public class HeaderView : MonoBehaviour
{
    [SerializeField]
    private Button _pauseButtton;

    public Action onPauseButtonClick;

    private void Start()
    {
        _pauseButtton.onClick.AddListener(() => onPauseButtonClick?.Invoke());
    }
}
