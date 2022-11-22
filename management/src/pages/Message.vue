<template>
  <div>
    <ul>
      <li v-for="m in messageList" :key="m.id">
        <!-- <router-link :to="`/home/message/detail?id=${m.id}&title=${m.title}`">{{
          m.title
        }}</router-link
        >&nbsp;&nbsp; -->
        <router-link
          :to="{
            name: 'xiangqing',
            query: {
              id: m.id,
              title: m.title,
            },
          }"
          >{{ m.title }}</router-link
        >
        <button @click="pushShow(m)">push查看</button>
        <button @click="replaceShow(m)">replaceShow查看</button>
      </li>
      <button @click="qianj">前进</button>
      <button @click="houtui">后退</button>
    </ul>
    <hr />
    <router-view></router-view>
  </div>
</template>

<script>
export default {
  name: 'Message',
  data () {
    return {
      messageList: [
        { id: '001', title: '消息001' },
        { id: '002', title: '消息002' },
        { id: '003', title: '消息003' }

      ]
    }
  },
  methods: {
    pushShow (m) {
      this.$router.push({
        name: 'xiangqing',
        query: {
          id: m.id,
          title: m.title
        }
      })
    },
    replaceShow (m) {
      this.$router.push({
        name: 'xiangqing',
        query: {
          id: m.id,
          title: m.title
        }
      })
    },
    qianj () {
      this.$router.forward()
    },
    houtui () {
      this.$router.back()
    }
  },
}
</script>