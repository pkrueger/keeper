<script>
import { computed } from "vue";
import { AppState } from "./AppState";
import AccountForm from "./components/forms/AccountForm.vue";
import KeepForm from "./components/forms/KeepForm.vue";
import VaultForm from "./components/forms/VaultForm.vue";
import KeepDetails from "./components/KeepDetails.vue";
import Navbar from "./components/Navbar.vue";

export default {
  setup() {
    return {
      appState: computed(() => AppState),
    };
  },
  components: { Navbar, KeepDetails, KeepForm, VaultForm, AccountForm },
};
</script>

<template>
  <header class="big-nav">
    <Navbar class="big-nav fixed-top" />
  </header>
  <main>
    <router-view />
  </main>
  <Navbar class="small-nav fixed-bottom" />
  <footer></footer>
  <KeepDetails />
  <KeepForm />
  <VaultForm />
  <AccountForm />
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

.small-nav {
  display: none;
}

@media (max-width: 850px) {
  .big-nav {
    display: none;
  }

  .small-nav {
    display: flex !important;
  }
}
</style>
