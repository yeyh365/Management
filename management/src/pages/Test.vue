<template>
  <div>
    <h2>test</h2>
    <div id="root">
      <!-- 遍历数组 -->
      <h2>人员列表（遍历数组）</h2>
      <ul>
        <li v-for="(p, index) of persons" :key="index">
          {{ p.name }}-{{ p.age }}
        </li>
      </ul>

      <!-- 遍历对象 -->
      <h2>汽车信息（遍历对象）</h2>
      <ul>
        <li v-for="(value, q) of car" :key="q">{{ q }}-{{ value }}</li>
      </ul>

      <!-- 遍历字符串 -->
      <h2>测试遍历字符串（用得少）</h2>
      <ul>
        <li v-for="(char, index) of str" :key="index">
          {{ char }}-{{ index }}
        </li>
      </ul>

      <!-- 遍历指定次数 -->
      <h2>测试遍历指定次数（用得少）</h2>
      <ul>
        <li v-for="(number, index) of 5" :key="index">
          {{ index }}-{{ number }}
        </li>
      </ul>
    </div>
  </div>
</template>

<script>
export default {
  name: 'Test',
  data () {
    return {
      persons: [
        { id: '001', name: '张三', age: 18 },
        { id: '002', name: '李四', age: 19 },
        { id: '003', name: '王五', age: 20 }
      ],
      car: {
        name: '奥迪A8',
        price: '70万',
        color: '黑色'
      },
      str: 'hello'
    }
  },
  created () {
    // 真实 DOM 操作
    console.time();
    for (let i = 0; i <= 10; i++) {
      console.log(i)
    }
    console.timeEnd();
  },
}
</script>

<style>
</style>
