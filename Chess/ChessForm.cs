using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Chess
{
    public partial class ChessForm : Form
    {
        private ChessBoard chessBoard;
        private Button[,] buttonGrid;
        private Color lightSquareColor = Color.Wheat;
        private Color darkSquareColor = Color.SaddleBrown;
        private Color highlightColor = Color.LightGreen;
        
        private Label lblStatus;
        private Button btnReset;
        private Label lblCapturedWhite;
        private Label lblCapturedBlack;
        private ListBox moveHistoryList;

        private ChessAI ai;
        private bool playingAgainstAI = false;
        private ChessAI.DifficultyLevel aiDifficulty = ChessAI.DifficultyLevel.Medium;


        public ChessForm()
        {
            InitializeComponent();

            InitializeChessComponents();

            chessBoard.SetChessFormReference(this);

            SetupButtonGrid();
            DrawBoard();
            SetupChessNotation();

            // Call this to ensure all UI elements are correctly updated
            UpdateUI();
        }

        private void InitializeChessComponents()
        {
            // Initialize the chess board
            chessBoard = new ChessBoard();

            SetupAIControls();

            // Create status label
            lblStatus = new Label
            {
                Location = new Point(510, 20),
                Size = new Size(250, 30),
                Font = new Font("Arial", 12, FontStyle.Bold),
                Text = "White's turn"
            };
            this.Controls.Add(lblStatus);

            // Create reset button
            btnReset = new Button
            {
                Location = new Point(510, 60),
                Size = new Size(100, 30),
                Text = "Reset Game"
            };
            btnReset.Click += BtnReset_Click;
            this.Controls.Add(btnReset);

            // Add captured pieces display
            lblCapturedWhite = new Label
            {
                Location = new Point(510, 100),
                Size = new Size(250, 30),
                Text = "Captured White Pieces:"
            };
            this.Controls.Add(lblCapturedWhite);

            lblCapturedBlack = new Label
            {
                Location = new Point(510, 140),
                Size = new Size(250, 30),
                Text = "Captured Black Pieces:"
            };
            this.Controls.Add(lblCapturedBlack);

            // Add move history
            moveHistoryList = new ListBox
            {
                Location = new Point(510, 190),
                Size = new Size(250, 250),
                Font = new Font("Consolas", 10)
            };
            this.Controls.Add(moveHistoryList);

            Label lblHistory = new Label
            {
                Location = new Point(510, 170),
                Size = new Size(100, 20),
                Text = "Move History:"
            };
            this.Controls.Add(lblHistory);
        }

        private void UpdateUI()
        {
            DrawBoard();
            UpdateStatusLabel();
            UpdateCapturedPieces();
            UpdateMoveHistory();
        }

        private void SetupButtonGrid()
        {
            buttonGrid = new Button[8, 8];

            // Map the buttons from the designer to our 2D array for easier access
            for (int x = 0; x < 8; x++)
            {
                for (int y = 0; y < 8; y++)
                {
                    // Get the button from the flowLayoutPanel
                    string buttonName = $"btn{x}{y}";
                    Button button = flowLayoutPanel1.Controls.Find(buttonName, false).FirstOrDefault() as Button;

                    if (button != null)
                    {
                        buttonGrid[x, y] = button;

                        // Add click event handler
                        button.Tag = (x, y); // Store coordinates in Tag property
                        button.Click += Chess_Square_Click;

                        // Set the color pattern
                        button.BackColor = (x + y) % 2 == 0 ? lightSquareColor : darkSquareColor;
                        button.FlatStyle = FlatStyle.Flat;
                    }
                }
            }
        }

        private void SetupChessNotation()
        {
            // Add file labels (A-H)
            for (int i = 0; i < 8; i++)
            {
                Label lbl = new Label
                {
                    Text = ((char)('A' + i)).ToString(),
                    Location = new Point(30 + i * 60, 495),
                    Size = new Size(20, 20),
                    TextAlign = ContentAlignment.MiddleCenter
                };
                this.Controls.Add(lbl);
            }

            // Add rank labels (1-8)
            for (int i = 0; i < 8; i++)
            {
                Label lbl = new Label
                {
                    Text = (8 - i).ToString(),
                    Location = new Point(0, 30 + i * 60),
                    Size = new Size(20, 20),
                    TextAlign = ContentAlignment.MiddleCenter
                };
                this.Controls.Add(lbl);
            }
        }

        private void SetupAIControls()
        {
            // Create AI controls panel
            Panel aiPanel = new Panel
            {
                Location = new Point(510, 450),
                Size = new Size(250, 100)
            };
            this.Controls.Add(aiPanel);

            CheckBox chkPlayAI = new CheckBox
            {
                Text = "Play Against AI",
                Location = new Point(0, 10),
                AutoSize = true
            };
            chkPlayAI.CheckedChanged += (s, e) =>
            {
                playingAgainstAI = chkPlayAI.Checked;
                if (playingAgainstAI)
                {
                    ai = new ChessAI(aiDifficulty);
                    // Reset game when switching to AI
                    BtnReset_Click(this, EventArgs.Empty);
                }
            };
            aiPanel.Controls.Add(chkPlayAI);

            // Add radio buttons for difficulty
            RadioButton rbEasy = new RadioButton { Text = "Easy", Location = new Point(0, 40), AutoSize = true };
            RadioButton rbMedium = new RadioButton { Text = "Medium", Location = new Point(80, 40), AutoSize = true, Checked = true };
            RadioButton rbHard = new RadioButton { Text = "Hard", Location = new Point(160, 40), AutoSize = true };

            rbEasy.CheckedChanged += (s, e) =>
            {
                if (rbEasy.Checked)
                {
                    aiDifficulty = ChessAI.DifficultyLevel.Easy;
                    if (ai != null) ai.Level = aiDifficulty;
                }
            };
            rbMedium.CheckedChanged += (s, e) =>
            {
                if (rbMedium.Checked)
                {
                    aiDifficulty = ChessAI.DifficultyLevel.Medium;
                    if (ai != null) ai.Level = aiDifficulty;
                }
            };
            rbHard.CheckedChanged += (s, e) =>
            {
                if (rbHard.Checked)
                {
                    aiDifficulty = ChessAI.DifficultyLevel.Hard;
                    if (ai != null) ai.Level = aiDifficulty;
                }
            };

            aiPanel.Controls.Add(rbEasy);
            aiPanel.Controls.Add(rbMedium);
            aiPanel.Controls.Add(rbHard);
        }

        private void Chess_Square_Click(object sender, EventArgs e)
        {
            if (chessBoard.GameOver)
                return;

            Button clickedButton = (Button)sender;
            (int x, int y) position = ((int, int))clickedButton.Tag;

            // If a piece is already selected, try to move it
            if (chessBoard.SelectedPiece != null)
            {
                if (chessBoard.MoveSelectedPiece(position))
                {
                    // Move was successful - update all UI elements
                    UpdateUI();

                    // Only trigger AI move after a successful player move
                    if (playingAgainstAI && !chessBoard.IsWhiteTurn() && !chessBoard.GameOver)
                    {
                        // Slight delay to make AI move visible to player
                        Timer timer = new Timer();
                        timer.Interval = 250;
                        timer.Tick += (s, args) =>
                        {
                            MakeAIMove();
                            timer.Stop();
                            timer.Dispose();
                        };
                        timer.Start();
                    }

                    // maybe update again?
                    //UpdateUI();
                }
                else
                {
                    // Check if we're selecting another piece of the same color
                    ChessPieceBase pieceAtPosition = chessBoard.GetPieceAtPosition(position);
                    if (pieceAtPosition != null && pieceAtPosition.IsWhite == chessBoard.IsWhiteTurn())
                    {
                        // Select the new piece
                        chessBoard.SelectPiece(position);
                        DrawBoard();
                        HighlightValidMoves();
                    }
                }
            }
            else // No piece selected yet, try to select one
            {
                if (chessBoard.SelectPiece(position))
                {
                    DrawBoard();
                    HighlightValidMoves();
                }
            }
        }

        private void DrawBoard()
        {
            // Clear all highlights first
            for (int x = 0; x < 8; x++)
            {
                for (int y = 0; y < 8; y++)
                {
                    buttonGrid[x, y].BackColor = (x + y) % 2 == 0 ? lightSquareColor : darkSquareColor;
                }
            }

            // Draw all pieces
            for (int x = 0; x < 8; x++)
            {
                for (int y = 0; y < 8; y++)
                {
                    ChessPieceBase piece = chessBoard.Board[x, y];
                    if (piece != null)
                    {
                        buttonGrid[x, y].Text = GetPieceSymbol(piece.Type, piece.IsWhite);
                        buttonGrid[x, y].ForeColor = piece.IsWhite ? Color.White : Color.Black;
                        buttonGrid[x, y].Font = new Font("Arial", 20, FontStyle.Bold);
                    }
                    else
                    {
                        buttonGrid[x, y].Text = "";
                    }
                }
            }

            // Highlight the selected piece if any
            if (chessBoard.SelectedPiece != null)
            {
                int x = chessBoard.SelectedPiece.Position.x;
                int y = chessBoard.SelectedPiece.Position.y;
                buttonGrid[x, y].BackColor = highlightColor;
            }
        }

        private void HighlightValidMoves()
        {
            foreach (var move in chessBoard.ValidMovesForSelected)
            {
                buttonGrid[move.x, move.y].BackColor = highlightColor;
            }
        }

        private string GetPieceSymbol(string pieceType, bool isWhite)
        {
            switch (pieceType)
            {
                case "Pawn": return isWhite ? "♙" : "♟";
                case "Castle": return isWhite ? "♖" : "♜";
                case "Knight": return isWhite ? "♘" : "♞";
                case "Bishop": return isWhite ? "♗" : "♝";
                case "Queen": return isWhite ? "♕" : "♛";
                case "King": return isWhite ? "♔" : "♚";
                default: return "?";
            }
        }

        private void UpdateStatusLabel()
        {
            if (chessBoard.GameOver)
            {
                lblStatus.Text = chessBoard.GameResult;
            }
            else
            {
                lblStatus.Text = chessBoard.IsWhiteTurn() ? "White's turn" : "Black's turn";
            }
        }

        private void UpdateCapturedPieces()
        {
            // Update white captured pieces display
            StringBuilder whiteCaptured = new StringBuilder("Captured White Pieces: ");
            foreach (var piece in chessBoard.CapturedWhitePieces)
            {
                whiteCaptured.Append(GetPieceSymbol(piece.Type, true) + " ");
            }
            lblCapturedWhite.Text = whiteCaptured.ToString();

            // Update black captured pieces display
            StringBuilder blackCaptured = new StringBuilder("Captured Black Pieces: ");
            foreach (var piece in chessBoard.CapturedBlackPieces)
            {
                blackCaptured.Append(GetPieceSymbol(piece.Type, false) + " ");
            }
            lblCapturedBlack.Text = blackCaptured.ToString();
        }

        private void UpdateMoveHistory()
        {
            moveHistoryList.Items.Clear();

            // Display moves in pairs (white and black)
            for (int i = 0; i < chessBoard.MoveHistory.Count; i += 2)
            {
                string moveEntry = $"{(i / 2) + 1}. {chessBoard.MoveHistory[i]}";

                // Add black's move if it exists
                if (i + 1 < chessBoard.MoveHistory.Count)
                    moveEntry += $" {chessBoard.MoveHistory[i + 1]}";

                moveHistoryList.Items.Add(moveEntry);
            }

            // Ensure the latest move is visible
            if (moveHistoryList.Items.Count > 0)
                moveHistoryList.SelectedIndex = moveHistoryList.Items.Count - 1;
        }

        public string ShowPromotionDialog(bool isWhite)
        {
            Form promotionForm = new Form
            {
                Text = "Promote Pawn",
                Size = new Size(300, 150),
                FormBorderStyle = FormBorderStyle.FixedDialog,
                StartPosition = FormStartPosition.CenterParent
            };

            Button btnQueen = new Button { Text = "Queen", Size = new Size(60, 30), Location = new Point(10, 50) };
            Button btnRook = new Button { Text = "Rook", Size = new Size(60, 30), Location = new Point(80, 50) };
            Button btnBishop = new Button { Text = "Bishop", Size = new Size(60, 30), Location = new Point(150, 50) };
            Button btnKnight = new Button { Text = "Knight", Size = new Size(60, 30), Location = new Point(220, 50) };

            string result = "Queen"; // Default

            btnQueen.Click += (s, e) => { result = "Queen"; promotionForm.Close(); };
            btnRook.Click += (s, e) => { result = "Rook"; promotionForm.Close(); };
            btnBishop.Click += (s, e) => { result = "Bishop"; promotionForm.Close(); };
            btnKnight.Click += (s, e) => { result = "Knight"; promotionForm.Close(); };

            promotionForm.Controls.Add(btnQueen);
            promotionForm.Controls.Add(btnRook);
            promotionForm.Controls.Add(btnBishop);
            promotionForm.Controls.Add(btnKnight);

            promotionForm.ShowDialog(this);

            return result;
        }

        private void MakeAIMove()
        {
            // Get AI move
            var aiMove = ai.CalculateBestMove(chessBoard);
            if (aiMove.piece != null)
            {
                // Select the piece and move it
                chessBoard.SelectPiece(aiMove.piece.Position);
                chessBoard.MoveSelectedPiece(aiMove.targetPosition);

                // Update UI
                UpdateUI();
            }
        }

        private void BtnReset_Click(object sender, EventArgs e)
        {
            chessBoard.ResetBoard();
            UpdateUI();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
