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
    /// <summary>
    /// キャラクタークラスを継承した乱数エネミークラス
    /// </summary>
    class RandomEnemy : Character
    {
        //乱数オブジェクトはRandomEnemyクラスで共通になるようにstatic
        private static Random rnd = new Random();
        private int changeTimer; //切り替え時間
        private Sound sound;

        ///<summary>
        ///コンストラクタ
        ///</summary>
        public RandomEnemy(IGameMediator mediator)
            :base("black",mediator)
        {
            changeTimer = 60; //60fos
            var gameDevice = GameDevice.Instance();
            sound = gameDevice.GetSound();
        }

        public override void Hit(Character other)
        {
            isDeadFlag = true;
            mediator.AddScore(200);
            //死んだら敵を増やす
            mediator.AddActor(new RandomEnemy(mediator));
            mediator.AddActor(new RandomEnemy(mediator));
            mediator.AddActor(new BurstEffect(position, mediator));
            sound.PlaySE("gameplayse");
        }

        ///<summary>
        ///初期化
        ///</summary>
        public override void Initialize()
        {
            //乱数で位置と切り替え時間を決定
            position = new Vector2(
                rnd.Next(Screen. Width - 64),
                rnd.Next(Screen. Height - 64));
            changeTimer = 60 * rnd.Next(2, 5);//60フレーム　＊２～５秒
        }

        ///<summary>
        ///終了処理
        ///</summary>
        public override void Shutdown()
        {
        }

        ///<summary>
        ///更新
        ///</summary>
        ///<param name="=gameTime"></param>
        public override void Update(GameTime gameTime)
        {
            //  切り替え時間を減らす
            changeTimer -= 1;
            //マイナスになったか？
            if(changeTimer < 0)
            {
                //位置ち時間を初期化
                Initialize();
            }
        }
    }
}
