namespace EventsAndDelegates
{
    class Program
    {
        static void Main(string[] args)
        {
            Video video = new() { Title = "Video1"};
            var videoEncoder = new VideoEncoder(); // publisher
            var mailService = new MailService();

            videoEncoder.VideoEncodedEvent += mailService.OnVideoEncoded;

            videoEncoder.Encode(video);
        }
    }

    public class MailService
    {
        public void OnVideoEncoded(object source,  EventArgs args)
        {
            Console.WriteLine("MailService: sending one mail");
        }
    }
}