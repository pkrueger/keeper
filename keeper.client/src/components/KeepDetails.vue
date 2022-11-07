<script>
import { computed, ref } from "@vue/reactivity";
import { reactive } from "vue";
import { AppState } from "../AppState.js";

export default {
  setup() {
    const state = reactive({
      keep: computed(() => AppState.activeKeep),
      myVaults: computed(() => AppState.myVaults),
    });
    const editable = ref({});
    return { state, editable };
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
    <div class="modal-dialog modal-xl" v-if="state.keep">
      <div class="modal-content">
        <div class="modal-body">
          <div class="image-container">
            <img :src="state.keep?.img" :alt="state.keep?.name" class="image" />
          </div>
          <div class="keep-content">
            <div class="trackers opacity">
              <span title="Views" class="no-select"
                ><i class="fa-solid fa-eye text-dark"></i>
                {{ state.keep?.views }}</span
              >
              |
              <span title="Keeps" class="no-select"
                ><i class="fa-solid fa-k text-dark"></i>
                {{ state.keep?.kept }}</span
              >
              <!-- TODO Make a delete button for keep outline danger -->
            </div>
            <div class="keep-info text-dark">
              <h1>{{ state.keep?.name }}</h1>
              <div class="opacity">{{ state.keep?.description }}</div>
            </div>
            <div class="logged-in">
              <div v-if="state.myVaults.length" class="save-and-remove me-auto">
                <form @submit="addKeepToVault()">
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
                  <button type="submit" class="btn btn-info">Save</button>
                </form>
              </div>
              <!-- TODO Make a router link to profile page -->
              <div class="account-info">
                <img
                  :src="state.keep?.creator.picture"
                  :alt="state.keep?.creator.name"
                  :title="state.keep?.creator.name"
                  class="rounded-circle elevation-3 img-fluid"
                />
                <div class="text-dark opacity">
                  {{ state.keep?.creator.name }}
                </div>
              </div>
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

      .account-info {
        display: flex;
        gap: 0.5rem;
        align-items: center;
        font-weight: 500;

        img {
          width: 3rem;
        }
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
