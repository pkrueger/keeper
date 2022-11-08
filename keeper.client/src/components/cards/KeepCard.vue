<script>
import { computed } from "@vue/reactivity";
import { Modal } from "bootstrap";
import { reactive } from "vue";
import { AppState } from "../../AppState.js";
import { Keep } from "../../models/Keep.js";
import { keepsService } from "../../services/KeepsService.js";
import { logger } from "../../utils/Logger.js";
import Pop from "../../utils/Pop.js";

export default {
  props: {
    keep: {
      type: Keep,
      required: true,
    },
  },
  setup() {
    const state = reactive({
      account: computed(() => AppState.account),
    });

    async function deleteKeep(keepId) {
      try {
        const decision = await Pop.confirm();
        if (!decision) {
          return;
        }
        await keepsService.deleteKeep(keepId);
        Pop.toast("Keep has been deleted.");
      } catch (error) {
        logger.log("[DeleteKeep]", error);
      }
    }

    async function setActiveKeep(keepId) {
      try {
        await keepsService.setActiveKeep(keepId);
      } catch (error) {
        logger.log("[GetKeepById]", error);
      }
    }

    async function openKeepDetails(keepId) {
      try {
        await setActiveKeep(keepId);
        Modal.getOrCreateInstance("#keepDetailsModal").show();
      } catch (error) {
        logger.log("[OpenKeepDetails]", error);
      }
    }

    return { state, deleteKeep, openKeepDetails };
  },
};
</script>

<template>
  <div
    class="keep-card selectable"
    :style="`background-image: url(${keep.img})`"
  >
    <img
      @click="openKeepDetails(keep.id)"
      :src="keep.img"
      :alt="keep.name"
      class="keep-card-image"
    />
    <div @click="openKeepDetails(keep.id)" class="card-content">
      <div class="text-container no-select">
        <div class="card-text text-light">{{ keep.name }}</div>
      </div>
      <img
        v-if="keep.creatorId != state.account.id"
        :src="keep.creator.picture"
        :alt="keep.creator.name"
        :title="keep.creator.name"
        class="rounded-circle elevation-3"
      />
    </div>
    <i
      v-if="keep.creatorId == state.account.id"
      @click="deleteKeep(keep.id)"
      class="fa-solid fa-delete-left delete-icon text-danger selectable"
      title="Delete"
      aria-label="Delete keep"
    ></i>
  </div>
</template>

<style lang="scss" scoped>
.keep-card {
  position: relative;
  background-position: center;
  background-size: contain;
  border-radius: 0.5rem;
  box-shadow: rgba(45, 52, 54, 0.6) 0 0.3rem 1rem 0;

  .keep-card-image {
    width: 100%;
    opacity: 0;
  }

  .card-content {
    display: flex;
    align-items: center;
    position: absolute;
    bottom: 0;
    width: 100%;
    padding: 1rem;
    height: 5.2rem;
    border-radius: 0.5rem;
    background: rgb(45, 52, 54);
    background: linear-gradient(
      0deg,
      rgba(45, 52, 54, 0.85) 0%,
      rgba(45, 52, 54, 0) 100%
    );
    .text-container {
      border-radius: 0.25rem;
      padding-inline: 0.5rem;
      overflow: hidden;
      max-width: 75%;
      .card-text {
        font-size: 1.5rem;
        text-shadow: 0.15rem 0.15rem 0 rgba(0, 0, 0, 0.5);
        letter-spacing: 0.05rem;
        overflow: hidden;
        white-space: nowrap;
        text-overflow: ellipsis;
        font-family: "Marko One", serif;
      }
    }

    img {
      margin-inline-start: auto;
      width: 20%;
      max-width: 4rem;
    }
  }

  .delete-icon {
    position: absolute;
    top: 0;
    right: 0;
    margin: 0.3rem 0.5rem;
  }

  @media (max-width: 550px) {
    .card-content {
      height: 3rem;

      .text-container {
        .card-text {
          font-size: 1.1rem;
        }
      }

      img {
        display: none;
      }
    }
  }
}
</style>
