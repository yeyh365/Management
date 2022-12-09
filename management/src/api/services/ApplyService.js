import axiosInstance from '../common/export'

export default {
  GetApplyList(page, limit, account) {

    return new Promise((resolve, reject) => {
      axiosInstance.get(`/Apply/GetUserApply`, {
          params: {
            Page: page,
            Limit: limit,
            Account: account
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
  GetApprovalApply(a, b, account) {

    return new Promise((resolve, reject) => {
      axiosInstance.get(`/Apply/ApprovalApply`, {
          params: {
            Page: a,
            Limit: b,
            Account: account
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
      axiosInstance.post(`/Apply/DeletApply`, applyProcess)
        .then(response => {
          resolve(response.data)
        })
        .catch(err => {
          reject(err)
        })
    })
  },
  ///同意审批
  AgreeApply(agreeApplyProcess) {
    return new Promise((resolve, reject) => {
      axiosInstance.post(`/Apply/ProcessApply`, agreeApplyProcess)
        .then(response => {
          resolve(response.data)
        })
        .catch(err => {
          reject(err)
        })
    })
  }

}