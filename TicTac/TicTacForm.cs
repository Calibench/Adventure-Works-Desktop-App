using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TicTac
{
    public partial class TicTacForm : Form
    {
        private TicTac ticTac;

        public TicTacForm()
        {
            InitializeComponent();
            ticTac = new TicTac();
        }

        private bool CheckForWin()
        {
            foreach (var combination in ticTac.winningCombinations)
            {
                if (ticTac.board[combination[0]] != "" &&
                    ticTac.board[combination[0]] == ticTac.board[combination[1]] &&
                    ticTac.board[combination[0]] == ticTac.board[combination[2]])
                {
                    return true;
                }
            }
            return false;
        }

        private void HandleWin()
        {
            ticTac.gameActive = false;
            lblTurn.Text = ticTac.currentPlayer + " Wins!";

            if (ticTac.currentPlayer == "X")
                ticTac.playerScore++;
            else
                ticTac.botScore++;

            UpdateScoreDisplay();
        }

        private void HandleDraw()
        {
            ticTac.gameActive = false;
            lblTurn.Text = "It's a Draw!";
        }

        private void UpdateScoreDisplay()
        {
            // Assuming you have labels for scores
            lblPlayerScore.Text = "Player: " + ticTac.playerScore;
            lblBotScore.Text = "Computer: " + ticTac.botScore;
        }

        private void MakeBotMove()
        {
            if (!ticTac.gameActive || ticTac.availableMoves.Count == 0) return;

            // Simple bot logic - random move
            int moveIndex = ticTac.availableMoves[ticTac.rand.Next(ticTac.availableMoves.Count)];
            Button buttonToClick = GetButtonByIndex(moveIndex);

            // Simulate button click after a short delay
            this.BeginInvoke(new Action(() =>
            {
                System.Threading.Thread.Sleep(500); // Short delay for better UX
                buttonToClick.Text = ticTac.currentPlayer;
                ticTac.board[moveIndex] = ticTac.currentPlayer;
                ticTac.availableMoves.Remove(moveIndex);

                if (CheckForWin())
                {
                    HandleWin();
                    return;
                }

                if (ticTac.availableMoves.Count == 0)
                {
                    HandleDraw();
                    return;
                }

                ticTac.currentPlayer = "X";
                lblTurn.Text = "X's Turn";
            }));
        }

        private Button GetButtonByIndex(int index)
        {
            switch (index)
            {
                case 0: return btnGrid1;
                case 1: return btnGrid2;
                case 2: return btnGrid3;
                case 3: return btnGrid4;
                case 4: return btnGrid5;
                case 5: return btnGrid6;
                case 6: return btnGrid7;
                case 7: return btnGrid8;
                case 8: return btnGrid9;
                default: return null;
            }
        }

        private void btnGrid_Click(object sender, EventArgs e)
        {
            if (!ticTac.gameActive) return;

            Button clickedButton = (Button)sender;
            int buttonIndex = int.Parse(clickedButton.Name.Substring(7)) - 1; // Extract index from btnGrid1, btnGrid2, etc.

            if (string.IsNullOrEmpty(clickedButton.Text))
            {
                // Update button and game state
                clickedButton.Text = ticTac.currentPlayer;
                ticTac.board[buttonIndex] = ticTac.currentPlayer;
                ticTac.availableMoves.Remove(buttonIndex);

                // Check if game is over
                if (CheckForWin())
                {
                    HandleWin();
                    return;
                }

                if (ticTac.availableMoves.Count == 0)
                {
                    HandleDraw();
                    return;
                }

                // Switch players
                ticTac.currentPlayer = ticTac.currentPlayer == "X" ? "O" : "X";
                lblTurn.Text = ticTac.currentPlayer + "'s Turn";

                // If playing against computer and it's O's turn
                if (ticTac.currentPlayer == "O")
                {
                    MakeBotMove();
                }
            }
        }

        private void btnRestart_Click(object sender, EventArgs e)
        {
            // Re-enable all buttons
            btnGrid1.Enabled = true;
            btnGrid2.Enabled = true;
            btnGrid3.Enabled = true;
            btnGrid4.Enabled = true;
            btnGrid5.Enabled = true;
            btnGrid6.Enabled = true;
            btnGrid7.Enabled = true;
            btnGrid8.Enabled = true;
            btnGrid9.Enabled = true;

            // Clear all button texts
            btnGrid1.Text = "";
            btnGrid2.Text = "";
            btnGrid3.Text = "";
            btnGrid4.Text = "";
            btnGrid5.Text = "";
            btnGrid6.Text = "";
            btnGrid7.Text = "";
            btnGrid8.Text = "";
            btnGrid9.Text = "";

            // Reset turn label
            lblTurn.Text = "X's Turn";

            // Reset game state
            ticTac.currentPlayer = "X";
            ticTac.gameActive = true;
            ticTac.availableMoves = new List<int>() { 0, 1, 2, 3, 4, 5, 6, 7, 8 };
            for (int i = 0; i < ticTac.board.Length; i++)
            {
                ticTac.board[i] = "";
            }


        }

        private void Form1_Load(object sender, EventArgs e)
        {
            UpdateScoreDisplay();
        }
    }

    public class TicTac
    {
        public string[] board = new string[9];
        public string[] types = new string[] { "X", "O" };
        public int playerScore = 0;
        public int botScore = 0;
        public string currentPlayer = "X";
        public bool gameActive = true;
        public Random rand = new Random();
        public List<int> availableMoves = new List<int>() { 0, 1, 2, 3, 4, 5, 6, 7, 8 };
        public List<int[]> winningCombinations = new List<int[]>()
        {
            new int[] {0, 1, 2},
            new int[] {3, 4, 5},
            new int[] {6, 7, 8},
            new int[] {0, 3, 6},
            new int[] {1, 4, 7},
            new int[] {2, 5, 8},
            new int[] {0, 4, 8},
            new int[] {2, 4, 6}
        };
        public TicTac()
        {
            for (int i = 0; i < board.Length; i++)
            {
                board[i] = "";
            }
        }
        public string GetCurrentPlayer()
        {
            return currentPlayer;
        }
        public bool IsGameActive()
        {
            return gameActive;
        }
        public string GetBoardPosition(int index)
        {
            return board[index];
        }
    }
}
