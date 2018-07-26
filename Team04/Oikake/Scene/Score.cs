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
        private Vector2 scale = Vector2.One;
        private float Z;
        private float D;
        
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

            if (poolScore > 0)
            {
                score = score + 1;
                poolScore = poolScore - 1;

                Z = Z - 0.01f;
                D = D + 0.1f;
            }

           

            //if(score = score+)
            //{
            //    scale.X = ("black").Left.X + 1.0f;
            //    scale.Y = ("black").Left.Y + 1.0f;
            //}
        }

        public void Draw(Renderer renderer)
        {
            renderer.DrawTexture("score", new Vector2(50, 10));
            renderer.DrawNumber("number",new Vector2(250, 13),score);
 
            renderer.DrawTexture("dekita3", new Vector2(700 + D, 500 + D), new Rectangle(0, 0, 100, 100), 0, new Vector2(0, 0), new Vector2(Z,Z));


        }
        public void Shutdown()
        {
            score = score + poolScore;
            poolScore = 0;
        }
           
        
    }
}
