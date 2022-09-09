using System;
using System.Collections;
using MiniclipTrick.Game.Piece;
using UnityEngine;
using Random = UnityEngine.Random;

namespace MiniclipTrick.Game.Player
{
    public class AiController : PlayerController
    {
        public override void Init(string playerId)
        {
            base.Init(playerId);

            _waitOneSecond = new WaitForSeconds(.25f);
        }

        protected override bool CanPlay()
        {
            return base.CanPlay() && !_isPlaying;
        }

        protected override void OnPiecePlaced(PieceController piece)
        {
            if(_gameOver) return;
            
            if (_movementCoroutine != null)
            {
                StopCoroutine(_movementCoroutine);
            }
            
            base.OnPiecePlaced(piece);
            
            _movementCoroutine = StartCoroutine(MovePieceCoroutine());
        }

        private IEnumerator MovePieceCoroutine()
        {
            AIPieceController piece = _piecesManager.CurrentPiece as AIPieceController;
            PieceMovement pieceMovement = piece.Movement;

            float targetPosition = Random.Range(-3, 4);
            int direction = targetPosition - pieceMovement.LocalPosition.x < 0 ? -1 : 1;

            float positionDist = Math.Abs(targetPosition - pieceMovement.LocalPosition.x);
            while (positionDist > 0.1f)
            {
                yield return _waitOneSecond;
                pieceMovement.HorizontalStep(direction);
                positionDist = Math.Abs(targetPosition - pieceMovement.LocalPosition.x);
            }

            int max = 0;
            Vector3 targetRotation = Vector3.zero;
            for (int i = 0; i < 4; i++)
            {
                yield return _waitOneSecond;
                int duplicates = piece.RaysWithSameDistance();
                if (duplicates > max)
                {
                    max = duplicates;
                    targetRotation = pieceMovement.LocalEulerAngles;
                }

                piece.Movement.Rotate();
            }

            float rotationDist = Math.Abs(targetRotation.z - pieceMovement.LocalEulerAngles.z);
            while (rotationDist > .1f)
            {
                yield return _waitOneSecond;
                pieceMovement.Rotate();
                rotationDist = Math.Abs(targetRotation.z - pieceMovement.LocalEulerAngles.z);
                print("Dist: " + rotationDist);
            }

            _isPlaying = false;
        }

        private bool _isPlaying;
        private WaitForSeconds _waitOneSecond;
        private Coroutine _movementCoroutine;
    }
}