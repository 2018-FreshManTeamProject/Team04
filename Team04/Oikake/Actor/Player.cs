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

        //フィールド
        // private Vector2 Position;
        private Motion motion;

        private enum Direction
        {
            DOWN, UP, RIGHT, LEFT
        };

        private Direction direction;//現在の向き
        //向きと範囲は管理
        private Dictionary<Direction, Range> directionRange;

        public Player(IGameMediator mediator)
            : base("oikake_player_4anime", mediator)
        {
        }

        ///<summary>
        ///初期化メソッド
        ///</summary>
        public override void Initialize()
        {
            //位置を（３００，４００）に変更
            position = new Vector2(300, 400);
            motion = new Motion();

            for (int i =0; i<16; i++)
            {
                motion.Add(i, new Rectangle(64 *(i%4),64 * (int)(i/4), 64, 64));
            }

           
         
            //最初はすべてのパーツ表示に設定
            motion.Initialize(new Range(0, 15), new CountDownTimer(0.2f));

            //最初は下向きに
            direction = Direction.DOWN;
            directionRange = new Dictionary<Direction, Range>()
            {
                {Direction.DOWN,new Range(0,3) },
                {Direction.UP,new Range(4,7) },
                {Direction.RIGHT,new Range(8,11) },
                {Direction.LEFT,new Range(12,15) }
            };
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
            var min = Vector2.Zero;
            var max = new Vector2(Screen.Width - 64, Screen.Height - 64);
            position = Vector2.Clamp(position, min, max);

            UpdateMotion();
            motion.Update(gameTme);
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

        }

        public override void Draw(Renderer renderer)
        {
            renderer.DrawTexture(name, position, motion.DrawingRange());
        }

        private void ChangeMotion(Direction direction)
        {
            this.direction = direction;
            motion.Initialize(directionRange[direction], new
                CountDownTimer(0.2f));
        }
        
        private void UpdateMotion()
        {
            //キー入力の状態を取得
            Vector2 vectocity  = Input.Velocity();

            //キー入力がなければ何もしない
            if(vectocity.Length() <= 0.0f)
            {
                return;
            }

            //キー入力があった時
            //下向きに変更
            if((vectocity.Y > 0.0f) && (direction != Direction.DOWN))
            {
                ChangeMotion(Direction.DOWN);
            }
            //上向きに変更
            else if((vectocity.Y < 0.0f) && (direction != Direction.UP))
            {
                ChangeMotion(Direction.UP);
            }

            //右向きに変更
            else if ((vectocity.X > 0.0f) && (direction != Direction.RIGHT))
            {
                ChangeMotion(Direction.RIGHT);
            }
            //左向きに変更
            else if ((vectocity.X < 0.0f) && (direction != Direction.LEFT))
            {
                ChangeMotion(Direction.LEFT);
            }

            //puddleの時
            motion.Add(0, new Rectangle(64 * 0, 0, 64, 64));
            motion.Add(1, new Rectangle(64 * 1, 0, 64, 64));
            motion.Add(2, new Rectangle(64 * 2, 0, 64, 64));
            motion.Add(3, new Rectangle(64 * 3, 0, 64, 64));

            motion.Add(0, new Rectangle(64 * 0, 64 * 0, 64, 64));
            motion.Add(1, new Rectangle(64 * 1, 64 * 0, 64, 64));
            motion.Add(2, new Rectangle(64 * 2, 64 * 0, 64, 64));
            motion.Add(3, new Rectangle(64 * 3, 64 * 0, 64, 64));

        }

       
    }
}

