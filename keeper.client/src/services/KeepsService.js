import { AppState } from "../AppState.js";
import { Keep } from "../models/Keep.js";
import { KeptKeep } from "../models/KeptKeep.js";
import { logger } from "../utils/Logger.js";
import { api } from "./AxiosService.js";

class KeepsService {
  async getAllKeeps() {
    const res = await api.get("api/keeps");
    AppState.keeps = res.data.map((k) => new Keep(k));
  }

  async deleteKeep(keepId) {
    await api.delete(`api/keeps/${keepId}`);
    AppState.keeps = AppState.keeps.filter((k) => k.id != keepId);
    AppState.myKeeps = AppState.myKeeps.filter((k) => k.id != keepId);
    AppState.keptKeeps = AppState.keptKeeps.filter((k) => k.id != keepId);
  }

  async setActiveKeep(keepId) {
    const res = await api.get(`api/keeps/${keepId}`);
    AppState.activeKeep = new Keep(res.data);
  }

  async createKeep(keepData) {
    const res = await api.post("api/keeps", keepData);
    logger.log(res.data);
    AppState.keeps.push(new Keep(res.data));
    AppState.myKeeps.push(new Keep(res.data));
  }

  clearActiveKeep() {
    AppState.activeKeep = null;
    AppState.activeKeptKeep = null;
  }
}

export const keepsService = new KeepsService();
