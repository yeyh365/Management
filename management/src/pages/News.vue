<template>
  <div>
    <ul>
      <li :style="{ opacity }">这就是我</li>
      <li>news001</li>
      <li>news002</li>
      <li>news003</li>
    </ul>
  </div>
</template>

<script>
export default {
  name: 'News',
  data () {
    return {
      opacity: 1
    }
  },
  beforeDestroy () {
    console.log("news被销毁了")
  },
  mounted () {
    console.log("news组件被加载了")
  },
  activated () {
    console.log('news组件被激活了')
    this.timer = setInterval(() => {
      console.log('@')
      this.opacity -= 0.01
      if (this.opacity <= 0) {
        this.opacity = 1
      }
    }, 16);
  },
  deactivated () {
    console.log("news组件失火了")
    clearInterval(this.timer)
  },
}
</script>

<style>
</style>