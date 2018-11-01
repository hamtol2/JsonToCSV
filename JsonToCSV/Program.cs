using System;
using System.Collections.Generic;
using System.IO;

namespace JsonToCSV
{
    class Program
    {
        private static string folderPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + @"\SurveyData";

        static void Main(string[] args)
        {
            List<string> surveyData = new List<string>();
            List<string> questionaireData = new List<string>();

            foreach (string filePath in Directory.EnumerateFiles(folderPath))
            {
                if (!IsJson(filePath))
                    continue;

                if (IsQuestionaire(filePath))
                {
                    questionaireData.Add(filePath);
                    //Console.WriteLine("Result: " + filePath);
                    continue;
                }

                surveyData.Add(filePath);
                //Console.WriteLine("Survey: " + filePath);
            }

            for (int ix = 0; ix < surveyData.Count; ++ix)
            {
                CSVCreator surveyConverter = new CSVCreator(surveyData[ix]);
                surveyConverter.StartConverting();

                QuestionaireConverter questionaireConverter = new QuestionaireConverter(questionaireData[ix]);
                questionaireConverter.StartConverting();
            }
        }

        static bool IsQuestionaire(string filePath)
        {
            return filePath.Contains("result");
        }

        static bool IsJson(string filePath)
        {
            return Path.GetExtension(filePath).Contains("json");
        }
    }
}