<script>
import { computed } from "@vue/reactivity";
import { onMounted, reactive } from "vue";
import { useRoute } from "vue-router";
import { AppState } from "../AppState.js";
import KeepCard from "../components/cards/KeepCard.vue";
import { keepsService } from "../services/KeepsService.js";
import { vaultsService } from "../services/VaultsService.js";
import { logger } from "../utils/Logger.js";

export default {
  setup() {
    const state = reactive({
      keptKeeps: computed(() => AppState.keptKeeps),
      vault: computed(() => AppState.activeVault),
    });
    const route = useRoute();

    async function getKeptKeeps() {
      try {
        await keepsService.getKeptKeeps(route.params.vaultId);
      } catch (error) {
        logger.log("[GetKeptKeeps]", error);
      }
    }

    async function getVaultById() {
      try {
        await vaultsService.getVaultById(route.params.vaultId);
      } catch (error) {
        logger.log("[GetVaultById]", error);
      }
    }

    onMounted(() => {
      getVaultById();
      getKeptKeeps();
    });
    return { state };
  },
  components: { KeepCard },
};
</script>

<template>
  <div class="container-fluid">
    <div class="row justify-content-center">
      <div
        class="col-md-5 d-flex flex-column justify-content-center align-items-center"
      >
        <div
          class="image-wrapper text-center text-light"
          :style="`background-image: url(${state.vault?.img})`"
        >
          <div class="gradient"></div>
          <div class="vault-text text-center mb-2">
            <h1 class="vault-name">{{ state.vault?.name }}</h1>
            <h3 class="vault-creator">by {{ state.vault?.creator.name }}</h3>
          </div>
        </div>
        <div class="keep-count bg-primary">
          {{ state.keptKeeps.length }} Keep<span
            v-if="state.keptKeeps.length != 1"
            >s</span
          >
        </div>
      </div>
    </div>
    <div class="row justify-content-center">
      <div class="col-md-8">
        <div class="masonry">
          <KeepCard
            v-for="k in state.keptKeeps"
            :key="k.id"
            :keep="k"
            class="keep-card-comp"
          />
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
    margin-bottom: 1.5rem;
    width: 100%;
    padding-top: 40%;
    border-radius: 0.5rem;
    background-size: cover;
    background-position: center;
    box-shadow: rgba(45, 52, 54, 0.6) 0 0.3rem 1rem 0;

    .vault-text {
      position: absolute;
      width: 100%;
      bottom: 0;
      text-shadow: 0.15rem 0.15rem 0 rgba(0, 0, 0, 0.5);
      font-family: "Quando", serif;

      .vault-name {
        font-weight: 600;
        letter-spacing: 0.4rem;
        text-transform: uppercase;
      }

      .vault-creator {
        font-size: 1rem;
        letter-spacing: 0.1rem;
      }
    }
    .gradient {
      position: absolute;
      border-radius: 0.5rem;
      width: 100%;
      height: 100%;
      bottom: 0;
      right: 0;
      top: 0;
      left: 0;
      background: rgb(45, 52, 54);
      background: linear-gradient(
        0deg,
        rgba(45, 52, 54, 0.85) 0%,
        rgba(45, 52, 54, 0) 60%
      );
    }
  }

  .keep-count {
    font-weight: 600;
    font-size: 1.25rem;
    padding: 0.25rem 0.6rem;
    border-radius: 1rem;
    margin-bottom: 1.5rem;
  }
}

.masonry {
  columns: 4;
  column-gap: 1.5rem;

  .keep-card-comp {
    margin-bottom: 1.5rem;
  }
}

@media (max-width: 1100px) {
  .masonry {
    columns: 3;
  }
}

@media (max-width: 850px) {
  .masonry {
    columns: 2;
    column-gap: 1rem;

    .keep-card-comp {
      margin-bottom: 1rem;
    }
  }
}
</style>
