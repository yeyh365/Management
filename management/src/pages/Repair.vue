<template>
  <div id="Repair">
    <el-header class="header" style="text-ag;height:50px;" v-if="Type == '1'">
      <div style="width: 20%; float: left" @click="go()">
        <!-- <i
          class="el-icon-arrow-left"
          style="color: #0079fe; font-size: 18px"
        ></i> -->
        <div style="color: #0079fe; font-size: 15px">取消</div>
      </div>
      <div style="width: 59%; float: left">
        <h1>报修申请</h1>
      </div>
      <div
        style="width: 20%; float: right"
        @click="ApplySubmit()"
        v-if="ProcInstType == 1"
      >
        <div style="color: #0079fe; font-size: 15px">提交</div>
      </div>
      <div
        style="width: 20%; float: right"
        @click="ReSubmit()"
        v-if="ProcInstType == 2"
      >
        <div style="color: #0079fe; font-size: 15px">重新提交</div>
      </div>
    </el-header>
    <el-main
      class="me"
      style="position: absolute; width: 100%; padding-bottom: 100px"
      v-if="Type == '1'"
    >
      <el-row style="padding-left: 5px">
        <el-col :span="24" style="text-align: left; margin: 10px auto">
          <span style="font-size: 15px; font-weight: 900"
            ><i class="el-icon-collection-tag"></i> 报修信息</span
          >
        </el-col>
      </el-row>
      <el-card>
        <el-row>
          <el-col
            :span="8"
            style="text-align: left; height: 32px; line-height: 32px"
          >
            医院代码
          </el-col>
          <el-col
            :span="10"
            style="text-align: left; height: 32px; line-height: 32px"
          >
            {{ ApplyInfo.HospitalCode }}
          </el-col>
          <el-col
            :span="24"
            style="text-align: right; height: 32px; line-height: 32px"
          >
            <el-col
              :span="8"
              style="text-align: left; height: 32px; line-height: 32px"
            >
              医院名称
            </el-col>
            <el-col
              :span="16"
              style="text-align: left; font-size: 14px; color: black"
            >
              {{ ApplyInfo.HospitalName }}
            </el-col>
          </el-col>
        </el-row>
        <el-row>
          <el-col
            :span="8"
            style="text-align: left; height: 32px; line-height: 32px"
          >
            提货地址<span style="color: red; font-size: 14px">*</span>
          </el-col>
          <el-col :span="16" style="text-align: right;margin-top: 6px;">
            <el-input
              type="textarea"
              :rows="2"
              placeholder="挥发罐的具体提货地址"
              size="small"
              v-model="ApplyInfo.PickupAddress"
            ></el-input>
          </el-col>
          <el-col
            :span="8"
            style="text-align: left; height: 32px; line-height: 32px"
          >
            联系方式<span style="color: red; font-size: 14px">*</span>
          </el-col>
          <el-col
            :span="16"
            style="
                text-align: left;
                height: 32px;
                line-height: 32px;
                font-size: 14px;
                color: black;
              "
          >
            <el-input
              placeholder=""
              v-model="ApplyInfo.PickupContact"
              size="small"
              clearable
            >
            </el-input>
          </el-col>
        </el-row>
        <el-row style="margin-top: 12px">
          <el-col
            :span="8"
            style="text-align: left; height: 32px; line-height: 32px"
          >
            报修数量
            <span style="color: red; font-size: 14px">*</span>
          </el-col>
          <el-col
            :span="10"
            style="
              text-align: left;
              height: 32px;
              line-height: 32px;
              color: black;
            "
          >
            {{ ApplyInfo.RepairQuantity }}
          </el-col>
        </el-row>
      </el-card>

      <el-row style="padding-left: 5px">
        <el-col :span="24" style="text-align: left; margin: 10px auto">
          <span style="font-size: 15px; font-weight: 900"
            ><i class="el-icon-collection-tag"></i> 挥发罐报修明细</span
          >
        </el-col>
      </el-row>
      <el-card
        v-for="(Asset, index) in ApplyInfo.AssetReturnList"
        :key="index"
        style="margin-top: 10px"
      >
        <div slot="header" class="clearfix" style="text-align: left">
          <span style="font-size: 15px; font-weight: 900"
            ><i class="el-icon-collection-tag"></i> 挥发罐报修明细{{
              index + 1
            }}</span
          >
          <el-button
            style="
              float: right;
              padding: 3px 0;
              color: red;
              padding-right: 10px;
            "
            type="text"
            v-on:click="DeleteReturn(index)"
            >删除</el-button
          >
        </div>
        <el-row>
          <el-col
            :span="8"
            style="text-align: left; height: 32px; line-height: 32px"
          >
            挥发罐序列号
          </el-col>
          <el-col
            :span="16"
            style="
              text-align: right;
              height: 32px;
              line-height: 32px;
              color: black;
            "
          >
            {{ Asset.VaporizerNo }}
          </el-col>
        </el-row>
        <el-row>
          <el-col
            :span="8"
            style="text-align: left; height: 32px; line-height: 32px"
          >
            适用产品
          </el-col>
          <el-col
            :span="16"
            style="
              text-align: right;
              height: 32px;
              line-height: 32px;
              color: black;
            "
          >
            {{ Asset.ApplicableProductName }}
          </el-col>
        </el-row>
        <el-row>
          <el-col
            :span="8"
            style="text-align: left; height: 32px; line-height: 32px"
          >
            挥发罐型号
          </el-col>
          <el-col
            :span="16"
            style="
              text-align: right;
              height: 32px;
              line-height: 32px;
              color: black;
            "
          >
            {{ Asset.VaporizerModelName }}
          </el-col>
        </el-row>
        <el-row>
          <el-col
            :span="8"
            style="text-align: left; height: 32px; line-height: 32px"
          >
            挥发罐品牌
          </el-col>
          <el-col
            :span="16"
            style="
              text-align: right;
              height: 32px;
              line-height: 32px;
              color: black;
            "
          >
            {{ Asset.VaporizerBrandName }}
          </el-col>
        </el-row>
        <el-row>
          <el-col
            :span="8"
            style="text-align: left; height: 32px; line-height: 32px"
          >
            挥发罐接口
          </el-col>
          <el-col
            :span="16"
            style="
              text-align: right;
              height: 32px;
              line-height: 32px;
              color: black;
            "
          >
            {{ Asset.VaporizerInterfaceName }}
          </el-col>
        </el-row>
        <el-row>
          <el-col
            :span="8"
            style="text-align: left; height: 32px; line-height: 32px"
          >
            数量
          </el-col>
          <el-col
            :span="16"
            style="
              text-align: right;
              height: 32px;
              line-height: 32px;
              color: black;
            "
          >
            {{ Asset.Quantity }}
          </el-col>
        </el-row>
        <el-row>
          <el-col
            :span="8"
            style="text-align: left; height: 32px; line-height: 32px"
          >
            报修图片或视频
            <span style="color: red; font-size: 14px">*</span>
          </el-col>
          <el-col :span="16" style="text-align: left">
            <el-upload
              class="avatar-uploader"
              :action="uploadUrl + '?index=' + index"
              :headers="{ enctype: 'multipart/form-data' }"
              :show-file-list="false"
              :on-success="handleAvatarSuccess"
              :before-upload="beforeAvatarUpload"
            >
              <img
                v-if="Asset.RepairImage"
                :src="Asset.RepairImage"
                class="avatar"
                style="width: 178px; height: 178px"
              />
              <i v-else class="el-icon-plus avatar-uploader-icon"></i>
            </el-upload>
          </el-col>
        </el-row>
        <el-row>
          <el-col
            :span="8"
            style="text-align: left; height: 32px; line-height: 32px"
          >
            报修原因<span style="color: red; font-size: 14px">*</span>
          </el-col>
          <el-col
            :span="16"
            style="text-align: right; margin-top: 3px; color: black"
          >
            <el-input
              type="textarea"
              :rows="3"
              placeholder="填写报修备注信息"
              v-model="Asset.Remark"
              size="small"
            >
            </el-input>
          </el-col>
        </el-row>
        <el-row>
          <el-col
            :span="8"
            style="text-align: left; height: 32px; line-height: 32px"
          >
            是否需要新罐<span style="color: red">*</span>
          </el-col>
          <el-col
            :span="16"
            style="text-align: right; height: 32px; line-height: 32px"
          >
            <el-switch v-model="Asset.IsNeedNew"></el-switch>
          </el-col>
        </el-row>
      </el-card>
      <div style="width: 100%; height: 40px">
        <span
          style="
            color: #0079fe;
            font-size: 14px;
            width: 100px;
            height: 40px;
            float: right;
            margin-right: 20px;
            margin-top: 10px;
            text-align: right;
          "
          v-on:click="SMAdd()"
          >扫码添加</span
        >
        <span
          style="
            color: #0079fe;
            font-size: 14px;
            width: 100px;
            height: 40px;
            float: right;
            margin-right: 20px;
            margin-top: 10px;
          "
          v-on:click="ManuallyAdd()"
          >手工添加</span
        >
      </div>
    </el-main>

    <el-header class="header" style="text-ag;height:50px;" v-if="Type == '2'">
      <div style="width: 5%; float: left" @click="Close()">
        <i
          class="el-icon-arrow-left"
          style="color: #0079fe; font-size: 18px"
        ></i>
      </div>
      <div style="width: 90%; float: right">
        <h3>选择序列号</h3>
      </div>
    </el-header>
    <el-main
      class="me"
      style="position: absolute; width: 100%; padding-bottom: 100px"
      v-if="Type == '2'"
    >
      <el-row>
        <el-col :span="24" style="text-align: left; margin: 10px auto">
          <el-input placeholder="序列号" v-model="Key"> </el-input>
        </el-col>
      </el-row>
      <el-card
        class="box-card"
        v-for="(item, index) in Assetlist"
        :key="index"
        style="text-align: left; margin-top: 10px"
        @click.native="SelectVaporizerNo(item)"
      >
        <el-row style="margin-top: 5px">
          <el-col :span="24" style="text-align: left">
            <h3>{{ item.HospitalName }}</h3>
          </el-col>
        </el-row>
        <el-row style="margin-top: 15px">
          <el-col :span="8" style="text-align: left">
            {{ item.VaporizerNo }}
          </el-col>
          <el-col :span="8" style="text-align: left">
            {{ item.ApplicableProductName }}
          </el-col>
          <el-col :span="8" style="text-align: left" v-if="item.ServiceYears">
            {{ item.ServiceYears }}年
          </el-col>
        </el-row>
        <el-row style="margin-top: 15px">
          <el-col :span="8" style="text-align: left">
            <font v-if="item.IsNeedCheck == 1" style="color: rgb(67, 184, 94)"
              ><strong>已盘点</strong></font
            >
            <font v-else style="color: red"><strong>未盘点</strong></font>
          </el-col>
          <el-col :span="8" style="text-align: left">
            {{ item.CurrentStatusName }}
          </el-col>
          <el-col :span="8" style="text-align: left">
            {{ item.SalesUserName }}
          </el-col>
        </el-row>
      </el-card>
    </el-main>
  </div>
</template>
<script src='https://res.wx.qq.com/open/js/jweixin-1.2.0.js'></script>
<script type='text/ecmascript-6'>
import AssetServer from '@/api/services/AssetServer'
import ProcessServer from '@/api/services/ProcessServer'
import enums from '@/api/common/enums'
import utils from '@/api/common/utils.js'
import scancodeServer from '@/api/services/scancodeServer'
export default {
  name: 'Repair',
  data () {
    return {
      Type: 1,
      Key: '',
      ProcInstID: this.$route.params.id,
      ProcInstType: this.$route.params.type, // 1.新增，2，重新提交
      VaporizerId: this.$route.params.VaporizerId,
      uploadUrl: window.g.base_url + '/AssetRepairImgUpload',
      ApplyInfo: {
        HospitalCode: '',
        HospitalName: '',
        PickupAddress: '',
        PickupContact: '',
        RepairQuantity: 0,
        RepairImage: '',
        Remark: '',
        AssetReturnList: []
      },
      formAsset: {
        Key: '',
        SalesUserId: JSON.parse(sessionStorage.getItem('userInfo')).UserID,
        HospitalCode: ''
      },
      Assetlist: [],
      Visible: false,
      loading: '',
    }
  },
  created () {
    this.ScanCode()
    if (this.ProcInstType === 2) {
      // 重新发起
      this.GetRepairApplyInfo()
    }
    else {
      this.GetHospitalInfo()
    }
  },
  watch: {
    Key: {
      deep: true,
      handler (NewVal, OldVal) {
        this.formAsset.Key = this.Key
        this.AssetChange()
      },
    },
  },
  methods: {
    Close () {
      this.Type = '1'
    },
    go () {
      this.$router.go(-1)
    },
    GetQuantity () {
      this.ApplyInfo.RepairQuantity = this.ApplyInfo.AssetReturnList.length
    },
    GetRepairApplyInfo () {
      // 获取流程详情
      ProcessServer.GetRepairApplyInfo(this.ProcInstID)
        .then(res => {
          if (res.Code === enums.responseCode.Success.value) {
            this.ApplyInfo = res.Data
            this.ApplyInfo.ApplicationDate = utils.formatDateNew(this.ApplyInfo.ApplicationDate)
            this.ApplyInfo.ExpectedDeliveryDate = utils.formatDateNew(this.ApplyInfo.ExpectedDeliveryDate)
            if (this.ApplyInfo.AssetList.length > 0) {
              this.IsNeedNew = true
            }
          } else {
            this.$message.error(res.Message)
          }
        })
    },
    GetHospitalInfo () {
      AssetServer.GetAsset(this.VaporizerId)
        .then(res => {
          if (res.Code === enums.responseCode.Success.value) {
            this.ApplyInfo.HospitalCode = res.Data.HospitalCode
            this.formAsset.HospitalCode = res.Data.HospitalCode
            this.ApplyInfo.HospitalName = res.Data.HospitalName
            let info = res.Data
            info.Quantity = 1
            info.OperationType = 1
            info.RepairImage = ''
            this.ApplyInfo.AssetReturnList.push(info)
            this.GetQuantity()
          } else {
            this.$message.error(res.Message)
          }
        })
    },
    // 条件搜索
    AssetChange () {
      this.GetAssetList()
    },
    // 获取挥发罐列表
    GetAssetList () {
      let formAsset = {
        Key: this.Key,
        HospitalCode: this.ApplyInfo.HospitalCode,
        CurrentStatusName:'使用'
      }
      ProcessServer.GetAssetList(formAsset).then((res) => {
        if (res.Code === enums.responseCode.Success.value) {
          this.Assetlist = res.Data
        } else {
          this.$message.error(res.Message)
        }
      })
    },
    // 选择序列号
    SelectVaporizerNo (item) {
      let info = JSON.parse(JSON.stringify(item))
      this.Type = '1'
      info.Quantity = 1
      info.OperationType = 1
      info.IsNeedNew = false
      info.RepairImage = ''
      this.ApplyInfo.AssetReturnList.push(item)
      this.GetQuantity()
    },
    ApplySubmit () {
       if(!this.ApplyInfo.PickupAddress){
        this.$message.error('请输入提货地址')
        return false
      } else if (!this.ApplyInfo.PickupContact) {
        this.$message.error('请输入联系方式')
        return false
      }
      let ErrowPicList = $.grep(this.ApplyInfo.AssetReturnList, function (da) {
        return da.RepairImage == null || da.RepairImage === ''
      })
      let ErrowRemarkList = $.grep(this.ApplyInfo.AssetReturnList, function (da) {
        return da.Remark == null || da.Remark === ''
      })
      if (ErrowPicList.length > 0) {
        this.$message.error('请上传报修图片!')
      } else if (ErrowRemarkList.length > 0) {
        this.$message.error('请填写报修原因!')
      } else if (!this.ApplyInfo.HospitalCode) {
        this.$message.error('没有医院信息')
      } else if (this.ApplyInfo.AssetReturnList.length <= 0) {
        this.$message.error('请至少添加一条明细')
      } else {
        this.$confirm('请确定是否提交?', '提示', {
          confirmButtonText: '确定',
          cancelButtonText: '取消',
          type: 'warning',
        }).then(() => {
          let loading = this.$loading({
            lock: true,
            text: '提交中。。。',
            spinner: 'el-icon-loading',
            background: 'rgba(0, 0, 0, 0.7)'
          })
          ProcessServer.RepairApplySubmit(this.ApplyInfo)
            .then((res) => {
              if (res.Code === enums.responseCode.Success.value) {
                this.$message.success(res.Message)
                this.$router.push({ name: 'application' })
              } else {
                this.$message.error(res.Message)
              }
              loading.close()
            })
            .catch(() => { })
        })
      }
    },
    ReSubmit () {
       if(!this.ApplyInfo.PickupAddress){
        this.$message.error('请输入提货地址')
        return false
      } else if (!this.ApplyInfo.PickupContact) {
        this.$message.error('请输入联系方式')
        return false
      }else if (!this.ApplyInfo.HospitalCode) {
        this.$message.error('没有医院信息')
      } else if (this.ApplyInfo.AssetReturnList.length <= 0) {
        this.$message.error('请至少添加一条明细')
      } else {
        this.$confirm('是否确定重新提交?', '提示', {
          confirmButtonText: '确定',
          cancelButtonText: '取消',
          type: 'warning',
        }).then(() => {
          let loading = this.$loading({
            lock: true,
            text: '提交中。。。',
            spinner: 'el-icon-loading',
            background: 'rgba(0, 0, 0, 0.7)'
          })
          ProcessServer.RepairResubmit(this.ApplyInfo)
            .then((res) => {
              if (res.Code === enums.responseCode.Success.value) {
                this.$message.success(res.Message)
                this.$router.push({ name: 'application' })
              } else {
                this.$message.error(res.Message)
              }
              loading.close()
            })
            .catch(() => { })
        })
      }
    },
    handleAvatarSuccess (res, file) {
      this.loading.close()
      // 图片 绑定
      this.ApplyInfo.AssetReturnList[res.Data.index].RepairImage = res.Data.FilePath
      this.$forceUpdate()
    },
    beforeAvatarUpload (file) {
      const isLt2M = file.size / 1024 / 1024 < 30
      if (!isLt2M) {
        this.$message.error('上传头像图片大小不能超过 30MB!')
      }
      if (isLt2M) {
        this.loading = this.$loading({
          lock: true,
          text: '上传中。。。',
          spinner: 'el-icon-loading',
          background: 'rgba(0, 0, 0, 0.7)',
        })
      }
      return isLt2M
    },
    handleRemove (file, fileList) {
      // console.log(file, fileList)
    },
    ManuallyAdd () {
      this.Type = 2
      this.GetAssetList()
    },
    // 加载扫码
    ScanCode () {
      var that = this
      let url = window.location.href.split('#')[0]
      scancodeServer.GetSign(url).then((res) => {
        if (res.Code === enums.responseCode.Success.value) {
          wx.config({
            beta: true,
            debug: false,
            appId: res.Data.appId,
            timestamp: res.Data.timestamp,
            nonceStr: res.Data.nonceStr, // 必填，生成签名的随机串
            signature: res.Data.signature, // 必填，签名，见附录1
            jsApiList: ['scanQRCode'], // 必填，需要使用的JS接口列表，所有JS接口列表见附录2
          })
        } else {
          this.$message.error(res.Message)
        }
      })
    },
    SMAdd () {
      var that = this
      wx.scanQRCode({
        desc: 'scanQRCode desc',
        needResult: 1, // 默认为0，扫描结果由企业微信处理，1则直接返回扫描结果，
        scanType: ['qrCode', 'barCode'], // 可以指定扫二维码还是一维码，默认二者都有
        success: function (res) {
          var str = res.resultStr.split('index/')[1]
          AssetServer.GetAsset(str).then((res) => {
            if (res.Code === enums.responseCode.Success.value) {
              let info = res.Data
              info.Quantity = 1
              info.IsNeedNew = false
              info.RepairImage = ''
              that.InterfaceErrorInfo.AssetReturnList.push(info)
            } else {
              this.$message.error(res.Message)
            }
          })
        },
        error: function (res) {
          if (res.errMsg.indexOf('function_not_exist') > 0) {
            alert('版本过低请升级')
          }
        },
      })
    },
    DeleteReturn (index) {
      this.$confirm('是否确定删除?', '提示', {
        confirmButtonText: '确定',
        cancelButtonText: '取消',
        type: 'warning',
      }).then(() => {
        this.ApplyInfo.AssetReturnList.splice(index, 1)
        this.GetQuantity()
        this.$message.success('已删除')
      })
    }
  },
}
</script>
<style>
#Repair .el-card {
  color: #999999;
}
#Repair .avatar-uploader-icon {
  font-size: 28px;
  color: #8c939d;
  width: 178px;
  height: 178px;
  line-height: 178px;
  text-align: center;
}
#Repair .el-input-number--small {
  width: 100%;
}
#Repair .el-input__inner {
  border-top-width: 0px;
  border-left-width: 0px;
  border-right-width: 0px;
  border-bottom-width: 1px;
  border-radius: 0px;
  padding: 5px;
  padding-left: 0px;
}
#Repair .el-textarea__inner {
  border-top-width: 0px;
  border-left-width: 0px;
  border-right-width: 0px;
  border-bottom-width: 0px;
  padding: 5px 5px;
}
</style>
