using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;//Vector2用
using Oikake.Actor;
using Oikake.Def;
using Oikake.Device;
using Oikake.Scene;


namespace Oikake.Actor
{
    
    class Enemy : Character
    {
        private Random rnd;
        
        public Enemy(IGameMediator mediator)
            :base("ame",mediator)
        {
           
        }
        public override void Initialize()
        {
            //位置を（1００，1００）に変更
            var gameDevice = GameDevice.Instance();
            rnd = gameDevice.GetRandom();
            position = new Vector2(
                rnd.Next(Screen.Width - 64), -32);
        }

        public override void Update(GameTime gameTime)
        {
            position.Y += 5;
            //AIが考えて決定した位置に
           
        }
           
       

        public override void Shutdown()
        {

        }

        public override void Hit(Character other)
        {
            //得点処理
            //int score = 0;
            
          
            //mediator.AddScore(score);

            //次のAIを決定
            
            mediator.AddActor(new Enemy(mediator));

            //死亡処理
            isDeadFlag = true;
            
        }
    }
}
