using Sandbox;

namespace ssl.Player
{
    public class Clothes : ModelEntity
    {
        public Clothes()
        {
        }

        public Clothes(string clothesModel)
        {
            EnableShadowInFirstPerson = true;
            EnableHideInFirstPerson = true;
            AddCollisionLayer(CollisionLayer.Debris);
            SetModel(clothesModel);
        }
    }
}