<template>
  <div class="gobang">
    <canvas id="gobang" width="800" height="600"></canvas>
    <el-button type="primary" @click="Init">初始化</el-button>
  </div>
</template>

<script>
import { time } from 'echarts';
import FiveQiService from '../api/services/FiveQiService'
const CheckStrWhite = "11111";
const CheckStrBlack = "22222";
export default {
  name: 'WebSocket',
  data () {
    return {
      ctx: null,
      winGame: false,
      whiteTurn: false, // false-白棋轮；true-黑棋轮
      resultArr: [], // 记录棋子位置的数组
      timer: ''

    }
  },
  mounted () {
    let _this = this;
    let container = document.getElementById("gobang");

    container.addEventListener("click", _this.handleClick);

    _this.ctx = container.getContext("2d");
    _this.ctx.translate(70, 70);
    _this.drawCheckerboard();

    this.getAllArray();
    this.initWebSocket();
    // this.timer = window.setInterval(() => {
    //   console.log('定时器')
    //   this.getAllFiveQi();
    // }, 2000);
    // setInterval(() => {
    //   ws.send("hello");
    // }, 3000);
  },
  computed: {
    chessText () {
      return this.whiteTurn ? '白棋' : '黑棋';
    },

  },
  methods: {
    initWebSocket () {
      const wsuri = "ws://1.14.96.49:8089";
      this.websock = new WebSocket(wsuri);
      this.websock.onmessage = this.websocketonmessage;
      this.websock.onopen = this.websocketonopen;
      this.websock.onerror = this.websocketonerror;
      this.websock.onclose = this.websocketclose;
    },
    websocketonopen () { //连接建立之后执行send方法发送数据
      // let actions = { "test": "12345" };
      // this.websocketsend('连接成功发送出来的数据');
      console.log('连接成功')
    },
    websocketonerror () {//连接建立失败重连
      this.initWebSocket();
    },
    websocketonmessage (e) { //数据接收
      this.getAllFiveQi(e)
      console.log(e.data)
    },
    websocketsend (Data) {//数据发送
      this.websock.send(Data);
    },
    websocketclose (e) {  //关闭
      console.log('断开连接', 'asdf');
    },
    drawCheckerboard () {
      // 画棋盘
      let _this = this;
      _this.ctx.beginPath();
      _this.ctx.fillStyle = "#fff";
      _this.ctx.rect(0, 0, 450, 450);
      _this.ctx.fill();
      for (var i = 0; i < 15; i++) {
        _this.ctx.beginPath();
        _this.ctx.strokeStyle = "#D6D1D1";
        _this.ctx.moveTo(15 + i * 30, 15); //垂直方向画15根线，相距30px;
        _this.ctx.lineTo(15 + i * 30, 435);
        _this.ctx.stroke();
        _this.ctx.moveTo(15, 15 + i * 30); //水平方向画15根线，相距30px;棋盘为14*14；
        _this.ctx.lineTo(435, 15 + i * 30);
        _this.ctx.stroke();
        _this.resultArr.push(new Array(15).fill(0));
      }
      _this.drawText();
    },
    ///画棋子的代码
    drawChess (x, y) {
      let _this = this;
      let xLine = Math.round((x - 15) / 30); // 竖线第x条
      let yLine = Math.round((y - 15) / 30); // 横线第y条
      if (_this.resultArr[xLine][yLine] !== 0) {
        return;
      }
      let grd = _this.ctx.createRadialGradient(
        xLine * 30 + 15,
        yLine * 30 + 15,
        4,
        xLine * 30 + 15,
        yLine * 30 + 15,
        10
      );
      grd.addColorStop(0, _this.whiteTurn ? "#fff" : "#4c4c4c");
      grd.addColorStop(1, _this.whiteTurn ? "#dadada" : "#000");

      _this.ctx.beginPath();
      _this.ctx.fillStyle = grd;
      _this.ctx.arc(
        xLine * 30 + 15,
        yLine * 30 + 15,
        10,
        0,
        2 * Math.PI,
        false
      );
      //画上棋子
      _this.ctx.fill();
      _this.ctx.closePath();

      _this.setResultArr(xLine, yLine);
      _this.checkResult(xLine, yLine);
    },
    setResultArr (m, n) {
      let _this = this;
      console.log('resultarr', typeof (m), typeof (n))
      _this.resultArr[m][n] = _this.whiteTurn ? 1 : 2; // 白棋为1；黑棋为2

    },
    ///画棋子的代码
    drawChessAll (x, y) {
      let _this = this;
      let xLine = Math.round((x - 15) / 30); // 竖线第x条
      let yLine = Math.round((y - 15) / 30); // 横线第y条
      // if (_this.resultArr[xLine][yLine] !== 0) {
      //   return;
      // }
      let grd = _this.ctx.createRadialGradient(
        xLine * 30 + 15,
        yLine * 30 + 15,
        4,
        xLine * 30 + 15,
        yLine * 30 + 15,
        10
      );
      grd.addColorStop(0, _this.whiteTurn ? "#fff" : "#4c4c4c");
      grd.addColorStop(1, _this.whiteTurn ? "#dadada" : "#000");

      _this.ctx.beginPath();
      _this.ctx.fillStyle = grd;
      _this.ctx.arc(
        xLine * 30 + 15,
        yLine * 30 + 15,
        10,
        0,
        2 * Math.PI,
        false
      );

      _this.ctx.fill();
      _this.ctx.closePath();

      _this.setResultArr(xLine, yLine);
      _this.checkResult(xLine, yLine);
    },
    // 判断是否有5子相连
    checkResult (m, n) {
      let _this = this;
      let checkStr = _this.whiteTurn ? CheckStrWhite : CheckStrBlack;
      // 取出[m,n]横竖斜四条线的一维数组
      let lineVertical = _this.resultArr[m].join('');
      if (lineVertical.indexOf(checkStr) > -1) {
        _this.winGame = true;
        return;
      }
      let lineHorizontal = [];
      for (let i = 0; i < 15; i++) {
        lineHorizontal.push(_this.resultArr[i][n]);
      }
      lineHorizontal = lineHorizontal.join('');
      if (lineHorizontal.indexOf(checkStr) > -1) {
        _this.winGame = true;
        return;
      }
      let line135 = [];
      for (let j = 0; j < 15; j++) {
        if (m - j >= 0 && n - j >= 0) { // 左上角
          line135.unshift(_this.resultArr[m - j][n - j]);
        }
        if (j > 0 && m + j < 15 && n + j < 15) { // 右下角
          line135.push(_this.resultArr[m + j][n + j]);
        }
      }
      line135 = line135.join('');
      if (line135.indexOf(checkStr) > -1) {
        _this.winGame = true;
        return;
      }
      let line45 = [];
      for (let j = 0; j < 15; j++) {
        if (m + j < 15 && n - j >= 0) { // 右上角
          line45.unshift(_this.resultArr[m + j][n - j]);
        }
        if (j > 0 && m - j >= 0 && n + j < 15) { // 左下角
          line45.push(_this.resultArr[m - j][n + j]);
        }
      }
      line45 = line45.join('');
      if (line45.indexOf(checkStr) > -1) {
        _this.winGame = true;
        return;
      }
    },
    drawText () {
      let _this = this;
      _this.ctx.clearRect(435 + 60, 0, 100, 70);
      _this.ctx.fillStyle = "#fff";
      _this.ctx.font = "20px Arial";
      _this.ctx.fillText('本轮：' + _this.chessText, 435 + 70, 35);
    },
    //画完棋子判断是否胜利
    drawResult () {
      let _this = this;
      _this.ctx.fillStyle = "#ff2424";
      _this.ctx.font = "20px Arial";
      _this.ctx.fillText(_this.chessText + '胜！', 435 + 70, 70);
    },
    //点击的时候画棋子
    handleClick (event) {
      let x = event.offsetX - 70;
      let y = event.offsetY - 70;
      if (x < 15 || x > 435 || y < 15 || y > 435) {
        // 点出界的
        return;
      }
      this.drawChess(x, y);
      if (this.winGame) {
        this.drawResult();
        return;
      }
      this.drawText();
      this.resultArr[15] = ([this.whiteTurn ? 8 : 9])
      console.log(this.resultArr)
      var str1 = JSON.stringify(this.resultArr);
      this.websocketsend(str1)
      // FiveQiService.postFive(this.resultArr)
      //   .then(res => {
      //     // const token = res.Data
      //     if (res) {

      //     } else {
      //       alert(res.Message)
      //     }
      //   })
      this.whiteTurn = !this.whiteTurn;
    },
    ///初始化画出数组类所有棋子
    getAllArray () {
      // // console.log(this.resultArr)
      // let result = this.resultArr;
      // for (let i = 0; i < result.length; i++) {
      //   // console.log('result', result[i])
      //   for (let j = 0; j < result[i].length; j++) {
      //     console.log(result[i][j].toString())
      //     if (result[i][j] != 0) {

      //       if (result[i][j] = 1) {
      //         _this.whiteTurn = false;
      //         this.drawChess(i * 30 + 15, j * 30 + 15)
      //       } else if (result[i][j] = 2) {
      //         _this.whiteTurn = true;
      //         this.drawChess(i * 30 + 15, j * 30 + 15)
      //       }

      //     }

      //   }

      // }
    },
    getAllFiveQi (res) {
      let _this = this;
      // FiveQiService.getFive()
      //   .then(res => {})
      // const token = res.Data
      if (res.data) {

        this.resultArr = JSON.parse(res.data)
        console.log('this.resultArr', this.resultArr)
        let result = this.resultArr
        console.log('result', result.length)
        for (let i = 0; i < result.length - 1; i++) {
          for (let j = 0; j < result[i].length; j++) {
            console.log('resultij', result[i][j])
            if (result[i][j] != 0) {
              if (result[i][j] == 1) {
                this.whiteTurn = true;
                this.drawChessAll(i * 30 + 15, j * 30 + 15)
              } else if (result[i][j] == 2) {
                this.whiteTurn = false;
                this.drawChessAll(i * 30 + 15, j * 30 + 15)
              }
            }

          }

        }
        this.whiteTurn = !(result[result.length - 1][0] == 8 ? true : false)
      } else {
        alert(res.Message)
      }

    },
    Init () {
      var Init = new Array();
      for (let i = 0; i < 15; i++) {
        Init[i] = new Array(i);
        for (let j = 0; j < 15; j++) {
          Init[i][j] = 0
        }
      }
      //黑色默认9  白色默认8
      Init[15] = 9
      console.log(Init)
      FiveQiService.postFive(Init)
        .then(res => {
          // const token = res.Data
          if (res) {
            this.getAllFiveQi();
          } else {
            alert(res.Message)
          }
        })
    }
  },
  destroyed () {
    console.log("清除定时器")
    window.clearInterval(this.timer)
    this.timer = null
    this.websock.onclose()
  },
}
</script>

<style scoped >
#gobang {
  background: #2a4546;
}
</style>