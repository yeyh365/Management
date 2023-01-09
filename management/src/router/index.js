// 该文件专门用于创建整个应用的路由器
import VueRouter from 'vue-router'
//引入组件
import About from '../pages/About'
import Message from '../pages/Message'
import News from '../pages/News'
import Detail from '../pages/Detail'
import Login from '../pages/Login'
import Home from '../pages/Home'
import Char from '../pages/Char'
import Echars from '../pages/Echars'
import UserList from '../pages/UserList'
import Upload from '../pages/Upload'
import Frame from '../components/Frame'
import Admin from '../pages/manage/Admin'
import Employees from '../pages/manage/Employees'
import Personal from '../pages/Personal'
import Notice from '../pages/Notice'
import Apply from '../pages/Apply'
import unRead from '../pages/notice/unRead'
import Read from '../pages/notice/Read'
import Employeestest from '../pages/manage/Employeestest'



const router = new VueRouter({
  routes: [{
      path: '/',
      redirect: '/login'
    },
    {
      path: '/Login',
      name: 'Login',
      component: Login,
      children: []
    },
    {
      path: '/Frame',
      name: 'Frame',
      component: Frame,
      redirect: '/home',
      meta: {
        isAuth: true,
        title: ''
      },
      children: [{
          path: '/Home',
          name: 'Home',
          meta: {
            isAuth: true,
            title: '主页'
          },
          component: Home
        }, {
          path: '/manage/Admin',
          name: 'Admin',
          component: Admin,
          meta: {
            isAuth: true,
            title: '用户信息'
          }
        }, {
          path: '/manage/Employees',
          name: 'Employees',
          component: Employees,
          meta: {
            isAuth: true,
            title: '员工信息'
          }
        }, {
          path: '/Personal',
          name: 'Personal',
          component: Personal,
          meta: {
            isAuth: true,
            title: '登录信息'
          }
        },
        {
          path: '/Notice',
          name: 'Notice',
          component: Notice,
          redirect: '/notice/unRead',
          meta: {
            isAuth: true,
            title: ''
          },
          children: [{
              path: '/notice/unRead',
              name: 'unRead',
              component: unRead,

              meta: {
                isAuth: true,
                title: '未读'
              }
            },
            {
              path: '/notice/Read',
              name: 'Read',
              component: Read,
              meta: {
                isAuth: true,
                title: '已读'
              }

            }
          ]
        },
        {
          path: '/Apply',
          name: 'Apply',
          component: Apply,
          redirect: '/Apply/ApplyFor',
          children: [{
            path: '/Apply/ApplyFor',
            name: 'ApplyFor',
            component: () => import('@/pages/apply/ApplyFor.vue'),
          }, {
            path: '/Apply/Approval',
            name: 'Approval',
            component: () => import('@/pages/apply/Approval.vue'),
          }]
        },
      ]
    },
    {
      path: '/char',
      name: 'Char',
      component: Char
    },
    {
      path: '/Echars',
      name: 'Echars',
      component: Echars
    },
    {
      path: '/UserList',
      name: 'UserList',
      component: UserList
    },
    {
      path: '/Upload',
      name: 'Upload',
      component: Upload
    },
    {
      path: '/Employeestest',
      name: 'Employeestest',
      component: Employeestest
    },
    // {
    //   path: '/QrCode',
    //   name: 'QrCode',
    //   component: () => import('@/pages/QrCode.vue')
    // },
    {
      path: '/Layout',
      name: 'Layout',
      // redirect: '/Layout/QrCode',
      component: () => import('@/pages/Layout.vue'),
      children: [{
        path: '/Layout/QrCode',
        name: 'QrCode',
        component: () => import('@/pages/QrCode.vue')
      }]
    },
    {
      path: '/FiveQi',
      name: 'FiveQi',
      component: () => import('@/pages/FiveQi.vue'),
    },
    {
      path: '/WebSocket',
      name: 'WebSocket',
      component: () => import('@/pages/WebSocket.vue'),
    },
    {
      path: '/websocket1',
      name: 'websocket1',
      component: () => import('@/pages/websocket1.vue'),
    }
  ]
})
// 全局前置路由守卫———— 初始化的时候被调用、 每次路由切换之前被调用
router.beforeEach((to, from, next) => {
  console.log('前置路由守卫', to, from)
  if (to.meta.isAuth) { //判断是否需要鉴权
    if (sessionStorage.getItem('user')) {
      next()
    } else {
      next({
        name: 'Login'
      })
    }
  } else {
    next()
  }
})

// 全局后置路由守卫———— 初始化的时候被调用、 每次路由切换之后被调用
router.afterEach((to, from) => {
  console.log('后置路由守卫', to, from)
  document.title = to.meta.title || 'TPOS系统'
})




//创建并暴露一个路由器
// const router = new VueRouter({
//   routes: [{
//       name: 'guangyu',
//       path: '/about',
//       component: About,
//       redirect: '/home',
//       meta: {
//         title: '关于'
//       },
//     },
//     {
//       name: 'zhuye',
//       path: '/home',
//       component: Home,
//       meta: {
//         title: '主页'
//       },
//       children: [{
//           name: 'xinwen',
//           path: 'news',
//           component: News,
//           meta: {
//             title: '新闻'
//           },
//           // beforeEnter: (to, from, next) => {
//           //   console.log('前置路由守卫', to, from)
//           //   if (to.meta.isAuth) { //判断是否需要鉴权
//           //     if (localStorage.getItem('name') === 'yyh') {
//           //       next()
//           //     } else {
//           //       alert('名不对，无权限查看！')
//           //     }
//           //   } else {
//           //     alert('无权限查看！')
//           //   }
//           // }
//         },
//         {
//           name: 'xinxi',
//           path: 'Message',
//           component: Message,
//           meta: {
//             isAuth: true,
//             title: '信息'
//           },
//           meta: {
//             isAuth: true
//           },
//           children: [{
//             name: 'xiangqing',
//             path: 'detail',
//             component: Detail,
//             meta: {
//               isAuth: true
//             },
//             // props: {
//             //   a: 1,
//             //   b: 'hello'
//             // }
//             props($route) {
//               return {
//                 id: $route.query.id,
//                 title: $route.query.title,
//                 a: 1,
//                 b: 'hello'
//               }
//             }
//           }]
//         }
//       ]
//     }
//   ]
// })
// 全局前置路由守卫———— 初始化的时候被调用、 每次路由切换之前被调用
// router.beforeEach((to, from, next) => {
//   console.log('前置路由守卫', to, from)
//   if (to.meta.isAuth) { //判断是否需要鉴权
//     if (localStorage.getItem('name') === 'yyh') {
//       next()
//     } else {
//       alert('名不对，无权限查看！')
//     }
//   } else {
//     next()
//   }
// })

// 全局后置路由守卫———— 初始化的时候被调用、 每次路由切换之后被调用
// router.afterEach((to, from) => {
//   console.log('后置路由守卫', to, from)
//   document.title = to.meta.title || 'TPOS系统'
// })

const VueRouterPush = VueRouter.prototype.push
VueRouter.prototype.push = function push(to) {
  return VueRouterPush.call(this, to).catch(err => err)
}
export default router