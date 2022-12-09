<template>
  <div class="admin">
    <searchBar
      @onAdd="addButton"
      @onSearch="search"
      @onShowAll="getList"
      @onExport="exportUser"
      v-bind:searchShowVal="CanShow"
      ref="searchBar"
    />
    <div class="table">
      <el-table
        :data="
          adminTabel.slice((currentPage - 1) * PageSize, currentPage * PageSize)
        "
        border
        style="width: 100%"
      >
        <!-- <el-table-column fixed prop="Id" label="id号" width="200">
        </el-table-column> -->

        <el-table-column fixed prop="Account" label="账户(登录名)" width="250">
        </el-table-column>
        <el-table-column prop="UserName" label="用户名称" width="250">
        </el-table-column>
        <el-table-column prop="Password" label="密码" width="250">
        </el-table-column>
        <el-table-column prop="Mobile" label="电话" width="250">
        </el-table-column>
        <el-table-column prop="Email" label="邮箱" width="250">
        </el-table-column>
        <el-table-column label="头像" prop="Picture">
          <template slot-scope="scope">
            <img :src="scope.row.Picture" min-width="70" height="70" />
          </template>
        </el-table-column>
        <el-table-column prop="Last_LoginTime" label="上次登录时间" width="250">
        </el-table-column>
        <el-table-column prop="Count" label="登录次数" width="250">
        </el-table-column>
        <el-table-column prop="IsDeleted" label="是否使用" width="250">
        </el-table-column>
        <el-table-column fixed="right" label="操作" width="200">
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
    <!-- 添加和编辑对话框 -->
    <el-dialog
      :title="operateType === 'add' ? '新增管理员' : '更新管理员'"
      :visible.sync="dialogAddEdit"
    >
      <el-form :model="adminData" :rules="rules" ref="adminData">
        <el-form-item label="id号" prop="id" :label-width="formLabelWidth">
          <el-input
            v-model="adminData.Id"
            placeholder="id号默认自增,无须添加"
            autocomplete="off"
            disabled
          ></el-input>
        </el-form-item>

        <el-form-item label="账号" prop="Account" :label-width="formLabelWidth">
          <el-input
            v-model="adminData.Account"
            placeholder="请输入账号"
            autocomplete="off"
          ></el-input>
        </el-form-item>
        <el-form-item
          label="用户名字"
          prop="UserName"
          :label-width="formLabelWidth"
        >
          <el-input
            v-model="adminData.UserName"
            placeholder="请输入姓名"
            autocomplete="off"
          ></el-input>
        </el-form-item>
        <el-form-item
          label="密码"
          prop="Password"
          :label-width="formLabelWidth"
        >
          <el-input
            v-model="adminData.Password"
            placeholder="请输入密码"
            autocomplete="off"
          ></el-input>
        </el-form-item>
        <el-form-item
          label="上传头像"
          prop="image"
          :label-width="formLabelWidth"
        >
          <!-- <el-input v-model="adminData.Picture"></el-input> -->
          <!-- <el-upload
            class="avatar-uploader"
            :show-file-list="false"
            :action="uplord"
            :headers="{
              enctype: 'multipart/form-data',
            }"
            :before-upload="beforeUpload"
            :on-change="handleChange"
            :auto-upload="false"
            :on-success="imgSuccess"
            :data="adminData.Picture"
            :before-upload="beforeAvatarUpload"
          >
            <img
              v-if="adminData.Picture"
              :src="adminData.Picture"
              class="avatar"
            />
            <i v-else class="el-icon-plus avatar-uploader-icon"></i>
          </el-upload> -->

          <el-upload
            class="avatar-uploader"
            :show-file-list="false"
            :action="uplord"
            :headers="{ enctype: 'multipart/form-data' }"
            :on-success="imgSuccess"
          >
            <img
              v-if="uplordP"
              :src="uplordP"
              class="avatar"
              style="width: 178px; height: 178px"
            />
            <i v-else class="el-icon-plus avatar-uploader-icon"></i>
          </el-upload>
        </el-form-item>
        <el-form-item label="电话" prop="Mobile" :label-width="formLabelWidth">
          <el-input
            v-model="adminData.Mobile"
            placeholder="请输入账号"
            autocomplete="off"
          ></el-input>
        </el-form-item>
        <el-form-item label="邮箱" prop="Email" :label-width="formLabelWidth">
          <el-input
            v-model="adminData.Email"
            placeholder="请输入账号"
            autocomplete="off"
          ></el-input>
        </el-form-item>
      </el-form>
      <div slot="footer" class="dialog-footer">
        <el-button @click="cancel">取消</el-button>
        <el-button type="primary" @click="onSubmit('adminData')" :plain="true"
          >确定</el-button
        >
      </div>
    </el-dialog>
  </div>
</template>
  </div>
</template>

<script>
import fileDownload from 'js-file-download'
import SearchBar from '../../components/SearchBar'
import User from '../../api/services/UserEmployee'
import Mock, { Random } from 'mockjs'
import { pUrl, baseUrl } from '../../api/env'

export default {
  name: 'Admin',
  components: { SearchBar },

  data () {
    return {
      value: false,
      dialogAddEdit: false,
      operateType: 'add',
      formLabelWidth: "100px",
      currentPage: 1,
      PageSize: 5,
      uplord: baseUrl + '/Files/ImgUpload',
      PageSizes: [5, 10, 25, 50],
      totalCount: 1,
      adminTabel: [],
      CanShow: false,
      adminData: {
        id: "",
        Account: "",
        UserName: "",
        Password: "",
        Mobile: "",
        Email: "",
        Picture: "",
        Last_LoginTime: "",
        Count: "",
      },
      uplordP: "",
      rules: {
        // image: [{ required: true, message: "头像不能为空", trigger: "blur" }],
        Account: [
          { required: true, message: "账号不能为空", trigger: "blur" },
          { min: 2, max: 10, message: "账号为4-10位", trigger: "blur" },
        ],
        UserName: [
          { required: true, message: "账号不能为空", trigger: "blur" },
          { min: 2, max: 10, message: "账号为4-10位", trigger: "blur" },
        ],
        Password: [
          { required: true, message: "密码不能为空", trigger: "blur" },
          { min: 4, max: 12, message: "密码长度为4-12位", trigger: "blur" },
        ],
      },
      tableData: [{
        id: '001',
        name: '王小虎',
        password: '上海',
        image: Random.image('100x100', '#894FC4', '#FFF', 'png', '001'),
      }, {
        id: '002',
        name: '王小虎',
        password: '上海',
        image: Random.image('100x100', '#894FC4', '#FFF', 'png', '002'),
      }, {
        id: '003',
        name: '王小虎',
        password: '上海',
        image: Random.image('100x100', '#894FC4', '#FFF', 'png', '003'),
      }, {
        id: '004',
        name: '王小虎',
        password: '上海',
        image: Random.image('100x100', '#894FC4', '#FFF', 'png', '004'),
      }, {
        id: '005',
        name: '王小虎',
        password: '上海',
        image: Random.image('100x100', '#894FC4', '#FFF', 'png', '005'),
      }, {
        id: '006',
        name: '王小虎',
        password: '上海',
        image: Random.image('100x100', '#894FC4', '#FFF', 'png', '006'),
      }, {
        id: '007',
        name: '王小虎',
        password: '上海',
        image: Random.image('100x100', '#894FC4', '#FFF', 'png', '007'),
      }, {
        id: '008',
        name: '王小虎',
        password: '上海',
        image: Random.image('100x100', '#894FC4', '#FFF', 'png', '008'),
      }, {
        id: '009',
        name: '王小虎',
        password: '上海',
        image: Random.image('100x100', '#894FC4', '#FFF', 'png', '009'),
      }, {
        id: '011',
        name: '王小虎',
        password: '上海',
        image: Random.image('100x100', '#894FC4', '#FFF', 'png', '011'),
      }, {
        id: '012',
        name: '王小虎',
        password: '上海',
        image: Random.image('100x100', '#894FC4', '#FFF', 'png', '012'),
      }, {
        id: '013',
        name: '王小虎',
        password: '上海',
        image: Random.image('100x100', '#894FC4', '#FFF', 'png', '013'),
      },
      ],
    }
  },

  methods: {
    // beforeAvatarUpload (file) {
    //   const isLt2M = file.size / 1024 / 1024 < 30
    //   if (!isLt2M) {
    //     this.$message.error('上传头像图片大小不能超过 30MB!')
    //   }
    //   if (isLt2M) {
    //     this.loading = this.$loading({
    //       lock: true,
    //       text: '上传中。。。',
    //       spinner: 'el-icon-loading',
    //       background: 'rgba(0, 0, 0, 0.7)',
    //     })
    //   }
    //   return isLt2M
    // },
    imgSuccess (response, file, fileList) {
      // alert(response.Data)
      console.log('imgSuccess', response)
      console.log('imgSuccess', file)
      console.log('imgSuccess', fileList)
      this.uplordP = pUrl + response.Data
      console.log(response.Data)
      this.adminData.Picture = response.Data
      // console.log(this.adminData.Picture)
      // this.CanShow = false;
      // this.CanShow = true;


    },
    editButton (row) {
      console.log("row", row);
      this.dialogAddEdit = true;
      this.operateType = "edit";
      this.adminData = row;
      // this.adminData.image = Row.image;
      // this.adminData.name = Row.name;
      // this.adminData.password = Row.password;
      console.log(this.adminData)

    },
    handleSizeChange (val) {
      this.PageSize = val;
      this.currentPage = 1;
      this.getList();
      console.log(`每页 ${val} 条`);
    },
    handleCurrentChange (val) {
      this.currentPage = val;
      this.getList();
      console.log(`当前页: ${val}`);
    },
    // 上传头像
    // handleChange (file) {
    //   this.adminData.Picture = URL.createObjectURL(file.raw);
    // },
    // beforeUpload (file) {
    //   console.log(file);
    //   return true;
    // },
    cancel () {
      this.$message({
        message: "操作取消",
        type: 'info'
      })
      this.dialogAddEdit = false;
    },
    del (row) {
      this.$confirm('此操作将永久删除改用户信息, 是否继续?', '提示', {
        confirmButtonText: '确定',
        cancelButtonText: '取消',
        type: 'warning'
      }).then(() => {
        User.DeleteUser(row.Id)
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
      })
        .catch(() => {
          this.$message({
            type: 'info',
            message: '已取消删除管理员'
          });
        });
    },
    getList () {
      User.GetUser()
        .then(res => {
          if (res.Success) {
            // var a = res.Data //= eval('(' + res.Data + ')')
            console.log(res.Data)
            this.adminTabel = res.Data
            this.totalCount = this.adminTabel.length
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
      // this.totalCount = this.tableData.length;
      // this.adminTabel = this.tableData;
      console.log('getlist')
    },
    addInit () {
      this.adminData = {
        id: '',
        image: '',
        name: '',
        password: ''
      }
    },
    addButton () {
      this.addInit();
      this.operateType = 'add';
      this.dialogAddEdit = true;
    },
    search () {
      var keywords = this.$refs.searchBar.keywords;
      console.log(keywords);
      this.$message({
        type: "success",
        message: '查询成功',
      });
    },
    onSubmit (adminData) {
      console.log(adminData)
      console.log('refs', this.$refs[adminData])
      this.$refs[adminData].validate((valid => {
        if (valid) {
          if (this.operateType == 'add') {
            User.AddUser(this.adminData)
              .then(res => {
                console.log(res.Data)
                if (res.Success) {
                  var a = res.Data.Id
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
            User.UpdateUser(this.adminData)
              .then(res => {
                console.log(res.Data)
                if (res.Success) {
                  var a = res.Data.Id
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

          this.dialogAddEdit = false;
          this.getList();
          console.log("success submit!!");
        } else {
          console.log("error submit!!");
        }
      }))
    },
    ///ExportUserList
    exportUser () {
      console.log('exportUser')
      User.ExportUserList()
        .then(res => {
          let fileName = '用户信息信息' + '.xlsx'
          fileDownload(res, fileName)
        })
      console.log('getlist')
    }
  },
  mounted () {
    this.getList();
  },
}
</script>

<style scoped>
.page {
  float: right;
  height: 50px;
  margin: 15px;
}

input[type="file"] {
  display: none;
}

.avatar-uploader .el-upload {
  border-radius: 6px;
  cursor: pointer;
  position: relative;
  overflow: hidden;
}

.avatar-uploader .el-upload:hover {
  border-color: #409eff;
}

.avatar-uploader-icon {
  font-size: 28px;
  color: #8c939d;
  width: 178px;
  height: 178px;
  line-height: 178px;
  text-align: center;
}

.avatar {
  width: 178px;
  height: 178px;
  display: block;
}
</style>>
</style>