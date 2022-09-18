using MiniclipTest.Game.Piece;

namespace MiniclipTest.Game.Events
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