<template>
  <div class="employess">
    <search-bar
      :placeholder="placeholder"
      @onAdd="addButton"
      @onSearch="serach"
      @onShowAll="getList"
      ref="searchBar"
    />
    <div class="tabel">
      <template>
        <el-table :data="employeesTabel" border style="width: 100%">
          <el-table-column fixed prop="EmployeeId" label="员工编号" width="180">
          </el-table-column>
          <el-table-column prop="EmployeeName" label="员工姓名" width="180">
          </el-table-column>
          <el-table-column prop="CredId" label="身份证号" width="180">
          </el-table-column>
          <el-table-column prop="Sex" label="性别" width="180">
          </el-table-column>
          <el-table-column prop="Mobile" label="手机号码" width="180">
          </el-table-column>
          <el-table-column prop="Email" label="邮箱" width="240">
          </el-table-column>
          <el-table-column prop="DepartmentName" label="部门名称" width="180">
          </el-table-column>
          <el-table-column prop="PosititonName" label="职位名称" width="180">
          </el-table-column>
          <el-table-column prop="ProjectName" label="项目名称" width="180">
          </el-table-column>
          <el-table-column fixed="right" label="操作" width="380">
            <template slot-scope="scope">
              <el-button
                @click="editButton(scope.row)"
                type="primary"
                icon="el-icon-edit"
                size="small"
                >编辑</el-button
              >
              <el-button
                @click="del(scope.row)"
                type="danger"
                icon="el-icon-delete"
                size="small"
                >删除</el-button
              >
            </template>
          </el-table-column>
        </el-table>
      </template>
    </div>
    <div class="page">
      <el-pagination
        background
        @size-change="handleSizeChange"
        @current-change="handleCurrentChange"
        :current-page="currentPage"
        :page-size="PageSize"
        :page-sizes="PageSizes"
        layout="total, sizes, prev, pager, next, jumper"
        :total="totalCount"
      >
      </el-pagination>
    </div>

    <!-- 添加/编辑对话框 -->
    <el-dialog
      :title="operateType === 'add' ? '新增员工' : '更新员工信息'"
      :visible.sync="dialogAddEdit"
    >
      <el-form :model="employeesData" :rules="rules" ref="employeesData">
        <el-form-item label="id号" prop="id" :label-width="formLabelWidth">
          <el-input
            v-model="employeesData.id"
            placeholder="id号默认自增,无须添加"
            autocomplete="off"
            disabled
          ></el-input>
        </el-form-item>
        <el-form-item
          label="员工ID"
          prop="employee_id"
          :label-width="formLabelWidth"
        >
          <el-input
            v-model="employeesData.employeeId"
            placeholder="请输入账号"
            autocomplete="off"
          ></el-input>
        </el-form-item>
        <el-form-item
          label="员工姓名"
          prop="employee_name"
          :label-width="formLabelWidth"
        >
          <el-input
            v-model="employeesData.employeeName"
            placeholder="请输入姓名"
            autocomplete="off"
          ></el-input>
        </el-form-item>
        <el-form-item label="性别" prop="sex" :label-width="formLabelWidth">
          <el-select v-model="employeesData.sex" placeholder="请选择">
            <el-option label="男" value="男"></el-option>
            <el-option label="女" value="女"></el-option>
          </el-select>
        </el-form-item>
        <el-form-item
          label="员工身份证号"
          prop="card_id"
          :label-width="formLabelWidth"
        >
          <el-input
            v-model="employeesData.cardscoped"
            placeholder="请输入身份证号码"
            autocomplete="off"
          ></el-input>
        </el-form-item>
        <el-form-item
          label="员工电话号码"
          prop="mobile"
          :label-width="formLabelWidth"
        >
          <el-input
            v-model="employeesData.mobile"
            placeholder="请输入电话号码"
            autocomplete="off"
          ></el-input>
        </el-form-item>
        <el-form-item
          label="员工邮箱"
          prop="email"
          :label-width="formLabelWidth"
        >
          <el-input
            v-model="employeesData.email"
            placeholder="请输入邮箱"
            autocomplete="off"
          ></el-input>
        </el-form-item>
        <el-form-item
          label="职位"
          prop="PositionNumber"
          :label-width="formLabelWidth"
        >
          <el-select
            v-model="employeesData.PositionNumber"
            placeholder="请选择"
          >
            <el-option label="股东" value="1"></el-option>
            <el-option label="总经理" value="2"></el-option>
            <el-option label="项目经理" value="3"></el-option>
            <el-option label="主管" value="4"></el-option>
          </el-select>
        </el-form-item>
        <el-form-item
          label="部门"
          prop="department"
          :label-width="formLabelWidth"
        >
          <el-select
            v-model="employeesData.DepartmentNumber"
            placeholder="请选择"
          >
            <el-option label="行政部" value="1"></el-option>
            <el-option label="财务部" value="2"></el-option>
            <el-option label="人事部" value="3"></el-option>
            <el-option label="研发部" value="4"></el-option>
            <el-option label="营销部" value="5"></el-option>
          </el-select>
        </el-form-item>
        <el-form-item
          label="项目"
          prop="ProjecNumber"
          :label-width="formLabelWidth"
        >
          <el-checkbox-group v-model="employeesData.ProjecNumber">
            <el-checkbox label="项目 A" disabled></el-checkbox>
            <el-checkbox label="项目 B" disabled></el-checkbox>
            <el-checkbox label="项目 C" disabled></el-checkbox>
            <el-checkbox label="项目 D" disabled></el-checkbox>
          </el-checkbox-group>
        </el-form-item>
        <el-form-item
          label="入职时间"
          prop="date"
          :label-width="formLabelWidth"
        >
          <el-date-picker
            v-model="employeesData.CreatedDate"
            type="date"
            placeholder="选择日期"
          >
          </el-date-picker>
        </el-form-item>
      </el-form>
      <div slot="footer" class="dialog-footer">
        <el-button @click="cancel">取消</el-button>
        <el-button
          type="primary"
          @click="onSubmit('employeesData')"
          :plain="true"
          >确定</el-button
        >
      </div>
    </el-dialog>
  </div>
</template>

<script>
import SearchBar from '../../components/SearchBar.vue'
import Mock, { Random } from 'mockjs'
export default {
  name: 'Employees',
  data () {
    return {
      placeholder: '请输入员工名字查询',
      dialogAddEdit: false,
      operateType: 'add',
      employeesTabel: [],
      employeesData: {
        id: "",
        employeeId: "",
        employeeName: "",
        cardId: "",
        mobile: "",
        email: "",
        sex: "",
        department_name: "",
        posititon_name: "",
        project_name: "",
        DepartmentNumber: "",
        PositionNumber: "",
        ProjecNumber: [],
        CreatedDate: ''
      },
      rules: {
        employeeId: [
          { required: true, message: '名字不能为空', trigger: 'blur' },
          { min: 2, max: 12, message: '名字长度为2-12位', trigger: 'blur' }
        ],
      },
      keywords: '',
      formLabelWidth: '100px',
      currentPage: 1,
      totalCount: 1,
      PageSize: 10,
      PageSizes: [5, 10, 20, 50]
    }
  },
  components: {
    SearchBar, Mock
  },
  methods: {

    getList () {
      const adc = [{
        id: "122",
        employeeId: "qwert",
        employeeName: "lifeasdf",
        cardId: "123",
        mobile: "123",
        email: "123",
        sex: "男",
        department_name: "",
        posititon_name: "",
        project_name: "",
        DepartmentNumber: "",
        PositionNumber: "",
        ProjecNumber: [],
        CreatedDate: ''
      },
      {
        id: "123",
        employeeId: "2001",
        employeeName: "lifei",
        cardId: "321",
        mobile: "321",
        email: "123",
        sex: "女",
        department_name: "",
        posititon_name: "",
        project_name: "",
        DepartmentNumber: "",
        PositionNumber: "",
        ProjecNumber: [],
        CreatedDate: ''
      },
      ];
      // let List = []
      // for (let i = 0; i < 45; i++) {
      //   List.push(
      //     Mock.mock({
      //       "id": i + 2001,
      //       "name": Random.cname(),
      //       "sex|1": ["男", "女"],
      //       "age": Random.natural(18, 60),
      //       "department|1": ["行政部", "财务部", "人事部", "研发部", "营销部"],
      //       "date": Random.date("yyyy-MM-dd"),
      //       "address": `${Random.province()}-${Random.city()}-${Random.county()}`
      //     })
      //   )
      // }
      // console.log(List)
      this.employeesTabel = adc
      this.totalCount = List.length

    },
    addinit () {
      this.employeesData = {
        id: '',
        name: '',
        sex: '',
        age: '',
        department: '',
        date: '',
        address: ''
      }
    },
    addButton () {
      this.addinit()
      this.operateType = 'add'
      this.dialogAddEdit = true
    },
    editButton (row) {
      this.employeesData = row
      this.operateType = 'edit'
      this.dialogAddEdit = true
    },
    cancel () {
      this.$message({
        message: '操作取消',
        type: 'info'
      });
      this.getList()
      this.dialogAddEdit = false
    },
    onSubmit (formName) {
      // this.$refs[formName].validate(valid => {
      //   if (valid) {
      //     if (this.operateType === 'add') {
      //       this.add()
      //     } else {
      //       this.edit()
      //     }
      //     this.dialogAddEdit = false
      //     console.log("success submit!!");
      //   } else {
      //     console.log("error submit!!");
      //   }
      // })
    },
    add () {
      // this.$axios.post('/employees/add', {
      //   params: {
      //     employeesData: this.employeesData
      //   }
      // }).then(res => {
      //   console.log(res)
      //   const { code, message } = res.data
      //   if (code === 200) {
      //     this.getList()
      //     this.$message({
      //       message: message,
      //       type: 'success'
      //     })
      //   } else {
      //     this.$message({
      //       type: 'info',
      //       message: message
      //     })
      //   }
      // })
    },
    serach () {
      var keywords = this.$refs.searchBar.keywords
      console.log(keywords)
      // this.$axios.post('/employees/search', {
      //   params: { keywords: keywords }
      // }).then(res => {
      //   console.log(res)
      //   const { code, message, data } = res.data
      //   if (code === 200) {
      //     this.totalCount = data.total
      //     this.employeesTabel = data.searchList
      //     this.$message({
      //       type: 'success',
      //       message: message
      //     });
      //   } else {
      //     this.getList()
      //     this.$message({
      //       type: 'info',
      //       message: message
      //     });
      //   }
      // })
    },
    edit () {
      // this.$axios.post('/employees/edit', {
      //   params: {
      //     employeesData: this.employeesData
      //   }
      // }).then(res => {
      //   console.log(res)
      //   const { code, message } = res.data
      //   if (code === 200) {
      //     this.getList()
      //     this.$message({
      //       message: message,
      //       type: 'success'
      //     })
      //   } else {
      //     this.$message({
      //       type: 'info',
      //       message: '编辑操作失败'
      //     })
      //   }
      // })
    },
    del (row) {
      // this.$confirm('此操作将永久删除该员工, 是否继续?', '提示', {
      //   confirmButtonText: '确定',
      //   cancelButtonText: '取消',
      //   type: 'warning'
      // }).then(() => {
      //   this.$axios.post('/employees/del', {
      //     params: { id: row.id }
      //   }).then(res => {
      //     console.log(res)
      //     const { code, message } = res.data
      //     if (code === 200) {
      //       this.getList()
      //       this.$message({
      //         type: 'success',
      //         message: message
      //       });
      //     } else {
      //       this.$message({
      //         type: 'info',
      //         message: message
      //       });
      //     }
      //   })
      // }).catch(() => {
      //   this.$message({
      //     type: 'info',
      //     message: '已取消删除员工！'
      //   });
      // });
    },
    // 分页
    // 每页要显示的条数
    handleSizeChange (val) {
      // 改变每页显示的条数
      this.PageSize = val
      this.currentPage = 1
      this.getList()
    },
    handleCurrentChange (val) {
      this.currentPage = val
      this.getList()
    }
  },
  mounted () {
    this.getList()
  }
}
</script>

<style scoped>
.page {
  float: right;
  height: 50px;
  margin: 15px;
}
</style>