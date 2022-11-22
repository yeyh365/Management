<template>
  <div>
    <div>
      <h2>我是About的内容</h2>
      <h2>我是About的内容</h2>
      <h2>我是About的内容</h2>
      <h2>我是About的内容</h2>
    </div>
  </div>
</template>

<script>
export default {
  name: 'About',
  beforeDestroy () {
    console.log('about被销毁了')
  },
  mounted () {
    console.log('about被挂载了')
  },
  beforeRouteEnter (to, from, next) {
    console.log('aboutbeforeRouteEnter组件内守卫调用')
    next()
  },
  beforeRouteLeave (to, from, next) {
    console.log('aboutbeforeRouteLeave组件内守卫调用')
    next()
  }
}
</script>