//引入Vue
import Vue from 'vue'
//引入App
import App from './App.vue'
import axios from 'axios'
import store from './store'
import element from 'element-ui'
import VueRouter from 'vue-router'
import router from './router'
import 'element-ui/lib/theme-chalk/index.css'
Vue.use(element)
Vue.config.productionTip = false

Vue.use(VueRouter)
new Vue({
  el: '#app',
  render: h => h(App),
  store,
  axios,
  element,
  router
})