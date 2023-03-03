<template>
  <div>
    <div class="applyBody">
      <el-table :data="applyData" style="width: 100%">
        <el-table-column prop="DocumentTime" label="日期" width="200">
        </el-table-column>
        <el-table-column prop="CreateUser" label="申请用户" width="100">
        </el-table-column>
        <el-table-column prop="ApplyTitle" label="申请分类" width="100">
        </el-table-column>
        <el-table-column prop="ApplyBoby" label="申请内容" width="300">
        </el-table-column>
        <el-table-column prop="FlowSerialnunber" label="当前环节">
        </el-table-column>
        <el-table-column prop="AssigneeId" label="当前审批人" width="100">
        </el-table-column>
        <!-- <el-table-column prop="GeneralManagerId" label="总经理" width="100">
        </el-table-column>
        <el-table-column
          prop="GeneralManagerResult"
          label="审批结果"
          width="100"
        >
        </el-table-column> -->
        <el-table-column prop="AuditStatus" label="审批结果" width="100">
        </el-table-column>
        <!-- <el-table-column prop="ApplyResult" label="流程结果" width="100">
        </el-table-column> -->
        <el-table-column fixed="right" label="操作" width="180">
          <template slot-scope="scope">
            <el-button
              @click="viewApply(scope.row)"
              type="primary"
              icon="el-icon-thumb"
              size="small"
              >查看</el-button
            >
            <!-- <el-button
              @click="del(scope.row)"
              type="danger"
              icon="el-icon-delete"
              size="small"
              >取消</el-button
            > -->
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

    <!-- 详情对话框 -->
    <el-dialog
      title="申请详情"
      :visible.sync="centerDialogVisible"
      width="60%"
      center
    >
      <div>
        <span>申请人:{{ dialogData.CreateUser }}</span>
        <span>申请分类:{{ dialogData.ApplyTitle }}</span>
      </div>

      <div class="applyHeard">
        <el-steps :active="pengingNumber">
          <el-step title="员工申请" :description="dialogData.ApplyBoby">{{
            dialogData.ApplyBoby
          }}</el-step>
          <el-step title="项目经理审批" description="备注："></el-step>
          <el-step title="总经理审批审批" description="备注"></el-step>
        </el-steps>
      </div>
      <div class="applyForm">
        <el-form
          :model="numberValidateForm"
          ref="numberValidateForm"
          label-width="100px"
          class="demo-ruleForm"
        >
          <el-form-item
            label="审批意见"
            prop="WorkflowBoby"
            :rules="[{ required: true, message: '意见不能为空' }]"
          >
            <el-input
              v-model="numberValidateForm.WorkflowBoby"
              autocomplete="off"
              :validate-event="false"
              style="width: 500px"
            ></el-input>
          </el-form-item>
          <el-form-item>
            <el-button @click="submitForm('numberValidateForm')"
              >同意</el-button
            >

            <el-button @click="submitForm('numberValidateForm')"
              >驳回</el-button
            >
            <el-button @click="centerDialogVisible = false">取消</el-button>
            <span slot="footer" class="dialog-footer"> </span>
          </el-form-item>
        </el-form>
      </div>
    </el-dialog>
  </div>
</template>

<script>
import Apply from '@/api/services/ApplyService'
import Workflow from '@/api/services/WorkflowService'
export default {
  name: 'PendingApproval',
  data () {
    return {
      applyProcess: {
        type: '',
        reason: ''
      },
      value: false,
      currentPage: 1,
      PageSizes: ['5', '10', '20'],
      PageSize: 5,
      applyData: [],
      totalCount: 30,
      centerDialogVisible: false,
      dialogData: {

      },
      pengingNumber: 1,
      numberValidateForm: {
        WorkflowCode: '',
        WorkflowTitle: '',
        WorkflowBoby: ''
      },
    }
  },
  methods: {
    viewApply (row) {
      this.pengingNumber = parseInt(row.FlowSerialnunber)
      this.centerDialogVisible = true;
      this.dialogData = row;

      console.log(row);
    },
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
    //获取待审批列表
    getApplyList () {
      var Account = sessionStorage.getItem('user')
      Workflow.GetUserPendingWorkflowList(this.currentPage, this.PageSize, Account)
        .then(res => {
          if (res.Success) {
            console.log('GetUserPendingWorkflowList', res)
            this.applyData = res.Data.List
            this.totalCount = res.Data.Count
            for (let i = 0; i < this.applyData.length; i++) {
              this.applyData[i].DocumentTime = testTime(this.applyData[i].DocumentTime)
              this.applyData[i].DocumentTime = testTime(this.applyData[i].HandleDate)
            }
            console.log(res.Data)
          } else {
            alert(res.Message)
          }
          function testTime (date) {
            let newDate = new Date(date)
            return new Date(newDate.getTime() + 8 * 3600 * 1000).toISOString().replace(/T/g, ' ').replace(/\.[\d]{3}Z/, '')
          }
        })
    },
    submitForm (formName) {
      console.log(formName)
      this.$refs[formName].validate((valid) => {
        if (valid) {
          this.numberValidateForm.Account = sessionStorage.getItem('user')
          console.log(this.numberValidateForm)
          // Workflow.AddWorkflow(this.numberValidateForm)
          //   .then(res => {
          //     console.log(res)
          //     if (res.Success) {
          //       this.$message({
          //         type: 'success',
          //         message: '添加成功!'
          //       });
          //       // this.getApplyList();
          //       this.$nextTick(() => {
          //         this.$refs['Childmain'].getApplyList()
          //       })

          //     } else {
          //       alert(res.Message)
          //     }
          //   })



        } else {
          console.log('error submit!!');
          return false;
        }
      });
      console.log(this.numberValidateForm.reason)
    },
  },
  mounted () {
    this.getApplyList();
  },
}
</script>

<style scope>
.applyHeard {
  margin-top: 30px;
}
.applyBody {
  margin-top: 30px;
}
</style>