using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oikake.Device
{
    static class Input
    {
        //移動量
        private static Vector2 velocity = Vector2.Zero;
        //キーボード
        private static KeyboardState CurrentKey;
        private static KeyboardState previouaKey;
        //マウス
        private static MouseState currentMouse;
        private static MouseState previousMouse;

        //ゲームパッド
        private static GamePadState gamePadState = GamePad.GetState(PlayerIndex.One);

        public static void Update()
        {
            //キーボード
            previouaKey = CurrentKey;
            CurrentKey = Keyboard.GetState();
            //マウス
            previousMouse = currentMouse;
            currentMouse = Mouse.GetState();
            //ゲームパッドの状態を取得
            gamePadState = GamePad.GetState(PlayerIndex.One);
            


            //更新
            UpdateVelocity();//これがあるか確認

        }

        //キーボード関連
        public static Vector2 Velocity()
        {
            return velocity;
        }

        private static void UpdateVelocity()
        {
            //毎ループ初期化
            velocity = Vector2.Zero;

            //右
            //if (CurrentKey.IsKeyDown(Keys.Right))
            if(gamePadState.DPad.Right == ButtonState.Pressed)
            {
                velocity.X += 1.0f;
            }
            //左
            //if (CurrentKey.IsKeyDown(Keys.Left))
            if (gamePadState.DPad.Left == ButtonState.Pressed)
            {
                velocity.X -= 1.0f;
            }
            //上
            //if (CurrentKey.IsKeyDown(Keys.Up))
            if (gamePadState.DPad.Up == ButtonState.Pressed)
            {
                velocity.Y -= 1.0f;
            }
            //下
            //if (CurrentKey.IsKeyDown(Keys.Down))
            if (gamePadState.DPad.Down == ButtonState.Pressed)
            {
                velocity.Y += 1.0f;
            }

            //正規化
            if (velocity.Length() != 0)
            {
                velocity.Normalize();
            }
        }

        public static bool IsKeyDown(Keys key)
        {
            return CurrentKey.IsKeyDown(key) && !previouaKey.IsKeyDown(key);
        }

        public static bool GetkeyTrigger(Keys key)
        {
            return IsKeyDown(key);
        }

        public static bool GetkeyState(Keys key)
        {
            return CurrentKey.IsKeyDown(key);
        }

        public static bool IsMouseLBottonDown()
        {
            return currentMouse.LeftButton == ButtonState.Pressed && previousMouse.LeftButton == ButtonState.Released;
        }

        public static bool IsMouseLBottonUp()
        {
            return currentMouse.LeftButton == ButtonState.Pressed && previousMouse.LeftButton == ButtonState.Released;
        }

        public static bool IsMouseLButton()
        {
            return currentMouse.LeftButton == ButtonState.Pressed;

        }
        public static bool IsMouseRBottonDown()
        {
            return currentMouse.RightButton == ButtonState.Pressed && previousMouse.RightButton == ButtonState.Released;
        }

        public static bool IsMouseRBottonUp()
        {
            return currentMouse.RightButton == ButtonState.Pressed && previousMouse.RightButton == ButtonState.Released;
        }

        public static bool IsMouseRButton()
        {
            return currentMouse.RightButton == ButtonState.Pressed;
        }

        public static Vector2 MousePosition
        {
            get
            {
                return new Vector2(currentMouse.X, currentMouse.Y);
            }
        }

        public static int GetMouseWheel()
        {
            return previousMouse.ScrollWheelValue -
                currentMouse.ScrollWheelValue;
        }
    }
}
