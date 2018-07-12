using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Oikake.Scene;
using Oikake.Def;
using Oikake.Device;

namespace Oikake.Actor
{
    class BoundEnemy : Character
    {
        private Sound sound;
        private Vector2 velocity;//移動量
        private static Random rnd = new Random();
        
        public BoundEnemy(IGameMediator mediator)
            :base("black",mediator)
        {
            velocity = Vector2.Zero;
            var gameDevice = GameDevice.Instance();
            sound = gameDevice.GetSound();
        }
        public override void Initialize()
        {
            position = new Vector2(
                rnd.Next(Screen.Width - 64),
                rnd.Next(Screen.Height - 64));
            //最初は左移動
            velocity = new Vector2(-10f, 0);
        }
        public override void Shutdown()
        {
        }

        public override void Update(GameTime gameTime)
        {
            //左壁で反射
            if(position .X <0 )
            {
                //移動量を反転
                velocity = -velocity;
            }
            //右壁で反射
            if(position .X>=Screen.Width - 64)
            {
                velocity = -velocity;
            }

            //移動処理（座標に移動量を足す）
            position += velocity;
        }

        public override void Hit(Character other)
        {
            
            isDeadFlag = true;
            mediator.AddScore(200);
            
            //死んだら敵を増やす
            mediator.AddActor(new BoundEnemy(mediator));
            mediator.AddActor(new BoundEnemy(mediator));
            mediator.AddActor(new BurstEffect(position, mediator));
            sound.PlaySE("gameplayse");
        }
    }
}
