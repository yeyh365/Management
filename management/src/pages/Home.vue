<template>
  <div>
    <el-row>
      <!-- 左侧 -->
      <el-col :span="10">
        <div class="clock">
          <el-card shadow="hover">
            <clock-bar />
          </el-card>
          <div>
            <el-card shadow="hover" class="box-card">
              <div slot="header" class="clearfix">
                <span>通知</span>
                <el-button
                  @click="notice()"
                  style="float: right; padding: 3px 0"
                  type="text"
                  >通知详情</el-button
                >
              </div>
              <div
                v-for="item in noticeData"
                :key="item.index"
                class="text item"
              >
                <div class="title">
                  <div class="point"></div>
                  {{ item.title }}
                </div>
                <div class="date">
                  {{ item.date }}
                </div>
              </div>
            </el-card>
          </div>
        </div>
      </el-col>
      <!-- 右侧 -->
      <el-col :span="14">
        <div>
          <el-card shadow="hover">
            <div class="cost">
              <h3>各部门六月份费用明细</h3>
            </div>
            <el-table :data="departmentData" style="width: 100%">
              <el-table-column type="expand">
                <template slot-scope="props">
                  <el-form
                    label-position="left"
                    inline
                    class="demo-table-expand"
                  >
                    <el-form-item label="部门名称">
                      <span>{{ props.row.department }}</span>
                    </el-form-item>
                    <el-form-item label="部门经理">
                      <span>{{ props.row.manager }}</span>
                    </el-form-item>
                    <el-form-item label="部门人数">
                      <span>{{ props.row.number_people }}</span>
                    </el-form-item>
                    <el-form-item label="办公费">
                      <span>{{ props.row.office_cost }}</span>
                    </el-form-item>
                    <el-form-item label="交通费">
                      <span>{{ props.row.traffic_cost }}</span>
                    </el-form-item>
                    <el-form-item label="其他费用">
                      <span>{{ props.row.other_cost }}</span>
                    </el-form-item>
                    <el-form-item label="总费用">
                      <span>{{ props.row.all_costs }}</span>
                    </el-form-item>
                  </el-form>
                </template>
              </el-table-column>
              <el-table-column label="部门" prop="department">
              </el-table-column>
              <el-table-column label="经理" prop="manager"> </el-table-column>
              <el-table-column label="总费用" prop="all_costs">
              </el-table-column>
            </el-table>
          </el-card>
        </div>
      </el-col>
    </el-row>
  </div>
</template>

<script>
import ClockBar from '../components/ClockBar.vue'
import axiosInstance from '@/api/common/export';
// import { config } from 'vue/types/umd';
// import ClockBar from '../components/ClockBar.vue'

export default {
  name: 'Home',
  data () {
    return {
      noticeData: [],
      incomeData: [],
      departmentData: []
    }
  },
  components: {
    ClockBar
  },
  methods: {
    notice () {

      // this.$router.push('/notice')
    },
    additem () {
      axiosInstance.get(
        '/System/teststring?name=1234'
      ).then(
        res => {

          console.log(res)
        }
      )
    }
  },
  mounted () {
    this.noticeData = [
      { title: '关于开展核实部门虚假消费报销工作', date: '2022-6-13' },
      { title: '部分需要有显式具名 slot 分发，同时也是可选的。', date: '2022-6-13' },
      { title: '将信息聚合在卡片容器中展示。', date: '2022-6-13' }
    ]
    this.departmentData = [
      {
        department: '行政部', manager: '吴熙捷', all_costs: '1200', number_people: '12',
        office_cost: 800,
        traffic_cost: 100,
        other_cost: 10,
        all_costs: 1000

      },

    ]
    // this.$axios.get('/home/getData').then(res => {
    //   const { code, data } = res.data
    //   if (code === 200) {
    //     this.incomeData = data.incomeData
    //     this.departmentData = data.departmentData
    //   }
    //   console.log(res)
    // }),
    //   this.$axios.get('/notice/getData').then(res => {
    //     const { code, data } = res.data
    //     if (code === 200) {
    //       this.noticeData = data.noticeList
    //     }
    //     console.log(res)
    //   })
  }
}
</script>

<style scoped>
.el-col {
  padding: 10px;
}

.clock {
  height: 240px;
  margin-bottom: 50px;
}

.text {
  display: flex;
  justify-content: space-between;
  align-items: center;
  font-size: 14px;
}

.text .point {
  margin: 10px;
  width: 5px;
  height: 5px;
  background-color: red;
  border-radius: 50%;
}

.text .date {
  font-size: 12px;
  color: #bbb;
}

.item {
  margin-bottom: 18px;
}

.text .title {
  display: flex;
  align-items: center;
}

.clearfix:before,
.clearfix:after {
  display: table;
  content: "";
}
.clearfix:after {
  clear: both;
}

.box-card {
  width: 100%;
}

.el-carousel__item:nth-child(2n) {
  background-color: #99a9bf;
}

.el-carousel__item:nth-child(2n + 1) {
  background-color: #d3dce6;
}

.card {
  display: flex;
  justify-content: space-between;
  margin-bottom: 10px;
}

.income {
  width: 23%;
  height: 160px;
}

.income .border {
  width: 100%;
  height: 50px;
}

.income .border h1 {
  margin: 0;
  font-size: 30px;
  line-height: 50px;
  font-weight: 800;
  color: #c02525;
  text-align: center;
}

.income .detail .annotation {
  margin: 0;
  height: 30px;
  line-height: 30px;
  font-size: 14px;
  color: #475669;
}

.income .detail .money {
  margin: 0;
  height: 40px;
  line-height: 40px;
  font-size: 20px;
  font-weight: 800;
  color: black;
}

.cost {
  height: 40px;
  border-bottom: 2px solid #bbb;
}

.demo-table-expand {
  width: 100%;
  font-size: 0;
}

.demo-table-expand label {
  width: 50%;
  color: #7f8691;
}

.demo-table-expand .el-form-item {
  margin-right: 0;
  margin-bottom: 0;
  width: 50%;
}

.demo-table-expand .el-form-item span {
  padding-left: 10px;
}
</style>