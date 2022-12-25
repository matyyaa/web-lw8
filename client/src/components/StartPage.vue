<template>
  <div class="try_again">
    <div class="text">      
      <router-link v-if="!isLoggedIn" to="/login">
        <div class="wrap">
          <button class="button">Sign in</button>
        </div>
      </router-link>
      <router-link v-if="!isLoggedIn" to="/register">
        <div class="wrap">
          <button class="button">Sign up</button>
        </div>
      </router-link>
      <div v-if="isLoggedIn">
        <h1 style="color: #00FFCB;">Welcome to your account!</h1>
        <Info/>
      </div>
      <div v-else>
        <h3 style="margin-top: 50px">Already have account? Sign in</h3>
        <p>or sign up to create one</p>
      </div>
      <router-view></router-view>
    </div>
  </div>
</template>

<script>
import {mapGetters} from "vuex";
import Info from "@/components/Info";

export default {
  name: "Welcome",
  components: {Info: Info},
  computed: {
    ...mapGetters(["isLoggedIn"])
  }
}
</script>
<style scoped>
  .try_again{
    height: 100vh;
    display: flex;
    align-items: center;
    justify-content: center;
    background-color: rgb(45, 45, 45);
  }

  .text{
    margin-left: auto;
    margin-right: auto;
    margin-top: auto;
    margin-bottom: auto;
    padding-top: 10px;
    padding-bottom: 10px;
    width: 500px;
    background-color: rgb(45, 45, 45);
  }
</style>