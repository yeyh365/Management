<template>
  <el-menu
    :default-active="$route.path"
    class="el-menu-vertical-demo"
    background-color="#324157"
    text-color="#fff"
    active-text-color="#20a0ff"
    @open="handleOpen"
    @close="handleClose"
    :collapse="isCollapse"
  >
    <el-menu-item
      @click="clickMenu(item)"
      v-for="item in noChildren"
      :index="item.path"
      :key="item.path"
    >
      <i :class="'el-icon-' + item.icon"></i>
      <span slot="title">{{ item.label }}</span>
    </el-menu-item>
    <el-submenu
      v-for="item in hasChildren"
      :index="item.path + ''"
      :key="item.path"
    >
      <template slot="title">
        <i :class="'el-icon-' + item.icon"></i>
        <span slot="title">{{ item.label }}</span>
      </template>
      <el-menu-item-group v-for="subItem in item.children" :key="subItem.path">
        <el-menu-item @click="clickMenu(subItem)" :index="subItem.path">{{
          subItem.label
        }}</el-menu-item>
      </el-menu-item-group>
    </el-submenu>
  </el-menu>
</template>

<script>
export default {
  name: 'CommonAside',
  data () {
    return {
      menu: [
        {
          path: '/home',
          name: 'Home',
          label: '首页',
          icon: 's-home',
        },
        {
          path: '/notice',
          name: 'Notice',
          label: '通知',
          icon: 'bell',
        },
        {
          path: '/personal',
          name: 'Personal',
          label: '个人中心',
          icon: 'user',
        },
        {
          label: '人员管理',
          icon: 's-custom',
          children: [
            {
              path: '/manage/admin',
              name: 'Admin',
              label: '管理员管理'
            },
            {
              path: '/manage/employees',
              name: 'Employees',
              label: '员工管理'
            }
          ]
        }
      ]
    }
  },
  methods: {
    handleOpen (key, keyPath) {
      console.log(key, keyPath);
    },
    handleClose (key, keyPath) {
      console.log(key, keyPath);
    },
    clickMenu (item) {
      console.log(item.label);
      this.$router.push({
        name: item.name
      })
      this.$store.commit('tab/selectMenu', item)
    }
  },
  computed: {
    isCollapse () {
      return this.$store.state.tab.isCollapse
    },
    noChildren () {
      const a = this.menu.filter(item => !item.children)
      return a
    },
    hasChildren () {
      return this.menu.filter(item => item.children)
    },
  }
}
</script>

<style>
</style>