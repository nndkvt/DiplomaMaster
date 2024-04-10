// This file was auto-generated by ML.NET Model Builder. 

using Microsoft.ML.Data;

namespace IncreaseRelationshipModelML.Model
{
    public class ModelInput
    {
        [ColumnName("ExpInt"), LoadColumn(0)]
        public bool ExpInt { get; set; }


        [ColumnName("FrndAgr"), LoadColumn(1)]
        public bool FrndAgr { get; set; }


        [ColumnName("SnInsn"), LoadColumn(2)]
        public bool SnInsn { get; set; }


        [ColumnName("BrvAfr"), LoadColumn(3)]
        public bool BrvAfr { get; set; }


        [ColumnName("RelData"), LoadColumn(4)]
        public float RelData { get; set; }


        [ColumnName("ItemLvl"), LoadColumn(5)]
        public float ItemLvl { get; set; }


        [ColumnName("IncrRel"), LoadColumn(6)]
        public float IncrRel { get; set; }

        public ModelInput(bool[] character, float relValue, float itemLvl)
        {
            ExpInt = character[0];
            FrndAgr = character[1];
            SnInsn = character[2];
            BrvAfr = character[3];

            RelData = relValue;
            ItemLvl = itemLvl;
        }
    }
}