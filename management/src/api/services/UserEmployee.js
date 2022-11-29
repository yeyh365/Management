import axiosInstance from '../common/export'

export default {
  GetEmployee(currentPage, PageSizes) {
    return new Promise((resolve, reject) => {
      axiosInstance.get(`/UserEmployee/GetEmployeeLimit`, {
          params: {
            page: currentPage,
            limit: PageSizes
          }
        })
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
  },
  delEmployee(id) {
    console.log('USER', id)
    return new Promise((resolve, reject) => {
      axiosInstance.get(`/UserEmployee/DeleteEmployee`, {
          params: {
            Id: id,
          }
        })
        .then(response => {
          resolve(response.data)
        })
        .catch(err => {
          reject(err)
        })
    })
  },
  AddEmployee(Employee) {
    console.log('USER', Employee)
    return new Promise((resolve, reject) => {
      axiosInstance.post(`/UserEmployee/AddEmployee`, Employee)
        .then(response => {
          resolve(response.data)
        })
        .catch(err => {
          reject(err)
        })
    })
  }
}