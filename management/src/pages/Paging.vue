<template>
  <div></div>
</template>

<script>
export default {
  name: 'Paging',
  data () {
    return {
      a: '1'
    }
  },
  //计算属性 计算呈现在页面
  computed: {

  },
  //方法写这里面
  methods: {

  },
  //vue还没有开始解析 可以通过vm访问data中的数据和methods的方法 只会触发一次
  created () {

  },
  //页面加载完成 一般开启定时器 发送网络请求
  mounted () {
    this.$axios.get('/home/getData').then(res => {
      const { code, data } = res.data
      if (code === 200) {
        this.incomeData = data.incomeData
        this.departmentData = data.departmentData
      }
      console.log(res)
    }),
      this.$axios.get('/notice/getData').then(res => {
        const { code, data } = res.data
        if (code === 200) {
          this.noticeData = data.noticeList
        }
        console.log(res)
      })
  },
  //在vue对象存活的情况下，进入当前存在activated()函数的页面时，一进入页面就触发；
  //可用于初始化页面数据、keepalive缓存组件后，可执行方法
  activated () {
    console.log('abc组件被激活了')
    this.timer = setInterval(() => {
      console.log('@')
      this.opacity -= 0.01
      if (this.opacity <= 0) {
        this.opacity = 1
      }
    }, 16);
  },
  //此时vm所有的data、methods等都处于可用状态 马上要销毁了 
  //一般在这里执行  关闭定时器 解绑自定义事件等收尾操作
  beforeDestroy () {

  },
  //vm销毁的时候 一般清除定时器
  deactivated () {
    console.log("abc组件失活了")
    clearInterval(this.timer)
  },
  //组件内守卫
  beforeRouteEnter (to, from, next) {
    console.log('aboutbeforeRouteEnter组件内守卫调用')
    next()
  },
  //组件内守卫
  beforeRouteLeave (to, from, next) {
    console.log('aboutbeforeRouteLeave组件内守卫调用')
    next()
  }
}
</script>

<style>
</style>