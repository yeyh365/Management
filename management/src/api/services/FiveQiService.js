import axiosInstance from '../common/export'
export default {
  getFive() {
    return new Promise((resolve, reject) => {
      axiosInstance.get(`/FiveQi/GetFive`)
        .then(response => {
          resolve(response.data)
        })
        .catch(err => {
          reject(err)
        })
    })
  },
  postFive(str1) {
    return new Promise((resolve, reject) => {
      axiosInstance.post(`/FiveQi/PostFive`, str1)
        .then(response => {
          resolve(response.data)
        })
        .catch(err => {
          reject(err)
        })
    })
  }
}