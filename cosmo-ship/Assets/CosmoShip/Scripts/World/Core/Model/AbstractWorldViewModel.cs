namespace CosmoShip.Scripts.World.Core.Model
{
    public interface IViewModel
    {
        void SetManager(IWorldManager manager);
    }

    public abstract class AbstractWorldViewModel : IViewModel
    {
        protected IWorldManager WorldManager { get; private set; }

        public void SetManager(IWorldManager manager)
        {
            WorldManager = manager;
        }
    }
}
