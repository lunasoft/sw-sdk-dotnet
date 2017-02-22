namespace SW.Helpers
{
    public class Response
    {
        public ResponseType Status { get; set; }
        public string Message { get; set; }
        public Data Data { get; set; }
    }
    public partial class Data
    {
       
    }
}