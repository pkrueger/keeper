import { AppState } from "../AppState.js";
import { KeptKeep } from "../models/KeptKeep.js";
import Pop from "../utils/Pop.js";
import { api } from "./AxiosService.js";

class KeptKeepsService {
  async addKeepToVault(keepId, vaultId) {
    await api.post(`api/vaultKeeps`, { keepId, vaultId });
  }

  async getKeptKeeps(vaultId) {
    const res = await api.get(`api/vaults/${vaultId}/keeps`);
    AppState.keptKeeps = res.data.map((k) => new KeptKeep(k));
  }

  async setActiveKeptKeep(keep) {
    const res = await api.get(
      `api/vaults/${keep.vaultId}/keeps/${keep.vaultKeepId}`
    );
    AppState.activeKeptKeep = new KeptKeep(res.data);
  }

  async deleteKeptKeep(vaultKeepId) {
    await api.delete(`api/vaultKeeps/${vaultKeepId}`);
    AppState.keptKeeps = AppState.keptKeeps.filter(
      (k) => k.vaultKeepId != vaultKeepId
    );
  }
}

export const keptKeepsService = new KeptKeepsService();
