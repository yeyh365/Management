import axios from "axios"
import {
  nanoid
} from "nanoid"
export default {
  namespaced: true,
  actions: {
    addperson(context, value) {
      context.commit('ADDPERSON', value)
    },
    addpersonserver(context) {
      axios.get('https://api.uixsj.cn/hitokoto/get?type=social').then(
        response => {
          context.commit('ADDPERSON', {
            id: nanoid(),
            name: response.data
          })
        },
        error => {
          alert(error.message)
        }
      )
    }
  },
  mutations: {
    ADDPERSON(state, value) {
      console.log(value)
      console.log(state)
      state.personList.unshift(value)
    }
  },
  state: {
    personList: [{
      id: '001',
      name: '李四'
    }]
  },
  getters: {
    firstPersonName(state) {
      return state.personList[0].name
    }
  }



}