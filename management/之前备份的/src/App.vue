<template>
  <div id="app">
    <!-- 原始html中我们使用a标签实现页面的跳转 -->
    <!-- <a class="list-group-item active" href="./about.html">About</a> -->
    <!-- <a class="list-group-item" href="./home.html">Home</a> -->

    <!-- Vue中借助router-link标签实现路由的切换 -->
    <router-link class="list-group-item" active-class="active" to="/about"
      >About</router-link
    >
    <router-link class="list-group-item" active-class="active" to="/home"
      >Home</router-link
    >

    <!-- 指定组件的呈现位置 -->
    <router-view> </router-view>
  </div>
</template>

<script>
// import Count from "./components/Count.vue";
// import Person from "./components/Person.vue";

// import HelloWorld from './components/HelloWorld.vue'
// import Testpr from "./components/Testpr.vue";
export default {
  name: "App",

  // Testpr,
};
</script>

<style>
#app {
  font-family: Avenir, Helvetica, Arial, sans-serif;
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
  text-align: center;
  color: #2c3e50;
  margin-top: 60px;
}
</style>
