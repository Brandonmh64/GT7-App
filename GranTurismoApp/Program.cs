namespace GranTurismoApp
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            //var x = typeof(System.Data.Entity.SqlServer.SqlProviderServices);

            ApplicationConfiguration.Initialize();
            Application.Run(new Main());
        }
    }
}