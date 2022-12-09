<template>
  <div class="apply">
    <h2>员工事由申请和审批</h2>
    <div class="applyForm">
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
      menu: [
        {
          path: '/Apply/ApplyFor',
          name: 'ApplyFor',
          label: '申请',
        },
        {
          path: '/Apply/Approval',
          name: 'Approval',
          label: '审批',
        }
      ],


    }
  },
  methods: {

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
          Apply.AddApply(this.numberValidateForm)
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