using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Oikake.Device;
using Microsoft.Xna.Framework;

namespace Oikake.Scene
{
    class Score
    {
        private int poolScore;
        private int score;
        public int GetScore()
        {
            return score;
        }
        public void Initialize()
        {
            poolScore = 0;
            score = 0;
        }
        public void Add()
        {
            poolScore = poolScore + 1;
        }

        public void Add(int num)
        {
            poolScore = poolScore + num;
        }

        public void Update(GameTime gameTime)
        {
            if(poolScore>0)
            {
                score = score + 1;
                poolScore =  poolScore - 1;
            }
        }

        public void Draw(Renderer renderer)
        {
            renderer.DrawTexture("score", new Vector2(50, 10));
            renderer.DrawNumber("number",new Vector2(250, 13),score);
        }
        public void Shutdown()
        {
            score = score + poolScore;
            poolScore = 0;
        }
           
    }
}
