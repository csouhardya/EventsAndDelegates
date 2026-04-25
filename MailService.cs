using System;
using System.Collections.Generic;
using System.Text;

namespace EventsAndDelegates
{
    public class MailService
    {
        public void OnVideoEncoded(object source, VideoEventArgs args)
        {
            Console.WriteLine("MailService: sending one mail of {0}", args.Video.Title);
        }
    }
}
