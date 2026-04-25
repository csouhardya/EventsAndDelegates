using System;
using System.Collections.Generic;
using System.Text;

namespace EventsAndDelegates
{
    public class MessageService
    {
        public void OnVideoEncoded(object source, VideoEventArgs args)
        {
            Console.WriteLine("MessageService: sending one message of {0}", args.Video.Title);
        }
    }
}
