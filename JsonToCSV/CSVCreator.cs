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
        private StringBuilder builder;
        Thread thread;

        public CSVCreator(string filePath)
        {
            this.filePath = filePath;
            builder = new StringBuilder();

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
            builder.Clear();
            SetHeader(builder);
            //SetHeader(ref csvString);

            int ix = 0;
            while (ix < dataArray.Length)
            {
                GenerateCSVLine(dataArray[ix], builder);
                //GenerateCSVLine(dataArray[ix], ref csvString);

                ++ix;
            }

            string csvPath = filePath.Replace("json", "csv");
            //File.WriteAllText(csvPath, csvString, Encoding.UTF8);
            File.WriteAllText(csvPath, builder.ToString(), Encoding.UTF8);

            Console.WriteLine("Survey Thread End");
        }

        void GenerateCSVLine(RecordData data, StringBuilder builder)
        {
            builder.Append(data.quizType);
            AddCSVValue(builder, data.age);
            AddCSVValue(builder, data.gender);
            AddCSVValue(builder, data.quizNumber.ToString());
            AddCSVValue(builder, data.elapsedTime.ToString("f4"));
            AddCSVValue(builder, data.contentState);
            AddCSVValue(builder, data.answer);
            AddCSVValue(builder, data.timeToAnswer.ToString("f4"));
            AddCSVValue(builder, data.modelType);
            AddCSVValue(builder, data.eyePosition.ToString().Replace(",", ":"));
            AddCSVValue(builder, data.targetRegion);
            AddCSVValue(builder, data.face);
            AddCSVValue(builder, data.motion);
            builder.Append(Constants.openLine);
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

        void SetHeader(StringBuilder builder)
        {
            FieldInfo[] fields = typeof(RecordData).GetFields();
            for (int ix = 0; ix < fields.Length; ++ix)
            {
                if (fields[ix].Name.Contains("recordEvent")) continue;

                builder.Append(fields[ix].Name);

                if (ix == fields.Length - 2)
                    builder.Append(Constants.openLine);
                else
                    builder.Append(Constants.comma);
            }
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

        void AddCSVValue(StringBuilder builder, string csvValue)
        {
            builder.Append(Constants.comma);
            builder.Append(csvValue);
        }

        void AddCSVValue(ref string csvString, string csvValue)
        {
            csvString += Constants.comma;
            csvString += csvValue;
        }
    }
}