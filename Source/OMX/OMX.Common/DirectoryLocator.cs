namespace OMX.Common
{
    public class DirectoryLocator
    {
        public static string GetCurrentDirectory(string path)
        {
            return System.Web.HttpContext.Current.Server.MapPath(path);
        }
    }
}