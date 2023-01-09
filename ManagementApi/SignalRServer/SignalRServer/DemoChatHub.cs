using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SignalRServer
{
    [HubName("demoChatHub")]
    public class DemoChatHub : Hub
    {
        /// <summary>
        /// 已登录的用户信息
        /// </summary>
        public static List<UserModel> OnlineUser { get; set; } = new List<UserModel>();

        /// <summary>
        /// 模拟存放在数据库里的用户信息
        /// </summary>
        private static readonly List<UserModel> _dbuser = new List<UserModel> {
          new UserModel{
            UserName = "test1", Password = "111", GroupName = "Group1"
          },
          new UserModel{
            UserName = "test2", Password = "111", GroupName = "Group2"
          },
          new UserModel{
            UserName = "test3", Password = "111", GroupName = "Group2"
          },
          new UserModel{
            UserName = "test4", Password = "111", GroupName = "Group1"
          },
          new UserModel{
            UserName = "test5", Password = "111", GroupName = "Group3"
          },
        };

        /// <summary>
        /// 登录验证
        /// </summary>
        [HubMethodName("login")]
        public async Task Login(string username, string password)
        {
            username = "test1";
            password = "111";
            string connid = Context.ConnectionId;
            ResultModel result = new ResultModel
            {
                Status = 0,
                Message = "登录成功！"
            };
            if (!OnlineUser.Exists(u => u.ID == connid))
            {
                var model = _dbuser.Find(u => u.UserName == username && u.Password == password);
                if (model != null)
                {
                    model.ID = connid;
                    OnlineUser.Add(model);
                    //给当前的连接分组
                    await Groups.Add(connid, model.GroupName);
                }
                else
                {
                    result.Status = 1;
                    result.Message = "账号或密码错误！";
                }
            }
            //给当前连接返回消息
            await Clients.Client(connid).LoginResponse(result);
        }

        /// <summary>
        /// 获取所在组的在线用户
        /// </summary>
        [HubMethodName("getUsers")]
        public async Task GetUsers()
        {
            var model = OnlineUser.Find(u => u.ID == Context.ConnectionId);
            ResultModel result = new ResultModel();
            if (model == null)
            {
                result.Status = 1;
                result.Message = "请先登录！";
            }
            else
            {
                result.Status = 0;
                result.OnlineUser = OnlineUser.FindAll(u => u.GroupName == model.GroupName);
            }
            //给所在组返回消息
            await Clients.Group(model.GroupName).GetUsersResponse(result);
        }

        /// <summary>
        /// 推送消息
        /// </summary>
        [HubMethodName("sendMessage")]
        public async Task SendMessage(string user, string message)
        {
            ResultModel result = new ResultModel();
            var model = OnlineUser.Find(u => u.ID == Context.ConnectionId);
            if (model == null)
            {
                result.Status = 1;
                result.Message = "请先登录！";
            }
            else
            {
                result.Status = 0;
                result.Message = $"“{user}”发送的消息：{message}";
            }
            await Clients.Group(model.GroupName).SendMessageResponse(result);
        }
        /// <summary>
        /// 接收前端消息，并发送给连接的全部用户
        /// </summary>
        /// <param name="user"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public async Task HandMessage(string user, string message)
        {
            //TO DO 可以加缓存，存储用户信息和对应的连接Id
            await Clients.All.SendAsync("ReceiveMessage", user, message);
        }
        /// <summary>
        /// 连接成功
        /// </summary>
        /// <returns></returns>
        public override async Task OnConnected()
        {
            await Clients.All.SendAsync("ConnectMessage", Context.ConnectionId);
        }
        ///// <summary>
        ///// 当连接成功时的处理
        ///// </summary>
        //public override Task OnConnected()
        //{
        //    return base.OnConnected();
        //}

        /// <summary>
        /// 当连接断开时的处理
        /// </summary>
        public override Task OnDisconnected(bool stopCalled)
        {
            string connid = Context.ConnectionId;
            var model = OnlineUser.Find(u => u.ID == connid);
            int count = OnlineUser.RemoveAll(u => u.ID == connid);
            if (model != null)
            {
                ResultModel result = new ResultModel()
                {
                    Status = 0,
                    OnlineUser = OnlineUser.FindAll(u => u.GroupName == model.GroupName)
                };
                Clients.Group(model.GroupName).GetUsersResponse(result);
            }
            return base.OnDisconnected(stopCalled);
        }
    }

    public class UserModel
    {
        [JsonProperty("id")]
        public string ID { get; set; }

        [JsonProperty("userName")]
        public string UserName { get; set; }

        [JsonProperty("password")]
        public string Password { get; set; }

        [JsonProperty("groupName")]
        public string GroupName { get; set; }
    }

    public class ResultModel
    {
        [JsonProperty("status")]
        public int Status { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }

        [JsonProperty("onlineUser")]
        public List<UserModel> OnlineUser { get; set; }
    }
}