using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Oikake.Util;
using Oikake.Scene;
using Oikake.Device;

namespace Oikake.Actor
{
    class BurstEffect:Character
    {
        private Timer timer;
        private int counter;
        private readonly int pictureNum = 7;

        public BurstEffect(Vector2 position,IGameMediator mediator )
            :base("pipo-btleffect",mediator)
        {
            this.position = position;
        }
public override void Initialize()
        {
            counter = 0;
            isDeadFlag = false;
            timer = new CountDownTimer(0.05f);
        }

        public override void Update(GameTime gameTime)
        {
            //タイマー更新
            timer.Update(gameTime);
            //指定時間か？
            if(timer.IsTime())
            {
                //次の画像へ
                counter += 1;
                //時間初期化
                timer.Initialize();
                //アニメーション画像の最後までたどり着いてたら死亡へ
                if(counter >= pictureNum)
                {
                    isDeadFlag = true;
                }
            }
        }

        public override void Shutdown()
        {
            
        }

        public override void Hit(Character other)
        {
        }
        public override void Draw(Renderer renderer)
        {
            renderer.DrawTexture(name, position, new Rectangle(counter * 120, 0, 120, 120));
        }
    }
}
