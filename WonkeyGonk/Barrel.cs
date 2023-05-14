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
        bool hasTouchedPlatform = false;

        List<Platform> _platforms;

        private Vector2 moveLeftSpeed = new Vector2(-2.1f, 0.21f);
        private Vector2 moveRightSpeed = new Vector2(2.1f, 0.21f);
        private Vector2 moveDownLSpeed = new Vector2(0.3f, 3f);
        private Vector2 moveDownRSpeed = new Vector2(-0.3f, 3f);

        public Barrel(Texture2D texture, List<Platform> platforms)
        {
            _texture = texture;
            _position = new Vector2(170, 120);
            _platforms = platforms;
        }

        //Coords for switch 381:131, 


        public void Update()
        {

            _rotation = _rotation > 360f ? 0f : _rotation += 0.1f * direction;
            _rotation = _rotation > 360f ? 0f : _rotation += 0.1f * direction;
            CheckIfBarrelIsFalling(_platforms);

            if (_position.X > 400)
            {
                direction = -1;
            } else if (_position.X < 70)
            {
                direction = 1;
            }
            setBarrelVelocity();
            _position += _velocity;
        }

        public void Draw(SpriteBatch _sb)
        {
            _sb.Draw(_texture, _position, null, Color.White, _rotation, Origin, 1, SpriteEffects.None, 0f);
        }


        //Cecks if barrel is falling
        public void CheckIfBarrelIsFalling(List<Platform> platforms)
        {
            foreach (Platform platform in platforms)
            {
                if (GetRectangle().Intersects(platform.GetRectangle()))
                {
                    hasTouchedPlatform = true;
                    isFalling = false;
                    return;
                }
            }
            direction *= -1;
            isFalling = true;
        }


        //Retturns barrels rect
        public Rectangle GetRectangle()
        {
            return new Rectangle((int)_position.X, (int)_position.Y, _texture.Width, _texture.Height);
        }

        //changes barrels velocity based on positions
        private void setBarrelVelocity()
        {
            if (direction == 1)
            {
                if (isFalling)
                    _velocity = moveDownRSpeed;
                else
                    _velocity = moveRightSpeed;
            }
            else
            {
                if (isFalling)
                    _velocity = moveDownLSpeed;
                else
                    _velocity = moveLeftSpeed;
            }
        }
    }
}
