<script>
import { Modal } from "bootstrap";
import { computed } from "vue";
import { AppState } from "../AppState";
import { AuthService } from "../services/AuthService";
import { logger } from "../utils/Logger.js";
export default {
  setup() {
    function openForm(modalId) {
      try {
        Modal.getOrCreateInstance(modalId).show();
      } catch (error) {
        logger.log("[OpenKeepForm]", error);
      }
    }

    return {
      user: computed(() => AppState.user),
      account: computed(() => AppState.account),
      openForm,
      async login() {
        AuthService.loginWithPopup();
      },
      async logout() {
        AuthService.logout({ returnTo: window.location.origin });
        AppState.myKeeps = [];
        AppState.myVaults = [];
      },
    };
  },
};
</script>

<template>
  <span class="navbar-text p-0">
    <button
      class="btn selectable text-dark text-uppercase my-2 my-lg-0"
      @click="login"
      v-if="!user.isAuthenticated"
    >
      Login
    </button>
    <div v-else>
      <div class="dropstart my-2 my-lg-0">
        <div
          type="button"
          class="border-0 selectable no-select"
          data-bs-toggle="dropdown"
          aria-expanded="false"
        >
          <div v-if="account.picture || user.picture" class="picture-container">
            <img
              :src="account.picture || user.picture"
              alt="account photo"
              class="picture elevation-4"
            />
          </div>
        </div>
        <div class="dropdown-menu p-0" aria-labelledby="authDropdown">
          <div class="list-group d-flex">
            <router-link :to="{ name: 'Account' }">
              <div class="list-group-item dropdown-item list-group-item-action">
                Account Page
              </div>
            </router-link>
            <div
              @click="openForm('#AccountEditModal')"
              class="list-group-item dropdown-item list-group-item-action selectable no-select"
            >
              Edit Account
            </div>
            <div
              class="list-group-item dropdown-item list-group-item-action text-danger selectable no-select"
              @click="logout"
            >
              <i class="mdi mdi-logout"></i>
              logout
            </div>
          </div>
        </div>
      </div>
    </div>
  </span>
</template>

<style lang="scss" scoped>
.picture-container {
  border-radius: 50%;
  .picture {
    object-fit: cover;
    object-position: center;
    height: 3.65rem;
    width: 3.65rem;
    border-radius: 50%;
    // box-shadow: rgba(45, 52, 54, 0.6) 0 0.3rem 1rem 0;
  }
}
</style>
