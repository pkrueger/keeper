<script>
import { Keep } from "../../models/Keep.js";

export default {
  props: {
    keep: {
      type: Keep,
      required: true,
    },
  },
  setup() {
    return {};
  },
};
</script>

<template>
  <div class="keep-card" :style="`background-image: url(${keep.img})`">
    <img :src="keep.img" :alt="keep.name" class="keep-card-image" />
    <div class="card-content">
      <!-- TODO No wrap and make ellipses appear on overflow -->
      <!-- TODO Make transparent glass -->
      <div class="text-light">{{ keep.name }}</div>
      <!-- TODO No image on mobile size -->
      <img
        :src="keep.creator.picture"
        :alt="keep.creator.name"
        :title="keep.creator.name"
        class="rounded-circle"
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

    div {
      font-size: 1.5rem;
      text-shadow: 0.25rem 0.25rem 0 rgba(0, 0, 0, 0.2);
    }

    img {
      margin-inline-start: auto;
      width: 20%;
      max-width: 4rem;
    }
  }
}
</style>
