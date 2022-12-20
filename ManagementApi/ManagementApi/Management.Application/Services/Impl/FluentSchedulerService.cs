using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FluentScheduler;
using System.Diagnostics;
using Management.Application.Dto;

namespace Management.Application.Services.Impl
{
    public  class MyFluentScheduler
    {
        /// <summary>
        /// 启动定时任务
        /// </summary>
        public static void StartUp()
        {
            JobManager.Initialize(new FluentSchedulerService());
        }

        /// <summary>
        /// 停止定时任务
        /// </summary>
        public static void Stop()
        {
            JobManager.Stop();
        }
    }
    public class FluentSchedulerService : Registry
        {
            public FluentSchedulerService()
            {
                // Schedule an IJob to run at an interval
                // 立即执行每两秒一次的计划任务。（指定一个时间间隔运行，根据自己需求，可以是秒、分、时、天、月、年等。）
                Schedule<MyJob>().ToRunNow().AndEvery(10).Seconds();

                // Schedule an IJob to run once, delayed by a specific time interval
                // 延迟一个指定时间间隔执行一次计划任务。（当然，这个间隔依然可以是秒、分、时、天、月、年等。）
                Schedule<MyJob>().ToRunOnceIn(5).Seconds();

                // Schedule a simple job to run at a specific time
                // 在一个指定时间执行计划任务（最常用。这里是在每天的下午 1:10 分执行）
                Schedule(() => Trace.WriteLine("It's 1:10 PM now.")).ToRunEvery(1).Days().At(13, 10);

                Schedule(() => {

                    // 做你想做的事儿。
                    Trace.WriteLine("It's 1:10 PM now.");

                }).ToRunEvery(1).Days().At(13, 10);

                // Schedule a more complex action to run immediately and on an monthly interval
                // 立即执行一个在每月的星期一 3:00 的计划任务（可以看出来这个一个比较复杂点的时间，它意思是它也能做到！）
                Schedule<MyComplexJob>().ToRunNow().AndEvery(1).Months().OnTheFirst(DayOfWeek.Monday).At(3, 0);

                // Schedule multiple jobs to be run in a single schedule
                // 在同一个计划中执行两个（多个）任务
                Schedule<MyJob>().AndThen<MyOtherJob>().ToRunNow().AndEvery(5).Minutes();

            }


        }

        public class MyJob : IJob
        {

            void IJob.Execute()
            {
                Trace.WriteLine("现在时间是：" + DateTime.Now);
              UserService userService = new UserService();
            SearchEmployeeDto searchEmployeeDto = new SearchEmployeeDto();
            searchEmployeeDto.EmployeeId = new Random().Next(3000, 4000).ToString();
            searchEmployeeDto.EmployeeName = "这是新加的";
            searchEmployeeDto.DepartmentNumber = 1;
            searchEmployeeDto.PositionNumber = 1;
            userService.AddEmployee(searchEmployeeDto);


            }
        }

        public class MyOtherJob : IJob
        {

            void IJob.Execute()
            {
                Trace.WriteLine("这是另一个 Job ，现在时间是：" + DateTime.Now);
            }
        }

        public class MyComplexJob : IJob
        {

            void IJob.Execute()
            {
                Trace.WriteLine("这是比较复杂的 Job ，现在时间是：" + DateTime.Now);
            }
        }

    }

