import axiosInstance from '../common/export'

export default {
  GetEmployee(EmployeeId, EmployeeName, DepartmentNumber, currentPage, PageSizes) {
    return new Promise((resolve, reject) => {
      axiosInstance.get(`/UserEmployee/GetEmployeeLimit`, {
          params: {
            EmployeeId: EmployeeId,
            EmployeeName: EmployeeName,
            DepartmentNumber: DepartmentNumber,
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
  },
  UpdateEmployee(Employee) {
    console.log('USER', Employee)
    return new Promise((resolve, reject) => {
      axiosInstance.post(`/UserEmployee/UpdateEmployee`, Employee)
        .then(response => {
          resolve(response.data)
        })
        .catch(err => {
          reject(err)
        })
    })
  },
  AddUser(User) {
    console.log('USER', User)
    return new Promise((resolve, reject) => {
      axiosInstance.post(`/UserEmployee/AddUser`, User)
        .then(response => {
          resolve(response.data)
        })
        .catch(err => {
          reject(err)
        })
    })
  },
  UpdateUser(User) {
    console.log('USER', User)
    return new Promise((resolve, reject) => {
      axiosInstance.post(`/UserEmployee/UpdateUser`, User)
        .then(response => {
          resolve(response.data)
        })
        .catch(err => {
          reject(err)
        })
    })
  },
  DeleteUser(Id) {
    console.log('USER', Id)
    return new Promise((resolve, reject) => {
      axiosInstance.get(`/UserEmployee/DeleteUser`, {
          params: {
            Id: Id
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
  ExportEmployeeList() {
    return new Promise((resolve, reject) => {
      axiosInstance
        .get(`UserEmployee/ExportEmployee`, {
          headers: {
            Accept: 'application/vnd.openxmlformats-officedocument' +
              '.spreadsheetml.sheet'
          },
          responseType: 'blob',

        })
        .then(response => {
          resolve(response.data)
        })
        .catch(err => {
          reject(err)
        })
    })
  },
  ExportUserList() {
    return new Promise((resolve, reject) => {
      axiosInstance
        .get(`UserEmployee/ExportUser`, {
          headers: {
            Accept: 'application/vnd.openxmlformats-officedocument' +
              '.spreadsheetml.sheet'
          },
          responseType: 'blob',

        })
        .then(response => {
          resolve(response.data)
        })
        .catch(err => {
          reject(err)
        })
    })
  },
  GetUserFace() {
    return new Promise((resolve, reject) => {
      axiosInstance
        .get(`Pictures/UserFace`, {
          // headers: {
          //   Accept: 'application/vnd.openxmlformats-officedocument' +
          //     '.spreadsheetml.sheet'
          // },
          // responseType: 'blob',

        })
        .then(response => {
          resolve(response.data)
        })
        .catch(err => {
          reject(err)
        })
    })
  },

}