namespace JsonToCSV
{
    [System.Serializable]
    class QuestionaireJsonFormat
    {
        public QuestionaireJsonFormat() { }

        public Questionaire[] saveData;

        public int Length
        {
            get { return saveData == null ? 0 : saveData.Length; }
        }

        public Questionaire this[int index]
        {
            get { return saveData[index]; }
        }

        public void AddData(Questionaire data)
        {
            if (saveData == null)
            {
                saveData = new Questionaire[1] { data };
                return;
            }

            Questionaire[] tempArray = new Questionaire[saveData.Length];
            for (int ix = 0; ix < saveData.Length; ++ix)
            {
                tempArray[ix] = saveData[ix];
            }

            saveData = new Questionaire[saveData.Length + 1];
            for (int ix = 0; ix < tempArray.Length; ++ix)
            {
                saveData[ix] = tempArray[ix];
            }

            saveData[saveData.Length - 1] = data;
        }
    }
}
