后端 public async Task<IActionResult> DownloadAddFile()
        {
            var p = _hostingEnvironment.ContentRootPath;
            var path = p + "\\ExcelTmps\\IV售后三包导入模板.xlsx";
            if (string.IsNullOrEmpty(path))
            {
                return NotFound();
            }
            var memoryStream = new MemoryStream();
            using (var stream = new FileStream(path, FileMode.Open))
            {
                await stream.CopyToAsync(memoryStream);
            }
            memoryStream.Seek(0, SeekOrigin.Begin);
            string[] strArry = path.Split('/');
            string fileName = "IV售后三包导入模板";
            //文件名必须编码，否则会有特殊字符(如中文)无法在此下载。
            string encodeFilename = System.Net.WebUtility.UrlEncode(fileName);
            Response.Headers.Add("Content-Disposition", "attachment; filename=" + encodeFilename);
            return new FileStreamResult(memoryStream, "application/octet-stream");//文件流方式，指定文件流对应的ContenType。
        }

前端

      downLoadAddFile: '/api/YJMonthInfo/downloadAddTmp',

async DownLoadTemplate(record) {
      await this.downLoad(this.downLoadAddFile, 'IV售后三包导入模板.xls')
    },
    downLoad(url, name) {
      return new Promise((resolve, reject) => {
        const xhr = new XMLHttpRequest()
        xhr.open('GET', `${url}`, true)
        xhr.responseType = 'blob'
        // var vmThis = this
        xhr.onload = () => {
          if (xhr.status === 200) {
            const file = xhr.response
            const fileName = name
            if ('msSaveOrOpenBlob' in navigator) {
              window.navigator.msSaveOrOpenBlob(file, fileName)
            } else {
              const fileUrl = window.URL.createObjectURL(file)
              const a = document.createElement('a')
              a.download = fileName
              a.href = fileUrl
              document.getElementsByTagName('body')[0].appendChild(a)
              a.click()
              a.parentNode.removeChild(a)
              window.URL.revokeObjectURL(fileUrl)
            }
            return resolve()
          }
        }
        xhr.setRequestHeader('Authorization', `${localStorage.getItem('token')}`)
        xhr.send()
      })
    },