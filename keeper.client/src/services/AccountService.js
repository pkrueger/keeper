import { AppState } from "../AppState";
import { Account } from "../models/Account.js";
import { Keep } from "../models/Keep.js";
import { Profile } from "../models/Profile.js";
import { Vault } from "../models/Vault.js";
import { logger } from "../utils/Logger";
import { api } from "./AxiosService";

class AccountService {
  async getAccount() {
    try {
      const res = await api.get("/account");
      AppState.account = new Account(res.data);
    } catch (err) {
      logger.error("HAVE YOU STARTED YOUR SERVER YET???", err);
    }
  }

  async getMyKeeps() {
    const res = await api.get("/account/keeps");
    AppState.myKeeps = res.data.map((k) => new Keep(k));
  }

  async getMyVaults() {
    const res = await api.get("/account/vaults");
    AppState.myVaults = res.data.map((v) => new Vault(v));
  }

  async getActiveProfile(profileId) {
    const res = await api.get(`api/profiles/${profileId}`);
    AppState.activeProfile = new Profile(res.data);
  }

  async getProfileVaults(profileId) {
    const res = await api.get(`api/profiles/${profileId}/vaults`);
    AppState.profileVaults = res.data.map((v) => new Vault(v));
  }

  async getProfileKeeps(profileId) {
    const res = await api.get(`api/profiles/${profileId}/keeps`);
    AppState.profileKeeps = res.data.map((k) => new Keep(k));
  }

  async clearProfileItems() {
    AppState.activeProfile = null;
    AppState.profileKeeps = [];
    AppState.profileVaults = [];
  }
}

export const accountService = new AccountService();
