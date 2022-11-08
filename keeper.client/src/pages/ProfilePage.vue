<script>
import { computed } from "@vue/reactivity";
import { onMounted, onUnmounted, reactive } from "vue";
import { useRoute } from "vue-router";
import { AppState } from "../AppState.js";
import KeepCard from "../components/cards/KeepCard.vue";
import VaultCard from "../components/cards/VaultCard.vue";
import { accountService } from "../services/AccountService.js";
import { logger } from "../utils/Logger.js";

export default {
  setup() {
    const state = reactive({
      vaultCount: computed(() => AppState.profileVaults.length),
      keepCount: computed(() => AppState.profileKeeps.length),
      profileVaults: computed(() => AppState.profileVaults),
      profileKeeps: computed(() => AppState.profileKeeps),
    });

    const route = useRoute();

    async function getProfileItems() {
      try {
        await accountService.getActiveProfile(route.params.profileId);
        await accountService.getProfileVaults(route.params.profileId);
        await accountService.getProfileKeeps(route.params.profileId);
      } catch (error) {
        logger.log("[getProfileItems]", error);
      }
    }

    async function clearProfileItems() {
      try {
        await accountService.clearProfileItems();
      } catch (error) {
        logger.log("[ClearProfileItems]", error);
      }
    }

    onMounted(() => {
      getProfileItems();
    });

    onUnmounted(() => {
      clearProfileItems();
    });

    return { state, profile: computed(() => AppState.activeProfile) };
  },
  components: { KeepCard, VaultCard },
};
</script>

<template>
  <div class="container-fluid">
    <div class="row justify-content-center">
      <div
        v-if="profile"
        class="col-md-7 d-flex flex-column align-items-center text-center"
      >
        <div class="image-wrapper">
          <img
            :src="profile?.coverImg"
            :alt="profile?.name + ' cover image'"
            class="cover-image"
          />
          <img
            :src="profile?.picture"
            :alt="profile?.name"
            class="rounded-circle profile-picture elevation-5"
          />
        </div>

        <h1 class="account-name text-dark">{{ profile?.name }}</h1>

        <div class="stats-wrapper text-dark">
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
      <div class="col-md-8">
        <div v-if="state.profileVaults.length">
          <h2 class="category-text">Vaults</h2>
          <div class="vaults-grid">
            <VaultCard
              v-for="v in state.profileVaults"
              :key="v.id"
              :vault="v"
              class="vault-card-wrapper"
            />
          </div>
        </div>
        <div v-if="state.profileKeeps.length">
          <h2 class="category-text">Keeps</h2>
          <div class="keeps-masonry">
            <KeepCard
              v-for="k in state.profileKeeps"
              :key="k.id"
              :keep="k"
              class="keep-card-comp"
            />
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<style lang="scss" scoped>
.container-fluid {
  padding: 5rem 0.75rem;
  .image-wrapper {
    position: relative;
    margin-bottom: 5rem;
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
      left: 0;
      right: 0;
      margin-inline: auto;
      transform: translateY(55%);
      outline: 0.1rem solid var(--bs-light);
    }
  }

  .account-name {
    font-size: 3rem;
    font-weight: 600;
    margin-bottom: 1rem;
    font-family: "Inter", sans-serif;
  }

  .stats-wrapper {
    font-weight: 600;
    letter-spacing: 0.05rem;
    font-size: 1.25rem;
    margin-bottom: 2rem;
    font-family: "Inter", sans-serif;
  }

  .category-text {
    font-size: 2.5rem;
    font-weight: 700;
    font-family: "Inter", sans-serif;
    margin-bottom: 1.25rem;
  }

  .vaults-grid {
    display: grid;
    grid-template-columns: repeat(4, 1fr);
    gap: 1.5rem;
    margin-bottom: 2.5rem;
  }

  .keeps-masonry {
    columns: 4;
    column-gap: 1.5rem;

    .keep-card-comp {
      margin-bottom: 1.5rem;
    }
  }

  @media (max-width: 1100px) {
    .vaults-grid {
      grid-template-columns: repeat(3, 1fr);
    }
    .keeps-masonry {
      columns: 3;
    }
  }

  @media (max-width: 850px) {
    .vaults-grid {
      grid-template-columns: repeat(2, 1fr);
      gap: 1rem;
      padding: 0 1rem;
    }
    .keeps-masonry {
      columns: 2;
      column-gap: 1rem;
      padding: 0 1rem;

      .keep-card-comp {
        margin-bottom: 1rem;
      }
    }
  }
}
</style>
