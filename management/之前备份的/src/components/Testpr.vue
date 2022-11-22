<template>
  <div>
    <h1>Testpr</h1>
    <button @click="add">事件</button>
  </div>
</template>
   
<script>
export default {
  name: "Testpr",
  data() {
    return {};
  },
  computed: {},
  methods: {
    add() {
      const promise = new Promise((resolve, reject) => {
        // resolve("fulfilled"); // 状态由 pending => fulfilled
        reject("onRejected"); // 状态由 pending => fulfilled
      });
      console.log(promise);
      promise.then(
        (result) => {
          // onFulfilled
          // console.log(result); // 'fulfilled'
        },
        (reason) => {
          console.log(reason); // 'fulfilled'
        }
      );
      const p1 = new Promise((resolve, reject) => {
        resolve({
          name: 1,
        });
      });

      const p2 = new Promise((resolve, reject) => {
        resolve(2);
      });

      const p3 = new Promise((resolve, reject) => {
        resolve(3);
      });

      Promise.all([p1, p2, p3]).then(
        (data) => {
          console.log(data); // [1, 2, 3] 结果顺序和promise实例数组顺序是一致的
        },
        (err) => {
          console.log(err);
        }
      );
    },
  },
};
</script>
  
<style>
</style>