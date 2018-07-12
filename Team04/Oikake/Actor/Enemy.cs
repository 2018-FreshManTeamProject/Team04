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
        private AI ai;
        public Enemy(IGameMediator mediator,AI ai)
            :base("black",mediator)
        {
            this.ai = ai;
        }
        public override void Initialize()
        {
            //位置を（1００，1００）に変更
            var gameDevice = GameDevice.Instance();
            rnd = gameDevice.GetRandom();
            position = new Vector2(
                rnd.Next(Screen.Width - 64),
                rnd.Next(Screen.Height - 64));
        }

        public override void Update(GameTime gameTime)
        {
            //AIが考えて決定した位置に
            position = ai.Think(this);
        }
           
       

        public override void Shutdown()
        {

        }

        public override void Hit(Character other)
        {
            //得点処理
            int score = 0;
            if(ai is BoundAI)
            {
                score = 100;
            }
            mediator.AddScore(score);

            //次のAIを決定
            AI nextAI = new BoundAI();//実体生成
            mediator.AddActor(new Enemy(mediator, nextAI));

            //死亡処理
            isDeadFlag = true;
            mediator.AddActor(new BurstEffect(position, mediator));
        }
    }
}
