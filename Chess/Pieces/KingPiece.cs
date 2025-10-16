using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Pieces
{
    internal class KingPiece : ChessPieceBase
    {
        public KingPiece((int x, int y) position, bool isWhite, string type = "King") : base(position, isWhite, type)
        {
            // Base constructor already handles the initialization
        }

        // Override GetPossibleMoves to implement king-specific movement logic
        public override List<(int x, int y)> GetPossibleMoves()
        {
            PossibleMoves.Clear();
            int x = Position.x;
            int y = Position.y;
            // Kings move one square in any direction, so we need to check all adjacent squares
            for (int dx = -1; dx <= 1; dx++)
            {
                for (int dy = -1; dy <= 1; dy++)
                {
                    if (dx == 0 && dy == 0)
                        continue; // Skip the current position
                    int newX = x + dx;
                    int newY = y + dy;
                    // Ensure the new position is within the bounds of the board
                    if (newX >= 0 && newX < 8 && newY >= 0 && newY < 8)
                    {
                        PossibleMoves.Add((newX, newY));
                    }
                }
            }
            return PossibleMoves;
        }

        // Additional method to check if the king is in check can be implemented here
        public bool IsInCheck((int x, int y) kingPosition, List<ChessPieceBase> opponentPieces)
        {
            foreach (var piece in opponentPieces)
            {
                var opponentMoves = piece.GetPossibleMoves();
                if (opponentMoves.Contains(kingPosition))
                {
                    return true; // King is in check
                }
            }
            return false; // King is not in check
        }
    }
}
