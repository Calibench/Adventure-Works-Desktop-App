using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Pieces
{
    internal class QueenPiece : ChessPieceBase
    {
        public QueenPiece((int x, int y) position, bool isWhite, string type = "Queen") : base(position, isWhite, type)
        {
            // Base constructor already handles the initialization
        }

        // Override GetPossibleMoves to implement queen-specific movement logic
        public override List<(int x, int y)> GetPossibleMoves()
        {
            PossibleMoves.Clear();
            int x = Position.x;
            int y = Position.y;
            // Queens move both like rooks and bishops, so we need to check all eight directions
            for (int i = 1; i < 8; i++)
            {
                // Horizontal and vertical moves (like a rook)
                if (x + i < 8) PossibleMoves.Add((x + i, y)); // Right
                if (x - i >= 0) PossibleMoves.Add((x - i, y)); // Left
                if (y + i < 8) PossibleMoves.Add((x, y + i)); // Up
                if (y - i >= 0) PossibleMoves.Add((x, y - i)); // Down
                // Diagonal moves (like a bishop)
                if (x + i < 8 && y + i < 8) PossibleMoves.Add((x + i, y + i)); // Top-right
                if (x - i >= 0 && y + i < 8) PossibleMoves.Add((x - i, y + i)); // Top-left
                if (x + i < 8 && y - i >= 0) PossibleMoves.Add((x + i, y - i)); // Bottom-right
                if (x - i >= 0 && y - i >= 0) PossibleMoves.Add((x - i, y - i)); // Bottom-left
            }
            return PossibleMoves;
        }
    }
}
