using MiniclipTest.Game.Piece;

namespace MiniclipTest.Game.Events
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