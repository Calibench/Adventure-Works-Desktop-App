using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Pieces
{
    internal class KnightPiece : ChessPieceBase
    {
        public KnightPiece((int x, int y) position, bool isWhite, string type = "Knight") : base(position, isWhite, type)
        {
            // Base constructor already handles the initialization
        }

        // Override GetPossibleMoves to implement knight-specific movement logic
        public override List<(int x, int y)> GetPossibleMoves()
        {
            PossibleMoves.Clear();
            int x = Position.x;
            int y = Position.y;
            // Knights move in an "L" shape: two squares in one direction and then one square perpendicular
            int[,] knightMoves = new int[,]
            {
                { 2, 1 }, { 2, -1 }, { -2, 1 }, { -2, -1 },
                { 1, 2 }, { 1, -2 }, { -1, 2 }, { -1, -2 }
            };
            for (int i = 0; i < knightMoves.GetLength(0); i++)
            {
                int newX = x + knightMoves[i, 0];
                int newY = y + knightMoves[i, 1];
                // Ensure the new position is within the bounds of the board
                if (newX >= 0 && newX < 8 && newY >= 0 && newY < 8)
                {
                    PossibleMoves.Add((newX, newY));
                }
            }
            return PossibleMoves;
        }
    }
}
