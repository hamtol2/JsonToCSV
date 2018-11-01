namespace JsonToCSV
{
    [System.Serializable]
    public class Vector2
    {
        public float x;
        public float y;

        public Vector2() { }

        public override string ToString()
        {
            return "(" + x.ToString("f4") + " : " + y.ToString("f4") + ")";
        }
    }

    [System.Serializable]
    public class Vector3
    {
        public float x;
        public float y;
        public float z;

        public Vector3() { }

        public override string ToString()
        {
            return "(" + x.ToString("f4") + " : " + y.ToString("f4") + " : " + z.ToString("f4") + ")";
        }
    }

    [System.Serializable]
    public class RecordEvent
    {
        // 0: motion, 1: facial.
        public int eventType = -1;
        public string eventValue = string.Empty;

        public RecordEvent() { }
        public RecordEvent(int eventType, string eventValue)
        {
            this.eventType = eventType;
            this.eventValue = eventValue;
        }
    }

    [System.Serializable]
    public class RecordData
    {
        public RecordData() { }

        public string quizTitle;
        public string quizType;
        public string age;
        public string gender;
        public int quizNumber;
        public float elapsedTime;
        public string contentState;
        public string answer;
        public string modelType;
        public Vector2 eyePosition;
        public string targetRegion;
        public Vector3 robotPosition;
        public string robotState;
        public string face;
        public string gesture;
        public RecordEvent recordEvent;
    }
}