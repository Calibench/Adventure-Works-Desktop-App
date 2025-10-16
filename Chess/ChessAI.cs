using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess
{
    internal class ChessAI
    {
        public enum DifficultyLevel
        {
            Easy,
            Medium,
            Hard
        }

        // Allow changing difficulty at runtime by the UI
        public DifficultyLevel Level { get; set; }

        private static readonly Random Rng = new Random();

        public ChessAI(DifficultyLevel level)
        {
            Level = level;
        }

        // Move carrier for the UI call-site: ChessForm.MakeAIMove
        public struct AIMove
        {
            public ChessPieceBase piece;
            public (int x, int y) targetPosition;
            public int score;
        }

        // Entry point called by the UI
        public AIMove CalculateBestMove(ChessBoard chessBoard)
        {
            // Side to move is taken from the board (works for AI as white or black)
            bool aiIsWhite = chessBoard.IsWhiteTurn();

            // Enumerate all legal moves for the side to move
            var legalMoves = GetAllLegalMoves(chessBoard, aiIsWhite);
            if (legalMoves.Count == 0)
            {
                // No legal moves (checkmate or stalemate)
                return default(AIMove);
            }

            switch (Level)
            {
                case DifficultyLevel.Easy:
                    return legalMoves[Rng.Next(legalMoves.Count)];

                case DifficultyLevel.Medium:
                    {
                        AIMove best = default(AIMove);
                        int bestScore = int.MinValue;
                        foreach (var m in legalMoves)
                        {
                            int score = ScoreMoveMedium(chessBoard, m, aiIsWhite);
                            if (score > bestScore)
                            {
                                bestScore = score;
                                best = new AIMove { piece = m.piece, targetPosition = m.targetPosition, score = score };
                            }
                        }
                        return best;
                    }

                case DifficultyLevel.Hard:
                    {
                        // Depth 2 minimax with alpha-beta for responsiveness in WinForms
                        int depth = 2;
                        int alpha = int.MinValue + 1;
                        int beta = int.MaxValue;

                        AIMove best = default(AIMove);
                        int bestScore = int.MinValue;

                        foreach (var m in legalMoves)
                        {
                            var token = ApplyMove(chessBoard, m.piece, m.targetPosition);

                            // Switch side
                            int score = Minimax(chessBoard, depth - 1, alpha, beta, maximizingWhite: !aiIsWhite, aiIsWhite: aiIsWhite);

                            RevertMove(chessBoard, token);

                            if (score > bestScore)
                            {
                                bestScore = score;
                                best = new AIMove { piece = m.piece, targetPosition = m.targetPosition, score = score };
                            }

                            if (score > alpha) alpha = score;
                            if (beta <= alpha) break;
                        }
                        return best;
                    }

                default:
                    return legalMoves[Rng.Next(legalMoves.Count)];
            }
        }

        // ========== Minimax and evaluation ==========

        private int Minimax(ChessBoard b, int depth, int alpha, int beta, bool maximizingWhite, bool aiIsWhite)
        {
            if (depth == 0)
                return EvaluateBoard(b, aiIsWhite);

            var moves = GetAllLegalMoves(b, maximizingWhite);
            if (moves.Count == 0)
            {
                // No legal moves: checkmate or stalemate
                // Use evaluation to reflect terminal score (board state already has check info; use static eval)
                return EvaluateBoard(b, aiIsWhite);
            }

            if (maximizingWhite)
            {
                int value = int.MinValue;
                foreach (var m in moves)
                {
                    var t = ApplyMove(b, m.piece, m.targetPosition);
                    int child = Minimax(b, depth - 1, alpha, beta, !maximizingWhite, aiIsWhite);
                    RevertMove(b, t);

                    if (child > value) value = child;
                    if (value > alpha) alpha = value;
                    if (alpha >= beta) break;
                }
                return value;
            }
            else
            {
                int value = int.MaxValue;
                foreach (var m in moves)
                {
                    var t = ApplyMove(b, m.piece, m.targetPosition);
                    int child = Minimax(b, depth - 1, alpha, beta, !maximizingWhite, aiIsWhite);
                    RevertMove(b, t);

                    if (child < value) value = child;
                    if (value < beta) beta = value;
                    if (alpha >= beta) break;
                }
                return value;
            }
        }

        private int EvaluateBoard(ChessBoard b, bool perspectiveWhite)
        {
            // Material evaluation
            int material = 0;
            for (int x = 0; x < 8; x++)
            {
                for (int y = 0; y < 8; y++)
                {
                    var p = b.Board[x, y];
                    if (p == null) continue;
                    int val = GetPieceValue(p);
                    material += p.IsWhite ? val : -val;
                }
            }

            // Mobility (legal moves) small bonus/penalty
            int mobility = 0;
            int originalTurn = b.turnCount;

            // Perspective mobility
            int myMoves = GetAllLegalMoves(b, perspectiveWhite).Count;
            int oppMoves = GetAllLegalMoves(b, !perspectiveWhite).Count;

            mobility = (myMoves - oppMoves) * 2;

            // Restore original turn
            b.turnCount = originalTurn;

            int score = material + mobility;
            // Make score from perspective
            return perspectiveWhite ? score : -score;
        }

        private int GetPieceValue(ChessPieceBase p)
        {
            // Type names: note rook appears as "Rook" in piece class and "Castle" elsewhere.
            switch (p.Type)
            {
                case "Pawn": return 100;
                case "Knight": return 320;
                case "Bishop": return 330;
                case "Rook":
                case "Castle": return 500;
                case "Queen": return 900;
                case "King": return 20000; // For evaluation only
                default: return 0;
            }
        }

        private int ScoreMoveMedium(ChessBoard b, AIMove move, bool aiIsWhite)
        {
            int score = 0;

            // Immediate capture value
            var targetPiece = b.Board[move.targetPosition.x, move.targetPosition.y];
            if (targetPiece != null)
                score += GetPieceValue(targetPiece);

            // Prefer centralization a bit
            score += CenterBonus(move.targetPosition);

            // Simulate the move and check immediate recapture risk
            var token = ApplyMove(b, move.piece, move.targetPosition);

            // Opponent replies: if they can capture our moved piece on its new square, penalize
            int originalTurn = b.turnCount;
            // Opponent is now the other color
            var oppMoves = GetAllLegalMoves(b, !aiIsWhite);
            foreach (var om in oppMoves)
            {
                if (om.targetPosition.x == move.targetPosition.x && om.targetPosition.y == move.targetPosition.y)
                {
                    score -= GetPieceValue(move.piece);
                    break;
                }
            }

            // Mobility small factor after move
            int myMovesAfter = GetAllLegalMoves(b, aiIsWhite).Count;
            int oppMovesAfter = GetAllLegalMoves(b, !aiIsWhite).Count;
            score += (myMovesAfter - oppMovesAfter);

            b.turnCount = originalTurn;
            RevertMove(b, token);

            // Make score from perspective (already from AI side; no flip needed)
            return score;
        }

        private int CenterBonus((int x, int y) pos)
        {
            // Encourage central squares slightly
            int dx = Math.Min(pos.x, 7 - pos.x);
            int dy = Math.Min(pos.y, 7 - pos.y);
            int distToEdge = Math.Min(dx, dy);
            return distToEdge; // 0..3 small bonus
        }

        // ========== Move generation leveraging board's own rules ==========

        private List<AIMove> GetAllLegalMoves(ChessBoard b, bool forWhite)
        {
            var moves = new List<AIMove>(64);

            int originalTurn = b.turnCount;
            ChessPieceBase originalSelected = b.SelectedPiece;

            // Force turn parity to the requested color for selection
            SetTurn(b, forWhite);

            for (int x = 0; x < 8; x++)
            {
                for (int y = 0; y < 8; y++)
                {
                    var piece = b.Board[x, y];
                    if (piece == null || piece.IsWhite != forWhite) continue;

                    if (b.SelectPiece((x, y)))
                    {
                        var legals = b.ValidMovesForSelected;
                        for (int i = 0; i < legals.Count; i++)
                        {
                            moves.Add(new AIMove
                            {
                                piece = piece,
                                targetPosition = legals[i],
                                score = 0
                            });
                        }
                    }
                }
            }

            // Restore selection and original turn
            b.SelectedPiece = originalSelected;
            b.turnCount = originalTurn;

            return moves;
        }

        private void SetTurn(ChessBoard b, bool whiteTurn)
        {
            bool current = (b.turnCount % 2) == 0;
            if (current != whiteTurn)
            {
                // Flip a single ply; avoid calling NextTurn to prevent side effects
                b.turnCount++;
            }
        }

        // ========== Lightweight simulation (no side effects to game state) ==========

        private struct MoveToken
        {
            public ChessPieceBase Piece;
            public (int x, int y) From;
            public (int x, int y) To;
            public ChessPieceBase Captured;
            public bool PrevHasMoved;
        }

        private MoveToken ApplyMove(ChessBoard b, ChessPieceBase piece, (int x, int y) target)
        {
            var token = new MoveToken
            {
                Piece = piece,
                From = piece.Position,
                To = target,
                Captured = b.Board[target.x, target.y],
                PrevHasMoved = piece.HasMoved
            };

            // Update board occupancy
            b.Board[token.From.x, token.From.y] = null;
            b.Board[target.x, target.y] = piece;

            // Update piece state
            piece.Position = target;
            piece.HasMoved = true;

            return token;
        }

        private void RevertMove(ChessBoard b, MoveToken token)
        {
            // Revert board
            b.Board[token.From.x, token.From.y] = token.Piece;
            b.Board[token.To.x, token.To.y] = token.Captured;

            // Revert piece
            token.Piece.Position = token.From;
            token.Piece.HasMoved = token.PrevHasMoved;
        }
    }
}
