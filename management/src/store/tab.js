export default {
  namespaced: true,
  actions: {

  },
  mutations: {
    collapseMenu(state) {
      state.isCollapse = !state.isCollapse
    },
    selectMenu(state, val) {
      if (val.name !== 'home') {
        state.currentMenu = val
        const result = state.tabList.findIndex(item => item.name === val.name)
        if (result === -1) {
          state.tabList.push(val)
        }
      } else {
        state.collapseMenu = null
      }
      console.log(state.tabList)
    }

  },
  state: {
    isCollapse: false,

    UserName: '',
    password: '',

    tabList: [{
      path: '/home',
      name: 'Home',
      label: '首页',
      icon: 'home'
    }],
    currentMenu: null
  },
  getters: {

  }
}