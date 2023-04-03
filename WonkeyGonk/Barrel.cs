using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WonkeyGonk
{
    internal class Barrel
    {
        private Texture2D _texture;
        private float _rotation;

        public Vector2 _position;
        public Vector2 Origin;

        public Vector2 _velocity;
        public int direction = 1;

        public float RotationVelocity = 3f;
        public float linearVelocity = 4f;


        bool isFalling = false;

        public Barrel(Texture2D texture)
        {
            _texture = texture;
            _position = new Vector2(170, 120);
        }

        //Coords for switch 381:131, 


        public void Update()
        {

            _rotation = _rotation > 360f ? 0f : _rotation += 0.1f;

            if (_position.X > 400)
            {
                direction = -1;
            }
            

            _position.X += 1 * direction;
            _position.Y += _velocity.Y;
        }

        public void Draw(SpriteBatch _sb)
        {
            _sb.Draw(_texture, _position, null, Color.White, _rotation, Origin, 1, SpriteEffects.None, 0f);
        }

        public void CheckIfBarrelIsFalling(List<Platform> platforms)
        {
            foreach (Platform platform in platforms) 
            {
                if (GetRectangle().Intersects(platform._rectangle))
                {
                    _velocity.Y = 0.085f;
                }
                else
                {
                    _velocity.Y = 2f;
                }
            }
        }

        public Rectangle GetRectangle()
        {
            return new Rectangle((int)_position.X, (int)_position.Y, _texture.Width, _texture.Height);
        }
    }
}
