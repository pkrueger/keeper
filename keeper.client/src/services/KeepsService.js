import { AppState } from "../AppState.js";
import { Keep } from "../models/Keep.js";
import { logger } from "../utils/Logger.js";
import { api } from "./AxiosService.js";

class KeepsService {
  async getAllKeeps() {
    const res = await api.get("api/keeps");
    AppState.keeps = res.data.map((k) => new Keep(k));
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
}

export const keepsService = new KeepsService();
