import { AppState } from "../AppState.js";
import { Keep } from "../models/Keep.js";
import { logger } from "../utils/Logger.js";
import { api } from "./AxiosService.js";

class KeepsService {
  async getAllKeeps() {
    const res = await api.get("api/keeps");
    AppState.keeps = res.data.map((k) => new Keep(k));
  }
}

export const keepsService = new KeepsService();
