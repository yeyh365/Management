<template>
  <div>
    <div style="width: 100%; height: 100px">
      <!-- 添加会员 -->
      <el-button
        type="primary"
        @click="sendmessage"
        style="
          margin-top: 30px;
          float: left;
          margin-left: 10px;
          margin-bottom: 25px;
        "
        ><i class="el-icon-s-promotion">发送</i></el-button
      >
      <el-button
        type="primary"
        style="
          margin-top: 30px;
          float: left;
          margin-left: 5px;
          margin-bottom: 25px;
        "
        >√全选</el-button
      >
      <el-button
        type="primary"
        style="
          margin-top: 30px;
          float: left;
          margin-left: 5px;
          margin-bottom: 25px;
        "
        >×批量删除</el-button
      >
    </div>

    <!-- 弹出的页面内容 -->
    <el-dialog :visible.sync="getmessage">
      <el-form
        :model="ruleForm"
        :rules="rules"
        ref="ruleForm"
        label-width="100px"
        class="demo-ruleForm"
      >
        <el-form-item label="收件人：">
          <!-- 下拉框 -->
          <el-select v-model="value" placeholder="请选择">
            <el-option
              v-for="item in options"
              :key="item.value"
              :label="item.label"
              :value="item.value"
            ></el-option>
          </el-select>
        </el-form-item>
        <el-form-item label="">
          <el-input
            v-model="ruleForm"
            placeholder="标题信息"
            style="margin-top: 10px"
          ></el-input>
        </el-form-item>
        <el-form-item label="">
          <el-card style="height: 610px">
            <quill-editor
              v-model="content"
              ref="myQuillEditor"
              style="height: 500px"
              :options="editorOption"
            >
            </quill-editor>
          </el-card>
        </el-form-item>
        <el-form-item>
          <el-button
            type="primary"
            @click="submitForm('ruleForm')"
            style="float: right"
            >确定</el-button
          >
        </el-form-item>
      </el-form>
    </el-dialog>

    <!-- 表格 -->
    <el-table :data="tableData" style="width: 100%">
      <el-table-column label="">
        <div>
          <input type="checkbox" v-model="use1" id="r1" />
        </div>
      </el-table-column>
      <el-table-column label="标题" prop="date"> </el-table-column>
      <el-table-column label="时间" prop="name"> </el-table-column>
      <el-table-column label="接受条件" prop="date"> </el-table-column>
      <el-table-column label="操作">
        <template slot-scope="scope">
          <el-button size="mini" @click="handleEdit(scope.$index, scope.row)"
            >Edit</el-button
          >
          <el-button
            size="mini"
            type="danger"
            @click="handleDelete(scope.$index, scope.row)"
            >Delete</el-button
          >
        </template>
      </el-table-column>
    </el-table>
  </div>
</template>
 

<script>
import element from "element-ui";

export default {
  name: "TestTC",
  components: {
    element,
  },

  data() {
    return {
      //弹框
      getmessage: false,
    };
  },
  methods: {
    //弹框
    sendmessage() {
      this.getmessage = true;
    },
  },
};
</script>
<style>
</style>