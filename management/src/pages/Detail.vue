<template>
  <div>
    <ul>
      <li>消息编号{{ id }}</li>
      <li>消息编号{{ title }}</li>
    </ul>
  </div>
</template>

<script>
export default {
  name: 'Detail',
  props: ['id', 'title'],
  beforeDestroy () {
    console.log('detail被销毁了')
  },
  mounted () {
    console.log('detail被挂载了')
    console.log(this.$route)
    console.log(this)
  }
}
</script>

<style>
</style>