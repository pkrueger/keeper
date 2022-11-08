<script>
import { computed } from "@vue/reactivity";
import { onMounted, reactive } from "vue";
import { AppState } from "../AppState.js";
import KeepCard from "../components/cards/KeepCard.vue";
import { keepsService } from "../services/KeepsService.js";
import { logger } from "../utils/Logger.js";

export default {
  setup() {
    const state = reactive({
      keeps: computed(() => AppState.keeps),
    });
    async function getAllKeeps() {
      try {
        await keepsService.getAllKeeps();
      } catch (error) {
        logger.log("[GetAllKeeps]", error);
      }
    }
    onMounted(() => {
      getAllKeeps();
    });
    return { state };
  },
  components: { KeepCard },
};
</script>

<template>
  <div class="masonry">
    <KeepCard
      v-for="k in state.keeps"
      :key="k.id"
      :keep="k"
      class="keep-card-comp"
    />
  </div>
</template>

<style scoped lang="scss">
.masonry {
  columns: 4;
  column-gap: 1.5rem;
  padding: 5rem;

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
    padding: 5rem 1rem;

    .keep-card-comp {
      margin-bottom: 1rem;
    }
  }
}
</style>
