<script>
import { Vault } from "../../models/Vault.js";

export default {
  props: {
    vault: {
      type: Vault,
      required: true,
    },
  },
  setup() {
    return {};
  },
};
</script>

<template>
  <router-link :to="{ name: 'Vault', params: { vaultId: vault.id } }">
    <div class="vault-card" :style="`background-image: url(${vault.img})`">
      <div class="gradient d-flex align-items-end">
        <div class="vault-name text-light">{{ vault.name }}</div>
      </div>
      <i
        class="fa-solid fa-lock private text-light"
        title="Private Vault"
        v-if="vault.isPrivate"
      ></i>
    </div>
  </router-link>
</template>

<style lang="scss" scoped>
.vault-card {
  background-size: cover;
  background-position: center;
  width: 100%;
  position: relative;
  padding-top: 67.18%;
  border-radius: 0.5rem;
  box-shadow: rgba(45, 52, 54, 0.6) 0 0.3rem 1rem 0;

  .gradient {
    position: absolute;
    top: 0;
    right: 0;
    bottom: 0;
    left: 0;
    border-radius: 0.5rem;
    width: 100%;
    // padding-top: 67.18%;
    background: rgb(45, 52, 54);
    background: linear-gradient(
      0deg,
      rgb(45, 52, 54) 0%,
      rgba(45, 52, 54, 0) 60%
    );

    .vault-name {
      font-size: 1.5rem;
      letter-spacing: 0.5rem;
      padding: 0.5rem;
      text-transform: uppercase;
      overflow: hidden;
      white-space: nowrap;
      text-overflow: ellipsis;
      text-shadow: 0.15rem 0.15rem 0 rgba(0, 0, 0, 0.5);
      font-family: "Quando", serif;
    }
    @media (max-width: 850px) {
      .vault-name {
        font-size: 1rem;
      }
    }
  }

  .private {
    position: absolute;
    top: 0;
    right: 0;
    font-size: 1.25rem;
    padding: 0.35rem 0.45rem;
    text-shadow: 0 0 0.17rem rgba(0, 0, 0, 0.8);
  }
}
</style>
