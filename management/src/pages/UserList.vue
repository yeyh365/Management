<template>
  <div class="admin">
    <div class="tabel">
      <template>
        <el-table
          :data="
            adminTabel.slice(
              (currentPage - 1) * PageSize,
              currentPage * PageSize
            )
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
  </div>
</template>

<script>

export default {
  name: "UserList",
  data () {
    return {
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
      keywords: "",
      formLabelWidth: "100px",
      currentPage: 1,
      totalCount: 1,
      PageSize: 10,
      PageSizes: [5, 10, 20, 50],
    };
  },
  components: {
  },
  methods: {
    // 上传头像
    handleChange (file) {
      this.adminData.image = URL.createObjectURL(file.raw);
    },
    beforeUpload (file) {
      console.log(file);
      return true;
    },
    getList () {
      this.$axios.get("/admin/getData").then((res) => {
        console.log(res);
        const { code, data } = res.data;
        if (code === 200) {
          this.totalCount = data.total;
          this.adminTabel = data.adminList;
        }
      });
    },
    addinit () {
      this.adminData = {
        id: "",
        image: "",
        name: "",
        password: "",
      };
    },
    addButton () {
      this.addinit();
      this.operateType = "add";
      this.dialogAddEdit = true;
    },
    editButton (row) {
      this.adminData = row;
      this.operateType = "edit";
      this.dialogAddEdit = true;
    },
    cancel () {
      this.$message({
        message: "操作取消",
        type: "info",
      });
      this.getList();
      this.dialogAddEdit = false;
    },
    add () {
      this.$axios
        .post("/admin/add", {
          params: {
            adminData: this.adminData,
          },
        })
        .then((res) => {
          console.log(res);
          const { code, message } = res.data;
          if (code === 200) {
            this.getList();
            this.$message({
              message: message,
              type: "success",
            });
          } else {
            this.$message({
              type: "info",
              message: message,
            });
          }
        });
    },
    serach () {
      var keywords = this.$refs.searchBar.keywords;
      console.log(keywords);
      this.$axios
        .post("/admin/search", {
          params: { keywords: keywords },
        })
        .then((res) => {
          console.log(res);
          const { code, message, data } = res.data;
          if (code === 200) {
            this.totalCount = data.total;
            this.adminTabel = data.searchList;
            this.$message({
              type: "success",
              message: message,
            });
          } else {
            this.getList();
            this.$message({
              type: "info",
              message: message,
            });
          }
        });
    },
    edit () {
      this.$axios
        .post("/admin/edit", {
          params: {
            adminData: this.adminData,
          },
        })
        .then((res) => {
          console.log(res);
          const { code, message } = res.data;
          if (code === 200) {
            this.getList();
            this.$message({
              message: message,
              type: "success",
            });
          } else {
            this.$message({
              type: "info",
              message: "编辑操作失败",
            });
          }
        });
    },
    del (row) {
      this.$confirm("此操作将永久删除该管理员, 是否继续?", "提示", {
        confirmButtonText: "确定",
        cancelButtonText: "取消",
        type: "warning",
      })
        .then(() => {
          this.$axios
            .post("/admin/del", {
              params: { id: row.id },
            })
            .then((res) => {
              console.log(res);
              const { code, message } = res.data;
              if (code === 200) {
                this.getList();
                this.$message({
                  type: "success",
                  message: message,
                });
              } else {
                this.$message({
                  type: "info",
                  message: message,
                });
              }
            });
        })
        .catch(() => {
          this.$message({
            type: "info",
            message: "已取消删除管理员！",
          });
        });
    },
    // 分页
    // 每页要显示的条数
    handleSizeChange (val) {
      // 改变每页显示的条数
      this.PageSize = val;
      this.currentPage = 1;
      this.getList();
    },
    handleCurrentChange (val) {
      this.currentPage = val;
      this.getList();
    },
  },
  mounted () {
    this.getList();
  },
};
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
</style>