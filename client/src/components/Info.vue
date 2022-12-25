<template>
  <h2 v-if="loading">Loading</h2>
  <div v-else>
    <h3 style="letter-spacing: 5px;">INFORMATION</h3>
    <!-- <label>Name:</label> -->
    <p>Name: {{user.name}}</p>
    <p>Group: {{user.group}}</p>
    <p>Variant: {{user.variant}}</p>
    <p>Phone number: {{user.phone}}</p>

  </div>
</template>

<script>
import {mapGetters} from "vuex";

export default {
  name: "Me",
  data: () => {
    return {
      user: null,
      loading: true
    }
  },
  methods: {
    ...mapGetters(["getToken"])
  },
  async created() {
    const token = 'Bearer ' + this.getToken()
    console.log(token)
    let response = await fetch("https://localhost:7220/api/User/me", {
      method: 'GET',
      headers: {
        'Authorization': token
      }
    })
    this.user = await response.json();
    console.log(this.user)
    this.loading = false
  }
}
</script>

<style scoped>

</style>