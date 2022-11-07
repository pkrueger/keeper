<script>
import { computed } from "@vue/reactivity";
import { Modal } from "bootstrap";
import { reactive } from "vue";
import { AppState } from "../AppState.js";
import { logger } from "../utils/Logger.js";
import Pop from "../utils/Pop.js";
import Login from "./Login.vue";
export default {
  setup() {
    const state = reactive({
      account: computed(() => AppState.account),
    });
    function openForm(modalId) {
      try {
        if (!state.account.id) {
          Pop.toast("Login to create keeps and vaults.");
          return;
        }
        Modal.getOrCreateInstance(modalId).show();
      } catch (error) {
        logger.log("[OpenKeepForm]", error);
      }
    }
    return { openForm };
  },
  components: { Login },
};
</script>

<template>
  <nav class="navbar bg-light px-5">
    <div class="nav-buttons">
      <button class="btn btn-outline bg-secondary text-dark elevation-2">
        Home
      </button>
      <button
        @click="openForm('#createKeepModal')"
        class="btn btn-outline bg-secondary text-dark elevation-2"
      >
        Create Keep
      </button>
      <button
        @click="openForm('#createVaultModal')"
        class="btn btn-outline bg-secondary text-dark elevation-2"
      >
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
  width: fit-content;
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
