import axiosInstance from '../common/export'

export default {
  GetApplyList(a, b) {

    return new Promise((resolve, reject) => {
      axiosInstance.get(`/Apply/GetUserApply`, {
          params: {
            Page: a,
            Limit: b,
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
  AddApply(applyProcess) {
    return new Promise((resolve, reject) => {
      axiosInstance.post(`/Apply/AddApply`, applyProcess)
        .then(response => {
          resolve(response.data)
        })
        .catch(err => {
          reject(err)
        })
    })
  },
  DelApply(applyProcess) {
    return new Promise((resolve, reject) => {
      axiosInstance.post(`/Apply/AddApply`, applyProcess)
        .then(response => {
          resolve(response.data)
        })
        .catch(err => {
          reject(err)
        })
    })
  },

}