<script>
import { Modal } from "bootstrap";
import { ref, watchEffect } from "vue";
import { AppState } from "../../AppState.js";
import { accountService } from "../../services/AccountService.js";
import { logger } from "../../utils/Logger.js";

export default {
  setup() {
    const editable = ref({});

    watchEffect(() => {
      editable.value = { ...AppState.account };
    });

    async function handleSubmit() {
      try {
        await accountService.updateAccount(editable.value);
        await accountService.getAccount();
        Modal.getInstance("#AccountEditModal").hide();
        editable.value = { ...AppState.account };
      } catch (error) {
        logger.log("[HandleSubmit]", error);
      }
    }
    return { editable, handleSubmit };
  },
};
</script>

<template>
  <div
    class="modal fade"
    id="AccountEditModal"
    tabindex="-1"
    aria-labelledby="AccountEditModalLabel"
    aria-hidden="true"
  >
    <div class="modal-dialog">
      <div class="modal-content bg-grey">
        <div class="modal-header border-0">
          <h3 class="modal-title text-dark opacity" id="AccountEditModalLabel">
            Edit your account
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
          <form @submit.prevent="handleSubmit()" class="text-dark">
            <div class="form-floating mb-3">
              <input
                type="text"
                class="form-control text-dark bg-grey"
                id="profileNameInput"
                placeholder="Name..."
                minlength="3"
                maxlength="50"
                required
                v-model="editable.name"
              />
              <label for="nameInput" class="form-label text-dark opacity"
                >Name...</label
              >
            </div>

            <div class="form-floating mb-3">
              <input
                type="url"
                class="form-control text-dark bg-grey"
                id="imageInput"
                placeholder="Image Url..."
                maxlength="255"
                required
                v-model="editable.picture"
              />
              <label for="imageInput" class="form-label text-dark opacity"
                >Image Url...</label
              >
            </div>

            <div class="form-floating mb-3">
              <input
                type="url"
                class="form-control text-dark bg-grey"
                id="coverImageInput"
                placeholder="url..."
                maxlength="500"
                required
                v-model="editable.coverImg"
              />
              <label for="coverImageInput" class="form-label text-dark opacity"
                >Cover Image</label
              >
            </div>

            <div class="text-end">
              <button type="submit" class="btn btn-dark mt-3 text-light">
                Save
              </button>
            </div>
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
.form-control {
  border: none;
  border-bottom: 0.1rem solid #2d3436;
  border-radius: 0;
}
.form-control:focus {
  border-color: #2d3436;
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
</style>
