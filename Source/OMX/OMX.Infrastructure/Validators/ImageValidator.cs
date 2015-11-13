namespace OMX.Infrastructure.Validators
{
    using System.Collections.Generic;
    using System.Web;

    public static class ImageValidator
    {
        public const string ContentTypeImageJpg = "image/jpeg";

        public const string ContentTypeImagePng = "image/png";

        public static bool IsValidImage(IEnumerable<HttpPostedFileBase> files)
        {
            foreach (var file in files)
            {
                if (file.ContentLength > 1 * 1024 * 1024)
                {
                    return false;
                }

                if (file.ContentType != ContentTypeImageJpg
                    && file.ContentType != ContentTypeImagePng)
                {
                    return false;
                }
            }

            return true;
        }
    }
}