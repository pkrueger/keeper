import { AppState } from "../AppState.js";
import { Vault } from "../models/Vault.js";
import { logger } from "../utils/Logger.js";
import { api } from "./AxiosService.js";

class VaultsService {
  async createVault(vaultData) {
    const res = await api.post("api/vaults", vaultData);
    logger.log(res.data);
    AppState.myVaults.push(new Vault(res.data));
  }
}

export const vaultsService = new VaultsService();
