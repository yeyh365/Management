<template>
  <div>
    <h2>QrCode</h2>
    <div class="test">
      <div class="qrcode">
        <!-- 二维码 -->
        <canvas ref="qrcodeCanvas"> </canvas>
      </div>
    </div>
  </div>
</template>

<script>
import QRcode from "qrcode";
export default {
  data () {
    return {
      url: 'http://1.14.96.49:8089/'
    }
  },
  name: 'QrCode',
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