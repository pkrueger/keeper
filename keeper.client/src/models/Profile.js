export class Profile {
  constructor(data) {
    this.id = data.id;
    this.name = data.name;
    this.coverImg =
      data.coverImg ||
      "https://user-images.githubusercontent.com/43302778/106805462-7a908400-6645-11eb-958f-cd72b74a17b3.jpg";
    this.picture = data.picture;
    this.createdAt = data.createdAt;
    this.updatedAt = data.updatedAt;
  }
}
