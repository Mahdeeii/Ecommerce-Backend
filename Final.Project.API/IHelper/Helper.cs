namespace Final.Project.API
{
    public class Helper:IHelper
    {
        public string ImageValidation(IFormFile? image)
        {

            if (image is null)
            {
                return "Image is not found";
            }

            if (image.Length > 1000_000)
            {
                return "Image size exceeded the limit";
            }

            var allowedExtensions = new string[] { ".jpg", ".svg", ".png", ".jpeg" };

            var sentExtension = Path.GetExtension(image.FileName).ToLower();

            if (!allowedExtensions.Contains(sentExtension))
            {
                return "Image extension is not valid";
            }

            return "ok";
        }
    }
}
