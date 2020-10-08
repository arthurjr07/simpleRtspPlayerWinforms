using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using SimpleRtspPlayer.RawFramesDecoding.FFmpeg;
using SimpleRtspPlayer.RawFramesDecoding;
using RtspClientSharp;
using RtspClientSharp.RawFrames.Video;
using RtspClientSharp.Rtsp;

namespace SimpleRTSPPlayerWinforms
{
    public partial class VideoControl : UserControl
    {
        private readonly Dictionary<FFmpegVideoCodecId, FFmpegVideoDecoder> _videoDecodersMap = new Dictionary<FFmpegVideoCodecId, FFmpegVideoDecoder>();
        private Bitmap bmp;
        private TransformParameters transformParameters;
        private CancellationTokenSource _cancellationTokenSource;

        public VideoControl()
        {
            InitializeComponent();
            this.DoubleBuffered = true;
            this.Paint += VideoControl_Paint;
        }

        private void VideoControl_Paint(object sender, PaintEventArgs e)
        {
            if (bmp != null)
            {
                e.Graphics.DrawImageUnscaled(bmp, Point.Empty);
            }
        }

        public async  void StartPlay(string url)
        {
            var connectionParameters = new ConnectionParameters(new Uri(url));
            if (_cancellationTokenSource != null)
            {
                _cancellationTokenSource.Dispose();
                _cancellationTokenSource = null;
            }
            _cancellationTokenSource = new CancellationTokenSource();
            TimeSpan delay = TimeSpan.FromSeconds(1);
            using (var rtspClient = new RtspClient(connectionParameters))
            {
                rtspClient.FrameReceived += RtspClient_FrameReceived;
                while (true)
                {
                    try
                    {
                        await rtspClient.ConnectAsync(_cancellationTokenSource.Token);
                        await rtspClient.ReceiveAsync(_cancellationTokenSource.Token);
                    }
                    catch (OperationCanceledException)
                    {
                        return;
                    }
                    catch (RtspClientException e)
                    {
                        await Task.Delay(delay);
                    }
                }
            }
        }

        public  void Stop()
        {
            _cancellationTokenSource?.Cancel();
            bmp?.Dispose();
            bmp = null;
            this.Invalidate();
        }

        private void RtspClient_FrameReceived(object sender, RtspClientSharp.RawFrames.RawFrame e)
        {
            if (_cancellationTokenSource == null || _cancellationTokenSource.IsCancellationRequested)
            {
                return;
            }

            if (!(e is RawVideoFrame rawVideoFrame))
            {
                return;
            }

            var codecId = DetectCodecId(rawVideoFrame);
            if (!_videoDecodersMap.TryGetValue(codecId, out FFmpegVideoDecoder decoder))
            {
                decoder = FFmpegVideoDecoder.CreateDecoder(codecId);
                _videoDecodersMap.Add(codecId, decoder);
            }
            var decodedVideoFrame = decoder.TryDecode(rawVideoFrame);
            if (decodedVideoFrame != null)
            {
                if (bmp != null)
                {
                    bmp.Dispose();
                }

                transformParameters = new TransformParameters(RectangleF.Empty, new System.Drawing.Size(this.Width, this.Height), ScalingPolicy.Stretch, PixelFormat.Bgra32, ScalingQuality.FastBilinear);
                bmp = new Bitmap(this.Width, this.Height, System.Drawing.Imaging.PixelFormat.Format32bppArgb);

                // Lock the bitmap's bits.  
                var bmpData = bmp.LockBits(this.ClientRectangle, System.Drawing.Imaging.ImageLockMode.ReadWrite, bmp.PixelFormat);

                IntPtr ptr = bmpData.Scan0;
                decodedVideoFrame.TransformTo(ptr, bmpData.Stride, transformParameters);

                //unlock bitmap's bits.
                bmp.UnlockBits(bmpData);
                this.Invalidate();
            }
        }

        private FFmpegVideoCodecId DetectCodecId(RawVideoFrame videoFrame)
        {
            if (videoFrame is RawJpegFrame)
                return FFmpegVideoCodecId.MJPEG;
            if (videoFrame is RawH264Frame)
                return FFmpegVideoCodecId.H264;

            throw new ArgumentOutOfRangeException(nameof(videoFrame));
        }
    }
}
