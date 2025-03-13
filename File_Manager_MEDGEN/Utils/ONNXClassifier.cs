using Compunet.YoloSharp;
using System.Drawing;
using System.IO;

namespace File_Manager_MEDGEN.Utils
{
    public class ONNXClassifier
    {
        private readonly YoloPredictor _predictor;

        public ONNXClassifier(string modelPath)
        {

            if (!File.Exists(modelPath))
                throw new FileNotFoundException("Model file not found.", modelPath);

            // Start YoloSharp Predictor
            _predictor = new YoloPredictor(modelPath);
        }

        public string ClassifyImage(string imagePath)
        {
            if (!File.Exists(imagePath))
                throw new FileNotFoundException("Image file not found.", imagePath);

            // Make a prediction
            var results = _predictor.Detect(imagePath);

            if (results == null || !results.Any())
            {
                return "No objects detected in the image.";
            }

            // Choose the prediction with the highest confidence
            var bestPrediction = results.OrderByDescending(r => r.Confidence).FirstOrDefault();

            if (bestPrediction != null)
            {
                // Get the class name using the `Name` property
                return $"Class: {bestPrediction.Name}, Confidence: {bestPrediction.Confidence:F2}";
            }

            // If there is no guess, return Unknown
            return "Class: Unknown, Confidence: 0.00";
        }
    }
}
