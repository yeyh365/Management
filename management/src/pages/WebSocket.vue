<template>
  <div>
    <!-- <input v-model="user" type="text" />
    <input v-model="message" type="text" /><br />
    <button @click="sendMsg">发送</button>
    <hr />
    <ul>
      <li v-for="(item, index) in msgList" :key="index">
        {{ item.user }}:&nbsp;&nbsp;&nbsp;&nbsp;{{ item.msg }}
      </li>
    </ul> -->
  </div>
</template>

<script>
import * as signalR from "@aspnet/signalr";
export default {
  name: 'WebSocket',
  data () {
    return {
      connection: "",
      user: "",
      message: "",
      msgList: []
    };
  },
  created () {
    this.init();
  },
  methods: {
    init () {
      let thisVue = this;
      debugger
      this.connection = new signalR.HubConnectionBuilder()
        .withUrl("https://localhost:44319/MsgHub", {
          skipNegotiation: true,
          transport: signalR.HttpTransportType.WebSockets
        })
      debugger
      this.connection = this.connection.configureLogging(signalR.LogLevel.Information)
      debugger
      this.connection = this.connection.build();
      debugger
      this.connection.on("ReceiveMessage", function (user, message) {
        console.log({ user, message });
      });
      this.connection.start().then(() => {
        this.connection.invoke("HandMessage");
      });;
      debugger
      // this.connection = new signalR.HubConnectionBuilder()
      //   .withUrl(`https://localhost:44340/signalr`, {})
      //   // .configureLogging(signalR.LogLevel.Error)
      //   .build();
      // this.connection.on("ReceiveMessage", (user, message) => {
      //   console.log(user, message)
      // });
      // this.connection.start().then(() => {
      //   this.connection.invoke("HandMessage", "123", "我连上啦");
      // });
    },
    sendMsg () {
      this.connection.invoke("SendMessage", "Msg");
    }
  }
};
</script>

<style></style>

