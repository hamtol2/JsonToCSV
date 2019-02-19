using System;
using System.IO;
using System.Text;
using System.Threading;
using System.Reflection;

namespace JsonToCSV
{
    class CSVCreator
    {
        private string filePath = "";
        Thread thread;

        public CSVCreator(string filePath)
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
            Console.WriteLine("Survey Thread Start");

            string jsonString = File.ReadAllText(filePath);
            RecordJsonFormat dataArray = SimpleJson.SimpleJson.DeserializeObject<RecordJsonFormat>(jsonString);
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

            Console.WriteLine("Survey Thread End");
        }

        void GenerateCSVLine(RecordData data, ref string csvString)
        {
            //csvString += data.quizTitle;
            //AddCSVValue(ref csvString, data.quizType);
            csvString += data.quizType;
            AddCSVValue(ref csvString, data.age);
            AddCSVValue(ref csvString, data.gender);
            AddCSVValue(ref csvString, data.quizNumber.ToString());
            AddCSVValue(ref csvString, data.elapsedTime.ToString("f4"));
            AddCSVValue(ref csvString, data.contentState);
            AddCSVValue(ref csvString, data.answer);
            AddCSVValue(ref csvString, data.timeToAnswer.ToString("f4"));
            AddCSVValue(ref csvString, data.modelType);
            AddCSVValue(ref csvString, data.eyePosition.ToString().Replace(",", ":"));
            AddCSVValue(ref csvString, data.targetRegion);
            //AddCSVValue(ref csvString, data.robotPosition.ToString().Replace(",", ":"));
            //AddCSVValue(ref csvString, data.robotState);
            AddCSVValue(ref csvString, data.face);
            AddCSVValue(ref csvString, data.motion);
            csvString += Constants.openLine;
        }

        void SetHeader(ref string csvString)
        {
            FieldInfo[] fields = typeof(RecordData).GetFields();
            for (int ix = 0; ix < fields.Length; ++ix)
            {
                if (fields[ix].Name.Contains("recordEvent")) continue;

                csvString += fields[ix].Name;

                if (ix == fields.Length - 2)
                    csvString += Constants.openLine;
                else
                    csvString += Constants.comma;
            }
        }

        void AddCSVValue(ref string csvString, string csvValue)
        {
            csvString += Constants.comma;
            csvString += csvValue;
        }
    }
}