using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Pieces
{
    internal class PawnPiece : ChessPieceBase
    {
        public PawnPiece((int x, int y) position, bool isWhite, string type = "Pawn") : base(position, isWhite, type)
        {
            // Base constructor already handles the initialization
        }

        // Override GetPossibleMoves to implement pawn-specific movement logic
        public override List<(int x, int y)> GetPossibleMoves()
        {
            PossibleMoves.Clear();
            int x = Position.x;
            int y = Position.y;
            // Pawns move differently based on their color
            int direction = IsWhite ? 1 : -1; // White pawns move "up" (y+1), black pawns move "down" (y-1)
            // Standard one-square move forward
            if (y + direction >= 0 && y + direction < 8)
            {
                PossibleMoves.Add((x, y + direction));
            }
            // Initial two-square move forward
            if (!HasMoved && y + 2 * direction >= 0 && y + 2 * direction < 8)
            {
                PossibleMoves.Add((x, y + 2 * direction));
            }
            // Diagonal captures
            if (x - 1 >= 0 && y + direction >= 0 && y + direction < 8)
            {
                PossibleMoves.Add((x - 1, y + direction));
            }
            if (x + 1 < 8 && y + direction >= 0 && y + direction < 8)
            {
                PossibleMoves.Add((x + 1, y + direction));
            }
            return PossibleMoves;
        }

        // Additional method to handle promotion can be implemented here
        public bool CanPromote()
        {
            return (IsWhite && Position.y == 7) || (!IsWhite && Position.y == 0);
        }
    }
}
