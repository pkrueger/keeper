<script>
import { Modal } from "bootstrap";
import { ref } from "vue";
import { vaultsService } from "../../services/VaultsService.js";
import { logger } from "../../utils/Logger.js";

export default {
  setup() {
    const editable = ref({ isPrivate: false, description: "Not implemented" });

    async function createVault() {
      try {
        await vaultsService.createVault(editable.value);
        Modal.getOrCreateInstance("#createVaultModal").hide();
        editable.value = { isPrivate: false, description: "Not implemented" };
      } catch (error) {
        logger.log("[CreateVault]", error);
      }
    }

    return { editable, createVault };
  },
};
</script>

<template>
  <div
    class="modal fade"
    id="createVaultModal"
    tabindex="-1"
    aria-labelledby="createVaultModalLabel"
    aria-hidden="true"
  >
    <div class="modal-dialog">
      <div class="modal-content bg-grey">
        <div class="modal-header border-0">
          <h3 class="modal-title text-dark opacity" id="createVaultModalLabel">
            Add your vault
          </h3>
          <button
            type="button"
            class="btn-close text-dark opacity"
            data-bs-dismiss="modal"
            title="Close"
            aria-label="Close"
          ></button>
        </div>
        <div class="modal-body">
          <form @submit.prevent="createVault()" class="vault-form">
            <div class="form-floating mb-3">
              <input
                type="text"
                minlength="1"
                maxlength="20"
                class="form-control text-dark bg-grey"
                id="vaultName"
                placeholder="Name..."
                required
                v-model="editable.name"
              />
              <label for="vaultName" class="text-dark opacity">Name...</label>
            </div>

            <div class="form-floating mb-3">
              <input
                type="url"
                minlength="10"
                maxlength="255"
                class="form-control text-dark bg-grey"
                id="vaultImageUrl"
                placeholder="Cover Image Url..."
                required
                v-model="editable.img"
              />
              <label for="vaultImageUrl" class="text-dark opacity"
                >Cover Image Url...</label
              >
            </div>

            <div class="private-text text-dark opacity no-select">
              Private Vaults can only be seen by you
            </div>
            <div class="d-flex justify-content-end">
              <div class="form-check">
                <input
                  class="form-check-input"
                  type="checkbox"
                  value=""
                  id="makePrivate"
                  v-model="editable.isPrivate"
                />
                <label
                  class="form-check-label text-dark no-select"
                  for="makePrivate"
                >
                  Make Vault Private?
                </label>
              </div>
            </div>

            <button type="submit" class="btn btn-dark mt-3 text-light">
              Create Vault
            </button>
          </form>
        </div>
      </div>
    </div>
  </div>
</template>

<style lang="scss" scoped>
.opacity {
  opacity: 0.75;
}

.modal-content {
  padding: 0 1rem;
}
.form-control {
  border: none;
  border-bottom: 0.1rem solid #2d3436;
  border-radius: 0;
}
.form-control:focus {
  border-color: #2d3436;
  box-shadow: none;
}

.form-check-input:focus {
  box-shadow: none;
}

.form-control:valid {
  border: none;
  border-bottom: 0.1rem solid #2d3436;
}

.form-control:invalid {
  border: none;
  border-bottom: 0.1rem solid #2d3436;
}

.vault-form {
  text-align: end;
}

.private-text {
  font-size: 0.7rem;
}
</style>
