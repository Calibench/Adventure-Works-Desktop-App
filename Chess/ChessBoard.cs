using Chess.Pieces;
using System;
using System.Collections.Generic;

namespace Chess
{
    internal class ChessBoard
    {
        private const int BoardSize = 8;
        private ChessForm chessFormRef;

        // 8x8 chess board represented as a 2D array for simplicity that will be open to a ChessForm for frontend display
        public ChessPieceBase[,] Board { get; set; } = new ChessPieceBase[BoardSize, BoardSize];
        public int turnCount = 0; // To track whose turn it is, even for white, odd for black

        private (int x, int y)? enPassantTarget = null;

        // Track kings for check/checkmate detection
        private KingPiece whiteKing;
        private KingPiece blackKing;

        // Game state
        public bool GameOver { get; private set; }
        public string GameResult { get; private set; }

        // Selected piece for UI interaction
        public ChessPieceBase SelectedPiece { get; set; }
        public List<(int x, int y)> ValidMovesForSelected { get; private set; } = new List<(int x, int y)>();

        // New properties for tracking captured pieces and move history
        public List<ChessPieceBase> CapturedWhitePieces { get; private set; } = new List<ChessPieceBase>();
        public List<ChessPieceBase> CapturedBlackPieces { get; private set; } = new List<ChessPieceBase>();
        public List<string> MoveHistory { get; private set; } = new List<string>();

        // Constructor to initialize the chess board with pieces in their starting positions
        public ChessBoard()
        {
            InitializeBoard();
        }

        // Method to initialize the chess board with pieces in their starting positions
        public void InitializeBoard()
        {
            // Clear the board
            Board = new ChessPieceBase[BoardSize, BoardSize];
            // Place pawns
            for (int i = 0; i < BoardSize; i++)
            {
                Board[i, 1] = new Pieces.PawnPiece((i, 1), true);  // White pawns
                Board[i, 6] = new Pieces.PawnPiece((i, 6), false); // Black pawns
            }
            // Place rooks (I have them named as castles)
            Board[0, 0] = new Pieces.CastlePiece((0, 0), true);
            Board[7, 0] = new Pieces.CastlePiece((7, 0), true);
            Board[0, 7] = new Pieces.CastlePiece((0, 7), false);
            Board[7, 7] = new Pieces.CastlePiece((7, 7), false);
            // Place knights
            Board[1, 0] = new Pieces.KnightPiece((1, 0), true);
            Board[6, 0] = new Pieces.KnightPiece((6, 0), true);
            Board[1, 7] = new Pieces.KnightPiece((1, 7), false);
            Board[6, 7] = new Pieces.KnightPiece((6, 7), false);
            // Place bishops
            Board[2, 0] = new Pieces.BishopPiece((2, 0), true);
            Board[5, 0] = new Pieces.BishopPiece((5, 0), true);
            Board[2, 7] = new Pieces.BishopPiece((2, 7), false);
            Board[5, 7] = new Pieces.BishopPiece((5, 7), false);
            // Place queens
            Board[3, 0] = new Pieces.QueenPiece((3, 0), true);
            Board[3, 7] = new Pieces.QueenPiece((3, 7), false);
            // Place kings
            whiteKing = new KingPiece((4, 0), true);
            blackKing = new KingPiece((4, 7), false);
            Board[4, 0] = whiteKing;
            Board[4, 7] = blackKing;

            // Reset game state
            turnCount = 0;
            GameOver = false;
            GameResult = string.Empty;
            SelectedPiece = null;
        }

        // Method to reset the board to its initial state
        public void ResetBoard()
        {
            InitializeBoard();

            CapturedWhitePieces.Clear();
            CapturedBlackPieces.Clear();
            MoveHistory.Clear();
        }

        public void SetChessFormReference(ChessForm form)
        {
            chessFormRef = form;
        }

        // Method to get the piece at a specific position
        public ChessPieceBase GetPieceAtPosition((int x, int y) position)
        {
            if (position.x < 0 || position.x >= BoardSize || position.y < 0 || position.y >= BoardSize)
            {
                throw new ArgumentOutOfRangeException("Position is out of bounds.");
            }
            return Board[position.x, position.y];
        }

        // Method to check whose turn it is
        public bool IsWhiteTurn()
        {
            return turnCount % 2 == 0; // Even turns for white, odd turns for black
        }

        // Method to advance to the next turn
        public void NextTurn()
        {
            turnCount++;
            // After each turn, check for check/checkmate/stalemate
            UpdateGameState();
        }

        // Select a piece at the given position
        public bool SelectPiece((int x, int y) position)
        {
            // Check if position is valid
            if (position.x < 0 || position.x >= BoardSize || position.y < 0 || position.y >= BoardSize)
                return false;

            // Get the piece at the position
            ChessPieceBase piece = Board[position.x, position.y];

            // Check if there is a piece and if it's the correct color for the current turn
            if (piece != null && piece.IsWhite == IsWhiteTurn())
            {
                SelectedPiece = piece;
                // Get valid moves considering other pieces and check
                ValidMovesForSelected = GetValidMovesForPiece(piece);
                return true;
            }

            return false;
        }

        // Move selected piece to the specified position if valid
        public bool MoveSelectedPiece((int x, int y) targetPosition)
        {
            // Check if a piece is selected and the move is valid
            if (SelectedPiece != null && ValidMovesForSelected.Contains(targetPosition))
            {
                // Execute the move
                ExecuteMove(SelectedPiece, targetPosition);

                // Clear selection
                SelectedPiece = null;
                ValidMovesForSelected.Clear();

                // Advance to the next turn
                NextTurn();
                return true;
            }

            return false;
        }

        // Get valid moves for a piece considering other pieces and check
        private List<(int x, int y)> GetValidMovesForPiece(ChessPieceBase piece)
        {
            List<(int x, int y)> validMoves = new List<(int x, int y)>();
            List<(int x, int y)> possibleMoves = piece.GetPossibleMoves();

            // Filter moves based on board state (other pieces, check)
            foreach (var move in possibleMoves)
            {
                // Skip if out of bounds
                if (move.x < 0 || move.x >= BoardSize || move.y < 0 || move.y >= BoardSize)
                    continue;

                // Check if the target position has a piece of the same color
                ChessPieceBase targetPiece = Board[move.x, move.y];
                if (targetPiece != null && targetPiece.IsWhite == piece.IsWhite)
                    continue;

                // Handle piece-specific move validation (e.g., path clear for bishops, rooks, queens)
                if (piece is BishopPiece || piece is CastlePiece || piece is QueenPiece)
                {
                    if (!IsPathClear(piece.Position, move))
                        continue;
                }

                // For pawns, validate movement vs. capture
                if (piece is PawnPiece)
                {
                    // For diagonal moves, require an opponent piece for capture
                    if (move.x != piece.Position.x)
                    {
                        if (targetPiece == null) // No piece to capture diagonally
                            continue;
                    }
                    else // For straight moves, require empty space
                    {
                        if (targetPiece != null) // Can't move forward into a piece
                            continue;

                        // For two-square moves, check if the intermediate square is empty
                        if (Math.Abs(move.y - piece.Position.y) == 2)
                        {
                            int midY = (piece.Position.y + move.y) / 2;
                            if (Board[move.x, midY] != null) // Path blocked
                                continue;
                        }
                    }
                }

                // Check if this move would put/leave the king in check
                if (WouldMoveResultInCheck(piece, move))
                    continue;

                // If we got here, the move is valid
                validMoves.Add(move);
            }

            return validMoves;
        }

        // Check if a path is clear between two positions (for sliding pieces)
        private bool IsPathClear((int x, int y) from, (int x, int y) to)
        {
            int dx = Math.Sign(to.x - from.x);
            int dy = Math.Sign(to.y - from.y);
            int x = from.x + dx;
            int y = from.y + dy;

            while (x != to.x || y != to.y)
            {
                if (Board[x, y] != null)
                    return false; // Path is blocked

                x += dx;
                y += dy;
            }

            return true; // Path is clear
        }

        // Check if a move would result in check for the current player
        private bool WouldMoveResultInCheck(ChessPieceBase piece, (int x, int y) move)
        {
            // Save the current state
            ChessPieceBase capturedPiece = Board[move.x, move.y];
            (int x, int y) originalPosition = piece.Position;

            // Execute the move temporarily
            Board[originalPosition.x, originalPosition.y] = null;
            Board[move.x, move.y] = piece;
            piece.Position = move;

            // Check if the king is in check
            bool inCheck = IsKingInCheck(piece.IsWhite);

            // Restore the original state
            piece.Position = originalPosition;
            Board[originalPosition.x, originalPosition.y] = piece;
            Board[move.x, move.y] = capturedPiece;

            return inCheck;
        }

        // Execute a move
        private void ExecuteMove(ChessPieceBase piece, (int x, int y) targetPosition)
        {
            // Record the move in chess notation
            string moveNotation = GenerateMoveNotation(piece, targetPosition);

            // Handle capture
            ChessPieceBase capturedPiece = Board[targetPosition.x, targetPosition.y];
            if (capturedPiece != null)
            {
                capturedPiece.Capture();
                // Add to appropriate captured list
                if (capturedPiece.IsWhite)
                    CapturedWhitePieces.Add(capturedPiece);
                else
                    CapturedBlackPieces.Add(capturedPiece);

                // Indicate capture in notation
                moveNotation += "x";
            }

            // Update board
            Board[piece.Position.x, piece.Position.y] = null;
            Board[targetPosition.x, targetPosition.y] = piece;

            // Store original position for move notation
            (int x, int y) originalPosition = piece.Position;

            // Update piece
            piece.Move(targetPosition);

            // Add move to history
            MoveHistory.Add(moveNotation);

            // En passant target update for pawns
            if (piece is PawnPiece && Math.Abs(targetPosition.y - piece.Position.y) == 2)
            {
                // Record a square behind the pawn as en passant target
                int direction = piece.IsWhite ? 1 : -1;
                enPassantTarget = (targetPosition.x, targetPosition.y - direction);
            }
            else
            {
                enPassantTarget = null;
            }


            // Handle special moves (pawn promotion, castling, en passant)
            HandleSpecialMoves(piece, targetPosition, originalPosition);
        }

        // Handle special chess moves
        private void HandleSpecialMoves(ChessPieceBase piece, (int x, int y) targetPosition, (int x, int y) originalPosition)
        {
            // Pawn promotion
            if (piece is PawnPiece && (targetPosition.y == 0 || targetPosition.y == 7))
            {
                // Call a method that will return the chosen piece type
                string promotionChoice = GetPromotionChoice(piece.IsWhite);
                ChessPieceBase newPiece;

                switch (promotionChoice)
                {
                    case "Queen":
                        newPiece = new QueenPiece(targetPosition, piece.IsWhite);
                        MoveHistory[MoveHistory.Count - 1] += "=Q"; // Add promotion to notation
                        break;
                    case "Rook":
                        newPiece = new CastlePiece(targetPosition, piece.IsWhite);
                        MoveHistory[MoveHistory.Count - 1] += "=R";
                        break;
                    case "Bishop":
                        newPiece = new BishopPiece(targetPosition, piece.IsWhite);
                        MoveHistory[MoveHistory.Count - 1] += "=B";
                        break;
                    case "Knight":
                        newPiece = new KnightPiece(targetPosition, piece.IsWhite);
                        MoveHistory[MoveHistory.Count - 1] += "=N";
                        break;
                    default:
                        newPiece = new QueenPiece(targetPosition, piece.IsWhite);
                        MoveHistory[MoveHistory.Count - 1] += "=Q";
                        break;
                }

                Board[targetPosition.x, targetPosition.y] = newPiece;
            }

            // Castling (simplified implementation)
            if (piece is KingPiece && Math.Abs(targetPosition.x - originalPosition.x) == 2)
            {
                // Update move notation for castling
                if (targetPosition.x > originalPosition.x)
                    MoveHistory[MoveHistory.Count - 1] = "O-O"; // Kingside castling
                else
                    MoveHistory[MoveHistory.Count - 1] = "O-O-O"; // Queenside castling

                int rookX = targetPosition.x > originalPosition.x ? 7 : 0;
                int rookNewX = targetPosition.x > originalPosition.x ? targetPosition.x - 1 : targetPosition.x + 1;

                // Move the rook
                ChessPieceBase rook = Board[rookX, originalPosition.y];
                Board[rookX, originalPosition.y] = null;
                Board[rookNewX, originalPosition.y] = rook;
                rook.Move((rookNewX, originalPosition.y));
            }

            // En passant could be implemented here
            if (piece is PawnPiece &&
                targetPosition.x != originalPosition.x &&
                Board[targetPosition.x, targetPosition.y] == null &&
                enPassantTarget.HasValue &&
                targetPosition == enPassantTarget.Value)
            {
                // Capture the pawn that just moved two squares
                int capturedY = piece.IsWhite ? targetPosition.y - 1 : targetPosition.y + 1;
                ChessPieceBase capturedPawn = Board[targetPosition.x, capturedY];

                // Add to captured pieces list
                if (capturedPawn.IsWhite)
                    CapturedWhitePieces.Add(capturedPawn);
                else
                    CapturedBlackPieces.Add(capturedPawn);

                capturedPawn.Capture();
                Board[targetPosition.x, capturedY] = null;

                // Update notation to show en passant
                MoveHistory[MoveHistory.Count - 1] += " e.p.";
            }

        }

        // Generate chess notation for a move
        private string GenerateMoveNotation(ChessPieceBase piece, (int x, int y) targetPosition)
        {
            string notation = "";

            // Add piece type (except for pawns)
            if (!(piece is PawnPiece))
            {
                switch (piece.Type)
                {
                    case "King": notation += "K"; break;
                    case "Queen": notation += "Q"; break;
                    case "Castle": notation += "R"; break;
                    case "Bishop": notation += "B"; break;
                    case "Knight": notation += "N"; break;
                }
            }

            // Add source position for clarity (always include for pawns capturing)
            if (piece is PawnPiece && targetPosition.x != piece.Position.x)
            {
                notation += (char)('a' + piece.Position.x);
            }

            // Add target position
            notation += (char)('a' + targetPosition.x);
            notation += (targetPosition.y + 1).ToString();

            return notation;
        }

        // This method would be called from the ChessForm
        public string GetPromotionChoice(bool isWhite)
        {
            if (chessFormRef != null)
                return chessFormRef.ShowPromotionDialog(isWhite);

            // Default to Queen if form reference is missing
            return "Queen";
        }

        // Check if a king is in check
        private bool IsKingInCheck(bool isWhiteKing)
        {
            KingPiece king = isWhiteKing ? whiteKing : blackKing;
            var kpos = king.Position;

            for (int x = 0; x < BoardSize; x++)
            {
                for (int y = 0; y < BoardSize; y++)
                {
                    var p = Board[x, y];
                    if (p == null || p.IsWhite == isWhiteKing) continue;

                    foreach (var sq in GetAttackedSquaresBy(p))
                    {
                        if (sq.x == kpos.x && sq.y == kpos.y)
                            return true;
                    }
                }
            }
            return false;
        }

        // Return squares a piece ATTACKS (not where it can legally move)
        // Used only for check detection.
        private IEnumerable<(int x, int y)> GetAttackedSquaresBy(ChessPieceBase piece)
        {
            var attacked = new List<(int x, int y)>(14);
            int x = piece.Position.x;
            int y = piece.Position.y;

            bool InBounds(int ix, int iy) => ix >= 0 && ix < BoardSize && iy >= 0 && iy < BoardSize;

            if (piece is PawnPiece)
            {
                int dir = piece.IsWhite ? 1 : -1;
                int ax1 = x - 1, ay1 = y + dir;
                int ax2 = x + 1, ay2 = y + dir;
                if (InBounds(ax1, ay1)) attacked.Add((ax1, ay1));
                if (InBounds(ax2, ay2)) attacked.Add((ax2, ay2));
                return attacked;
            }

            if (piece is KnightPiece)
            {
                int[] dx = { -2, -2, -1, -1, 1, 1, 2, 2 };
                int[] dy = { -1, 1, -2, 2, -2, 2, -1, 1 };
                for (int i = 0; i < 8; i++)
                {
                    int nx = x + dx[i], ny = y + dy[i];
                    if (InBounds(nx, ny)) attacked.Add((nx, ny));
                }
                return attacked;
            }

            if (piece is KingPiece)
            {
                for (int dx = -1; dx <= 1; dx++)
                {
                    for (int dy = -1; dy <= 1; dy++)
                    {
                        if (dx == 0 && dy == 0) continue;
                        int nx = x + dx, ny = y + dy;
                        if (InBounds(nx, ny)) attacked.Add((nx, ny));
                    }
                }
                return attacked;
            }

            // Sliding pieces: Rook, Bishop, Queen
            var directions = new List<(int dx, int dy)>();
            if (piece is BishopPiece || piece is QueenPiece)
            {
                directions.AddRange(new[] { (1, 1), (1, -1), (-1, 1), (-1, -1) });
            }
            if (piece is CastlePiece || piece is QueenPiece)
            {
                directions.AddRange(new[] { (1, 0), (-1, 0), (0, 1), (0, -1) });
            }

            foreach (var d in directions)
            {
                int nx = x + d.dx, ny = y + d.dy;
                while (InBounds(nx, ny))
                {
                    attacked.Add((nx, ny));
                    if (Board[nx, ny] != null) break; // stop at first blocker
                    nx += d.dx; ny += d.dy;
                }
            }

            return attacked;
        }

        // Check for checkmate or stalemate
        private bool IsCheckmate(bool isWhiteKing)
        {
            // If the king is not in check, it can't be checkmate
            if (!IsKingInCheck(isWhiteKing))
                return false;

            // Check if any piece of the same color can make a move that gets out of check
            for (int x = 0; x < BoardSize; x++)
            {
                for (int y = 0; y < BoardSize; y++)
                {
                    ChessPieceBase piece = Board[x, y];
                    if (piece != null && piece.IsWhite == isWhiteKing)
                    {
                        List<(int x, int y)> validMoves = GetValidMovesForPiece(piece);
                        if (validMoves.Count > 0)
                            return false; // At least one valid move exists
                    }
                }
            }

            return true; // No valid moves, it's checkmate
        }

        // Check for stalemate
        private bool IsStalemate(bool isWhiteTurn)
        {
            // If the king is in check, it's not stalemate
            if (IsKingInCheck(isWhiteTurn))
                return false;

            // Check if any piece of the current player can make a valid move
            for (int x = 0; x < BoardSize; x++)
            {
                for (int y = 0; y < BoardSize; y++)
                {
                    ChessPieceBase piece = Board[x, y];
                    if (piece != null && piece.IsWhite == isWhiteTurn)
                    {
                        List<(int x, int y)> validMoves = GetValidMovesForPiece(piece);
                        if (validMoves.Count > 0)
                            return false; // At least one valid move exists
                    }
                }
            }

            return true; // No valid moves, it's stalemate
        }

        // Update the game state (check for check, checkmate, stalemate)
        private void UpdateGameState()
        {
            bool currentPlayerIsWhite = IsWhiteTurn();

            bool inCheck = IsKingInCheck(currentPlayerIsWhite);

            if (IsCheckmate(currentPlayerIsWhite))
            {
                GameOver = true;
                GameResult = currentPlayerIsWhite ? "Black wins by checkmate" : "White wins by checkmate";

                // Add checkmate notation
                if (MoveHistory.Count > 0)
                    MoveHistory[MoveHistory.Count - 1] += "#";
            }
            else if (IsStalemate(currentPlayerIsWhite))
            {
                GameOver = true;
                GameResult = "Game drawn by stalemate";
            }
            else if (inCheck)
            {
                // Add check notation
                if (MoveHistory.Count > 0)
                    MoveHistory[MoveHistory.Count - 1] += "+";
            }
            // Additional draw conditions could be added here (e.g., threefold repetition, 50-move rule)
        }
    }
}
