namespace Collectibles
{
    public class BluePotion : ICollectible
    {
        protected override void Collect()
        {
            Destroy(gameObject);
        }
    }
}
