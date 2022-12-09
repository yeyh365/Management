<template>
  <div>
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
        <el-table-column fixed="right" label="操作" width="100">
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
        <span>申请人:{{ dialogData.Account }}</span>
        <span>申请分类:{{ dialogData.ApplyTitle }}</span>
      </div>

      <div class="applyHeard">
        <el-steps :active="1">
          <el-step title="员工申请" :description="dialogData.ApplyBoby">{{
            dialogData.ApplyBoby
          }}</el-step>
          <el-step title="项目经理审批" description="备注："></el-step>
          <el-step title="总经理审批审批" description="备注"></el-step>
        </el-steps>
        <el-input v-model="opinion" placeholder="备注："></el-input>
      </div>
      <span slot="footer" class="dialog-footer">
        <el-button type="primary" @click="AgreeApply(dialogData)"
          >同意</el-button
        >
        <el-button type="danger" @click="DissagreeApply(dialogData)"
          >驳回</el-button
        >
      </span>
    </el-dialog>
  </div>
</template>

<script>
import Apply from '@/api/services/ApplyService'
export default {
  name: 'Approval',
  data () {
    return {
      value: false,
      currentPage: 1,
      PageSizes: ['5', '10', '20'],
      PageSize: 5,
      applyData: [],
      totalCount: 30,
      centerDialogVisible: false,
      dialogData: {

      },
      opinion: ''
    }
  },
  mounted () {
    this.getApplyList();
  },
  methods: {
    AgreeApply (row) {

      var userName = sessionStorage.getItem('user')
      console.log('userName', userName)
      if (userName === row.ApproverDepartmentId) {
        row.ApproverrDepartmenResult = true

      } else if (userName === row.GeneralManagerId) {
        row.GeneralManagerResult = true
        row.ApplyState = false;
        row.ApplyResult = '申请成功';
      }
      console.log('AgreeApply', row)
      Apply.AgreeApply(row)
        .then(res => {
          if (res.Success) {
            console.log(res.Data)
            this.getApplyList()
          } else {
            alert(res.Message)
          }
        })
    },
    DissagreeApply (row) {
      console.log('DissagreeApply', row)
    },
    viewApply (row) {
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
    // del (row) {
    //   Apply.DelApply(row)
    //     .then(res => {
    //       console.log(res)
    //       if (res.Success) {
    //         console.log(res.Data)
    //         this.getApplyList()
    //       } else {
    //         alert(res.Message)
    //       }
    //     })

    // },
    getApplyList () {
      var Account = sessionStorage.getItem('user')
      console.log(Account)
      Apply.GetApprovalApply(this.currentPage, this.PageSize, Account)
        .then(res => {
          if (res.Success) {
            this.applyData = res.Data
            this.applyData.forEach(element => {
              element.ApproverrDepartmenResult = element.ApproverrDepartmenResult ? '同意' : ''

            });

            this.totalCount = res.Data[0].Count ? res.Data[0].Count : 0
            console.log(res.Data)

          } else {
            alert(res.Message)
          }
        })
    }
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