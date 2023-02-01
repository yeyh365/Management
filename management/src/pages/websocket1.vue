<template>
  <div class="hello">
    <ul>
      <li v-for="item in remsg" :index="item" :key="item">
        {{ item }}
      </li>
    </ul>
    <input type="text" placeholder="请输入用户名" v-model="user" />
    <input type="text" placeholder="请输入内容" v-model="msg" />
    <button @click="login">登录</button>
    <button @click="handle">发送消息</button>
  </div>
</template>

<script>

import * as signalR from "@microsoft/signalr";

let token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJVc2VyTmFtZSI6ImFkbWluIiwiSUQiOiIyIiwiZXhwIjoxNTk5NjM3NjIxLCJpc3MiOiJuZXRsb2NrIiwiYXVkIjoibmV0bG9ja3MifQ.9T1zw2LaCx4enZLj5RCfxhJ85a169NPMqmW0n5OlzgI";
let hubUrl = "https://localhost:44317/chatHub";

//.net core 版本中默认不会自动重连，需手动调用 withAutomaticReconnect 
const connection = new signalR.HubConnectionBuilder()
  .withAutomaticReconnect()//断线自动重连
  .withUrl(hubUrl, { accessTokenFactory: () => token })//传递参数Query["access_token"]
  .build();

//启动
connection.start().catch(err => {
  console.log(err);
});

//自动重连成功后的处理
connection.onreconnected(connectionId => {
  console.log(connectionId);
});

export default {
  name: "First",
  mounted () {
    var _this = this;

    //调用后端方法 SendMessageResponse 接收定时数据
    connection.on("SendMessageResponse", function (data) {
      console.log("SendMessageResponse", data.message)
      _this.remsg.push(data.message)
      console.log("SendMessageResponse_remsg", _this.remsg)
    });

    //调用后端方法 SendMessage 接受自己人发送消息
    connection.on("SendMessage", function (data) {
      if (data.state == 0)
        _this.remsg.push(data.message)

    });

    //调用后端方法 ConnectResponse 接收连接成功
    connection.on("ConnectResponse", function (data) {
      if (data.state == 0)
        _this.remsg.push(data.message)
    });
    //调用后端方法 ConnectResponse 接收连接成功
    connection.on("LoginResponse", function (data) {
      console.log(data)
    });
  },
  data () {
    return {
      user: "",
      msg: "",
      remsg: ['test']
    };
  },

  methods: {
    handle: function () {
      if (this.msg.trim() == "") {
        alert("不能发送空白消息");
        return;
      }
      //调用后端方法 SendMessage 传入参数
      connection.invoke("SendMessage", this.user, this.msg);
      this.msg = "";
    },
    login () {
      console.log('login')
      connection.invoke("Login", this.user, this.msg).catch(function (err) {
        return console.error(err.toString());
      });
    }
  }
};
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>
#message {
  overflow-y: auto;
  text-align: left;
  border: #42b983 solid 1px;
  height: 500px;
}
</style>