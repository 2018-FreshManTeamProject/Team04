using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Oikake.Device;
using Oikake.Util;
using Oikake.Scene;

namespace Oikake.Actor 
{
    class Floor : Character
    {
        public override void Hit(Character other)
        {
          if(other is Enemy)
            {
                mediator.AddScore(10);
            }
        }

        public override void Initialize()
        {
            position = new Vector2(0, 600);
        }

        public override void Shutdown()
        {
            
        }

        public override void Update(GameTime gameTime)
        {
            
        }

        public Floor(IGameMediator mediator):base("oikake_enemy_4anime", mediator)
        {

        }

        public override bool IsCollision(Character other)
        {
            return other.GetPosition().Y > position.Y;
        }
    }
}
