namespace JsonToCSV
{
    [System.Serializable]
    public class RecordJsonFormat
    {
        public RecordJsonFormat() { }

        public RecordData[] recordData;

        public int Length
        {
            get { return recordData == null ? 0 : recordData.Length; }
        }

        public RecordData this[int index]
        {
            get { return recordData[index]; }
        }

        public void AddData(RecordData data)
        {
            if (recordData == null)
            {
                recordData = new RecordData[1] { data };
                return;
            }

            RecordData[] tempArray = new RecordData[recordData.Length];
            for (int ix = 0; ix < recordData.Length; ++ix)
            {
                tempArray[ix] = recordData[ix];
            }

            recordData = new RecordData[recordData.Length + 1];
            for (int ix = 0; ix < tempArray.Length; ++ix)
            {
                recordData[ix] = tempArray[ix];
            }

            recordData[recordData.Length - 1] = data;
        }
    }
}
