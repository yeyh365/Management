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
  GetWorkflowMenu() {
    return new Promise((resolve, reject) => {
      axiosInstance.get(`/Workflow/GetWorkflowMenu`)
        .then(response => {
          resolve(response.data)
        })
        .catch(err => {
          reject(err)
        })
    })
  },
  GetUserPendingWorkflowList(page, limit, account) {
    return new Promise((resolve, reject) => {
      axiosInstance.get(`/Workflow/GetUserPendingWorkflow`, {
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
  GetUserPendingWorkflowCount(account) {
    return new Promise((resolve, reject) => {
      axiosInstance.get(`/Workflow/GetUserPendingWorkflowCount`, {
          params: {
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
  AddWorkflow(workflowProcess) {
    return new Promise((resolve, reject) => {
      axiosInstance.get(`/Workflow/AddWorkflow`, workflowProcess)
        .then(response => {
          resolve(response.data)
        })
        .catch(err => {
          reject(err)
        })
    })
  }

}