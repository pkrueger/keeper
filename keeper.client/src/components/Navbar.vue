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
  <nav class="navbar bg-light">
    <router-link class="navbar-brand" :to="{ name: 'Home' }">
      <img alt="logo" src="logo.png" class="logo" />
    </router-link>
    <div class="nav-buttons">
      <router-link :to="{ name: 'Home' }" class="home-button">
        <button
          class="btn btn-outline bg-secondary text-dark elevation-2 home-button"
        >
          Home
        </button>
      </router-link>
      <div class="dropdown big-nav">
        <button
          class="btn btn-outline bg-secondary text-dark elevation-2"
          data-bs-toggle="dropdown"
          aria-expanded="false"
        >
          Create
        </button>
        <ul class="dropdown-menu text-center text-dark bg-primary">
          <li @click="openForm('#createKeepModal')" class="selectable">Keep</li>
          <li @click="openForm('#createVaultModal')" class="selectable">
            Vault
          </li>
        </ul>
      </div>
      <div class="dropup-center dropup small-nav">
        <button
          class="btn btn-outline bg-secondary text-dark elevation-2"
          data-bs-toggle="dropdown"
          aria-expanded="false"
        >
          Create
        </button>
        <ul class="dropdown-menu text-center text-dark bg-primary">
          <li @click="openForm('#createKeepModal')" class="selectable">Keep</li>
          <li @click="openForm('#createVaultModal')" class="selectable">
            Vault
          </li>
        </ul>
      </div>
    </div>
    <!-- LOGIN COMPONENT HERE -->
    <Login />
  </nav>
</template>

<style scoped lang="scss">
@import "../assets/scss/main.scss";
.navbar {
  box-shadow: #2d343635 0 1.56rem 1.25rem -1.25rem;
  // position: relative;
  padding-inline: 3rem;
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

.small-nav {
  display: none;
}

@media screen and (min-width: 768px) {
  nav {
    height: 6.4rem;
  }
}

@media (max-width: 850px) {
  .navbar {
    padding-inline: 1rem;
    box-shadow: rgba(45, 52, 54, 0.208) 0 -1.56rem 1.25rem -1.25rem;
  }

  .home-button {
    display: none;
  }
  .navbar-brand {
    position: unset;
    top: 50%;
    transform: translateY(0%);
    left: 0;
    right: 0;
    margin: 0;
    text-align: center;
    width: fit-content;
  }
  .small-nav {
    display: block;
  }

  .big-nav {
    display: none;
  }
}
</style>
