import { AppState } from "../AppState.js";
import { Vault } from "../models/Vault.js";
import { logger } from "../utils/Logger.js";
import { api } from "./AxiosService.js";

class VaultsService {
  async createVault(vaultData) {
    const res = await api.post("api/vaults", vaultData);
    AppState.myVaults.push(new Vault(res.data));
  }

  async getVaultById(vaultId) {
    const res = await api.get(`api/vaults/${vaultId}`);
    AppState.activeVault = new Vault(res.data);
  }

  async deleteVault(vaultId) {
    await api.delete(`api/vaults/${vaultId}`);
  }

  clearVariables() {
    AppState.activeVault = null;
    AppState.keptKeeps = [];
  }
}

export const vaultsService = new VaultsService();
