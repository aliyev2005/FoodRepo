namespace FoodProject.Libraries.Repository
{
    public interface IFileManager
    {
        string Upload(IFormFile file, string _PATH);
        void Delete(string fileName, string _PATH);
        bool FileExists(string fileName, string _PATH);
    }
}
