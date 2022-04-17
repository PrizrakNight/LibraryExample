namespace LibraryExample.DataSources.EntityFramework
{
    public static class ConnectionStrings
    {
        // Намеренное вынесение строк подключения в readonly поля.
        // При работе с App.config было бы больше мороки.
        public static readonly string LocalhostSqlServer = "Server=localhost;Database=LibraryExample;Trusted_Connection=True;";
        public static readonly string LocalhostMySql = "Server=localhost;Database=LibraryExample;Uid=myUsername;Pwd=Password;";
    }
}
