<template>
  <div class="employess">
    <search-bar
      @onAdd="onAdd"
      @onSearch="search"
      @onExport="exportEmpoyee"
      :placeholder="placeholder"
      ref="searchBar"
    ></search-bar>
    <div class="table">
      <el-table :data="dataTable" border style="width: 100%">
        <el-table-column fixed prop="EmployeeId" label="员工编号" width="180">
        </el-table-column>
        <el-table-column prop="EmployeeName" label="员工姓名" width="180">
        </el-table-column>
        <el-table-column prop="CredId" label="身份证号" width="180">
        </el-table-column>
        <el-table-column prop="Sex" label="性别" width="180"> </el-table-column>
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
              @click="editButton(scope.row)"
              type="primary"
              icon="el-icon-edit"
              size="small"
              >登录权限</el-button
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
    </div>
    <div class="page">
      <div class="block">
        <el-pagination
          :hide-on-single-page="value"
          @size-change="handleSizeChange"
          @current-change="handleCurrentChange"
          :current-page="currentPage"
          :page-sizes="PageSizes"
          :page-size="PageSize"
          layout="total, sizes, prev, pager, next, jumper"
          :total="totalCount"
        >
        </el-pagination>
      </div>
    </div>
    <!-- 添加对话框 -->
    <el-dialog
      :title="operateType === 'add' ? '新增员工' : '更新员工信息'"
      :visible.sync="dialogAddEdit"
    >
      <el-form :model="employeesData" :rules="rules" ref="employeesData">
        <el-form-item label="id号" prop="id" :label-width="formLabelWidth">
          <el-input
            v-model="employeesData.Id"
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
            v-model="employeesData.EmployeeId"
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
            v-model="employeesData.EmployeeName"
            placeholder="请输入姓名"
            autocomplete="off"
          ></el-input>
        </el-form-item>
        <el-form-item label="性别" prop="sex" :label-width="formLabelWidth">
          <el-select v-model="employeesData.Sex" placeholder="请选择">
            <el-option label="男" value="男"></el-option>
            <el-option label="女" value="女"></el-option>
          </el-select>
        </el-form-item>
        <el-form-item
          label="员工身份证号"
          prop="CardId"
          :label-width="formLabelWidth"
        >
          <el-input
            v-model="employeesData.CardId"
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
            v-model="employeesData.Mobile"
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
            v-model="employeesData.Email"
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

import fileDownload from 'js-file-download'
import searchBar from '../../components/SearchBar.vue'
import { Row } from 'element-ui'
import Employee from '../../api/services/UserEmployee'
export default {
  name: 'Employees',
  components: {
    searchBar
  },
  data () {
    return {
      placeholder: '请输入姓名',
      adminTabel: [],
      dataTable: [],
      value: false,
      currentPage: 1,
      PageSizes: [5, 10, 20, 30],
      PageSize: 5,
      totalCount: 1,
      operateType: 'add',
      formLabelWidth: '100px',
      dialogAddEdit: false,
      employeesData: {
        // id: "",
        // employeeId: "",
        // employeeName: "",
        // cardId: "",
        // mobile: "",
        // email: "",
        // sex: "",
        // department_name: "",
        // posititon_name: "",
        // project_name: "",
        // DepartmentNumber: "",
        // PositionNumber: "",
        // ProjecNumber: [],
        // CreatedDate: ''


      },
      rules: {
        EmployeeId: [
          { required: true, message: "账号不能为空", trigger: "blur" },
          { min: 4, max: 10, message: "账号为4-10位", trigger: "blur" },
        ],
        EmployeeName: [
          { required: true, message: "账号不能为空", trigger: "blur" },
          { min: 1, max: 10, message: "账号为1-10位", trigger: "blur" },
        ],
        CardId: [
          { required: true, message: "身份证不能为空", trigger: "blur" },
          { min: 8, max: 16, message: "年龄不正确", trigger: "blur" },
        ],
        Email: [
          { required: true, message: "邮箱不能为空", trigger: "blur" },
        ],
        Sex: [
          { required: true, message: "性别不能为空", trigger: "blur" },
        ],
        DepartmentNumber: [
          { required: true, message: "部门不能为空", trigger: "blur" },
        ],
        CreatedDate: [
          { required: true, message: "日期不能为空", trigger: "blur" },
        ],
      },
    }
  },
  methods: {
    addinit () {
      this.employeesData = {
        id: "",
        employee_id: "",
        employee_name: "",
        card_id: "",
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


      }
    },
    handleSizeChange (val) {
      this.PageSize = val;

      console.log('handleSizeChange', val)
      this.getList()
    },
    handleCurrentChange (val) {
      this.currentPage = val;
      this.getList();
      console.log(`当前页: ${val}`);
    },
    editButton (row) {
      console.log(row)
      this.employeesData = row

      this.dialogAddEdit = true;
      this.operateType = 'edit';
      console.log(this.employeesData)

    },
    del (row) {
      console.log('ID', row.Id);
      this.$confirm('此操作将永久删除该员工信息, 是否继续?', '提示', {
        confirmButtonText: '确定',
        cancelButtonText: '取消',
        type: 'warning'
      }).then(() => {
        Employee.delEmployee(row.Id)
          .then(res => {
            console.log(res)
            if (res.Success) {
              var a = res.Data

              this.$message({
                type: 'success',
                message: '删除成功!'
              });
              this.getList();
            } else {
              alert(res.Message)
            }
          })


      }).catch(() => {
        this.$message({
          type: 'info',
          message: '已取消删除管理员'
        });
      });
    },
    getList () {
      console.log('getList')
      Employee.GetEmployee(this.currentPage, this.PageSize)
        .then(res => {
          // const token = res.Data
          if (res.Success) {
            // let List = []
            // for (let i = 0; i < 4; i++) {
            //   List.push(
            //     Mock.mock({
            //       "id": i + 2001,
            //       "employee_name": Random.cname(),
            //       "sex|1": ["男", "女"],
            //       "age": Random.natural(18, 60),
            //       "department|1": ["行政部", "财务部", "人事部", "研发部", "营销部"],
            //       "date": Random.date("yyyy-MM-dd"),
            //       "address": `${Random.province()}-${Random.city()}-${Random.county()}`
            //     })
            //   )
            // }
            // this.dataTable = List
            // console.log(this.dataTable)
            // console.log('List', typeof (List))
            // console.log(res.Data)
            var a = res.Data //= eval('(' + res.Data + ')')
            this.dataTable = a
            this.totalCount = res.Data[0].Count
            // this.totalCount = this.dataTable.length
            // console.log(this.dataTable.length)
            // this.dataTable.forEach(a => {
            //   console.log(a)

            // })
            // this.$message({
            //   message: res.data.message,
            //   type: 'success'
            // })
          } else {
            alert(res.Message)
          }
        })

      // this.dataTable = List
      // this.totalCount = this.dataTable.length
    },
    onAdd () {
      this.dialogAddEdit = true;
      for (let key in this.employeesData) {
        this.employeesData[key] = ''
      }
    },
    onSubmit (employeesData) {
      console.log(this.employeesData)
      console.log('refs', this.$refs[employeesData])
      this.$refs[employeesData].validate((valid => {
        if (valid) {
          console.log('@operateType', this.operateType)
          if (this.operateType === 'add') {
            console.log('TypeoperateType', this.operateType)
            Employee.AddEmployee(this.employeesData)
              .then(res => {
                console.log(res)
                if (res.Success) {
                  var a = res.Data
                  this.$message({
                    type: 'success',
                    message: '新增成功!'
                  });
                  this.getList();
                } else {
                  alert(res.Message)
                }
              })
          } else {
            Employee.UpdateEmployee(this.employeesData)
              .then(res => {
                console.log(res)
                if (res.Success) {
                  var a = res.Data
                  this.$message({
                    type: 'success',
                    message: '修改成功!'
                  });
                  this.getList();
                } else {
                  alert(res.Message)
                }
              })
          }
          this.getList();
          this.dialogAddEdit = false;

        } else {
          console.log("error submit!!");
        }
      }))
    },
    cancel () {
      this.$message({
        message: '操作取消',
        type: 'info'
      });
      this.getList()
      this.dialogAddEdit = false;
    },
    search () {
      console.log("aaa");
      var keywordss = this.$refs.searchBar.keywords;
      if (keywordss) {
        this.$message({
          type: "success",
          message: '查询成功',
        });
      }

    },
    exportEmpoyee () {
      console.log('export')
      Employee.ExportEmployeeList()
        .then(res => {
          let fileName = '员工信息' + '.xlsx'
          fileDownload(res, fileName)
        })
    }

  },
  mounted () {
    console.log('getlist')
    this.getList()
  }

}
</script>

<style>
.page {
  float: right;
  height: 50px;
  margin: 15px;
}
</style>