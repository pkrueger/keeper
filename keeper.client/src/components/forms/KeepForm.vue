<script>
import { Modal } from "bootstrap";
import { ref } from "vue";
import { keepsService } from "../../services/KeepsService.js";
import { logger } from "../../utils/Logger.js";

export default {
  setup() {
    const editable = ref({});

    async function createKeep() {
      try {
        await keepsService.createKeep(editable.value);
        Modal.getOrCreateInstance("#createKeepModal").hide();
        editable.value = {};
      } catch (error) {
        logger.log("[CreateKeep]", error);
      }
    }

    return { editable, createKeep };
  },
};
</script>

<template>
  <div
    class="modal fade"
    id="createKeepModal"
    tabindex="-1"
    aria-labelledby="createKeepModalLabel"
    aria-hidden="true"
  >
    <div class="modal-dialog">
      <div class="modal-content bg-grey">
        <div class="modal-header border-0">
          <h3 class="modal-title text-dark opacity" id="createKeepModalLabel">
            Add your keep
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
          <form @submit.prevent="createKeep()" class="keep-form">
            <div class="form-floating mb-3">
              <input
                type="text"
                minlength="1"
                maxlength="40"
                class="form-control text-dark bg-grey"
                id="keepName"
                placeholder="Name..."
                required
                v-model="editable.name"
              />
              <label for="keepName" class="text-dark opacity">Name...</label>
            </div>

            <div class="form-floating mb-3">
              <input
                type="url"
                minlength="10"
                maxlength="255"
                class="form-control text-dark bg-grey"
                id="keepImageUrl"
                placeholder="Image Url..."
                required
                v-model="editable.img"
              />
              <label for="keepImageUrl" class="text-dark opacity"
                >Image Url...</label
              >
            </div>

            <div class="form-floating mb-3">
              <textarea
                minlength="1"
                maxlength="255"
                class="form-control text-dark bg-grey"
                id="keepDescription"
                placeholder="Description..."
                style="height: 25vh"
                required
                v-model="editable.description"
              ></textarea>
              <label for="keepDescription" class="text-dark opacity"
                >Description...</label
              >
            </div>
            <button type="submit" class="btn btn-dark mt-3 text-light">
              Create
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

.form-control:valid {
  border: none;
  border-bottom: 0.1rem solid #2d3436;
}

.form-control:invalid {
  border: none;
  border-bottom: 0.1rem solid #2d3436;
}

.keep-form {
  text-align: end;
}
</style>
