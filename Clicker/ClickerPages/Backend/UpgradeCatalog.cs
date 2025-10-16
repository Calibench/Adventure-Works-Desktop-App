using System;
using System.Collections.Generic;
using System.Numerics;

namespace Clicker.ClickerPages.Backend
{
    internal static class UpgradeCatalog
    {
        public static readonly List<UpgradeDefinition> All = new List<UpgradeDefinition>
        {
            // Click Power: +1 per click per level
            new UpgradeDefinition(
                key: UpgradeTypes.ClickPower,
                displayName: "Click Power",
                subtextRight: "(+1 per click)",
                costFunc: level =>
                {
                    double baseCost = 10.0;
                    double cost = baseCost * Math.Pow(1.15, level);
                    return new BigInteger(Math.Ceiling(cost));
                },
                clickPowerPerLevel: 1,
                passivePerSecondPerLevel: 0
            ),

            // Auto Clicker: +1 per second per level
            new UpgradeDefinition(
                key: UpgradeTypes.AutoClicker,
                displayName: "Auto Clicker",
                subtextRight: "(+1/s)",
                costFunc: level =>
                {
                    double baseCost = 50.0;
                    double cost = baseCost * Math.Pow(1.17, level);
                    return new BigInteger(Math.Ceiling(cost));
                },
                clickPowerPerLevel: 0,
                passivePerSecondPerLevel: 1
            ),

            // Mega Click: +5 per click per level
            new UpgradeDefinition(
                key: UpgradeTypes.MegaClick,
                displayName: "Mega Click",
                subtextRight: "(+5 per click)",
                costFunc: level =>
                {
                    double baseCost = 250.0;
                    double growth = 1.25;
                    double cost = baseCost * Math.Pow(growth, level);
                    return new BigInteger(Math.Ceiling(cost));
                },
                clickPowerPerLevel: 5,
                passivePerSecondPerLevel: 0
            ),

            // Grandma: +5/s per level (entry-level passive)
            new UpgradeDefinition(
                key: UpgradeTypes.Grandma,
                displayName: "Grandma",
                subtextRight: "(+5/s)",
                costFunc: level =>
                {
                    double baseCost = 1000.0;
                    double growth = 1.20;
                    double cost = baseCost * Math.Pow(growth, level);
                    return new BigInteger(Math.Ceiling(cost));
                },
                clickPowerPerLevel: 0,
                passivePerSecondPerLevel: 5
            ),

            // Factory: +25/s per level (mid/late passive)
            new UpgradeDefinition(
                key: UpgradeTypes.Factory,
                displayName: "Factory",
                subtextRight: "(+25/s)",
                costFunc: level =>
                {
                    double baseCost = 10000.0;
                    double growth = 1.22;
                    double cost = baseCost * Math.Pow(growth, level);
                    return new BigInteger(Math.Ceiling(cost));
                },
                clickPowerPerLevel: 0,
                passivePerSecondPerLevel: 25
            )
        };
    }
}