export default {
  namespaced: true,
  actions: {
    jia(context, value) {
      context.commit('JIA', value)
    },
    jian(context, value) {
      context.commit('JIAN', value)
      console.log('actions')
    },
    jiaodd(context, value) {
      if (context.state.sum % 2) {
        context.commit('JIA', value)
      }
    },
    jiaWait(context, value) {
      setTimeout(() => {
        context.commit("JIAWAIT", value)
      }, 1000);
    },
  },
  mutations: {
    JIA(state, value) {
      state.sum += value
    },
    JIAN(state, value) {
      state.sum -= value
    },
    JIAODD(state, value) {
      state.sum += value
    },
    JIAWAIT(state, value) {
      state.sum += value
    },
  },
  state: {
    sum: 2,
    school: '大飞',
    subject: '前端',
  },
  getters: {
    bigSum(state) {
      return state.sum * 100
    }
  }
}