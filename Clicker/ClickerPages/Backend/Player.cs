using System.Numerics;

namespace Clicker.ClickerPages.Backend
{
    internal class Player
    {
        public BigInteger UserPower
        { get; set; }
        public BigInteger Currency
        { get; set; }

        public Player()
        {
            UserPower = 1;
            Currency = 0;
        }
    }
}
