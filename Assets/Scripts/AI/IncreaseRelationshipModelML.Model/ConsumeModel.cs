// This file was auto-generated by ML.NET Model Builder. 
// It was later modified for Unity integration

using System;
using Microsoft.ML;
using System.IO;

namespace IncreaseRelationshipModelML.Model
{
    public class ConsumeModel
    {
        private static Lazy<PredictionEngine<ModelInput, ModelOutput>> PredictionEngine = new Lazy<PredictionEngine<ModelInput, ModelOutput>>(CreatePredictionEngine);

        // For more info on consuming ML.NET models, visit https://aka.ms/mlnet-consume
        // Method for consuming model in your app
        public static ModelOutput Predict(ModelInput input)
        {
            ModelOutput result = PredictionEngine.Value.Predict(input);
            return result;
        }

        public static PredictionEngine<ModelInput, ModelOutput> CreatePredictionEngine()
        {
            // Create new MLContext
            MLContext mlContext = new MLContext();

            // Load model & create prediction engine
            //string modelPath = @"C:\Users\Oleg\AppData\Local\Temp\MLVSTools\IncreaseRelationshipModelML\IncreaseRelationshipModelML.Model\MLModel.zip";
            string modelPath = "Assets/Scripts/AI/IncreaseRelationshipModelML.Model/MLModel.zip";

            //ITransformer mlModel = mlContext.Model.Load(modelPath, out var modelInputSchema);
            PredictionEngine<ModelInput, ModelOutput> predEngine;

            using (FileStream modelStream = File.Open(modelPath, FileMode.Open, FileAccess.Read))
            {
                Microsoft.ML.Core.Data.ITransformer mlModel = mlContext.Model.Load(modelStream);
                predEngine = mlContext.Model.CreatePredictionEngine<ModelInput, ModelOutput>(mlModel);
            }

            return predEngine;
        }
    }
}
