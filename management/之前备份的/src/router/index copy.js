// 该文件专门用于创建整个应用的路由器
import VueRouter from 'vue-router'
//引入组件
import About from '../pages/About'
import Home from '../pages/Home'
import Message from '../pages/Message'
import News from '../pages/News'
import Detail from '../pages/Detail'


//创建并暴露一个路由器
const router = new VueRouter({
  routes: [{
      name: 'guangyu',
      path: '/about',
      component: About,
      redirect: '/home',
      meta: {
        title: '关于'
      },
    },
    {
      name: 'zhuye',
      path: '/home',
      component: Home,
      meta: {
        title: '主页'
      },
      children: [{
          name: 'xinwen',
          path: 'news',
          component: News,
          meta: {
            title: '新闻'
          },
          // beforeEnter: (to, from, next) => {
          //   console.log('前置路由守卫', to, from)
          //   if (to.meta.isAuth) { //判断是否需要鉴权
          //     if (localStorage.getItem('name') === 'yyh') {
          //       next()
          //     } else {
          //       alert('名不对，无权限查看！')
          //     }
          //   } else {
          //     alert('无权限查看！')
          //   }
          // }
        },
        {
          name: 'xinxi',
          path: 'Message',
          component: Message,
          meta: {
            isAuth: true,
            title: '信息'
          },
          meta: {
            isAuth: true
          },
          children: [{
            name: 'xiangqing',
            path: 'detail',
            component: Detail,
            meta: {
              isAuth: true
            },
            // props: {
            //   a: 1,
            //   b: 'hello'
            // }
            props($route) {
              return {
                id: $route.query.id,
                title: $route.query.title,
                a: 1,
                b: 'hello'
              }
            }
          }]
        }
      ]
    }
  ]
})
// 全局前置路由守卫———— 初始化的时候被调用、 每次路由切换之前被调用
router.beforeEach((to, from, next) => {
  console.log('前置路由守卫', to, from)
  if (to.meta.isAuth) { //判断是否需要鉴权
    if (localStorage.getItem('name') === 'yyh') {
      next()
    } else {
      alert('名不对，无权限查看！')
    }
  } else {
    next()
  }
})

// 全局后置路由守卫———— 初始化的时候被调用、 每次路由切换之后被调用
router.afterEach((to, from) => {
  console.log('后置路由守卫', to, from)
  document.title = to.meta.title || '硅谷系统'
})

export default router