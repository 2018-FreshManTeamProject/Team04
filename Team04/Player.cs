using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Team04
{
    class Player
    {
        private Vector2 position;//位置

        public Player()
        {
            position = Vector2.Zero;
        }

        public void Intialize()
        {
            //位置を(300,400)に設定
            position = new Vector2(300, 400);
        }

        public void Update(GameTime gameTime)
        {
            //移動量
        }
    }
}
