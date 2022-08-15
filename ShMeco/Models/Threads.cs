using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Accord.Video.FFMPEG;

namespace Exhibition.Models
{
    internal static class Threads
    {
        public static VideoFileReader VideoRider1;
        public static VideoFileReader VideoRider2;
        public static Thread VideoThread1;
        public static Thread VideoThread2;
    }
}
