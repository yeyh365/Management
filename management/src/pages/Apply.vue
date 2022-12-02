<template>
  <div class="apply">
    <h2>员工事由申请</h2>
    <div class="applyForm">
      <!-- <el-form
        :inline="true"
        :model="numberValidateForm"
        class="demo-form-inline"
        ref="numberValidateForm"
      >
        <el-form-item label="申请分类">
          <el-select v-model="applyProcess.type" placeholder="请选择">
            <el-option label="请假申请" value="请假申请"></el-option>
            <el-option label="休假申请" value="休假申请"></el-option>
            <el-option label="项目资金申请" value="项目资金申请"></el-option>
            <el-option label="其他" value="其他"></el-option>
          </el-select>
        </el-form-item>

        <el-form-item
          label="申请原因"
          prop="age"
          ref="numberValidateForm"
          style="width: 300px"
          :rules="[{ required: true, message: '年龄不能为空', blur: false }]"
        >
          <el-input
            v-model="numberValidateForm.age"
            placeholder="请输入理由"
            autocomplete="off"
            :validate-event="false"
          ></el-input>
        </el-form-item>
      </el-form> -->
      <el-form
        :model="numberValidateForm"
        ref="numberValidateForm"
        label-width="100px"
        class="demo-ruleForm"
      >
        <el-form-item label="申请分类">
          <el-select
            v-model="numberValidateForm.ApplyTitle"
            placeholder="请选择"
          >
            <el-option label="请假申请" value="请假申请"></el-option>
            <el-option label="休假申请" value="休假申请"></el-option>
            <el-option label="项目资金申请" value="项目资金申请"></el-option>
            <el-option label="其他" value="其他"></el-option>
          </el-select>
        </el-form-item>
        <el-form-item
          label="申请原因"
          prop="ApplyBoby"
          :rules="[{ required: true, message: '申请不能为空' }]"
        >
          <el-input
            v-model="numberValidateForm.ApplyBoby"
            autocomplete="off"
            :validate-event="false"
            style="width: 500px"
          ></el-input>
        </el-form-item>
        <el-form-item>
          <el-button type="primary" @click="submitForm('numberValidateForm')"
            >提交</el-button
          >
        </el-form-item>
      </el-form>
    </div>

    <div class="applyBody">
      <el-table :data="applyData" style="width: 100%">
        <el-table-column prop="ApplyDate" label="日期" width="200">
        </el-table-column>
        <el-table-column prop="Account" label="姓名" width="100">
        </el-table-column>
        <el-table-column prop="ApplyTitle" label="申请分类" width="100">
        </el-table-column>
        <el-table-column prop="ApplyBoby" label="申请内容" width="300">
        </el-table-column>
        <el-table-column prop="ApproverDepartmentId" label="部门经理">
        </el-table-column>
        <el-table-column
          prop="ApproverrDepartmenResult"
          label="审批结果"
          width="100"
        >
        </el-table-column>
        <el-table-column prop="GeneralManagerId" label="总经理" width="100">
        </el-table-column>
        <el-table-column
          prop="GeneralManagerResult"
          label="审批结果"
          width="100"
        >
        </el-table-column>
        <el-table-column prop="ApplyState" label="流程状态" width="100">
        </el-table-column>
        <el-table-column prop="ApplyResult" label="流程结果" width="100">
        </el-table-column>
        <el-table-column fixed="right" label="操作" width="120">
          <template slot-scope="scope">
            <el-button
              @click="del(scope.row)"
              type="danger"
              icon="el-icon-delete"
              size="small"
              >取消申请</el-button
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
    <div class="applyHeard">
      <el-steps :active="3">
        <el-step title="员工申请" description="申请原因"></el-step>
        <el-step title="项目经理审批" description="备注："></el-step>
        <el-step title="总经理审批审批" description="备注"></el-step>
      </el-steps>
    </div>
  </div>
</template>

<script>
import Apply from '../api/services/ApplyService'
export default {
  name: 'Apply',
  data () {
    return {
      applyProcess: {
        type: '',
        reason: ''
      },
      numberValidateForm: {
        ApplyTitle: '',
        ApplyBoby: ''
      },
      value: false,
      currentPage: 1,
      PageSizes: ['5', '10', '20'],
      PageSize: 5,
      applyData: [],
      totalCount: 30

    }
  },
  mounted () {
    this.getApplyList();
  },
  methods: {
    handleSizeChange (val) {
      this.PageSize = val;
      this.getApplyList();
      console.log('handleSizeChange')
    },
    handleCurrentChange (val) {
      this.currentPage = val
      this.getApplyList();
      console.log('handleCurrentChange')
    },
    submitForm (formName) {
      console.log(formName)
      this.$refs[formName].validate((valid) => {
        if (valid) {
          this.numberValidateForm.Account = sessionStorage.getItem('user')
          console.log(this.numberValidateForm)
          Apply.AddApply(this.numberValidateForm)
            .then(res => {
              console.log(res)
              if (res.Success) {
                this.$message({
                  type: 'success',
                  message: '添加成功!'
                });
                this.getApplyList();

              } else {
                alert(res.Message)
              }
            })



        } else {
          console.log('error submit!!');
          return false;
        }
      });

      console.log(this.numberValidateForm.reason)
    },
    del (row) {
      Apply.DelApply(row)
        .then(res => {
          console.log(res)
          if (res.Success) {
            console.log(res.Data)
            this.getApplyList()
          } else {
            alert(res.Message)
          }
        })

    },
    getApplyList () {
      Apply.GetApplyList(this.currentPage, this.PageSize)
        .then(res => {
          if (res.Success) {
            this.applyData = res.Data
            this.totalCount = res.Data[0].Count
            console.log(res.Data)

          } else {
            alert(res.Message)
          }
        })
    }
  },

}
</script>

<style>
.apply {
  margin: 30px;
}
.applyForm {
  background-color: #fff;
}
.applyHeard {
  margin-top: 30px;
}
.applyBody {
  margin-top: 30px;
}
</style>