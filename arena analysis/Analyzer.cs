//#define ONLY_ME
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace arena_analysis
{
    class Analyzer
    {
        static List<ArenaRecord>[] ArenaRecordsByWins = new List<ArenaRecord>[13];

        static ArenaRecord[] averageRecords = new ArenaRecord[13];

        static float[] averageValuesByWins = new float[13];
        static float[] averageVPWByWins = new float[13];
        static float[] averageProfitByWins = new float[13];
        static float[] averageDustByWins = new float[13];

        static Vector3[] increasePercents = new Vector3[13];
        static Vector3[] returnOnInvestment = new Vector3[13];

        public static float AverageWins { get; private set; }
        public static float AverageLosses { get; private set; }
        public static float AverageGold { get; private set; }
        public static float AveragePacks { get; private set; }
        public static float AverageDust { get; private set; }
        public static float AverageValue { get; private set; }
        public static float AverageROI { get; private set; }

        public static float AverageValueToWinsRatio { get; private set; }

        public static string WarningMsg = "";


        // methods

        public static void GetRecordsFromFile(string filePath)
        {
            List<string> lines = new List<string>(File.ReadLines(filePath));

            for (int i = 0; i < lines.Count; i++)
            {
                string line = lines[i];

                if (line.Length >= 2 && line.Substring(0, 2) == "//")
                    continue;

                string[] values = line.Split(' ');

                if (values.Length == 5)
                    AddRecord(new ArenaRecord(int.Parse(values[0]), int.Parse(values[1]), int.Parse(values[2]), int.Parse(values[3]), int.Parse(values[4])));
                else
                {
#if (ONLY_ME)
                    break;
#endif
                    WarningMsg += "Error on line " + i + " of input file.\n";
                }
            }

            // initialize and populate list separated by wins
            for (int i = 0; i < ArenaRecordsByWins.Length; i++)
                ArenaRecordsByWins[i] = new List<ArenaRecord>();
            foreach (ArenaRecord record in ArenaRecord.ArenaRecords)
                ArenaRecordsByWins[record.Wins].Add(record);
        }

        public static void CalculateOverallAverages()
        {
            float totalWins = 0, totalLosses = 0, totalGold = 0, totalPacks = 0, totalDust = 0;
            float totalValue = 0f, totalValueToWinsRatio = 0f;

            foreach (ArenaRecord record in ArenaRecord.ArenaRecords)
            {
                totalWins += record.Wins;
                totalLosses += record.Losses;
                totalGold += record.Gold;
                totalPacks += record.Packs;
                totalDust += record.Dust;
                totalValue += record.Value;
                if (record.Wins != 0)
                    totalValueToWinsRatio += record.Value / record.Wins;
            }

            float count = (float)ArenaRecord.ArenaRecords.Count;

            AverageWins = totalWins / count;
            AverageLosses = totalLosses / count;
            AveragePacks = totalPacks / count;
            AverageGold = totalGold / count;
            AverageDust = totalDust / count;
            AverageValue = totalValue / count;
            AverageValueToWinsRatio = totalValueToWinsRatio / (count - numberOfZeroWins());

            /*float totalROI = 0;
            count = 0;
            for (int i = 0; i < 10; i++)
            {
                if (averageRecords[i] == null)
                    continue;

                totalROI += returnOnInvestment[i].Z;
                count++;
            }
            AverageROI = totalROI / count;*/

            AverageROI = (AverageValue - 150) / 150;
        }

        public static void CalculateIncreasePercents()
        {
            for (int i = 1; i < 13; i++)
            {
                if (averageRecords[i] == null || averageRecords[i - 1] == null)
                    continue;

                increasePercents[i].Z = (averageRecords[i].Value - averageRecords[i - 1].Value) / averageRecords[i - 1].Value;
                increasePercents[i].X = (averageRecords[i].Gold - averageRecords[i - 1].Gold) / averageRecords[i - 1].Gold;
                increasePercents[i].Y = (averageRecords[i].Dust - averageRecords[i - 1].Dust) / averageRecords[i - 1].Dust;
            }
        }

        public static void CalculateSeparateAverages()
        {

            for (int i = 0; i < ArenaRecordsByWins.Length; i++)
            {
                List<ArenaRecord> list = ArenaRecordsByWins[i];

                if (list.Count == 0)
                {
                    averageValuesByWins[i] = averageVPWByWins[i] = averageProfitByWins[i] = averageDustByWins[i] = 0;
                    continue;
                }

                float totalValue = 0;
                float totalLosses = 0, totalPacks = 0, totalGold = 0, totalDust = 0;
                foreach (ArenaRecord record in list)
                {
                    totalValue += record.Value;
                    totalLosses += record.Losses;
                    totalPacks += record.Packs;
                    totalGold += record.Gold;
                    totalDust += record.Dust;
                }

                averageRecords[i] = new ArenaRecord(i, totalLosses / list.Count, totalPacks / list.Count, totalGold / list.Count, totalDust / list.Count);

                averageValuesByWins[i] = totalValue / list.Count;
                averageVPWByWins[i] = averageValuesByWins[i] / i;
                averageProfitByWins[i] = averageValuesByWins[i] - 150;
                averageDustByWins[i] = totalDust / list.Count;
            }
        }

        public static void CalculateROI()
        {
            for (int i = 0; i < 13; i++)
            {
                ArenaRecord record = averageRecords[i];
                
                if (record == null)
                    continue;

                returnOnInvestment[i].X = (record.Gold - 150) / 150;
                returnOnInvestment[i].Y = (record.Dust - 150) / 150;
                returnOnInvestment[i].Z = (record.Value - 150) / 150;
            }
        }

        public static void DoAllCalculations()
        {
            CalculateSeparateAverages();
            CalculateIncreasePercents();
            CalculateROI();
            CalculateOverallAverages();
        }

        public static void PrintResults(RichTextBox textBox)
        {
            textBox.Clear();

            if (WarningMsg != "")
                textBox.AppendText(WarningMsg + "\n");

            textBox.AppendText("Number of records: " + ArenaRecord.ArenaRecords.Count + " (");
            for (int i = 0; i < 13; i++)
            {
                List<ArenaRecord> recordList = ArenaRecordsByWins[i];
                textBox.AppendText(recordList.Count.ToString());
                if (i < 12)
                    textBox.AppendText(", ");
            }
            textBox.AppendText(")");

            textBox.AppendText("\n\nPacks are valued at " + ArenaRecord.PackValue + " gold per pack.");
            textBox.AppendText("\nDust is valued at " + ArenaRecord.DustValue + " gold per dust.");

            textBox.AppendText("\n\nAverage wins: " + AverageWins);
#if (ONLY_ME)
            textBox.AppendText("\nAverage gold: " + AverageGold);
            textBox.AppendText("\nAverage dust: " + AverageDust);
            textBox.AppendText("\nAverage value: " + AverageValue);
#endif
            textBox.AppendText("\nAverage value ROI: " + (AverageROI * 100f).ToString("0.00") + "%");
            //textBox.AppendText("\nAverage losses: " + AverageLosses);
            //textBox.AppendText("\nAverage packs: " + AveragePacks);
            //textBox.AppendText("\nAverage gold: " + AverageGold);
            //textBox.AppendText("\nAverage dust: " + AverageDust);

            //textBox.AppendText("\nAverage value: " + AverageValue);
            //textBox.AppendText("\nAverage value per win: " + AverageValueToWinsRatio);*/

            textBox.AppendText("\n\nAverage amounts:");

            for (int i = 0; i < 13; i++)
            {
                if (averageValuesByWins[i] == 0)
                    continue;

                textBox.AppendText("\n" + i + " wins: " + averageRecords[i].Gold.ToString("0.00") + " gold, " + averageRecords[i].Dust.ToString("0.00") + " dust, " + averageRecords[i].Value.ToString("0.00") + " value.");
            }

            textBox.AppendText("\n\nAverage increases:");
            for (int i = 0; i < 13; i++)
            {
                if (averageRecords[i] == null || increasePercents[i].Z == 0)
                    continue;

                textBox.AppendText("\n" + i + " wins: " + (increasePercents[i].X * 100f).ToString("0.00") + "% gold, " + (increasePercents[i].Y * 100f).ToString("0.00") + "% dust, " + (increasePercents[i].Z * 100f).ToString("0.00") + "% value.");
            }

            textBox.AppendText("\n\nROI:");
            for (int i = 0; i < 13; i++)
            {
                if (averageRecords[i] == null)
                    continue;

                textBox.AppendText("\n" + i + " wins: " + (returnOnInvestment[i].X * 100f).ToString("0.00") + "% gold, " + (returnOnInvestment[i].Z * 100f).ToString("0.00") + "% value");
            }
        }

        public static void AddRecord(ArenaRecord record)
        {
            ArenaRecord.ArenaRecords.Add(record);
        }

        public static void RemoveRecord(ArenaRecord record)
        {
            ArenaRecord.ArenaRecords.Remove(record);
        }


        // properties

        public static float LowestValue
        {
            get
            {
                float lowest = float.MaxValue;

                foreach (ArenaRecord record in ArenaRecord.ArenaRecords)
                {
                    float value = record.Value;

                    if (value < lowest)
                        lowest = value;
                }

                return lowest;
            }
        }

        public static float HighestValue
        {
            get
            {
                float highest = 0;

                foreach (ArenaRecord record in ArenaRecord.ArenaRecords)
                {
                    float value = record.Value;
                    if (value > highest)
                        highest = value;
                }

                return highest;
            }
        }

        public static float LowestProfit
        {
            get
            {
                float lowest = float.MaxValue;

                foreach (ArenaRecord record in ArenaRecord.ArenaRecords)
                {
                    float profit = record.Value - 150;

                    if (profit < lowest)
                        lowest = profit;
                }

                return lowest;
            }
        }

        public static float HighestProfit
        {
            get
            {
                float highest = 0;

                foreach (ArenaRecord record in ArenaRecord.ArenaRecords)
                {
                    float profit = record.Value - 150;
                    if (profit > highest)
                        highest = profit;
                }

                return highest;
            }
        }

        public static float LowestDust
        {
            get
            {
                float lowest = float.MaxValue;

                foreach (ArenaRecord record in ArenaRecord.ArenaRecords)
                {
                    float dust = record.Dust;

                    if (dust < lowest)
                        lowest = dust;
                }

                return lowest;
            }
        }

        public static float HighestDust
        {
            get
            {
                float highest = 0;

                foreach (ArenaRecord record in ArenaRecord.ArenaRecords)
                {
                    float dust = record.Dust;
                    if (dust > highest)
                        highest = dust;
                }

                return highest;
            }
        }

        public static float LowestGold
        {
            get
            {
                float lowest = float.MaxValue;

                foreach (ArenaRecord record in ArenaRecord.ArenaRecords)
                {
                    float gold = record.Gold;

                    if (gold < lowest)
                        lowest = gold;
                }

                return lowest;
            }
        }

        public static float HighestGold
        {
            get
            {
                float highest = 0;

                foreach (ArenaRecord record in ArenaRecord.ArenaRecords)
                {
                    float gold = record.Gold;
                    if (gold > highest)
                        highest = gold;
                }

                return highest;
            }
        }

        public static int LowestWins
        {
            get
            {
                int lowest = int.MaxValue;

                foreach (ArenaRecord record in ArenaRecord.ArenaRecords)
                {
                    if (record.Wins < lowest)
                        lowest = record.Wins;
                }

                return lowest;
            }
        }

        public static int HighestWins
        {
            get
            {
                int highest = 0;

                foreach (ArenaRecord record in ArenaRecord.ArenaRecords)
                {
                    if (record.Wins > highest)
                        highest = record.Wins;
                }

                return highest;
            }
        }

        /*public static float[] ProfitsByWins
        {
            get
            {
                float[] profits = new float[10];

                for (int i = 0; i < 10; i++)
                {
                    if (averageValuesByWins[i] == 0)
                        profits[i] = -999;
                    else
                        profits[i] = averageProfitByWins[i];
                }

                return profits;
            }
        }

        public static float[] DustByWins
        {
            get
            {
                float[] dust = new float[10];

                for (int i = 0; i < 10; i++)
                {
                    if (averageValuesByWins[i] == 0)
                        dust[i] = -999;
                    else
                        dust[i] = averageDustByWins[i];
                }

                return dust;
            }
        }*/

        static int numberOfZeroWins()
        {
            int count = 0;

            foreach (ArenaRecord record in ArenaRecord.ArenaRecords)
            {
                if (record.Wins == 0)
                    count++;
            }

            return count;
        }

        public static ArenaRecord[] AverageRecords
        {
            get
            {
                return (ArenaRecord[])averageRecords.Clone();
            }
        }
    }
}
