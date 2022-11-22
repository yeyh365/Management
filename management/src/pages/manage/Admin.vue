<template>
  <div class="admin">
    <searchBar
      @onAdd="addButton"
      @onSearch="search"
      @onShowAll="getList"
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
        <el-table-column fixed prop="id" label="id号" width="200">
        </el-table-column>
        <el-table-column label="头像" prop="image">
          <template slot-scope="scope">
            <img :src="scope.row.image" min-width="70" height="70" />
          </template>
        </el-table-column>
        <el-table-column prop="name" label="账号" width="250">
        </el-table-column>
        <el-table-column prop="password" label="密码" width="250">
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
            v-model="adminData.id"
            placeholder="id号默认自增,无须添加"
            autocomplete="off"
            disabled
          ></el-input>
        </el-form-item>
        <el-form-item
          label="上传头像"
          prop="image"
          :label-width="formLabelWidth"
        >
          <el-input v-model="adminData.image" v-if="false"></el-input>
          <el-upload
            class="avatar-uploader"
            :show-file-list="false"
            action="/admin/upload"
            :before-upload="beforeUpload"
            :on-change="handleChange"
            :auto-upload="false"
            :data="adminData"
          >
            <img v-if="adminData.image" :src="adminData.image" class="avatar" />
            <i v-else class="el-icon-plus avatar-uploader-icon"></i>
          </el-upload>
        </el-form-item>
        <el-form-item label="账号" prop="name" :label-width="formLabelWidth">
          <el-input
            v-model="adminData.name"
            placeholder="请输入账号"
            autocomplete="off"
          ></el-input>
        </el-form-item>
        <el-form-item
          label="密码"
          prop="password"
          :label-width="formLabelWidth"
        >
          <el-input
            v-model="adminData.password"
            placeholder="请输入密码"
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
import SearchBar from '../../components/SearchBar'
import Mock, { Random } from 'mockjs'
import { Row } from 'element-ui'
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
      PageSizes: [5, 10, 25, 50],
      totalCount: 1,
      adminTabel: [],
      adminData: {
        id: "",
        image: "",
        name: "",
        password: ""
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
    handleChange (file) {
      this.adminData.image = URL.createObjectURL(file.raw);
    },
    beforeUpload (file) {
      console.log(file);
      return true;
    },
    cancel () {
      this.$message({
        message: "操作取消",
        type: 'info'
      })
      this.dialogAddEdit = false;
    },
    del (row) {
      this.$confirm('此操作将永久删除该文件, 是否继续?', '提示', {
        confirmButtonText: '确定',
        cancelButtonText: '取消',
        type: 'warning'
      }).then(() => {
        this.$message({
          type: 'success',
          message: '删除成功!'
        });
      }).catch(() => {
        this.$message({
          type: 'info',
          message: '已取消删除管理员'
        });
      });
    },
    getList () {
      this.totalCount = this.tableData.length;
      this.adminTabel = this.tableData;
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
    onSubmit (fromName) {
      console.log(fromName)
      console.log('refs', this.$refs[fromName])
      this.$refs[fromName].validate((valid => {
        if (valid) {
          console.log("success submit!!");
          this.dialogAddEdit = false;
        } else {
          console.log("error submit!!");
        }
      }))
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