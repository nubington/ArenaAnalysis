using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace arena_analysis
{
    class ArenaRecord
    {
        public static List<ArenaRecord> ArenaRecords = new List<ArenaRecord>();

        static float packValue = 100f;
        static float dustValue = 1f;

        public int Wins { get; private set; }
        public float Losses { get; private set; }
        public float Packs { get; private set; }
        public float Gold { get; private set; }
        public float Dust { get; private set; }

        public float Value { get; private set; }
        public float Profit { get; private set; }

        public ArenaRecord(int wins, float losses, float packs, float gold, float dust)
        {
            Wins = wins;
            Losses = losses;
            Packs = packs;
            Gold = gold;
            Dust = dust;

            Value = gold + packs * packValue + dust * dustValue;
            Profit = Value - 150;
        }

        static void refresh()
        {
            foreach (ArenaRecord record in ArenaRecords)
            {
                record.Value = record.Gold + record.Packs * packValue + record.Dust * dustValue;
                record.Profit = record.Value - 150;
            }
        }

        public static float PackValue
        {
            get
            {
                return packValue;
            }
            set
            {
                packValue = value;
                refresh();
            }
        }

        public static float DustValue
        {
            get
            {
                return dustValue;
            }
            set
            {
                dustValue = value;
                refresh();
            }
        }
    }
}
