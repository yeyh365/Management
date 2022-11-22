// async DownLoadTemplate(record) {
//   await this.downLoad(this.downLoadAddFile, 'IV售后三包导入模板.xls')
// },
// downLoad(url, name) {
//   return new Promise((resolve, reject) => {
//     const xhr = new XMLHttpRequest()
//     xhr.open('GET', `${url}`, true)
//     xhr.responseType = 'blob'
//     // var vmThis = this
//     xhr.onload = () => {
//       if (xhr.status === 200) {
//         const file = xhr.response
//         const fileName = name
//         if ('msSaveOrOpenBlob' in navigator) {
//           window.navigator.msSaveOrOpenBlob(file, fileName)
//         } else {
//           const fileUrl = window.URL.createObjectURL(file)
//           const a = document.createElement('a')
//           a.download = fileName
//           a.href = fileUrl
//           document.getElementsByTagName('body')[0].appendChild(a)
//           a.click()
//           a.parentNode.removeChild(a)
//           window.URL.revokeObjectURL(fileUrl)
//         }
//         return resolve()
//       }
//     }
//     xhr.setRequestHeader('Authorization', `${localStorage.getItem('token')}`)
//     xhr.send()
//   })
// }