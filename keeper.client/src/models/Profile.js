export class Profile {
  constructor(data) {
    this.id = data.id;
    this.name = data.name;
    this.coverImg = data.coverImg;
    this.picture = data.picture;
    this.createdAt = data.createdAt;
    this.updatedAt = data.updatedAt;
  }
}
