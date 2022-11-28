<template>
  <div>
    <h2>Personal</h2>
    <el-row>
      <!-- 个人信息卡 -->
      <el-col :span="9">
        <el-card shadow="hover">
          <div class="admin">
            <img class="admin_picture" :src="adminImg" />
            <div class="admin_info">
              <p class="name">账号:{{ name }}</p>
              <p class="pwd"><i>密码:</i>{{ password }}</p>
            </div>
          </div>
        </el-card>
      </el-col>
      <el-col :span="15">
        <el-card shadow="hover">
          <div style="width: 100%; height: 230px" ref="people"></div>
        </el-card>
      </el-col>
    </el-row>
  </div>
</template>

<script>
export default {
  name: 'Personal',
  data () {
    return {
      name: 'yerik',
      password: '12345'
    }
  },
}
</script>

<style scoped>
.el-col {
  padding: 10px;
}

.admin {
  height: 230px;
  border-bottom: 2px solid #bbb;
}

.admin .admin_picture {
  margin-top: 50px;
  float: left;
  width: 120px;
  height: 120px;
  border-radius: 50%;
}

.admin .admin_info {
  margin-top: 50px;
  float: left;
  margin-left: 40px;
}

.admin .admin_info .name {
  font-size: 26px;
  font-weight: 800;
  color: black;
}

.admin .admin_info .pwd {
  font-size: 16px;
  color: #bbb;
}

.admin .admin_info .pwd i {
  margin-right: 20px;
}
</style>