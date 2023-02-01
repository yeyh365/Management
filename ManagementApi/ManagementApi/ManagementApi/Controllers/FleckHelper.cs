using Fleck;
using Management.Core.Helper;
using ManagementApi.Helper;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ManagementApi.Controllers
{
    /// <summary>
    /// webSocket接口
    /// </summary>
    public class FleckHelper
    {
        private static List<IWebSocketConnection> allSockets = new List<IWebSocketConnection>();
        private static Dictionary<string, IWebSocketConnection> socketDic = new Dictionary<string, IWebSocketConnection>();

        /// <summary>
        /// WebSocket初始化
        /// </summary>
        public static void WebSocketInit()
        {
            var server = new WebSocketServer("ws://1.14.96.49:8089");//此处端口随意指定，但是不能被占用
            //server.Start(socket =>
            //{
            //    socket.OnOpen = () =>
            //    {
            //        //Console.WriteLine("Open!");
            //        allSockets.Add(socket);
            //        //socketDic.Add(socket.ConnectionInfo.Id.ToString(), socket);//此处想要存储指定的key值，但是无法接收指定参数，仅供测试玩
            //        //获取客户端网页的url
            //        string key = socket.ConnectionInfo.ClientIpAddress + ":" + socket.ConnectionInfo.ClientPort;
            //        //var key = socket.ConnectionInfo.Path.Substring(1);
            //        if (!socketDic.Keys.Contains(key))
            //        {
            //            socketDic.Add(key, socket);//此处想要存储指定的key值，但是无法接收指定参数，仅供测试玩
            //        }
            //        else
            //        {
            //            if (socketDic[key] != null)
            //            {
            //                socketDic[key].Close();
            //            }
            //            socketDic[key] = socket;
            //        }
            //    };
            //    socket.OnClose = () =>
            //    {
            //        //Console.WriteLine("Close!");
            //        allSockets.Remove(socket);
            //        //socketDic.Remove(socket.ConnectionInfo.Id.ToString());//此处想要删除指定的key值，但是无法接收指定参数，仅供测试玩
            //        //获取客户端网页的url
            //        string key = socket.ConnectionInfo.ClientIpAddress + ":" + socket.ConnectionInfo.ClientPort;
            //        //var key = socket.ConnectionInfo.Path.Substring(1);
            //        socketDic.Remove(key);//此处想要删除指定的key值，但是无法接收指定参数，仅供测试玩
            //    };
            //    socket.OnMessage = message =>
            //    {
            //        string value =message;
            //        //int[][] arrys = JsonConvert.DeserializeObject<int[][]>(str);
            //        string result = "";
            //        try
            //        {
            //            string key = "FiveQi";
            //            string time = "10000";
            //            RedisHelper redis = new RedisHelper();
            //            redis.SetStringKey(key, value, int.Parse(time));
            //            value = redis.GetStringValue(key);
            //        }
            //        catch (Exception)
            //        {
            //            LogHelper.Log("Exception");
            //        }
            //        //Console.WriteLine(message);
            //        allSockets.ToList().ForEach(s => s.Send(value));
            //    };
            //});
        }

        /// <summary>
        /// 消息发送
        /// </summary>
        /// <param name="message">自定义json字符串</param>
        public static void Send(string message)
        {
            allSockets.ToList().ForEach(s => s.Send("Echo: " + message));
            //var msgInfo = JsonConvert.DeserializeObject<MessageInfo>(message);
            //var toUser = msgInfo.ToUser;
            //var msg = msgInfo.Msg;
            //if (!string.IsNullOrWhiteSpace(toUser))//已指定接收人
            //{
            //    if (socketDic.Keys.Contains(toUser))//确认是否有接收人的WebSocket
            //    {
            //        socketDic[toUser].Send(msg);//发送给指定接收人
            //    }
            //}
            //else
            //{
            //    socketDic.Values.ToList().ForEach(p => p.Send(msg));//未指定接收人全部发送
            //}
            try
            {
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// 自定义消息类
        /// </summary>
        public class MessageInfo
        {
            /// <summary>
            /// 接收人
            /// </summary>
            public string ToUser { get; set; }

            /// <summary>
            /// 发送信息
            /// </summary>
            public string Msg { get; set; }
        }
    }
}
