using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Pieces
{
    internal class CastlePiece : ChessPieceBase
    {
        public CastlePiece((int x, int y) position, bool isWhite, string type = "Rook") : base(position, isWhite, type)
        {
            // Base constructor already handles the initialization
        }

        // Override GetPossibleMoves to implement castle-specific movement logic
        public override List<(int x, int y)> GetPossibleMoves()
        {
            PossibleMoves.Clear();
            int x = Position.x;
            int y = Position.y;
            // Castles (Rooks) move horizontally and vertically, so we need to check all four directions
            for (int i = 1; i < 8; i++)
            {
                // Move right
                if (x + i < 8)
                    PossibleMoves.Add((x + i, y));
                // Move left
                if (x - i >= 0)
                    PossibleMoves.Add((x - i, y));
                // Move up
                if (y + i < 8)
                    PossibleMoves.Add((x, y + i));
                // Move down
                if (y - i >= 0)
                    PossibleMoves.Add((x, y - i));
            }
            return PossibleMoves;
        }
    }
}
