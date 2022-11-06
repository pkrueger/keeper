import { Keep } from "./Keep.js";

export class KeptKeep extends Keep {
  constructor(data) {
    super(data);
    this.vaultKeepId = data.vaultKeepId;
    this.keeperId = data.keeperId;
    this.vaultId = data.vaultId;
  }
}
