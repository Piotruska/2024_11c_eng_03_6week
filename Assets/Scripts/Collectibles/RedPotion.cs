namespace Collectibles
{
    public class RedPotion : ICollectible
    {
        protected override void Collect()
        {
            Destroy(gameObject);
        }
    }
}
