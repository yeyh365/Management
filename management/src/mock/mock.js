import Mock from 'mockjs'




//登录
Mock.mock('/login', 'post', adminApi.login)