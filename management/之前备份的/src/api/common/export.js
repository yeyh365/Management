import axios from 'axios'
import store from '@/store/index.js'
import router from '../../router'
// 引入nprogress
import NProgress from 'nprogress'
import 'nprogress/nprogress.css' // 这个样式必须引入
// import router from '@/router'
// import {BASE_URL} from './const.js'
// import {showFullScreenLoading, tryHideFullScreenLoading} from './loading'
var baseURL = window.g.base_url
var axiosInstance = axios.create({
  baseURL: "https://localhost:44327/api/excel1/TableUser",
  timeout: window.g.timeout, // 20s
  headers: {'Content-Type': 'application/json'}
})
axiosInstance.interceptors.request.use(
  config => {
    NProgress.start()
    // showFullScreenLoading()
    // 请求头添加token
    if (store.getters.userToken) {
      config.headers.Authorization = 'Bearer ' + store.getters.userToken
      config.headers.ApiKey = store.getters.userToken
    }
    // const token = sessionStorage.getItem('userToken')
    // if (token) {
    //   config.headers.Authorization = 'Bearer ' + token
    // }
    return config
  },
  error => {
    // tryHideFullScreenLoading()
    return Promise.reject(error)
  }
)

axiosInstance.interceptors.response.use(
  response => {
    NProgress.done()
    return response
  },
  error => {
    if (error.response) {
      switch (error.response.status) {
        case 401:
          router.replace({name: '404'})
          break
        case 403:
          router.replace({name: '404'})
          break
        case 500:
          router.replace({name: '404'})
          break
      }
    }
    return Promise.reject(error)
  }
)

export default axiosInstance
