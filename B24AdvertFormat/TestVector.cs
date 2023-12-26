namespace B24AdvertFormat
{
    public class TestVector
    {
        public string PIN { get; set; }
        public byte[] EncodedData { get; set; }

        public TestVector(string pin, byte[] encodedData)
        {
            PIN = pin;
            EncodedData = encodedData;
        }
        public TestVector() { }
    }
}
