using System;
using System.Collections;
using DG.Tweening;
using TMPro;
using UnityEngine;

namespace MiniclipTest.Game
{
    public class FinishLineController : MonoBehaviour
    {
        private void Start()
        {
            transform.localPosition = new Vector3(0, GameSettings.Instance.FinishLineHeight, 0);
            
            pieceDetectorRay.raySource.y = transform.localPosition.y;
            pieceDetectorRay.onStatusChanged += OnStatusChanged;
            
            _waitOneSecond = new WaitForSeconds(1);
            _countdownText.DOFade(0, 0);
        }

        private void FixedUpdate()
        {
            if(!CanDetect) return;

            pieceDetectorRay.CheckPiece();
        }

        private void OnStatusChanged(bool status)
        {
            if (status)
            {
                if (_countdownCoroutine != null) return;
                
                CanDetect = true;
                OnCountdownStarted?.Invoke();
                _countdownCoroutine = StartCoroutine(CountdownCoroutine());
            }
            else
            {
                CanDetect = false;
                if (_countdownCoroutine == null) return;
                
                _countdownText.DOFade(0, .15f).SetUpdate(true);
                StopCoroutine(_countdownCoroutine);
                _countdownCoroutine = null;
                    
                OnCountdownCanceled?.Invoke();
            }
        }

        private IEnumerator CountdownCoroutine()
        {
            _countdownText.DOFade(1, .25f);
            for (int i = _countdownSeconds; i > 0; i--)
            {
                _countdownText.text = i.ToString();
                yield return _waitOneSecond;
            }

            if (pieceDetectorRay.RayHasPiece)
            {
                OnCountdownComplete?.Invoke();
                CanDetect = false;
                _countdownText.DOFade(0, .15f);
            }
        }


#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawRay(pieceDetectorRay.raySource, Vector3.right * pieceDetectorRay.rayDistance);
        }
#endif
        
        public PieceDetectorRay pieceDetectorRay;
        
        [SerializeField]
        private TextMeshPro _countdownText;

        private readonly int _countdownSeconds = 3;
        private WaitForSeconds _waitOneSecond;
        private Coroutine _countdownCoroutine;
        
        public bool CanDetect { get; set; }

        public float Height
        {
            set
            {
                transform.DOMoveY(value, .5f);
                pieceDetectorRay.raySource.y = value;
            }
        }

        public Action OnCountdownStarted;
        public Action OnCountdownCanceled;
        public Action OnCountdownComplete;
    }
}