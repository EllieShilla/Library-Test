export class ImageFileToBase64 {

  ImageToBase64(file: File):Promise<any>{
    return new Promise((resolve, reject) => {
      const reader = new FileReader();
      reader.readAsDataURL(file);

      reader.onload = () => {
        let strBuff = reader.result;
        let cover = strBuff?.toString();
        resolve(cover);
      };
    });
  }
}
