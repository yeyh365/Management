using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Management.Core.Helper
{
    public class LogHelper
    {

        private static readonly log4net.ILog loginfo = log4net.LogManager.GetLogger("loginfo");   //选择<logger name="loginfo">的配置 
        private const string UsageTracking = "UsageTracking";
        public static void Log(string info)
        {
            if (loginfo.IsInfoEnabled)
            {
                loginfo.Info(info);
            }
        }

        public static void Log(string info, Exception se)
        {
            if (loginfo.IsErrorEnabled)
            {
                loginfo.Error(info, se);
            }
        }

        /// <summary>
        /// 用户使用情况统计, 便于分析用户操作, 利于系统优化
        /// </summary>
        /// <param name="info"></param>
        public static void LogUsage(string info)
        {
            Log(string.Format("[{0}],{1}", UsageTracking, info));
        }




        private static void WriteTxtLog(string logstr, bool isException, Exception ex)
        {
            if (isException)
            {
                loginfo.Error(logstr, ex);
            }
            else
            {
                loginfo.Info(logstr);
            }
        }
    }
}
