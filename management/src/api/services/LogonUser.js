import axiosInstance from '../common/export'
export default {
  Login(UserName, PassWord) {
    return new Promise((resolve, reject) => {
      axiosInstance.get(`/Logins/Login`, {
          params: {
            UserName: UserName,
            PassWord: PassWord
          }
        })
        .then(response => {
          resolve(response.data)
        })
        .catch(err => {
          reject(err)
        })
    })
  }
}