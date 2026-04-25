namespace EventsAndDelegates
{
    class Program
    {
        static void Main(string[] args)
        {
            Video video = new() { Title = "Video1"};
            var videoEncoder = new VideoEncoder(); // publisher
            var mailService = new MailService(); // subscriber
            var msgService = new MessageService(); //subscriber


            videoEncoder.VideoEncodedEvent += mailService.OnVideoEncoded;
            videoEncoder.VideoEncodedEvent += msgService.OnVideoEncoded;

            videoEncoder.Encode(video);
        }
    }

    
}