using System;
using System.Numerics;

namespace Clicker.ClickerPages.Backend
{
    internal sealed class UpgradeDefinition
    {
        public string Key { get; }
        public string DisplayName { get; }
        public string SubtextRight { get; }
        public Func<int, BigInteger> CostFunc { get; }
        public int ClickPowerPerLevel { get; }
        public int PassivePerSecondPerLevel { get; }

        public UpgradeDefinition(
            string key,
            string displayName,
            string subtextRight,
            Func<int, BigInteger> costFunc,
            int clickPowerPerLevel,
            int passivePerSecondPerLevel)
        {
            if (string.IsNullOrWhiteSpace(key)) throw new ArgumentNullException(nameof(key));
            if (costFunc == null) throw new ArgumentNullException(nameof(costFunc));

            Key = key;
            DisplayName = displayName ?? key;
            SubtextRight = subtextRight ?? string.Empty;
            CostFunc = costFunc;
            ClickPowerPerLevel = clickPowerPerLevel;
            PassivePerSecondPerLevel = passivePerSecondPerLevel;
        }
    }
}
