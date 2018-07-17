using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;//Vector2用
using Microsoft.Xna.Framework.Input;//入力処理用
using Oikake.Device;
using Oikake.Def;
using Oikake.Scene;
using Oikake.Util;

namespace Oikake.Actor
{

    /// <summary>
    /// 白玉（プレイヤー）
    /// </summary>
    class Player : Character
    {
        private int playerID;
        private int[] addScores = { 20, 55, 40 };

        public Player(IGameMediator mediator,int playerID)
            : base("black", mediator)
        {
            this.playerID = playerID;
        }

        ///<summary>
        ///初期化メソッド
        ///</summary>
        public override void Initialize()
        {
            //位置を（３００，４００）に変更,
            position = new Vector2(300+playerID*64 , 400);






        }
        ///<summary>
        ///更新処理
        ///</summarTi)
        ///<param name="="gameTime">ゲーム時間<param>
        public override void Update(GameTime gameTme)
        {

            //移動処理
            float speed = 5.0f;
            position = position + Input.Velocity() * speed;
            //当たり判定
            var min = new Vector2(64*playerID,0);
            var max = new Vector2(Screen.Width - 64 - 64 * (2-playerID), Screen.Height - 64);

            position = Vector2.Clamp(position, min, max);

            
            
        }

        // public override void Draw(Renderer renderer)
        //{
        //    renderer.DrawTexture("white", Position);
        //}

        public override void Shutdown()
        {

        }

        public override void Hit(Character other)
        {
            mediator.AddScore(addScores[playerID]);
        }

        public override void Draw(Renderer renderer)
        {
            renderer.DrawTexture(name, position);
        }

        
        
       
        

       
    }
}

