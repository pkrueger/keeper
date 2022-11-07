<script>
import { computed } from "vue";
import { AppState } from "../AppState";
import { AuthService } from "../services/AuthService";
export default {
  setup() {
    return {
      user: computed(() => AppState.user),
      account: computed(() => AppState.account),
      async login() {
        AuthService.loginWithPopup();
      },
      async logout() {
        AuthService.logout({ returnTo: window.location.origin });
      },
    };
  },
};
</script>

<template>
  <span class="navbar-text">
    <button
      class="btn selectable text-dark text-uppercase my-2 my-lg-0"
      @click="login"
      v-if="!user.isAuthenticated"
    >
      Login
    </button>
    <div v-else>
      <div class="dropdown dropstart my-2 my-lg-0">
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
        <div
          class="dropdown-menu dropdown-menu-lg-left p-0"
          aria-labelledby="authDropdown"
        >
          <div class="list-group">
            <router-link :to="{ name: 'Account' }">
              <div class="list-group-item dropdown-item list-group-item-action">
                Manage Account
              </div>
            </router-link>
            <div
              class="list-group-item dropdown-item list-group-item-action text-danger selectable"
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
    height: 4rem;
    width: 4rem;
    border-radius: 50%;
    // box-shadow: rgba(45, 52, 54, 0.6) 0 0.3rem 1rem 0;
  }
}
</style>
