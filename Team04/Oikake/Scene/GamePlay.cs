using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Oikake.Actor;
using Oikake.Device;
using Oikake.Util;

namespace Oikake.Scene
{
    class GamePlay : IScene,IGameMediator
    {
        //  private Player player;
        // private List<Character> characters;
        private CharacterManager characterManager;
        private Timer timer;
        private TimerUI timerUI;
        private Score score;
        

        private bool isEndFlag;
        private Sound sound;

       

        public GamePlay()
        {
            isEndFlag = false;
            var gameDevice = GameDevice.Instance();
            sound = gameDevice.GetSound();
        }

        public void GetScore()
        {
            
        }
        public void Draw(Renderer renderer)
        {
            //描画開始
            renderer.Begin();
            //背景を描画
            renderer.DrawTexture("stage", Vector2.Zero);
            timerUI.Draw(renderer);
            score.Draw(renderer);

            //キャラクターを一括描画
           // characters.ForEach(c => c.Draw(renderer));
            //プレイヤーを描画
         //   player.Draw(renderer);

            characterManager.Draw(renderer);

            renderer.End();
        }
        public void Initialize()
        {
            isEndFlag = false;

            //キャラクターマネージャーの実態生成
            characterManager = new CharacterManager();

            //キャラクターマネージャの実態生成
            //   characterManager = new CharacterManager();
            //characterManager.Add(new Bag(this));
            //----プレイヤー追加処理----
            //キャラクターマネージャにプレイヤー追加
            for (int i = 0; i< 3; ++i)
            {
                characterManager.Add(new Player(this, i));
            }


            //----エネミー追加処理----
            //動かない敵を追加

            characterManager.Add(new Enemy(this));
            characterManager.Add(new Floor(this));
           
            //時間関連
            timer = new CountDownTimer(20);

            timerUI = new TimerUI(timer);

            //スコア関連
            score = new Score();

            //プレイヤーの実体生成
            //  player = new Player();
            //プレイヤーを初期化
            //    player.Initialize();

            //Listの実体生成
            //   characters = new List<Character>();

            //ListにCharacterもオブジェクト(継承した子たち)
            //   characters.Add(new Enemy());
            //   characters.Add(new BoundEnemy());
            //１０体登録
            //   for (int i = 0; i < 10; i++)
            //   {
            //       characters.Add(new RandomEnemy());
            //   }

            //登録したキャラクターを一気に初期化
            //    foreach (var c in characters)
            //   {
            //         c.Initialize();
            //    }


        }
        


        public void Update(GameTime gameTime)
        {
            timer.Update(gameTime);
            score.Update(gameTime);
           // sound.PlayBGM("gameplaybgm");

            //キャラクターマネージャーを更新
            characterManager.Update(gameTime);
            sound.PlayBGM("gameplaybgm");
            //時間切れか？
            if( score.GetScore()> 500)
            {
                //計算途中のスコアを全部加算
                score.Shutdown();

                //シーン終了
                isEndFlag = true;
                sound.PlaySE("gameplayse");
            }
           else if (timer.IsTime())
            {
                //計算途中のスコアを全部加算
                score.Shutdown();

                //シーン終了
                isEndFlag = true;
                sound.PlaySE("gameplayse");
            }

            
            
            

        }


        public void Shutdown()
        {
            sound.StopBGM();
        }

        public bool IsEnd()
        {
            return isEndFlag;
        }

        public Scene Next()
        {
            return Scene.Ending;

        }

        public void AddActor(Character character)
        {
            characterManager.Add(character);
        }

        public void AddScore()
        {
            score.Add();
        }

        public void AddScore(int num)
        {
            score.Add(num);
        }

        int IGameMediator.GetScore()
        {
            throw new NotImplementedException();
        }

        //public int GetScore()
        //{
        //    return score.GetScore();
        //}
    }
}

