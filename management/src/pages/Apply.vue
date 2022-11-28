<template>
  <div>
    <h2>Apply</h2>
    <div>
      <el-steps :active="3">
        <el-step title="员工申请" description="申请原因"></el-step>
        <el-step title="项目经理审批" description="备注："></el-step>
        <el-step title="老板审批" description="备注"></el-step>
      </el-steps>
    </div>
  </div>
</template>

<script>
export default {
  name: 'Apply'
}
</script>

<style>
</style>