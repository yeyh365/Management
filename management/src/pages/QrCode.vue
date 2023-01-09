<template>
  <div>
    <h2>QrCode</h2>
    <div class="test">
      <div class="qrcode" style="width: 100%">
        <!-- 二维码 -->
        <canvas ref="qrcodeCanvas"> </canvas>
      </div>
      <div style="width: 100px; height: 100px">
        <p>
          <i class="iconfont icon-bankuailundong" style="font-size: 100px"></i>
        </p>
      </div>

      <div class="verification" style="font-size: 100px">
        <img :src="this.verificationImg" alt="" style="font-size: 100px" />
        <button @click="acquireVerification">aaaaa</button>
      </div>
    </div>
  </div>
</template>

<script>
import axios from 'axios'
import QRcode from "qrcode";
export default {
  data () {
    return {
      url: 'http://1.14.96.49:8089/',
      verificationImg: '',
      ruleForm: {
        verification: ''
      },
      name: 'QrCode',

    }
  },
  methods: {

    acquireVerification () {
      // {responseType: 'blob'} ，不加这个返回的就是乱码
      //直接获取
      axios.get('https://localhost:44369/api/Pictures/UserFace', { responseType: 'blob' })
        .then((response) => {
          console.log(response.data)
          this.verificationImg = window.URL.createObjectURL(response.data)
          // console.log(this.verificationImg)
        })
      //加参数的
      // axios({
      //   method: "get",
      //   url: "/purchase/captcha.jpg",
      //   params: {
      //     uuid: this.loginForm.uuid,
      //   },
      //   responseType: "blob",
      // })
      //   .then(response => {
      //     console.log(response.data);
      //     this.verificationImg = window.URL.createObjectURL(response.data)
      //   })
      //   .catch(error => {
      //     console.log(error.data);
      //   });

    }

  },
  mounted () {
    // 这个库传入两个参数
    // 1. canvas dom
    // 2. 字符串
    QRcode.toCanvas(
      this.$refs.qrcodeCanvas,
      this.url,
      {
        width: 200,//自定义宽度
        color: {
          dark: '#1e1e1e', //自定义码的颜色
          light: "#fff", //自定义背景颜色
        },
      }
    );
  },
}
</script>

<style>
</style>