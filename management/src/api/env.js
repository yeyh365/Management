// 根据不同环境更改不同baseUrl
let baseUrl = '';
if (process.env.NODE_ENV == 'development') {
  baseUrl = 'https://localhost:44369/api';
} else if (process.env.NODE_ENV == 'production') {
  // baseUrl = '灰度地址';
  baseUrl = 'http://1.14.96.49:8088/api';
}

export {
  baseUrl
}