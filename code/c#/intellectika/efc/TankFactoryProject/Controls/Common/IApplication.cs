namespace MainEntityProject.Controls.Common
{
    public interface IApplication
    {
        public Task Start();

        public IServiceProvider GetProvider();

    }
}
