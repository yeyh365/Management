<template>
  <div class="header">
    <div class="l-content">
      <el-button
        @click="asideMenu()"
        plain
        icon="el-icon-s-unfold"
        size="mini"
      ></el-button>
      <h2>后台管理系统</h2>
      <el-breadcrumb separator="/">
        <el-breadcrumb-item
          v-for="item in tags"
          :key="item.path"
          :to="{ path: item.path }"
          >{{ item.label }}</el-breadcrumb-item
        >
      </el-breadcrumb>
    </div>
    <div class="r-content">
      <el-dropdown @command="clickMenu" trigger="click" size="mini">
        <span>
          <img :src="adminImg" class="user_picture" />
        </span>
        <el-dropdown-menu slot="dropdown">
          <el-dropdown-item
            v-for="item in menu"
            :key="item.path"
            :command="item"
            >{{ item.label }}</el-dropdown-item
          >
        </el-dropdown-menu>
      </el-dropdown>
    </div>
  </div>
</template>

<script>
export default {
  name: 'CommonHeader',
  data () {
    return {
      adminImg: '',
      menu: [
        {
          path: '/login123',
          name: 'Login1',
          label: '个人中心'
        },
        {
          path: '/login',
          name: 'Login',
          label: '退出登录'
        }
      ]

    }
  },
  computed: {
    tags () {
      return this.$store.state.tabList
    }
  },
  methods: {
    asideMenu () {
      this.$store.commit('tab/collapseMenu')
    },
    getImg () {
      let loginData = JSON.parse(window.sessionStorage.getItem('token'))
      this.adminImg = loginData[0].image
    },
    clickMenu (item) {
      console.log(item.label);
      console.log(this.$route)
      if (item.name === 'Login') {
        // window.sessionStorage.clear()
        this.$confirm('是否退出登录, 是否继续?', '提示', {
          confirmButtonText: '确定',
          cancelButtonText: '取消',
          type: 'warning'
        }).then(() => {
          this.$message({
            type: 'success',
            message: '成功退出登录!'
          });
        }).catch(() => {
          this.$message({
            type: 'info',
            message: '已取消删除'
          });
        });
      }
      this.$router.push({
        name: item.name
      })
      this.$store.commit('tab/selectMenu', item)
    },

  },
  mounted () {
    this.getImg()
  }

}
</script>

<style>
.header {
  display: flex;
  height: 100%;
  justify-content: space-between;
  align-items: center;
}
.l-content {
  margin: -10px;
  display: flex;
  align-items: center;
}
.l-content h2 {
  margin-left: 12px;
  margin-right: 12px;
  float: left;
  font-size: 22px;
  color: #fff;
}
.r-content .user_picture {
  width: 40px;
  height: 40px;
  border-radius: 50%;
}
</style>