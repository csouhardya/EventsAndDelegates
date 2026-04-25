using System;
using System.Collections.Generic;
using System.Text;

namespace EventsAndDelegates
{
    public class VideoEncoder
    {
        // 1. define a delegate
        // 2. define an event based on that delegate
        // 3. raise an event


        public delegate void VideoEncodedEventHandler(object source, VideoEventArgs args);
        public event VideoEncodedEventHandler VideoEncodedEvent;
        //public event EventHandler<VideoEventArgs> VideoEncodedEvent; //instead of creating custom event(above) we can use built in event handler 
        // EventHandler<> is for sending custom event args, EventHandler is for default EventArgs

        public void Encode(Video video)
        {
            this.OnVideoEncoded(video);
        }

        protected virtual void OnVideoEncoded(Video video)
        {
            if (VideoEncodedEvent != null)
                VideoEncodedEvent(this, new VideoEventArgs() { Video = video });
        }
    }
}
