<template>
  <div>
    <h1>当前求和为：{{ sum }}</h1>
    <h3>当前求和放大10倍为：{{ bigSum }}</h3>
    <h3>我在{{ school }}</h3>
    <h4>person里面的人数为{{ personList.length }}</h4>
    <select v-model.number="n">
      <option value="1">1</option>
      <option value="2">2</option>
      <option value="3">3</option>
    </select>
    <button @click="increment(n)">+</button>
    <button @click="decrement(n)">-</button>
    <button @click="incrementOdd(n)">奇数加</button>
    <button @click="incrementWait(n)">等一会再加</button>
  </div>
</template>

<script>
import { mapState, mapGetters, mapActions, mapMutations } from "vuex";

export default {
  name: "Count",
  data () {
    return {
      n: 1,
    };
  },
  computed: {
    // sum () {
    //   return this.$store.state.sum
    // },
    // ...mapState('conutAbout', ['sum', 'school']),
    ...mapState('countAbout', ['sum', 'school']),
    // ...mapState('personAbout', ['personList']),
    ...mapState('personAbout', ['personList']),
    // ...mapGetters('countAbout', ['bigSum']),
    ...mapGetters('countAbout', ['bigSum'])
  },
  methods: {
    // increment () {
    //   console.log(this);
    //   this.$store.dispatch('jia', this.n);
    // },
    // decrement () {
    //   // this.sum -= this.n;
    //   console.log(this);
    //   this.$store.dispatch('jian', this.n);
    // },
    ...mapActions('countAbout', { increment: 'jia', decrement: 'jian' }),
    ...mapActions('countAbout', { incrementOdd: 'jiaodd', incrementWait: 'jiaWait' }),

    // incrementOdd () {
    //   // if (this.sum % 2 == 1) {
    //   //   this.sum += this.n;
    //   // }
    //   this.$store.dispatch('jiaodd', this.n);
    // },
    // incrementWait () {
    //   // setTimeout(() => {
    //   //   this.sum += this.n;
    //   // }, 500);
    //   this.$store.dispatch('jiaWait', this.n);
    // },
  },
  mounter () {
    console.log(this.$store)
  }
};
</script>

<style>
</style>