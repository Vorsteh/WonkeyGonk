﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;


namespace WonkeyGonk
{
    internal class Mario
    {
        public Vector2 _position;
        private Vector2 _velocity;

        private Texture2D _climbTexture, _runTextureOne, _runTextureTwo, _currentTexture;
        public Rectangle _rectangle;
        public Rectangle topRect, leftRect, rightRect, bottomRect;

        List<Platform> _platforms;
        List<Ladder> _ladders;


        bool _isClimbing, _isGrounded, _hasJumped, _hitHead;

        private float gravity = 2.0f;
        private bool noclip = false;
        
        private int textureTimer = 10;

        public Input Input;
        SpriteEffects se = SpriteEffects.FlipHorizontally;

        public Mario(Vector2 position, Texture2D climbTexture, Texture2D runTextureOne, Texture2D runTextureTwo, List<Platform> platforms, List<Ladder> ladders)
        {
            this._position = position;
            this._climbTexture = climbTexture;
            this._runTextureOne = runTextureOne;
            this._runTextureTwo = runTextureTwo;
            this._currentTexture = runTextureOne;

            this._platforms = platforms;
            this._ladders = ladders;

            this._isGrounded = true;
            this._isClimbing = false;
            this._hasJumped = false;
            this._hitHead = false;

            this._rectangle = new Rectangle((int)position.X, (int)position.Y,runTextureOne.Width, runTextureOne.Height);
        }


        //return rectuable
        public Rectangle GetRectangle()
        {
            return new Rectangle((int)_position.X, (int)_position.Y, _runTextureOne.Width, _runTextureOne.Height);
        }

        //check barrle collsion
        public bool CollidedWithBarrle(List<Barrel> barrels)
        {
            foreach(Barrel barrel in barrels)
            {
                if (this.GetRectangle().Intersects(barrel.GetRectangle())) return true;
            }
            return false;
        }

        public void Update(GameTime gameTime)
        {

            Move();
            topRect = new Rectangle((int)_position.X, (int)_position.Y, _runTextureOne.Width, -2);
            bottomRect = new Rectangle((int)_position.X, (int)_position.Y + _runTextureOne.Height, _runTextureOne.Width, 2);

            if (_hasJumped && _isClimbing == false)
            {
                float i = 0.7f;
                _velocity.Y += i * 0.15f;
            }
            if (_isGrounded == false && _isClimbing == false)
            {
                float i = 0.7f;
                _velocity.Y += i * 0.15f;
            }

            if (_isGrounded)
            {
                _velocity.Y = 0f;
            }

            foreach(Platform platform in _platforms)
            {
                if(topRect.Intersects(platform._rectangle) && !_hitHead)
                {
                    _velocity.Y = 0f;
                    _hitHead = true;
                }

                if(bottomRect.Intersects(platform._rectangle))
                {
                    _isGrounded = true;
                    _hasJumped = false;
                    _velocity.Y = 0f;
                    _position.Y -= 0.2f;
                    _hitHead = false;
                }
                else
                {
                    _isGrounded = false;
                }
            }

            _position.Y += _velocity.Y;
            _position.X += _velocity.X;
        }
        public void Draw(SpriteBatch _spriteBatch)
        {
            _spriteBatch.Draw(_currentTexture, _position, null, Color.White, 0, Vector2.Zero, 1, se, 0);
        }

        //looks keyboard input and moves mario absed on it
        private void Move()
        {
            if (Input == null) return;

            if (Keyboard.GetState().IsKeyDown(Keys.I))
            {
                noclip = !noclip;
            }

            if (!noclip)
            {
                if (Keyboard.GetState().IsKeyDown(Input.Up))
                {
                    foreach (Ladder ladder in _ladders)
                    {
                        if (GetRectangle().Intersects(ladder._rectangle))
                        {
                            _position.Y -= 0.5f;
                            _isClimbing = true;
                            _isGrounded = true;
                            _currentTexture = _climbTexture;
                        }
                        else
                        {
                            _isClimbing = false;
                        }
                    }
                }

                if (Keyboard.GetState().IsKeyDown(Input.Left) && !_isClimbing)
                {
                    _velocity.X = -2f;
                    _isGrounded = false;
                    _currentTexture = _runTextureOne;
                    se = SpriteEffects.FlipHorizontally;
                }

                else if (Keyboard.GetState().IsKeyDown(Input.Right) && !_isClimbing)
                {
                    _velocity.X = 2f;
                    _isGrounded = false;
                    se = SpriteEffects.None;
                    _currentTexture = _runTextureOne;
                }
                else
                {
                    _velocity.X = 0f;
                }
                if (Keyboard.GetState().IsKeyDown(Input.Jump) && _hasJumped == false && !_isClimbing)
                {
                    _position.Y -= 5f;
                    _velocity.Y = -4f;
                    _hasJumped = true;
                    _isGrounded = false;
                }
            }
            else
            {
                _velocity = Vector2.Zero;
                if (Keyboard.GetState().IsKeyDown(Input.Left))
                {
                    _position.X -= 2f;
                }
                else if (Keyboard.GetState().IsKeyDown(Input.Right))
                {
                    _position.X += 2f;
                }
                else if (Keyboard.GetState().IsKeyDown(Keys.S))
                {
                    _position.Y += 2f;
                }
                else if (Keyboard.GetState().IsKeyDown(Input.Up))
                {
                    _position.Y -= 2f;
                }
            }
        }
    }
}