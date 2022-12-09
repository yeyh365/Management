<template>
  <div class="bigdiv">
    <h2>Upload</h2>
    <el-upload
      class="upload-demo"
      ref="upload"
      action="https://localhost:44369/api/Files/ImgUpload"
      :on-preview="handlePreview"
      :on-remove="handleRemove"
      :file-list="fileList"
      :auto-upload="false"
    >
      <el-button slot="trigger" size="small" type="primary">选取文件</el-button>
      <el-button
        style="margin-left: 10px"
        size="small"
        type="success"
        @click="submitUpload"
        >上传到服务器</el-button
      >
      <div slot="tip" class="el-upload__tip">
        只能上传jpg/png文件，且不超过500kb
      </div>
    </el-upload>
    <div style="margin-top: 30px">
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
            :action="uploadUrl"
            :headers="{ enctype: 'multipart/form-data' }"
            :show-file-list="false"
            :on-success="handleAvatarSuccess"
            :before-upload="beforeAvatarUpload"
          >
            <img
              v-if="RepairImage"
              :src="RepairImage"
              class="avatar"
              style="width: 178px; height: 178px"
            />
            <i v-else class="el-icon-plus avatar-uploader-icon"></i>
          </el-upload>
        </el-col>
      </el-row>
    </div>
  </div>
</template>

<script>
export default {
  name: 'Upload',
  data () {
    return {
      fileList: [{ name: 'food.jpeg', url: 'https://fuss10.elemecdn.com/3/63/4e7f3a15429bfda99bce42a18cdd1jpeg.jpeg?imageMogr2/thumbnail/360x360/format/webp/quality/100' }, { name: 'food2.jpeg', url: 'https://fuss10.elemecdn.com/3/63/4e7f3a15429bfda99bce42a18cdd1jpeg.jpeg?imageMogr2/thumbnail/360x360/format/webp/quality/100' }],
      uploadUrl: 'https://localhost:44369/api/Files/ImgUpload',
      RepairImage: 'https://localhost:44369/Files/Pictures/abcd.png',


    }
  },
  methods: {
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
    handleAvatarSuccess (res, file) {
      // this.loading.close()
      // // 图片 绑定
      // this.ApplyInfo.AssetReturnList[res.Data.index].RepairImage = res.Data.FilePath
      // this.$forceUpdate()
    },
    submitUpload () {
      this.$refs.upload.submit();
    },
    handleRemove (file, fileList) {
      console.log(file, fileList);
    },
    handlePreview (file) {
      console.log(file);
    }
  }
}
</script>

<style>
.bigdiv {
  width: 300px;
}
</style>