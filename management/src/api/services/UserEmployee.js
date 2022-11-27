import axiosInstance from '../common/export'

export default {
  GetEmployee() {
    return new Promise((resolve, reject) => {
      axiosInstance.get(`/UserEmployee/GetEmployeeInfo`)
        .then(response => {
          resolve(response.data)
        })
        .catch(err => {
          reject(err)
        })
    })
  },
  GetUser() {
    return new Promise((resolve, reject) => {
      axiosInstance.get(`/UserEmployee/GetUserInfo`)
        .then(response => {
          resolve(response.data)
        })
        .catch(err => {
          reject(err)
        })
    })
  }
}