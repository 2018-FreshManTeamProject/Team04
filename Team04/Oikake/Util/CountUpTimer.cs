using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace Oikake.Util
{
    class CountUpTimer :Timer
    {
        public CountUpTimer()
           : base()
        {
            //自分の初期化メソッドで初期化
            Initialize();
        }

        public CountUpTimer(float second)
            : base(second)
        {
            Initialize();
        }

        public override void Initialize()
        {
            currentTime = 0.0f;
        }

        public override bool IsTime()
        {
            //0以下になっていたら設定した時間を超えたのでtrueを返す
            return currentTime >= limitTime;
        }

        public override float Rate()
        {
            return currentTime / limitTime;
        }

        public override void Update(GameTime gameTime)
        {
            //現在の時間を減らす。ただし最小値0.0
            currentTime = Math.Min(currentTime + 1f,limitTime);
        }
    }
}
