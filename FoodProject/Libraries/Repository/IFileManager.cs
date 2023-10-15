namespace FoodProject.Libraries.Repository
{
    public interface IFileManager
    {
        string Upload(IFormFile file, string _PATH);
        void Delete(string fileName);
        bool FileExists(string fileName, string _PATH);
    }
}
