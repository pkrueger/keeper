<script>
import { computed } from "vue";
import { AppState } from "./AppState";
import KeepDetails from "./components/KeepDetails.vue";
import Navbar from "./components/Navbar.vue";

export default {
  setup() {
    return {
      appState: computed(() => AppState),
    };
  },
  components: { Navbar, KeepDetails },
};
</script>

<template>
  <header>
    <Navbar />
  </header>
  <main>
    <router-view />
  </main>
  <footer class="bg-dark text-light">Made with 💖 by CodeWorks</footer>
  <KeepDetails />
</template>

<style lang="scss">
@import "./assets/scss/main.scss";

:root {
  --main-height: calc(100vh - 32px - 64px);
}

header,
main,
footer {
  font-family: "Inter", sans-serif;
}

footer {
  display: grid;
  place-content: center;
  height: 32px;
}
</style>
