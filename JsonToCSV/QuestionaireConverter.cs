using System;
using System.IO;
using System.Reflection;
using System.Text;
using System.Threading;
using SimpleJson;

namespace JsonToCSV
{
    class QuestionaireConverter
    {
        private string filePath = string.Empty;
        Thread thread;

        public QuestionaireConverter(string filePath)
        {
            this.filePath = filePath;

            ThreadStart threadStart = new ThreadStart(ConverToCSV);
            thread = new Thread(threadStart);
        }

        public void StartConverting()
        {
            thread.Start();
        }

        void ConverToCSV()
        {
            Console.WriteLine("Result Thread Start");

            string jsonString = File.ReadAllText(filePath);
            QuestionaireJsonFormat dataArray = SimpleJson.SimpleJson.DeserializeObject<QuestionaireJsonFormat>(jsonString);
            RemoveOpenLineFromJson(ref dataArray);

            string csvString = string.Empty;
            SetHeader(ref csvString);

            int ix = 0;
            while (ix < dataArray.Length)
            {
                GenerateCSVLine(dataArray[ix], ref csvString);
                ++ix;
            }

            string csvPath = filePath.Replace("json", "csv");
            File.WriteAllText(csvPath, csvString, Encoding.UTF8);

            Console.WriteLine("Result Thread End");
        }
        
        void RemoveOpenLineFromJson(ref QuestionaireJsonFormat dataArray)
        {
            for (int index = 0; index < dataArray.Length; ++index)
            {
                string[] datas = dataArray[index].question.Split(new char[] { '\n', ',' }, StringSplitOptions.RemoveEmptyEntries);
                string finalQuestion = string.Empty;
                foreach (string data in datas)
                {
                    finalQuestion += data;
                }

                dataArray[index].question = finalQuestion;
            }
        }

        void GenerateCSVLine(Questionaire data, ref string csvString)
        {
            csvString += data.question;
            AddCSVValue(ref csvString, data.score.ToString());
            csvString += Constants.openLine;
        }

        void SetHeader(ref string csvString)
        {
            FieldInfo[] fields = typeof(Questionaire).GetFields();
            for (int ix = 0; ix < fields.Length; ++ix)
            {
                csvString += fields[ix].Name;
                csvString += Constants.comma;
            }

            csvString += Constants.openLine;
        }

        void AddCSVValue(ref string csvString, string csvValue)
        {
            csvString += Constants.comma;
            csvString += csvValue;
        }
    }
}