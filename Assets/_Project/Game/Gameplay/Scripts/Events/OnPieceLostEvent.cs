using MiniclipTrick.Game.Piece;

namespace MiniclipTrick.Game.Events
{
    public struct OnPieceLostEvent
    {
        public PieceController piece;

        public OnPieceLostEvent(PieceController piece)
        {
            this.piece = piece;
        }
    }
}