import axios from "axios";
import store from "@/store";
import router from "@/router";
import NProgress from "nprogress";


var axiosInstance = axios.create({
  baseURL: 'https://localhost:44369/api/',
  timeout: 100000,
  headers: {
    'Content-Type': 'application/json'
  }
})
// 请求拦截
axiosInstance.interceptors.request.use(
  config => {
    NProgress.start()
    const token = sessionStorage.getItem('token')
    if (token) {
      // config.headers.Authorization = token
      config.headers.Authorization = 'Bearer ' + token
    }
    return config;
  },
  error => {
    return Promise.reject(error)
  }
)

// 响应拦截
axiosInstance.interceptors.response.use(
  response => {
    NProgress.done()
    return response
  },
  error => {
    if (error.response) {
      switch (error.response.status) {
        case 401:
          router.replace({
            name: '404'
          })
          break
        case 403:
          router.replace({
            name: '404'
          })
          break
        case 500:
          router.replace({
            name: '404'
          })
          break
      }
    }
    return Promise.reject(error)
  }
)

export default axiosInstance