using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Timers;
using TimeApp.Data;
using TimeApp.Models;

namespace TimeApp.Infrastructure.TimerService
{
    public class TimerService
    {
        private Timer Timer = new Timer();
        public double Seconds { get; private set; }
        private readonly ApplicationDbContext _context;

        public TimerService(ApplicationDbContext context)
        {
            _context = context;
        }


        public void SetSeconds(List<Time> userTimes) {
            double totalSeconds = 0;
            if (!userTimes.Any()) this.Seconds = 0;
            for (int i = 0; i < userTimes.Count(); i++)
            {
                if (i % 2 != 0)
                {
                    totalSeconds += ((userTimes[i].DateTime.Hour * 3600) - (userTimes[i - 1].DateTime.Hour * 3600)) + ((userTimes[i].DateTime.Minute * 60) - (userTimes[i - 1].DateTime.Hour * 60)) + (userTimes[i].DateTime.Second - userTimes[i - 1].DateTime.Second);
                }
            }
            this.Seconds = totalSeconds;
        }

        public void ChangeState(bool isStarted) 
        {
            if (isStarted) StopTimer(); 
            else StartTimer();
        }

        private void StartTimer() {
            Timer.Interval = 1000;
            Timer.Elapsed += (Object? sender, ElapsedEventArgs eventArgs) => Seconds++;
            Timer.Start();
        }
        private void StopTimer() => Timer.Stop();

        public string EllapsedTime()
        {
            var hours = Math.Floor(Seconds / 3600);
            var minutes = Math.Floor((Seconds - (hours * 3600)) / 60);
            return ($"{hours.ToString("{0:00}")}:{minutes.ToString("{0:00}")}:{(Seconds % 60).ToString("{0:00}")}");
        }
    }
}
