using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WonkeyGonk
{
    internal class Ladder
    {
        public Vector2 _position;
        public Texture2D _texture;
        public Rectangle _rectangle;

        public Ladder(Vector2 position, Texture2D texture)
        {
            _position = position;
            _texture = texture;
            _rectangle = new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height);
        }

        public void Draw(SpriteBatch _spriteBatch)
        {
            _spriteBatch.Draw(_texture, _position, Color.White);
        }

    }
}
