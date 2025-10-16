using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Numerics;

namespace Clicker.ClickerPages.Backend
{
    internal class ClickerRepository
    {
        private readonly string _connectionString;

        public ClickerRepository()
        {
            _connectionString = ConfigurationManager.ConnectionStrings["Clicker"].ConnectionString;
        }

        public void EnsureUserExists(string loginId)
        {
            // First ensure tables exist
            EnsureTablesExist();

            using (var con = new SqlConnection(_connectionString))
            using (var cmd = new SqlCommand(@"
                IF NOT EXISTS (SELECT 1 FROM [dbo].[ClickerUserData] WHERE [LoginID] = @loginId)
                BEGIN
                    INSERT INTO [dbo].[ClickerUserData] ([LoginID], [ClickCount], [CreatedDate])
                    VALUES (@loginId, 0, GETUTCDATE());
                END
                ", con))
            {
                cmd.Parameters.Add("@loginId", SqlDbType.NVarChar, 256).Value = (object)loginId ?? DBNull.Value;
                con.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public long GetClickCount(string loginId)
        {
            using (var con = new SqlConnection(_connectionString))
            using (var cmd = new SqlCommand(@"
                SELECT COALESCE(CAST([ClickCount] AS BIGINT), 0)
                FROM [dbo].[ClickerUserData]
                WHERE [LoginID] = @loginId;
                ", con))
            {
                cmd.Parameters.Add("@loginId", SqlDbType.NVarChar, 256).Value = (object)loginId ?? DBNull.Value;
                con.Open();
                
                var obj = cmd.ExecuteScalar();
                
                if (obj == null || obj == DBNull.Value) 
                    return 0L;
                
                long value;
                
                if (long.TryParse(obj.ToString(), out value)) 
                    return value;
                
                return 0L;
            }
        }

        public void UpdateClickCount(string loginId, long count)
        {
            using (var con = new SqlConnection(_connectionString))
            using (var cmd = new SqlCommand(@"
                UPDATE [dbo].[ClickerUserData]
                SET [ClickCount] = @count, [LastClickDate] = GETUTCDATE()
                WHERE [LoginID] = @loginId;
                ", con))
            {
                cmd.Parameters.Add("@loginId", SqlDbType.NVarChar, 256).Value = (object)loginId ?? DBNull.Value;
                cmd.Parameters.Add("@count", SqlDbType.BigInt).Value = count;
                con.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public int GetUpgradeLevel(string loginId, string upgradeType)
        {
            try
            {
                using (var con = new SqlConnection(_connectionString))
                {
                    con.Open();

                    // First get the numeric user ID
                    int userId;
                    using (var cmdGetId = new SqlCommand(@"
                        SELECT [Id] 
                        FROM [dbo].[ClickerUserData] 
                        WHERE [LoginID] = @loginId", con))
                    {
                        cmdGetId.Parameters.Add("@loginId", SqlDbType.NVarChar, 50).Value = (object)loginId ?? DBNull.Value;
                        var result = cmdGetId.ExecuteScalar();
                        if (result == null || result == DBNull.Value)
                            return 0; // User not found, default level

                        userId = Convert.ToInt32(result);
                    }

                    // Now get the upgrade level
                    using (var cmd = new SqlCommand(@"
                        SELECT TOP 1 [Level]
                        FROM [dbo].[ClickerUpgrade]
                        WHERE [UserId] = @userId AND [UpgradeType] = @upgradeType
                        ORDER BY [PurchasedDate] DESC;", con))
                    {
                        cmd.Parameters.Add("@userId", SqlDbType.Int).Value = userId;
                        cmd.Parameters.Add("@upgradeType", SqlDbType.NVarChar, 50).Value = (object)upgradeType ?? DBNull.Value;
                        var obj = cmd.ExecuteScalar();
                        if (obj == null || obj == DBNull.Value)
                            return 0;

                        int level;
                        if (int.TryParse(obj.ToString(), out level))
                            return level;

                        return 0;
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Error getting upgrade level: " + ex.Message);
                return 0;
            }
        }

        public void UpsertUpgrade(string loginId, string upgradeType, int level)
        {
            try
            {
                using (var con = new SqlConnection(_connectionString))
                {
                    con.Open();

                    // First get the numeric user ID from the ClickerUserData table
                    int userId;
                    using (var cmdGetId = new SqlCommand(@"
                        SELECT [Id] 
                        FROM [dbo].[ClickerUserData] 
                        WHERE [LoginID] = @loginId", con))
                    {
                        cmdGetId.Parameters.Add("@loginId", SqlDbType.NVarChar, 50).Value = (object)loginId ?? DBNull.Value;
                        var result = cmdGetId.ExecuteScalar();
                        if (result == null || result == DBNull.Value)
                        {
                            throw new ApplicationException($"User '{loginId}' not found in the database.");
                        }
                        userId = Convert.ToInt32(result);
                    }

                    // Now use the numeric ID to update the upgrade
                    using (var cmd = new SqlCommand(@"
                        IF EXISTS (SELECT 1 FROM [dbo].[ClickerUpgrade] WHERE [UserId] = @userId AND [UpgradeType] = @upgradeType)
                        BEGIN
                            UPDATE [dbo].[ClickerUpgrade]
                            SET [Level] = @level, [PurchasedDate] = GETUTCDATE()
                            WHERE [UserId] = @userId AND [UpgradeType] = @upgradeType;
                        END
                        ELSE
                        BEGIN
                            INSERT INTO [dbo].[ClickerUpgrade] ([UserId], [UpgradeType], [Level], [PurchasedDate])
                            VALUES (@userId, @upgradeType, @level, GETUTCDATE());
                        END", con))
                    {
                        cmd.Parameters.Add("@userId", SqlDbType.Int).Value = userId;
                        cmd.Parameters.Add("@upgradeType", SqlDbType.NVarChar, 50).Value = (object)upgradeType ?? DBNull.Value;
                        cmd.Parameters.Add("@level", SqlDbType.Int).Value = level;
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Error updating upgrade: " + ex.Message);
                throw new ApplicationException("Failed to update upgrade. Please try again later.", ex);
            }
        }

        public void EnsureTablesExist()
        {
            using (var con = new SqlConnection(_connectionString))
            {
                con.Open();

                // Check and create ClickerUpgrade table if needed
                using (var cmd = new SqlCommand(@"
                    IF NOT EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'ClickerUpgrade')
                    BEGIN
                        CREATE TABLE [dbo].[ClickerUpgrade] (
                            [Id] INT IDENTITY(1,1) PRIMARY KEY,
                            [UserId] NVARCHAR(256) NOT NULL,
                            [UpgradeType] NVARCHAR(128) NOT NULL,
                            [Level] INT NOT NULL DEFAULT(0),
                            [PurchasedDate] DATETIME NOT NULL
                        );
                        
                        CREATE INDEX IX_ClickerUpgrade_UserId ON [dbo].[ClickerUpgrade] ([UserId]);
                        CREATE INDEX IX_ClickerUpgrade_UpgradeType ON [dbo].[ClickerUpgrade] ([UpgradeType]);
                    END", con))
                {
                    cmd.ExecuteNonQuery();
                }
            }
        }

        // Get as BigInteger from DECIMAL(38,0)
        public BigInteger GetClickCountBig(string loginId)
        {
            using (var con = new SqlConnection(_connectionString))
            using (var cmd = new SqlCommand(@"
                SELECT CONVERT(VARCHAR(50), [ClickCount])
                FROM [dbo].[ClickerUserData]
                WHERE [LoginID] = @loginId;", con))
            {
                cmd.Parameters.Add("@loginId", SqlDbType.NVarChar, 256).Value = (object)loginId ?? DBNull.Value;
                con.Open();
                var obj = cmd.ExecuteScalar();
                if (obj == null || obj == DBNull.Value) return BigInteger.Zero;

                BigInteger value;
                if (BigInteger.TryParse(obj.ToString(), out value)) return value;
                return BigInteger.Zero;
            }
        }

        // Set absolute value using BigInteger
        public void UpdateClickCountBig(string loginId, BigInteger count)
        {
            using (var con = new SqlConnection(_connectionString))
            using (var cmd = new SqlCommand(@"
                UPDATE [dbo].[ClickerUserData]
                SET [ClickCount] = @count, [LastClickDate] = GETUTCDATE()
                WHERE [LoginID] = @loginId;", con))
            {
                cmd.Parameters.Add("@loginId", SqlDbType.NVarChar, 256).Value = (object)loginId ?? DBNull.Value;

                var p = cmd.Parameters.Add("@count", SqlDbType.Decimal);
                p.Precision = 38;
                p.Scale = 0;
                p.Value = SqlDecimal.Parse(count.ToString());

                con.Open();
                cmd.ExecuteNonQuery();
            }
        }

        // Atomic increment using BigInteger (to avoid race conditions)
        public void IncrementClickCountBig(string loginId, BigInteger delta)
        {
            using (var con = new SqlConnection(_connectionString))
            using (var cmd = new SqlCommand(@"
                UPDATE [dbo].[ClickerUserData]
                SET [ClickCount] = [ClickCount] + @delta,
                    [LastClickDate] = GETUTCDATE()
                WHERE [LoginID] = @loginId;", con))
            {
                cmd.Parameters.Add("@loginId", SqlDbType.NVarChar, 256).Value = (object)loginId ?? DBNull.Value;

                var p = cmd.Parameters.Add("@delta", SqlDbType.Decimal);
                p.Precision = 38;
                p.Scale = 0;
                p.Value = SqlDecimal.Parse(delta.ToString());

                con.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }
}