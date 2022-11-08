<script>
import { computed, reactive } from "vue";
import { AppState } from "../AppState";
export default {
  setup() {
    const state = reactive({
      vaultCount: computed(() => AppState.myVaults.length),
      keepCount: computed(() => AppState.myKeeps.length),
    });
    return {
      state,
      account: computed(() => AppState.account),
    };
  },
};
</script>

<template>
  <div class="container-fluid">
    <div class="row justify-content-center">
      <div class="col-md-7 d-flex flex-column align-items-center text-center">
        <div class="image-wrapper">
          <img
            :src="account.coverImg"
            :alt="account.name + ' cover image'"
            class="cover-image"
          />
          <img
            :src="account.picture"
            :alt="account.name"
            class="rounded-circle profile-picture"
          />
        </div>

        <div>{{ account.name }}</div>

        <div class="stats-wrapper">
          <span class="vault-count"
            >{{ state.vaultCount }} Vault<span v-if="state.vaultCount != 1"
              >s</span
            ></span
          >
          |
          <span class="keep-count"
            >{{ state.keepCount }} Keep<span v-if="state.keepCount != 1"
              >s</span
            ></span
          >
        </div>
      </div>
    </div>
  </div>
</template>

<style lang="scss" scoped>
.container-fluid {
  padding: 5rem 0;
  .image-wrapper {
    position: relative;
    .cover-image {
      object-fit: contain;
      object-position: center;
      width: 100%;
      height: 100%;
      max-height: 20rem;
      border-radius: 1rem;
    }

    .profile-picture {
      position: absolute;
      bottom: 0;
      left: 50%;
    }
  }
}
</style>
