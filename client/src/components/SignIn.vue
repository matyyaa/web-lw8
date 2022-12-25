<template>
  <div class="first">
    <div class="login">
      <h1 style="letter-spacing: 5px;">SIGNING IN</h1>
      <form @submit.prevent="login">
        <input v-model="email" placeholder="email" type="email" style="width: 180px; height: 20px; border-radius: 5px; border: solid white 2px;"/>
        <br />
        <br />
        <input v-model="password" placeholder="password" type="password" style="width: 180px; height: 20px; border-radius: 5px; border: solid white 2px;"/>
        <br />
        <br />
        <div class="wrap">
          <button class="button" type="submit">Submit</button>
        </div>
      </form>
    </div>
  </div>
</template>

<script>
import {mapMutations} from "vuex";

export default {
  name: "Login.vue",
  data: () => {
    return {
      email: "",
      password: "",
    };
  },
  methods: {
    ...mapMutations(["setUser", "setToken"]),
    async login(e) {
      e.preventDefault()
      const response = await fetch("https://localhost:7220/api/user/login", {
        method: "POST",
        headers: {
          "Content-Type": "application/json",
        },
        body: JSON.stringify({
          email: this.email,
          password: this.password,
        }),
      })
      const { token } = await response.json();
      this.setToken(token)
      this.$router.push('/')
    }
  }
}
</script>

<style scoped>
  .first{
    height: 100vh;
    display: flex;
    align-items: center;
    justify-content: center;
    background-color: rgb(45, 45, 45);
  }
  .login{
    margin-left: auto;
    margin-right: auto;
    margin-top: auto;
    margin-bottom: auto;
    padding-bottom: 10px;
    width: 500px;
    border-radius: 20px;
    background: linear-gradient(rgba(0, 251, 255, 0.5), rgba(69, 69, 69, 0));
  }
</style>