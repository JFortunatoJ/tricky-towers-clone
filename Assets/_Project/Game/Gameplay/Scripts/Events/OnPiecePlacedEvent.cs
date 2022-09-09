using MiniclipTrick.Game.Piece;

namespace MiniclipTrick.Game.Events
{
    public struct OnPiecePlacedEvent
    {
        public PieceController piece;

        public OnPiecePlacedEvent(PieceController piece)
        {
            this.piece = piece;
        }
    }
}