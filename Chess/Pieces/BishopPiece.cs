using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Pieces
{
    internal class BishopPiece : ChessPieceBase
    {
        public BishopPiece((int x, int y) position, bool isWhite, string type = "Bishop") : base(position, isWhite, type)
        {
            // Base constructor already handles the initialization
        }

        // Override GetPossibleMoves to implement bishop-specific movement logic
        public override List<(int x, int y)> GetPossibleMoves()
        {
            PossibleMoves.Clear();
            int x = Position.x;
            int y = Position.y;
            // Bishops move diagonally, so we need to check all four diagonal directions
            for (int i = 1; i < 8; i++)
            {
                // Top-right diagonal
                if (x + i < 8 && y + i < 8)
                    PossibleMoves.Add((x + i, y + i));
                // Top-left diagonal
                if (x - i >= 0 && y + i < 8)
                    PossibleMoves.Add((x - i, y + i));
                // Bottom-right diagonal
                if (x + i < 8 && y - i >= 0)
                    PossibleMoves.Add((x + i, y - i));
                // Bottom-left diagonal
                if (x - i >= 0 && y - i >= 0)
                    PossibleMoves.Add((x - i, y - i));
            }
            return PossibleMoves;
        }
    }
}
