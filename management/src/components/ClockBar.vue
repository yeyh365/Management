<template>
  <div>
    <div class="time">
      <div class="time">{{ time }}</div>
    </div>
    <div class="day">
      <span>{{ date }}</span>
      <span>{{ week }}</span>
    </div>
  </div>
</template> 

<script>
export default {
  name: 'ClockBar',
  data () {
    return {
      time: '',
      date: '',
      week: '',
    }
  },
  mounted () {
    this.updateTime();
    var timeer = setInterval(this.updateTime, 1000)
  },
  methods: {
    zeroPadding (num, digit) {
      let zero = "";
      for (let i = 0; i < digit; i++) {
        zero += "0";
      }
      return (zero + num).slice(-digit);
    },
    updateTime () {
      const week = [
        "星期日",
        "星期一",
        "星期二",
        "星期三",
        "星期四",
        "星期五",
        "星期六",
      ];
      const cd = new Date();
      this.time =
        this.zeroPadding(cd.getHours(), 2) +
        ":" +
        this.zeroPadding(cd.getMinutes(), 2) +
        ":" +
        this.zeroPadding(cd.getSeconds(), 2);
      this.date =
        this.zeroPadding(cd.getFullYear(), 4) +
        "年" +
        this.zeroPadding(cd.getMonth() + 1, 2) +
        "月" +
        this.zeroPadding(cd.getDate(), 2) +
        "日";

      this.week = week[cd.getDay()];
    },
    destroyed () {
      // 销毁定时器
      clearInterval(this.timeer);
    }
  },
}
</script>

<style scoped>
.time {
  display: flex;
  width: 100%;
  justify-content: space-between;
}
.time > div {
  width: 30%;
  height: 150px;
  font-size: 90px;
  font-weight: 600;
  text-align: center;
  line-height: 150px;
  color: #1c1a1a;
}
.day {
  /* display: flex; */
  justify-content: center;
  height: 50px;
  font-size: 26px;
  /* text-align: center; */
  line-height: 40px;
  color: #1c1a1a;
}
.day > span {
  margin-left: 30px;
}
</style>