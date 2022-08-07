namespace Library_
{
    public class ImagesToBase64
    {
        public bool SaveImage(string ImgStr, string ImgName)
        {
            string imgPath = ImgName + "." + ImgStr.Trim('.')[ImgStr.Trim('.').Length];

            byte[] imageBytes = Convert.FromBase64String(imgPath);

            File.WriteAllBytes(imgPath, imageBytes);

            byte[] imageArray = System.IO.File.ReadAllBytes(ImgStr);
            string base64ImageRepresentation = Convert.ToBase64String(imageArray);

            return true;
        }
    }
}
