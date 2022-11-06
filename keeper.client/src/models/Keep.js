import { Profile } from "./Profile.js";

export class Keep {
  constructor(data) {
    this.id = data.id;
    this.creatorId = data.creatorId;
    this.name = data.name;
    this.description = data.description;
    this.img = data.img;
    this.views = data.views;
    this.createdAt = data.createdAt;
    this.updatedAt = data.updatedAt;
    this.creator = new Profile(data.creator);
    this.kept = data.kept;
  }
}
