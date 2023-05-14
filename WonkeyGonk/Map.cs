using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WonkeyGonk
{
    internal class Map
    {
        public List<Platform> platforms;
        Texture2D barrelTexture;

        List<Ladder> ladderList;
        
        
        //The map i guess
        public Map(List<Platform> platforms, Texture2D barrelTexture, List<Ladder> ladders) 
        {
            this.platforms = platforms;
            this.barrelTexture = barrelTexture;
            this.ladderList = ladders;
        }

        public void Draw(SpriteBatch _spriteBatch)
        {
            foreach(var platform in platforms)
            {
                _spriteBatch.Draw(platform._texture, platform._position, Color.Red);
            }
            foreach(var ladder in ladderList)
            {
                _spriteBatch.Draw(ladder._texture, ladder._position, Color.White);
            }

            for (int i = 0; i < 2; i++)
            {
                _spriteBatch.Draw(barrelTexture, new Vector2(60 + (i * barrelTexture.Width), 120), Color.White);
                _spriteBatch.Draw(barrelTexture, new Vector2(60 + (i * barrelTexture.Width), 120 - barrelTexture.Height), Color.White);
            }
        }
    }

    //The platform class used to make platforms
    public class Platform
    {
        public Texture2D _texture;
        public Vector2 _position;
        public Rectangle _rectangle;

        public Platform(Texture2D texture, Vector2 position)
        {
            this._texture = texture;
            this._position = position;
            this._rectangle = new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height);
        }

        public Rectangle GetRectangle()
        {
            return new Rectangle((int)_position.X, (int)_position.Y, _texture.Width, _texture.Height);
        }
    }
}
