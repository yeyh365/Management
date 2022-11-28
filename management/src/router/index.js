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
      children: [{
          path: '/Home',
          name: 'Home',
          component: Home
        }, {
          path: '/manage/Admin',
          name: 'Admin',
          component: Admin
        }, {
          path: '/manage/Employees',
          name: 'Employees',
          component: Employees
        }, {
          path: '/Personal',
          name: 'Personal',
          component: Personal,
        },
        {
          path: '/Notice',
          name: 'Notice',
          component: Notice,
          redirect: '/notice/unRead',
          children: [{
              path: '/notice/unRead',
              name: 'unRead',
              component: unRead,
            },
            {
              path: '/notice/Read',
              name: 'Read',
              component: Read,
            }
          ]
        },
        {
          path: '/Apply',
          name: 'Apply',
          component: Apply,
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
    }
  ]
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