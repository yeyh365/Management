<template>
  <div class="wrap">
    <h2>Layout</h2>
    <el-container class="main-container">
      <div>test</div>
      <router-view />
    </el-container>
  </div>
</template>

<script>
export default {
  name: 'Layout',
  data () {
    return {
      Type: 4,
      Num: 5
    }
  },
  created () {
  },
  methods: {
  },
  components: {
  }
}
</script>

<style>
::v-deep .el-tabs__nav-scroll {
  width: 50% !important;
  margin: 20px auto !important;
}
.el-tabs__content {
  display: none !important;
}
</style>