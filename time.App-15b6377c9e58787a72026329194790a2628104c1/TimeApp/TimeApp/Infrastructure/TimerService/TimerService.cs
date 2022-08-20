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
        public double Seconds(List<Time> userTimes) {
            double totalSeconds = 0;
            if (!userTimes.Any()) return 0;
            for (int i = 0; i < userTimes.Count(); i++)
            {
                if (i % 2 != 0)
                {
                    totalSeconds += ((userTimes[i].DateTime.Hour * 3600) - (userTimes[i - 1].DateTime.Hour * 3600)) + ((userTimes[i].DateTime.Minute * 60) - (userTimes[i - 1].DateTime.Hour * 60)) + (userTimes[i].DateTime.Second - userTimes[i - 1].DateTime.Second);
                }
            }
            return totalSeconds;
        }
    }
}
