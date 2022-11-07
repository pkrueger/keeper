<script>
import { computed } from "@vue/reactivity";
import { reactive } from "vue";
import { AppState } from "../../AppState.js";
import { Keep } from "../../models/Keep.js";

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
    return { state };
  },
};
</script>

<template>
  <div class="keep-card" :style="`background-image: url(${keep.img})`">
    <img :src="keep.img" :alt="keep.name" class="keep-card-image" />
    <div class="card-content">
      <!-- TODO No wrap and make ellipses appear on overflow -->
      <!-- TODO Make transparent glass -->
      <div class="text-container">
        <div class="card-text text-light">{{ keep.name }}</div>
      </div>
      <!-- TODO No image on mobile size -->
      <img
        v-if="keep.creatorId != state.account.id"
        :src="keep.creator.picture"
        :alt="keep.creator.name"
        :title="keep.creator.name"
        class="rounded-circle elevation-3"
      />
    </div>
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
    .text-container {
      background-color: rgba(45, 52, 54, 0.55);
      backdrop-filter: blur(7px);
      border-radius: 0.25rem;
      padding-inline: 0.5rem;
      overflow: hidden;
      .card-text {
        font-size: 1.5rem;
        text-shadow: 0.15rem 0.15rem 0 rgba(0, 0, 0, 0.5);
        letter-spacing: 0.05rem;
        overflow: hidden;
        white-space: nowrap;
        text-overflow: ellipsis;
      }
    }

    img {
      margin-inline-start: auto;
      width: 20%;
      max-width: 4rem;
    }
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
