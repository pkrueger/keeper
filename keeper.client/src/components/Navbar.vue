<script>
import Login from "./Login.vue";
export default {
  setup() {
    return {};
  },
  components: { Login },
};
</script>

<template>
  <nav class="navbar bg-light px-5">
    <div class="nav-buttons">
      <button class="btn btn-outline bg-secondary text-dark">Home</button>
      <button class="btn btn-outline bg-secondary text-dark">
        Create Keep
      </button>
      <button class="btn btn-outline bg-secondary text-dark">
        Create Vault
      </button>
    </div>
    <router-link class="navbar-brand" :to="{ name: 'Home' }">
      <img alt="logo" src="logo.png" class="logo" />
    </router-link>
    <!-- LOGIN COMPONENT HERE -->
    <Login />
  </nav>
</template>

<style scoped lang="scss">
@import "../assets/scss/main.scss";
.navbar {
  box-shadow: #2d343635 0 1.56rem 1.25rem -1.25rem;
  position: relative;
}

.nav-buttons {
  display: flex;
  gap: 1.5rem;
}

a:hover {
  text-decoration: none;
}

.nav-link {
  text-transform: uppercase;
}

.navbar-nav .router-link-exact-active {
  border-bottom: 2px solid var(--bs-success);
  border-bottom-left-radius: 0;
  border-bottom-right-radius: 0;
}

.navbar-brand {
  position: absolute;
  top: 50%;
  transform: translateY(-50%);
  left: 0;
  right: 0;
  margin: auto;
  text-align: center;
}

.logo {
  height: 4rem;
}

@media screen and (min-width: 768px) {
  nav {
    height: 6.4rem;
  }
}
</style>
