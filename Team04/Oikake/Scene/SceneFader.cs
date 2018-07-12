using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Oikake.Device;
using Oikake.Util;
using Oikake.Def;

namespace Oikake.Scene
{
    class SceneFader:IScene
    {
        private enum SceneFaderState
        {
            In,
            Out,
            None
        };
        private Timer timer;//フェード時間
        private readonly float FADE_TIME = 2.0f;//２秒で
        private SceneFaderState state;//状態
        private IScene scene;//現在のシーン
        private bool isEndFlsg = false;//終了フラグ


        public SceneFader(IScene scene)
        {
            this.scene = scene;
        }

        public void Initialize()
        {
            scene.Initialize();
            state = SceneFaderState.In;
            timer = new CountDownTimer(FADE_TIME);
            isEndFlsg = false;
        }

        public void Update(GameTime gameTime)
        {
            switch(state)
            {
                case SceneFaderState.In:
                    UpdateFadeIn(gameTime);
                    break;
                case SceneFaderState.Out:
                    UpdateFadeOut(gameTime);
                    break;
                case SceneFaderState.None:
                    UpdateFadeNone(gameTime);
                    break;
            }
        }

        private void UpdateFadeIn(GameTime gameTime)
        {
            //シーン更新
            scene.Update(gameTime);
            if(scene.IsEnd())
            {
                state = SceneFaderState.Out;
            }
            //時間の更新
            timer.Update(gameTime);
            if(timer.IsTime())
            {
                state = SceneFaderState.None;
            }
        }

        private void DrawFadeIn(Renderer renderer)
        {
            scene.Draw(renderer);
            DrawEffect(renderer, 1 - timer.Rate());
        }

        private void UpdateFadeOut(GameTime gameTime)
        {
            scene.Update(gameTime);
            if(scene.IsEnd())
            {
                state = SceneFaderState.Out;
            }
            timer.Update(gameTime);
            if(timer.IsTime())
            {
                isEndFlsg = true;
            }
        }

        private void DrawFadeOut(Renderer renderer)
        {
            scene.Draw(renderer);
            DrawEffect(renderer, timer.Rate());
        }

        private void UpdateFadeNone(GameTime gameTime)
        {
            scene.Update(gameTime);
            if(scene.IsEnd())
            {
                state = SceneFaderState.Out;
                timer.Initialize();
            }
        }

        private void DrawFadeNone(Renderer renderer)
        {
            scene.Draw(renderer);
        }

        private void DrawEffect(Renderer renderer,float alpha)
        {
            renderer.Begin();
            renderer.DrawTexture(
                "fade",
                Vector2.Zero,
                null,
                0.0f,
                Vector2.Zero,
                new Vector2(Screen.Width, Screen.Height),
                SpriteEffects.None,
                0.0f,
                alpha);
            renderer.End();
        }
        public void Draw(Renderer renderer)
        {
            switch(state)
            {
                case SceneFaderState.In:
                    DrawFadeIn(renderer);
                    break;
                case SceneFaderState.Out:
                    DrawFadeOut(renderer);
                    break;
                case SceneFaderState.None:
                    DrawFadeNone(renderer);
                    break;
            }
        }

        public void Shutdown()
        {
            scene.Shutdown();
        }

        public bool IsEnd()
        {
            return isEndFlsg;
        }

        public Scene Next()
        {
            return scene.Next();
        }
    }
}
