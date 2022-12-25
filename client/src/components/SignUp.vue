<template>
  <div class="registration">
    <div class="form">
      <h1 style="letter-spacing: 5px;">SIGNING UP</h1>
    <form @submit.prevent="register">
      <input v-model="firstName" placeholder="First Name" style="width: 180px; height: 20px; border-radius: 5px; border: solid white 2px;"/>
      <br />
      <br />
      <input v-model="lastName" placeholder="Last Name" style="width: 180px; height: 20px; border-radius: 5px; border: solid white 2px;"/>
      <br />
      <br />
      <input v-model="email" placeholder="Email" type="email" style="width: 180px; height: 20px; border-radius: 5px; border: solid white 2px;"/>
      <br />
      <br />
      <input v-model="group" placeholder="Group" style="width: 180px; height: 20px; border-radius: 5px; border: solid white 2px;"/>
      <br />
      <br />
      <input v-model="phone" placeholder="Phone number" style="width: 180px; height: 20px; border-radius: 5px; border: solid white 2px;"/>
      <br />
      <br />
      <input v-model="variant" placeholder="Variant" style="width: 180px; height: 20px; border-radius: 5px; border: solid white 2px;"/>
      <br />
      <br />
      <input v-model="password" placeholder="Password" type="password" style="width: 180px; height: 20px; border-radius: 5px; border: solid white 2px;"/>
      <br />
      <br />
      <input type="radio" id="user" value="User" v-model="role" style="margin-left: 15px; margin-right: 5px;" />
      <label for="user">User</label>
      <input type="radio" id="admin" value="Admin" v-model="role" style="margin-left: 15px; margin-right: 5px;" />
      <label for="admin">Admin</label>
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
export default {
  name: "Register.vue",
  data: () => {
    return {
      firstName: "",
      lastName: "",
      email: "",
      group: "",
      phone: "",
      variant: "",
      password: "",
      role: ""
    };
  },
  methods: {
    async register(e) {
      e.preventDefault()
      let response;
      switch (this.role) {
        case "User":
          response = await fetch("https://localhost:7220/api/user/register", {
            method: "POST",
            headers: {
              "Content-Type": "application/json",
            },
            body: JSON.stringify({
              firstName: this.firstName,
              lastName: this.lastName,
              email: this.email,
              group: this.group,
              phone: this.phone,
              variant: this.variant,
              password: this.password,
            }),
          })
          console.log(await response)
          break
        case "Admin":
          response = await fetch("https://localhost:7220/api/user/register-admin", {
            method: "POST",
            headers: {
              "Content-Type": "application/json",
            },
            body: JSON.stringify({
              firstName: this.firstName,
              lastName: this.lastName,
              email: this.email,
              group: this.group,
              phone: this.phone,
              variant: this.variant,
              password: this.password,
            }),
          })
          console.log(await response)
          break
        default:
          alert("Select role!")
      }
    }
  }
}
</script>

<style scoped>
  .registration{
    height: 100vh;
    display: flex;
    align-items: center;
    justify-content: center;
    background: rgba(14, 14, 14, 0.9);
  }

  .form{
    margin-left: auto;
    margin-right: auto;
    margin-top: auto;
    margin-bottom: auto;
    padding-bottom: 10px;
    width: 500px;
    /* border: 1px solid rgb(255, 255, 255); */
    border-radius: 20px;
    background: linear-gradient(rgba(0, 251, 255, 0.6), rgba(69, 69, 69, 0));
  }
</style>