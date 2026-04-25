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


        public delegate void VideoEncodedEventHandler(object source, EventArgs args);

        public event VideoEncodedEventHandler VideoEncodedEvent;

        public void Encode(Video video)
        {
            this.OnVideoEncoded();
        }

        protected virtual void OnVideoEncoded()
        {
            if (VideoEncodedEvent != null)
                VideoEncodedEvent(this, EventArgs.Empty);
        }
    }
}
