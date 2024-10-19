namespace Collectibles
{
    public class Key : ICollectible
    {
        protected override void Collect()
        {
            Destroy(gameObject);
        }
    }
}
