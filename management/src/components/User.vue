<template>
  <div>
    <h1>新建vue</h1>
    <p>{{ a }}</p>
    <button @click="GetData">获取数据</button>
    <input v-bind="abc" type="text" />
    <button @click="GetData2">传递数据</button>
    {{ abc }}
  </div>
</template>

<script>
import axios from "axios";
import fileDownload from "js-file-download";
export default {
  name: "User",
  data () {
    return {
      a: "",
      abc: "",
    };
  },
  components: {
    axios,
  },

  methods: {
    GetData () {
      axios
        .get("https://localhost:44327/api/excel1/TableUser")
        .then((res) => {
          console.log(res.data);
          this.a = res.data;
          fileDownload(res.data, "12345.xlsx");
        })
        .catch((err) => {
          console.log(arr);
        });
    },
    // GetData1() {
    //   axios({
    //     method: "post",
    //     url: "https://localhost:44327/api/excel1/table",
    //     data: {},
    //     responseType: "arraybuffer",
    //   }).then((res) => {
    //     let blob = new Blob([res.data]);
    //     const url = window.URL.createObjectURL(blob);
    //     this.src = url;
    //   });
    // },
    GetData2 (record) {
      this.downLoad(
        "https://localhost:7014/api/Excel/api/Excel/excel",
        "IV售后三包导入模板.xls"
      );
    },
    downLoad (url, name) {
      return new Promise((resolve, reject) => {
        const xhr = new XMLHttpRequest();
        xhr.open("GET", `${url}`, true);
        xhr.responseType = "blob";
        // var vmThis = this
        xhr.onload = () => {
          if (xhr.status === 200) {
            const file = xhr.response;
            const fileName = name;
            if ("msSaveOrOpenBlob" in navigator) {
              window.navigator.msSaveOrOpenBlob(file, fileName);
            } else {
              const fileUrl = window.URL.createObjectURL(file);
              const a = document.createElement("a");
              a.download = fileName;
              a.href = fileUrl;
              document.getElementsByTagName("body")[0].appendChild(a);
              a.click();
              a.parentNode.removeChild(a);
              window.URL.revokeObjectURL(fileUrl);
            }
            return resolve();
          }
        };
        xhr.send();
      });
    },
  },
  // GetData2() {
  //   axios({
  //     method: "get",
  //     url: "https://localhost:44327/api/excel1/table",
  //     data: this.abc,
  //     responseType: "blob",
  //     headers: {
  //       "Content-Type": "application/json", //请求数据格式
  //     },
  //   })
  //     .then((res) => {
  //       congsole.log(res);
  //       let link = document.createElement("a");
  //       let blob = new Blob([res.data], { type: "application/vnd.ms-excel" });
  //       link.style.display = "none";
  //       link.href = URL.createObjectURL(blob);
  //       link.download = res.headers["content-disposition"]; //下载后文件名
  //       let name = link.download.split("=")[1];
  //       link.download = name; //下载的文件名
  //       document.body.appendChild(link);
  //       link.click();
  //       document.body.removeChild(link);
  //     })
  //     .catch((error) => {
  //       this.$message.error("下载失败");
  //     });
  // },
  // fileDownload(res.data, this.filename);
  // GetData1() {
  //   axios
  //     .post("https://localhost:44327/api/Get1", this.abc)
  //     .then((data) => {
  //       console.log(data);
  //       this.a = data.data;
  //     })
  //     .catch((err) => {
  //       console.log(arr);
  //     });
  // },
};
</script>

<style>
</style>