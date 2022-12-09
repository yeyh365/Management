// 根据不同环境更改不同baseUrl
let baseUrl = '';
let pUrl = '';
if (process.env.NODE_ENV == 'development') {
  baseUrl = 'https://localhost:44369/api';
  pUrl = 'https://localhost:44369';
} else if (process.env.NODE_ENV == 'production') {
  // baseUrl = '灰度地址';
  baseUrl = 'http://1.14.96.49:8088/api';
  pUrl = 'http://1.14.96.49:8088';
}

export {
  baseUrl,
  pUrl
}