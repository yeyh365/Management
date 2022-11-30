<template>
  <div class="top">
    <div class="add">
      <el-button type="primary" @click="add" icon="el-icon-circle-plus-outline"
        >新增
      </el-button>
    </div>
    <div class="serach">
      <el-input
        v-model="EmployeeId"
        placeholder="员工编号"
        style="width: 140px"
      ></el-input>
      <el-input
        v-model="EmployeeName"
        placeholder="员工姓名"
        style="width: 140px"
      ></el-input>
      <!-- <el-input :placeholder="员工编号" v-model="EmployeeId" width="180">
      </el-input>
      <el-input :placeholder="员工姓名" v-model="EmployeeName" width="180">
      </el-input> -->
      <el-select v-model="DepartmentNumber" placeholder="请选择">
        <el-option label="行政部" value="1"></el-option>
        <el-option label="财务部" value="2"></el-option>
        <el-option label="人事部" value="3"></el-option>
        <el-option label="研发部" value="4"></el-option>
        <el-option label="营销部" value="5"></el-option>
      </el-select>

      <el-button type="primary" icon="el-icon-search" @click="search"
        >查询
      </el-button>
    </div>
    <div class="all">
      <el-button type="primary" @click="showAll">显示列表 </el-button>
      <el-button type="primary" @click="export1">导出EXCEL </el-button>
    </div>
  </div>
</template>

<script>
export default {
  name: 'SearchBar',
  data () {
    return {
      EmployeeId: '',
      EmployeeName: '',
      DepartmentNumber: ''
    }
  },
  props: {
    placeholder: String,
    placeholder: String,
    placeholder: String,

  },
  methods: {
    //新增
    add () {
      this.$emit('onAdd')
    },
    // 搜索
    search () {
      this.$emit('onSearch')
    },
    // 显示所有员工
    showAll () {
      this.$emit('onShowAll')
    },
    export1 () {
      this.$emit('onExport')
      console.log('export1_bar')
    }
  }
}
</script>

<style scoped>
.top {
  display: flex;
  margin-bottom: 10px;
  justify-content: space-between;
}

.add {
  margin-right: 15px;
}

.serach {
  display: flex;
}

.serach .el-input {
  margin-right: 15px;
}

.all {
  margin-left: auto;
}
</style>