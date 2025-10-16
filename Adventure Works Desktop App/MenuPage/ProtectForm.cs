using System;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Adventure_Works_Desktop_App.MenuPage
{
    public partial class ProtectForm : Form
    {
        private int[] code = new int[4];
        private int currIndex = 0;
        private const int MAX_CODE_LENGTH = 4;
        private static readonly int[] correctClickerCode = DecodeCode(Properties.Resources.CodeKey);
        private static readonly int[] correctTicTacCode = new int[] { 8, 2, 7, 8 };
        private static readonly int[] correctChessCode = new int[] { 1, 2, 3, 4 };

        public enum CustomDialogResult
        {
            None = 0,
            OK = 1,
            Cancel = 2,
            Clicker = 101,
            TicTac = 102,
            Chess = 103
        }

        public CustomDialogResult MyDialogResult { get; private set; } = CustomDialogResult.None;

        public ProtectForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Handles the event of adding a code number from a button click.
        /// </summary>
        /// <remarks>This method processes the button click event by extracting a numeric code from the
        /// button's tag. If the code reaches the maximum length and is valid, it triggers the <see cref="CorrectCode"/>
        /// method. If the code is invalid, it clears the current code and displays an error message.</remarks>
        /// <param name="sender">The source of the event, expected to be a <see cref="Button"/>.</param>
        /// <param name="e">The event data.</param>
        private void AddCode(object sender, EventArgs e)
        {
            incorrectCodeLabel.Visible = false;

            // not a button, return
            if (!(sender is Button numberButton))
            {
                return;
            }

            // tag isnt a string for some reason, return
            if (!(numberButton.Tag is string tagString))
            {
                return;
            }

            // tag can not be parsed to a number, return
            if (!int.TryParse(tagString, out int number))
            {
                return;
            }

            code[currIndex] = number;
            currIndex++;

            // code is not the max length, return
            if (currIndex != MAX_CODE_LENGTH)
            {
                return;
            }

            // code is not valid
            if (!CheckValidCode())
            {
                Array.Clear(code, 0, code.Length);
                currIndex = 0;
                incorrectCodeLabel.Visible = true;
                return;
            }

            CorrectCode();
        }

        /// <summary>
        /// Determines whether the current code matches the correct code sequence.
        /// </summary>
        /// <returns><see langword="true"/> if the current code is identical to the correct code sequence; otherwise, <see
        /// langword="false"/>.</returns>
        private bool CheckValidCode()
        {
            if (code.SequenceEqual(correctClickerCode))
            {
                MyDialogResult = CustomDialogResult.Clicker;
                return true;
            }
            else if (code.SequenceEqual(correctTicTacCode))
            {
                MyDialogResult = CustomDialogResult.TicTac;
                return true;
            }
            else if (code.SequenceEqual(correctChessCode))
            {
                MyDialogResult = CustomDialogResult.Chess;
                return true;
            }
            return false;
        }

        /// <summary>
        /// Closes the current form.
        /// </summary>
        /// <remarks>Displays a message box indicating a correct code entry and
        /// sets the dialog result to <see cref="DialogResult.OK"/> to signal successful completion.</remarks>
        private void CorrectCode()
        {
            MessageBox.Show("CORRECT CODE");
            this.Close();
        }

        // Helper Method
        /// <summary>
        /// Decodes a Base64-encoded string into an array of integers.
        /// </summary>
        /// <param name="encoded">The Base64-encoded string to decode. Must not be null or empty.</param>
        /// <returns>An array of integers representing the decoded characters from the input string.</returns>
        private static int[] DecodeCode(string encoded)
        {
            var decoded = Encoding.UTF8.GetString(Convert.FromBase64String(encoded));
            return decoded.Select(c => int.Parse(c.ToString())).ToArray();
        }
    }
}
