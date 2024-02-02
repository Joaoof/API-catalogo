namespace ApiCatalogo.Services
{
    public class MyService : IMyService
    {
        public string Salutation(string name)
        {
            return $"Welcome, {name} \n\n {DateTime.UtcNow}";
        }
    }
}
