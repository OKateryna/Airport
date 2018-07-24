using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Timers;
using Airport.BL.Abstractions;
using Airport.BL.Dto.Flight;

namespace Airport.BL.Helpers
{
    public static class DelayServiceHelper
    {
        private const int TimerDelay = 5000;

        public static async Task<IEnumerable<FlightDto>> GetFlightsWithDelay(this IFlightService service)
        {
            var taskCompletionSource = new TaskCompletionSource<IEnumerable<FlightDto>>();
            var timer = new Timer(TimerDelay);

            timer.Enabled = true;
            timer.AutoReset = false;

            try
            {
                ElapsedEventHandler timerOnElapsed =
                    async (obj, args) =>
                    {
                        taskCompletionSource.SetResult(await service.GetAll());
                        timer.Enabled = false;
                    };

                timer.Elapsed += timerOnElapsed;
            }
            catch (Exception ex)
            {
                taskCompletionSource.SetException(ex);
            }

            return await taskCompletionSource.Task;
        }
    }
}