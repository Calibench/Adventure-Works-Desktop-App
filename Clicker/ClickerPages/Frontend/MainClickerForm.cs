using Clicker.ClickerPages.Backend;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Numerics;
using System.Windows.Forms;

namespace Clicker.ClickerPages.Frontend
{
    public partial class MainClickerForm : Form
    {
        //private readonly Player player = new Player();
        //private readonly ClickerRepository _repo = new ClickerRepository();
        //private readonly NumberHandler _numberHandler = new NumberHandler();

        //private readonly Timer _idleTimer = new Timer();
        //private readonly Timer _saveTimer = new Timer(); // periodic flush


        //private readonly string _loginId;
        //private int _clickPowerLevel;
        //private int _autoClickerLevel;

        //// Accumulated unsaved currency delta
        //private BigInteger _pendingDelta = BigInteger.Zero;

        private readonly Player player = new Player();
        private readonly ClickerRepository _repo = new ClickerRepository();
        private readonly NumberHandler _numberHandler = new NumberHandler();

        private readonly Timer _idleTimer = new Timer();
        private readonly Timer _saveTimer = new Timer(); // periodic flush

        private readonly string _loginId;

        // New: upgrade model and levels
        private readonly List<UpgradeDefinition> _upgradeDefs = UpgradeCatalog.All;
        private readonly Dictionary<string, int> _upgradeLevels = new Dictionary<string, int>(StringComparer.Ordinal);

        // Accumulated unsaved currency delta
        private BigInteger _pendingDelta = BigInteger.Zero;

        // Upgrade UI model for easy updates/expansion
        private sealed class UpgradeCard
        {
            public string Key;
            public Panel CardPanel;
            public Label TitleLabel;
            public Label LevelLabel;
            public Label CostLabel;
            public Button BuyButton;
        }

        private readonly Dictionary<string, UpgradeCard> _upgradeCards = new Dictionary<string, UpgradeCard>(StringComparer.Ordinal);

        public MainClickerForm(string username)
        {
            InitializeComponent();

            this.DoubleBuffered = true;

            _loginId = username ?? string.Empty;

            // Load state from DB
            LoadPlayerState();

            // Build dynamic upgrade cards (expandable)
            BuildUpgradesUI();

            // Start idle income
            _idleTimer.Interval = 1000;
            _idleTimer.Tick += IdleTimer_Tick;
            _idleTimer.Start();

            // Periodic save (every 5 minutes)
            _saveTimer.Interval = 5 * 60 * 1000; // 5 minutes
            _saveTimer.Tick += SaveTimer_Tick;
            _saveTimer.Start();

            // Initial UI
            UpdateAllUI();
            CenterClickNumber();
        }

        private void clickMeButton_Click(object sender, EventArgs e)
        {
            QueueDelta(player.UserPower);
            UpdateCurrencyUI();
            UpdateUpgradeCardsUI();
        }

        // Legacy buttons now forward to generic flow
        private void btnBuyClickPower_Click(object sender, EventArgs e)
        {
            BuyUpgrade(UpgradeTypes.ClickPower);
        }

        private void btnBuyAutoClicker_Click(object sender, EventArgs e)
        {
            BuyUpgrade(UpgradeTypes.AutoClicker);
        }

        private void IdleTimer_Tick(object sender, EventArgs e)
        {
            var passive = ComputePassivePerSecond();
            if (passive > 0)
            {
                QueueDelta(passive);
                UpdateCurrencyUI();
                UpdateUpgradeCardsUI();
            }
        }

        private void SaveTimer_Tick(object sender, EventArgs e)
        {
            FlushPendingDelta(); // periodic DB write
        }

        private void LoadPlayerState()
        {
            try
            {
                _repo.EnsureUserExists(_loginId);

                var count = _repo.GetClickCountBig(_loginId);
                if (count < 0) count = BigInteger.Zero;
                player.Currency = count;

                // Load all upgrade levels
                foreach (var def in _upgradeDefs)
                {
                    var lvl = _repo.GetUpgradeLevel(_loginId, def.Key);
                    if (lvl < 0) lvl = 0;
                    _upgradeLevels[def.Key] = lvl;
                }

                RecalculatePower();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to load player state.\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Accumulate an in-memory delta and keep UI in sync
        private void QueueDelta(BigInteger delta)
        {
            if (delta == 0) return;
            player.Currency += delta;
            _pendingDelta += delta;

            // flush if pending gets large to reduce risk of loss on crash
            //if (_pendingDelta >= new BigInteger(1000000)) FlushPendingDelta();
        }

        // Flush the accumulated delta to DB using atomic increment
        private void FlushPendingDelta()
        {
            if (_pendingDelta == 0) return;

            var toFlush = _pendingDelta;
            _pendingDelta = BigInteger.Zero;

            try
            {
                _repo.IncrementClickCountBig(_loginId, toFlush);
            }
            catch (Exception ex)
            {
                // Put it back so we don't lose progress
                _pendingDelta += toFlush;
                System.Diagnostics.Debug.WriteLine("FlushPendingDelta failed: " + ex);
            }
        }

        private void RecalculatePower()
        {
            // Base power 1 plus sum of all upgrades that affect click power
            BigInteger power = new BigInteger(1);
            foreach (var def in _upgradeDefs)
            {
                if (def.ClickPowerPerLevel != 0)
                {
                    var lvl = GetLevel(def.Key);
                    if (lvl > 0)
                    {
                        power += new BigInteger(def.ClickPowerPerLevel) * lvl;
                    }
                }
            }
            if (power < 1) power = 1;
            player.UserPower = power;
        }

        private BigInteger ComputePassivePerSecond()
        {
            BigInteger total = BigInteger.Zero;
            foreach (var def in _upgradeDefs)
            {
                if (def.PassivePerSecondPerLevel != 0)
                {
                    var lvl = GetLevel(def.Key);
                    if (lvl > 0)
                    {
                        total += new BigInteger(def.PassivePerSecondPerLevel) * lvl;
                    }
                }
            }
            return total;
        }


        private void UpdateAllUI()
        {
            UpdateCurrencyUI();
            lblPower.Text = "Power: " + player.UserPower;

            // Keep legacy labels updated (even though hidden)
            var cpLvl = GetLevel(UpgradeTypes.ClickPower);
            var acLvl = GetLevel(UpgradeTypes.AutoClicker);
            lblClickPowerLevel.Text = "Click Power Lvl: " + cpLvl + " (+1)";
            lblClickPowerCost.Text = "Cost: " + _numberHandler.FormatLargeNumber(GetCost(UpgradeTypes.ClickPower, cpLvl));
            lblAutoClickerLevel.Text = "Auto Clicker Lvl: " + acLvl + " (+1/s)";
            lblAutoClickerCost.Text = "Cost: " + _numberHandler.FormatLargeNumber(GetCost(UpgradeTypes.AutoClicker, acLvl));

            // Update the new dynamic upgrade cards
            UpdateUpgradeCardsUI();
        }


        private void UpdateCurrencyUI()
        {
            numOfClicksLabel.Text = _numberHandler.FormatLargeNumber(player.Currency);
            CenterClickNumber();
        }

        private void CenterClickNumber()
        {
            int x = (numOfClicksPanels.Size.Width - numOfClicksLabel.Size.Width) / 2;
            int y = (numOfClicksPanels.Size.Height - numOfClicksLabel.Size.Height) / 2;
            numOfClicksLabel.Location = new Point(x, y);
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            _idleTimer.Stop();
            _saveTimer.Stop();

            // Ensure we persist before exit
            FlushPendingDelta();
            base.OnFormClosing(e);
        }

        // ---------- Dynamic, expandable upgrade UI ----------

        private void BuildUpgradesUI()
        {
            flpUpgrades.SuspendLayout();
            try
            {
                flpUpgrades.Controls.Clear();
                _upgradeCards.Clear();

                foreach (var def in _upgradeDefs)
                {
                    var card = CreateUpgradeCard(
                        key: def.Key,
                        displayName: def.DisplayName,
                        subtextRight: def.SubtextRight,
                        buyHandler: (s, e) => BuyUpgrade(def.Key)
                    );
                    flpUpgrades.Controls.Add(card.CardPanel);
                    _upgradeCards[def.Key] = card;
                }
            }
            finally
            {
                flpUpgrades.ResumeLayout();
            }
        }

        private UpgradeCard CreateUpgradeCard(string key, string displayName, string subtextRight, EventHandler buyHandler)
        {
            var card = new Panel
            {
                Width = flpUpgrades.ClientSize.Width - 40, // padding
                Height = 90,
                Margin = new Padding(10),
                Padding = new Padding(12),
                BackColor = Color.FromArgb(45, 45, 48)
            };

            // Title
            var lblTitle = new Label
            {
                AutoSize = true,
                Text = displayName + " " + subtextRight,
                Font = new Font("Segoe UI", 11F, FontStyle.Bold),
                ForeColor = Color.WhiteSmoke,
                Location = new Point(12, 10)
            };

            // Level
            var lblLevel = new Label
            {
                AutoSize = true,
                Text = "Lvl: 0",
                Font = new Font("Segoe UI", 9F, FontStyle.Regular),
                ForeColor = Color.Gainsboro,
                Location = new Point(14, 40)
            };

            // Cost
            var lblCost = new Label
            {
                AutoSize = true,
                Text = "Cost: -",
                Font = new Font("Segoe UI", 9F, FontStyle.Regular),
                ForeColor = Color.Silver,
                Location = new Point(14, 60)
            };

            // Buy button
            var btnBuy = new Button
            {
                Text = "Buy",
                AutoSize = false,
                Width = 120,
                Height = 36,
                Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                BackColor = Color.FromArgb(0, 120, 215),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };
            btnBuy.FlatAppearance.BorderSize = 0;
            btnBuy.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnBuy.Location = new Point(card.Width - btnBuy.Width - 12, 24);
            btnBuy.Click += buyHandler;

            // Reposition button when card size changes (resilience on resize)
            card.Resize += (s, e) =>
            {
                btnBuy.Location = new Point(card.Width - btnBuy.Width - 12, 24);
            };

            card.Controls.Add(lblTitle);
            card.Controls.Add(lblLevel);
            card.Controls.Add(lblCost);
            card.Controls.Add(btnBuy);

            return new UpgradeCard
            {
                Key = key,
                CardPanel = card,
                TitleLabel = lblTitle,
                LevelLabel = lblLevel,
                CostLabel = lblCost,
                BuyButton = btnBuy
            };
        }

        private void UpdateUpgradeCardsUI()
        {
            foreach (var def in _upgradeDefs)
            {
                UpgradeCard card;
                if (!_upgradeCards.TryGetValue(def.Key, out card)) continue;

                var level = GetLevel(def.Key);
                var cost = def.CostFunc(level);

                // Decorate level line to hint effect
                string effectHint = string.Empty;
                if (def.ClickPowerPerLevel != 0 && def.PassivePerSecondPerLevel != 0)
                    effectHint = "  (+" + def.ClickPowerPerLevel + ", +" + def.PassivePerSecondPerLevel + "/s)";
                else if (def.ClickPowerPerLevel != 0)
                    effectHint = "  (+" + def.ClickPowerPerLevel + ")";
                else if (def.PassivePerSecondPerLevel != 0)
                    effectHint = "  (+" + def.PassivePerSecondPerLevel + "/s)";

                card.LevelLabel.Text = "Lvl: " + level + effectHint;
                card.CostLabel.Text = "Cost: " + _numberHandler.FormatLargeNumber(cost);
                card.BuyButton.Enabled = player.Currency >= cost;
            }
        }

        // ---------- Generic upgrade helpers ----------

        private int GetLevel(string key)
        {
            int lvl;
            return _upgradeLevels.TryGetValue(key, out lvl) ? lvl : 0;
        }

        private BigInteger GetCost(string key, int level)
        {
            var def = GetDefinition(key);
            return def.CostFunc(level);
        }

        private UpgradeDefinition GetDefinition(string key)
        {
            for (int i = 0; i < _upgradeDefs.Count; i++)
            {
                var d = _upgradeDefs[i];
                if (string.Equals(d.Key, key, StringComparison.Ordinal)) return d;
            }
            throw new InvalidOperationException("Unknown upgrade key: " + key);
        }

        private void BuyUpgrade(string key)
        {
            var def = GetDefinition(key);
            var currentLevel = GetLevel(key);
            var cost = def.CostFunc(currentLevel);

            if (player.Currency >= cost)
            {
                QueueDelta(-cost); // spend currency
                var newLevel = currentLevel + 1;
                _upgradeLevels[key] = newLevel;

                try
                {
                    _repo.UpsertUpgrade(_loginId, key, newLevel);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Failed to save upgrade.\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                RecalculatePower();
                UpdateAllUI();
            }
            else
            {
                MessageBox.Show("Not enough currency.", "Purchase failed", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}