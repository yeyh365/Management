<template>
  <div>
    <div class="page">
      <el-pagination background layout="prev, pager, next" :total="100">
      </el-pagination>
    </div>
    <div id="myChart" :style="{ width: '600px', height: '300px' }">1234</div>
    <div>
      <el-row :gutter="10">
        <el-col :xs="8" :sm="6" :md="4" :lg="3" :xl="1"
          ><div class="grid-content bg-purple"></div
        ></el-col>
        <el-col :xs="4" :sm="6" :md="8" :lg="9" :xl="11"
          ><div class="grid-content bg-purple-light"></div
        ></el-col>
        <el-col :xs="4" :sm="6" :md="8" :lg="9" :xl="11"
          ><div class="grid-content bg-purple"></div
        ></el-col>
        <el-col :xs="8" :sm="6" :md="4" :lg="3" :xl="1"
          ><div class="grid-content bg-purple-light"></div
        ></el-col>
      </el-row>
    </div>
  </div>
</template>

<script>
import * as echarts from 'echarts'

export default {
  name: 'Echars',
  data () {
    return {
      a: 14,
      b: 23,
      c: 56,
      placeholder: "请输入账号查询",
      dialogAddEdit: false,
      operateType: "add",
      adminTabel: [],
      adminData: {
        id: "",
        image: "",
        name: "",
        password: "",
      },
      rules: {
        image: [{ required: true, message: "头像不能为空", trigger: "blur" }],
        name: [
          { required: true, message: "账号不能为空", trigger: "blur" },
          { min: 4, max: 10, message: "账号为4-10位", trigger: "blur" },
        ],
        password: [
          { required: true, message: "密码不能为空", trigger: "blur" },
          { min: 4, max: 12, message: "密码长度为4-12位", trigger: "blur" },
        ],
      },
      keywords: "",
      formLabelWidth: "100px",
      currentPage: 1,
      totalCount: 1,
      PageSize: 10,
      PageSizes: [5, 10, 20, 50],
    }
  },
  mounted () {
    this.drawLine();
  },
  methods: {
    drawLine () {
      let myChart = echarts.init(document.getElementById('myChart'))
      myChart.setOption({
        title: {
          text: 'Completion Rate',
          y: '0',
          x: 'center',
          textStyle: {
            fontSize: 28,
            fontWeight: 'bolder',
            color: '#333' // 主标题文字颜色
          }
        },
        tooltip: {
          trigger: 'item',
          formatter: '{a} <br/>{b}: {c} ({d}%)'
        },
        legend: {
          x: 'center',
          y: '270',
          data: ['GERT',
            'GA&COI',
            'Laws and Regulations',
            'CNQx Documents'
          ]
        },
        series: [
          {
            name: 'Completion Rate',
            type: 'pie',
            radius: ['45%', '60%'],
            labelLine: {
              length: 30
            },
            itemStyle: {
              borderRadius: 5,
              borderColor: '#fff',
              borderWidth: 2
            },
            label: {
              formatter: '{per|{d}%}  ',
              backgroundColor: '#F6F8FC',
              borderColor: '#8C8D8E',
              borderWidth: 1,
              borderRadius: 3,
              rich: {
                a: {
                  color: '#6E7079',
                  lineHeight: 22,
                  align: 'center'
                },
                hr: {
                  borderColor: '#8C8D8E',
                  width: '100%',
                  borderWidth: 1,
                  height: 0
                },
                b: {
                  color: '#4C5058',
                  fontSize: 14,
                  fontWeight: 'bold',
                  lineHeight: 33
                },
                per: {
                  color: '#000',
                  backgroundColor: '#FFF',
                  padding: [3, 4],
                  borderRadius: 4
                }
              }
            },
            data: [
              { value: this.a, name: 'GERT' },
              { value: this.b, name: 'GA&COI' },
              { value: this.c, name: 'Laws and Regulations' },
              { value: 43, name: 'CNQx Documents' }
            ]
          }
        ]


      })

    }
  }
}


</script>

<style scope>
.page {
  /* float: left; */
  height: 50px;
  margin: 15px;
}

.el-col {
  border-radius: 4px;
}
.bg-purple-dark {
  background: #99a9bf;
}
.bg-purple {
  background: #d3dce6;
}
.bg-purple-light {
  background: #e5e9f2;
}
.grid-content {
  border-radius: 4px;
  min-height: 36px;
}
</style>