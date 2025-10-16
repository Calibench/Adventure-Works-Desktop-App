using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess
{
    internal class ChessPieceBase
    {
        // Current position of the piece on the board
        public (int x, int y) Position { get; set; }
        // Color of the piece (true for white, false for black)
        public bool IsWhite { get; set; }
        // Check if the piece is captured
        public bool IsCaptured { get; set; } = false;
        // Type of the piece (e.g., "Pawn", "Rook", "Knight", etc.)
        public string Type { get; set; }
        // Indicates if the piece has moved (useful for special moves like castling or pawn's first move)
        public bool HasMoved { get; set; }
        // Possible moves for the piece (can be populated based on game rules)
        public List<(int x, int y)> PossibleMoves { get; set; } = new List<(int x, int y)>();
        // Constructor to initialize the piece
        public ChessPieceBase((int x, int y) position, bool isWhite, string type)
        {
            Position = position;
            IsWhite = isWhite;
            Type = type;
            HasMoved = false;
            IsCaptured = false;
            PossibleMoves = new List<(int x, int y)>();
        }

        // Method to move the piece to a new position
        public void Move((int x, int y) newPosition)
        {
            Position = newPosition;
            HasMoved = true;
        }

        // Method to capture the piece
        public void Capture()
        {
            IsCaptured = true;
        }

        // Method to reset the piece (e.g., when resetting the game)
        public void Reset((int x, int y) startPosition)
        {
            Position = startPosition;
            IsCaptured = false;
            HasMoved = false;
            PossibleMoves.Clear();
        }

        // Get possible moves (stub method, to be implemented based on piece type)
        public virtual List<(int x, int y)> GetPossibleMoves()
        {
            // This method should be overridden in derived classes to provide specific move logic
            return PossibleMoves;
        }
    }
}
