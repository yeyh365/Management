<template>
  <div>
    <h2>Personal</h2>
    <el-row>
      <!-- 个人信息卡 -->
      <el-col :span="9">
        <el-card shadow="hover">
          <div class="admin">
            <!-- <img class="admin_picture" :src="adminImg" /> -->
            <div class="admin_info">
              <p class="name">账号:{{ UserName }}</p>
              <p class="pwd"><i>密码:</i>{{ Password }}</p>
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
    <el-row>
      <el-col :span="24">
        <el-card shadow="hover">
          <div style="width: 100%; height: 250px" ref="week"></div>
        </el-card>
      </el-col>
    </el-row>
  </div>
</template>

<script>
import { mapState } from 'vuex'
import * as echarts from 'echarts'
export default {
  name: 'Personal',
  data () {
    return {
      // UserName: '',
      // Password: ''
    }
  },
  mounted () {
    this.getbin();
    this.getzhu()

  },
  computed: {
    ...mapState('tab', ['UserName', 'Password'])
    // UserName () {
    //   console.log(this.$store.state.tab.users.UserName)
    //   return this.$store.state.tab.users.UserName
    // }
  },
  methods: {
    getbin () {
      console.log(this.$store.state.tab)
      const peopleOption = {
        title: {
          text: '公司人数分布情况(人)'
        },
        tooltip: {},
        series: [{
          type: 'pie',
          data: [
            { name: '行政部', value: 23 },
            { name: '财务部', value: 15 },
            { name: '人事部', value: 11 },
            { name: '研发部', value: 22 },
            { name: '营销部', value: 10 },

          ]
        }]
      }
      const Pie = echarts.init(this.$refs.people)
      Pie.setOption(peopleOption)
    },
    getzhu () {
      var weekData = [
        {
          date: '周一',
          value: 8000
        },
        {
          date: '周二',
          value: 6000
        },
        {
          date: '周三',
          value: 6000
        },
        {
          date: '周四',
          value: 5000
        },
        {
          date: '周五',
          value: 5000
        },
        {
          date: '周六',
          value: 12000
        },
        {
          date: '周日',
          value: 18000
        }
      ];
      //柱状图
      const weekOption = {
        title: {
          text: '上周公司收入详情'
        },
        tooltip: {},
        legend: {
          data: ['收入(元)']
        },
        xAxis: {
          data: ['周一', '周二', '周三', '周四', '周五', '周六', '周日',]
        },
        yAxis: {},
        series: [{
          name: '收入(元)',
          data: [8000, 12000, 2900, 12440, 4300, 2300, 1200],
          type: 'bar'
        }]
      }
      const Bar = echarts.init(this.$refs.week)
      Bar.setOption(weekOption)
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