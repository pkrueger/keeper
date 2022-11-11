<script>
import { computed } from "@vue/reactivity";
import { onMounted, onUnmounted, reactive } from "vue";
import { useRoute, useRouter } from "vue-router";
import { AppState } from "../AppState.js";
import KeepCard from "../components/cards/KeepCard.vue";
import { keepsService } from "../services/KeepsService.js";
import { keptKeepsService } from "../services/KeptKeepsService.js";
import { vaultsService } from "../services/VaultsService.js";
import { logger } from "../utils/Logger.js";
import Pop from "../utils/Pop.js";

export default {
  setup() {
    const state = reactive({
      keptKeeps: computed(() => AppState.keptKeeps),
      vault: computed(() => AppState.activeVault),
      account: computed(() => AppState.account),
    });
    const route = useRoute();
    const router = useRouter();

    async function getKeptKeeps() {
      try {
        await keptKeepsService.getKeptKeeps(route.params.vaultId);
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

    async function deleteVault(vaultId) {
      try {
        const decision = await Pop.confirm();
        if (!decision) {
          return;
        }
        await vaultsService.deleteVault(vaultId);
        Pop.success("Vault has been deleted.");
        router.push({
          name: "Profile",
          params: { profileId: state.vault.creatorId },
        });
      } catch (error) {
        logger.log("[DeleteVault]");
      }
    }

    function redirect() {
      try {
        if (!state.vault) {
          Pop.toast("You can't access that private vault.");
          router.push({ name: "Home" });
        }
      } catch (error) {
        logger.log("[Redirect]", error);
      }
    }

    onMounted(async () => {
      await getVaultById();
      await getKeptKeeps();
      redirect();
    });

    onUnmounted(() => {
      vaultsService.clearVariables();
    });
    return { state, deleteVault };
  },
  components: { KeepCard },
};
</script>

<template>
  <div class="container-fluid" v-if="state.vault">
    <div class="row justify-content-center">
      <div
        class="col-md-8 col-lg-5 d-flex flex-column justify-content-center align-items-center"
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
          <i
            class="fa-solid fa-lock private text-light"
            title="Private Vault"
            v-if="state.vault.isPrivate"
          ></i>
          <div
            class="dropdown"
            v-if="state.account?.id == state.vault.creatorId"
          >
            <i
              class="fa-solid fa-ellipsis menu text-dark selectable"
              title="Vault Options"
              aria-label="Vault Options"
              data-bs-toggle="dropdown"
              aria-expanded="false"
            ></i>
            <ul class="dropdown-menu bg-primary">
              <li
                class="selectable text-dark"
                @click="deleteVault(state.vault.id)"
              >
                &nbspDelete Vault
              </li>
            </ul>
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
  padding-block: 10rem;
  .image-wrapper {
    position: relative;
    margin-bottom: 1.5rem;
    width: 100%;
    padding-top: 50%;
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
    .private {
      position: absolute;
      top: 0;
      right: 0;
      font-size: 1.5rem;
      padding: 0.35rem 0.45rem;
      text-shadow: 0 0 0.21rem rgba(0, 0, 0, 0.8);
    }
    .dropdown {
      position: absolute;
      bottom: 0;
      right: 0;
      transform: translateY(125%);
      padding-inline: 2%;

      .menu {
        padding: 0 0.25rem;
        font-size: 1.5rem;
      }
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
  .container-fluid {
    padding-block: 1rem;
  }

  .masonry {
    columns: 2;
    column-gap: 1rem;

    .keep-card-comp {
      margin-bottom: 1rem;
    }
  }
}
</style>
