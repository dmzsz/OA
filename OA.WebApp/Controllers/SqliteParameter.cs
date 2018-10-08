namespace OA.WebApp.Controllers
{
    internal class SqliteParameter
    {
        private string v;
        private object id;

        public SqliteParameter(string v, object id)
        {
            this.v = v;
            this.id = id;
        }
    }
}