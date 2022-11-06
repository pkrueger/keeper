import { Profile } from "./Profile.js";

export class Vault {
  constructor(data) {
    this.id = data.id;
    this.creatorId = data.creatorId;
    this.name = data.name;
    this.description = data.description;
    this.img = data.img;
    this.isPrivate = data.isPrivate;
    this.createdAt = data.createdAt;
    this.updatedAt = data.updatedAt;
    this.creator = new Profile(data.creator);
  }
}
