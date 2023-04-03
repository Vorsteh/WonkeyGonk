using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;

namespace WonkeyGonk
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        public static Texture2D platformTexture, donkeyKongTexture, marioClimbTexture, marioRunTextureOne, marioRunTextureTwo, princessTexture, ladderTexture, barrelTexture;

        Map map;
        List<Barrel> barrelList;
        List<Platform> platforms;

        Mario mario;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            _graphics.PreferredBackBufferWidth = 480;
            _graphics.PreferredBackBufferHeight = 640;
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            // TODO: use this.Content to load your game content here

            platformTexture = Content.Load<Texture2D>("platform2");
            donkeyKongTexture = Content.Load<Texture2D>("DonkeyKong2");
            marioClimbTexture = Content.Load<Texture2D>("Mario_Climb2");
            marioRunTextureOne = Content.Load<Texture2D>("Mario_Run22");
            marioRunTextureTwo = Content.Load<Texture2D>("Mario_Run32");
            princessTexture = Content.Load<Texture2D>("Princess2");
            ladderTexture = Content.Load<Texture2D>("Ladder2");
            barrelTexture = Content.Load<Texture2D>("Barrel2");




            platforms = createPlatforms();


            map = new Map(platforms, Content.Load<Texture2D>("Barrel2"), createLadders());

            mario = new Mario(new Vector2(80, 540), marioClimbTexture, marioRunTextureOne, marioRunTextureTwo, platforms, createLadders())
            {
                Input = new Input() { Left = Keys.A, Right = Keys.D, Jump = Keys.Space, Up = Keys.W }
            };

            barrelList = new List<Barrel>()
            {
                new Barrel(barrelTexture)
                {
                    Origin = new Vector2(barrelTexture.Width / 2, barrelTexture.Height / 2)
                }
            };
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();


            // TODO: Add your update logic here

            mario.Update(gameTime);

            for (int i = barrelList.Count - 1; i >= 0; i--)
            {
                Barrel barrel = barrelList[i];

                barrel.Update();

                if (barrel._position.Y > 700)
                {
                    barrelList.RemoveAt(i);
                }
            }

            foreach (Barrel barrel in barrelList)
            {
                barrel.CheckIfBarrelIsFalling(platforms);
            }

            //Debug.WriteLine(Mouse.GetState().X.ToString() + ':' + Mouse.GetState().Y.ToString());

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            // TODO: Add your drawing code here

            _spriteBatch.Begin();

            map.Draw(_spriteBatch);
            _spriteBatch.Draw(donkeyKongTexture, new Vector2(80, 80), Color.White);
            _spriteBatch.Draw(princessTexture, new Vector2(153, 25), Color.White);

            foreach(var barrel in barrelList)
                barrel.Draw(_spriteBatch);

            mario.Draw(_spriteBatch);
            _spriteBatch.End();

            base.Draw(gameTime);
        }


        private static List<Platform> createPlatforms()
        {
            List<Platform> platforms = new List<Platform>();

            for (int i = 0; i < 11; i++)
            {
                if (i >= 5)
                    platforms.Add(new Platform(platformTexture, new Vector2(60 + i * 32, 580 - (i - 5) * 2)));
                else
                    platforms.Add(new Platform(platformTexture, new Vector2(60 + i * 32, 580)));
            }

            for (int i = 0; i < 43; i++)
            {
                if (i < 10)
                {
                    platforms.Add(new Platform(platformTexture, new Vector2(60 + i * 32, 500 + (i - 5) * 3)));
                } else if (i > 10 && i < 21)
                {
                    platforms.Add(new Platform(platformTexture, new Vector2(60 + (i - 10) * 32, 440 - (i - 5) * 3)));
                }
                else if (i > 21 && i < 32)
                {
                    platforms.Add(new Platform(platformTexture, new Vector2(60 + (i - 22) * 32, 350 + (i - 37) * 3)));
                }
                else if (i > 32 && i < 43)
                {
                    platforms.Add(new Platform(platformTexture, new Vector2(60 + (i - 32) * 32, 210 - (i - 43) * 3)));
                }
            }

            for (int i = 0; i < 10; i++)
            {
                if (i >= 6)
                    platforms.Add(new Platform(platformTexture, new Vector2(60 + i * 32, 143 + (i - 5) * 2)));
                else
                    platforms.Add(new Platform(platformTexture, new Vector2(60 + i * 32, 143)));
            }

            for (int i = 0; i < 3; i++)
            {
                platforms.Add(new Platform(platformTexture, new Vector2(150 + i * 32, 70)));
            }

            return platforms;
        }

        private static List<Ladder> createLadders()
        {
            List<Ladder> ladderList = new List<Ladder>()
            {
                new Ladder(new Vector2(360, 556), ladderTexture),
                new Ladder(new Vector2(360, 556 - ladderTexture.Height), ladderTexture),
                new Ladder(new Vector2(360, 556 - ladderTexture.Height * 2), ladderTexture),

                new Ladder(new Vector2(110, 476), ladderTexture),
                new Ladder(new Vector2(110, 476 - ladderTexture.Height), ladderTexture),
                new Ladder(new Vector2(110, 476 - ladderTexture.Height * 2), ladderTexture),
                new Ladder(new Vector2(110, 476 - ladderTexture.Height * 3), ladderTexture),

                new Ladder(new Vector2(215, 395), ladderTexture),
                new Ladder(new Vector2(215, 395 - ladderTexture.Height), ladderTexture),
                new Ladder(new Vector2(215, 395 - ladderTexture.Height * 4), ladderTexture),

                new Ladder(new Vector2(360, 385), ladderTexture),
                new Ladder(new Vector2(360, 385 - ladderTexture.Height), ladderTexture),
                new Ladder(new Vector2(360, 385 - ladderTexture.Height * 2), ladderTexture),
                new Ladder(new Vector2(360, 385 - ladderTexture.Height * 3), ladderTexture),

                new Ladder(new Vector2(110, 294), ladderTexture),
                new Ladder(new Vector2(110, 294 - ladderTexture.Height), ladderTexture),
                new Ladder(new Vector2(110, 294 - ladderTexture.Height * 2), ladderTexture),
                new Ladder(new Vector2(110, 294 - ladderTexture.Height * 3), ladderTexture),
            };

            return ladderList;
        }
    }
}