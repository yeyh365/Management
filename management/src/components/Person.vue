<template>
  <div>
    <h1>人员列表</h1>
    <h3>Count组件求和为{{ $store.state.sum }}</h3>
    <h3>列表中的第一个名字是{{ firstPersonName }}</h3>
    <input type="text" placeholder="请输入名字" v-model="name" />
    <button @click="add">添加会员</button>
    <button @click="addPersonServer">随机添加一个人</button>
    <ul>
      <li v-for="p in personList" :key="p.id">{{ p.name }}</li>
    </ul>
  </div>
</template>

<script>
import { nanoid } from 'nanoid'
export default {
  name: 'Person',
  data () {
    return {
      name: ''
    }
  },
  computed: {
    personList () {
      return this.$store.state.personAbout.personList
    },
    sum () {
      return this.$store.state.countAbout.sum
    },
    firstPersonName () {
      return this.$store.getters['personAbout/firstPersonName']
    }
  },
  methods: {
    add () {
      const personObj = { id: nanoid(), name: this.name }
      this.$store.dispatch('personAbout/addperson', personObj)
    },
    addPersonServer () {
      this.$store.dispatch('personAbout/addpersonserver')
    }
  },
}
</script>

<style>
</style>