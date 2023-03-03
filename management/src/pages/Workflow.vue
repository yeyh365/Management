<template>
  <div class="apply">
    <h2>工作流审批</h2>
    <div class="applyForm">
      <el-form
        :model="numberValidateForm"
        ref="numberValidateForm"
        label-width="100px"
        class="demo-ruleForm"
      >
        <el-form-item label="申请流程ID">
          <el-select v-model="numberValidateForm.WorkflowTitle">
            <!-- <el-option label="请假申请" value="请假申请"></el-option>
            <el-option label="休假申请" value="休假申请"></el-option>
            <el-option label="项目资金申请" value="项目资金申请"></el-option>
            <el-option label="其他" value="其他"></el-option> -->
          </el-select>
          <el-select
            v-model="numberValidateForm.WorkflowTitle"
            placeholder="请选择"
          >
            <el-option
              v-for="item in WorkflowMenu"
              :key="item.value"
              :label="item.label"
              :value="item.value"
            >
            </el-option>
          </el-select>
        </el-form-item>
        <el-form-item
          label="申请原因"
          prop="WorkflowBoby"
          :rules="[{ required: true, message: '申请不能为空' }]"
        >
          <el-input
            v-model="numberValidateForm.WorkflowBoby"
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
    <div class="notice">
      <div class="nav">
        <el-menu
          :default-active="$route.path"
          class="el-menu-demo"
          mode="horizontal"
        >
          <el-menu-item
            @click="clickMenu(item)"
            v-for="item in menu"
            :index="item.path"
            :key="item.path"
          >
            <span slot="title">{{ item.label }}</span>
          </el-menu-item>
        </el-menu>
      </div>
      <router-view ref="Childmain" />
    </div>
  </div>
</template>

<script>
import Workflow from '../api/services/WorkflowService'
export default {
  name: 'Workflow',
  data () {
    return {
      applyProcess: {
        type: '',
        reason: ''
      },
      numberValidateForm: {
        WorkflowCode: '',
        WorkflowTitle: '',
        WorkflowBoby: ''
      },
      menu: [
        {
          path: '/workflow/PendingApproval',
          name: 'PendingApproval',
          label: '待办审批',
        },
        {
          path: '/workflow/ApprovedAll',
          name: 'ApprovedAll',
          label: '全部审批',
        },
        {
          path: '/workflow/ApprovedEnd',
          name: 'ApprovedEnd',
          label: '我的申请',
        }
      ],
      WorkflowMenu: [{}],
      value: '',

    }
  },
  mounted () {
    this.GetWorkflowMenu();
  },
  methods: {
    GetWorkflowMenu () {
      Workflow.GetWorkflowMenu().then(res => {
        console.log('res', res)
        for (let i = 0; i < res.Data.length; i++) {
          // debugger
          // this.WorkflowMenuTest.value = res.Data[i].Id
          // this.WorkflowMenuTest.label = res.Data[i].ApplyTitle
          this.WorkflowMenu.push({
            value: res.Data[i].Id,
            label: res.Data[i].ApplyTitle
          })


        }
      })
    },

    clickMenu (item) {
      console.log(item.label);
      this.$router.push({
        name: item.name
      })
    },

    submitForm (formName) {
      console.log(formName)
      this.$refs[formName].validate((valid) => {
        if (valid) {
          this.numberValidateForm.Account = sessionStorage.getItem('user')
          console.log(this.numberValidateForm)
          Workflow.AddWorkflow(this.numberValidateForm)
            .then(res => {
              console.log(res)
              if (res.Success) {
                this.$message({
                  type: 'success',
                  message: '添加成功!'
                });
                // this.getApplyList();
                this.$nextTick(() => {
                  this.$refs['Childmain'].getApplyList()
                })

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