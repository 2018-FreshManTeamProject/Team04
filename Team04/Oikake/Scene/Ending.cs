using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Oikake.Device;
using Microsoft.Xna.Framework.Input;

namespace Oikake.Scene
{
    class Ending : IScene
    {
        private bool isEndFlag;
        IScene backGroundScene;
        private Sound sound;

       


        //ゲームパッド
        public static GamePadState gamePadState = GamePad.GetState(PlayerIndex.One);
        public Ending(IScene scene)
        {
            isEndFlag = false;
            backGroundScene = scene;
            var gameDevice = GameDevice.Instance();
            sound = gameDevice.GetSound();
        }
        public void Draw(Renderer renderer)
        {
            //シーンごとにrenderer.Begin() End()を
            //書いているのに注意
            //背景となるゲームプレイシーン
            backGroundScene.Draw(renderer);

            renderer.Begin();
            renderer.DrawTexture("ending", new Vector2(150, 150));
            renderer.End();
        }

        public void Initialize()
        {
            isEndFlag = false;
        }

        public bool IsEnd()
        {
            return isEndFlag;
        }

        public Scene Next()
        {
            return Scene.Title;

        }

        public void Shutdown()
        {
            sound.StopBGM();
        }
            

        public void Update(GameTime gameTime)
        {
            gamePadState = GamePad.GetState(PlayerIndex.One);
            sound.PlayBGM("endingbgm");
            if (Input.GetkeyTrigger(Keys.Space) || (gamePadState.Buttons.Start == ButtonState.Pressed))
            {
                isEndFlag = true;
                sound.PlaySE("endingse");
            }
        }
    }
}
