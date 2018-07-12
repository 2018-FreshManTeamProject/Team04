using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oikake.Util
{
    class Motion
    {
        private Range range;//範囲
        private Timer timer;//切り替え時間
        private int motionNumber;//現在のモーション番号

        //表示位置を番号で管理
        //Dictionaryを使えば登録順番を気にしなくてよい
        private Dictionary<int, Rectangle> rectangles = new Dictionary<int, Rectangle>();

        public Motion()
        {
            //何もしない
            Initialize(new Range(0, 0), new CountDownTimer());
        }

        public Motion (Range range ,Timer timer)
        {
            Initialize(range, timer);
        }

        public void Initialize(Range range,Timer timer)
        {
            this.range = range;
            this.timer = timer;

            //モーション番号は、範囲の最初の設定
            motionNumber = range.First();
        }

        public void Add(int index , Rectangle rect)
        {
            //すでに登録されていたら何もしない
            if(rectangles.ContainsKey(index))
            {
                return;
            }
            //登録
            rectangles.Add(index, rect);
        }

        private void MotionUpdate()
        {
            //モーション番号をインクリメント
            motionNumber += 1;

            //範囲外なら最初に戻す
            if(range.IsOutOfRange(motionNumber))
            {
                motionNumber = range.First();
            }
        }

        public void Update( GameTime gameTime)
        {
            //ガード説（範囲外なら何もしない
            if(range.IsOutOfRange())
            {
                return;
            }

            //時間を更新
            timer.Update(gameTime);
            //指定時間になってたらモーション更新
            if(timer.IsTime())
            {
                timer.Initialize();
                MotionUpdate();
            }
        }

        public Rectangle DrawingRange()
        {
            return rectangles[motionNumber];
        }
        
            
    }
        
}

