<script>
import { computed, ref } from "@vue/reactivity";
import { Modal } from "bootstrap";
import { onMounted, reactive } from "vue";
import { AppState } from "../AppState.js";
import { keepsService } from "../services/KeepsService.js";
import { logger } from "../utils/Logger.js";
import Pop from "../utils/Pop.js";

export default {
  setup() {
    const state = reactive({
      keep: computed(() => AppState.activeKeep || AppState.activeKeptKeep),
      myVaults: computed(() => AppState.myVaults),
      account: computed(() => AppState.account),
    });

    function dismissModal() {
      try {
        Modal.getInstance("#keepDetailsModal").hide();
        keepsService.clearActiveKeep();
      } catch (error) {
        logger.log("[DismissModal]", error);
      }
    }

    async function deleteKeptKeep(vaultKeepId) {
      try {
        const decision = await Pop.confirm();
        if (!decision) {
          return;
        }
        await keepsService.deleteKeptKeep(vaultKeepId);
        dismissModal();
      } catch (error) {
        logger.log("[DeleteKeptKeep]", error);
      }
    }

    async function addKeepToVault(keepId) {
      try {
      } catch (error) {}
    }

    onMounted(() => {
      const modal = document.getElementById("keepDetailsModal");
      modal.addEventListener("hidden.bs.modal", () =>
        keepsService.clearActiveKeep()
      );
    });

    const editable = ref({});
    return { state, editable, dismissModal, deleteKeptKeep, addKeepToVault };
  },
};
</script>

<template>
  <div
    class="modal fade"
    id="keepDetailsModal"
    tabindex="-1"
    aria-labelledby="keepDetailsModalLabel"
    aria-hidden="true"
  >
    <div class="modal-dialog modal-xl">
      <div class="modal-content">
        <!-- NOTE SCREW THIS V-IF IT RUINED EVERYTHING -->
        <div class="modal-body" v-if="state.keep">
          <div class="image-container">
            <img :src="state.keep.img" :alt="state.keep.name" class="image" />
          </div>
          <div class="keep-content">
            <div class="trackers opacity">
              <span title="Views" class=""
                ><i class="fa-solid fa-eye text-dark"></i>
                {{ state.keep.views }}</span
              >
              |
              <span title="Keeps" class=""
                ><i class="fa-solid fa-k text-dark"></i>
                {{ state.keep.kept }}</span
              >
            </div>

            <div class="keep-info text-dark">
              <h1 id="keepDetailsModalLabel">
                {{ state.keep.name }}
              </h1>
              <div class="opacity">
                {{ state.keep.description }}
              </div>
            </div>
            <div class="logged-in">
              <div
                v-if="state.myVaults.length && !state.keep.vaultKeepId"
                class="save-and-remove me-auto"
              >
                <form @submit="addKeepToVault(state.keep.id)">
                  <select
                    class="form-select"
                    aria-label="Vault select"
                    v-model="editable.vaultId"
                  >
                    <option selected>Choose a vault</option>
                    <option v-for="v in state.myVaults" :value="v.id">
                      {{ v.name }}
                    </option>
                  </select>
                  <button
                    type="submit"
                    class="btn bg-info text-light save-button"
                  >
                    save
                  </button>
                </form>
              </div>

              <div
                v-if="
                  state.keep.vaultKeepId &&
                  state.account?.id == state.keep.keeperId
                "
                class="me-auto"
              >
                <button
                  @click="deleteKeptKeep(state.keep.vaultKeepId)"
                  class="btn btn-outline text-info border-bottom"
                >
                  <i class="fa-solid fa-ban"></i> Remove
                </button>
              </div>

              <router-link
                @click="dismissModal()"
                :to="{
                  name: 'Profile',
                  params: {
                    profileId: state.keep.creatorId,
                  },
                }"
              >
                <div class="account-info">
                  <img
                    :src="state.keep.creator.picture"
                    :alt="state.keep.creator.name"
                    :title="state.keep.creator.name"
                    class="rounded-circle elevation-3 img-fluid"
                  />
                  <div class="text-dark opacity">
                    {{ state.keep.creator.name }}
                  </div>
                </div>
              </router-link>
            </div>
          </div>
          <i
            class="fa-solid fa-minus text-dark opacity selectable close"
            data-bs-dismiss="modal"
            title="Close"
            aria-label="Close"
          ></i>
        </div>
      </div>
    </div>
  </div>
</template>

<style lang="scss" scoped>
.opacity {
  opacity: 0.75;
}

.modal-body {
  display: grid;
  grid-template-columns: repeat(2, 1fr);
  position: relative;
  padding: 0;

  .image-container {
    .image {
      object-fit: cover;
      object-position: center;
      width: 100%;
      height: 90vh;
      max-height: 90vh;
      border-bottom-left-radius: 0.4rem;
      border-top-left-radius: 0.4rem;
    }
  }

  .keep-content {
    display: flex;
    flex-direction: column;
    justify-content: space-between;
    padding: 2.5rem 4rem;

    .trackers {
      display: flex;
      gap: 1rem;
      justify-content: center;
    }

    .keep-info {
      h1 {
        text-align: center;
        font-family: "Marko One", serif;
        font-size: 3rem;
        margin-bottom: 1.5rem;
      }
      div {
        font-size: 1.25rem;
      }
    }
    .logged-in {
      display: flex;
      justify-content: center;
      align-items: center;

      .account-info {
        display: flex;
        gap: 0.5rem;
        align-items: center;
        font-weight: 500;

        img {
          width: 3rem;
        }
      }

      .save-button {
        padding: 0.15rem 0.5rem;
        font-size: 1rem;
      }
    }
  }

  .close {
    position: absolute;
    top: 0;
    right: 0;
    margin: 0.3rem 0.5rem;
  }
}
</style>
