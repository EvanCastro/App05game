using App05MonoGame.Sprites;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;


namespace App05MonoGame.Controllers
{
    public enum CoinColours
    {
        copper = 100,
        Silver = 200,
        Gold = 500
    }

    /// <summary>
    /// This class creates a list of coins which
    /// can be updated and drawn and checked for
    /// collisions with the player sprite
    /// </summary>
    /// <authors>
    /// Derek Peacock & Andrei Cruceru
    /// </authors>
    public class CoinsController : IUpdateableInterface, 
        IDrawableInterface, ICollideableInterface
    {
        private Random generator = new Random();

        private double maxTime = 3.0;
        private double elapsedTime = 0;

        private Texture2D coinSheet;
        private GraphicsDevice graphics;

        private readonly List<AnimatedSprite> Coins;        

        public CoinsController()
        {
            Coins = new List<AnimatedSprite>();
        }

        /// <summary>
        /// Create an animated sprite of a copper coin
        /// which could be collected by the player for a score
        /// </summary>
        public void CreateFirstCoin(GraphicsDevice graphics, Texture2D coinSheet)
        {
            this.graphics = graphics;
            this.coinSheet = coinSheet;

            SoundController.PlaySoundEffect(Sounds.Coins);
            CreateNewCoin();
        }

        public void CreateNewCoin()
        {
            Animation animation = new Animation(graphics, "coin", coinSheet, 8);

            AnimatedSprite coin = new AnimatedSprite()
            {
                Animation = animation,
                Image = animation.FirstFrame,
                Scale = 2.0f,
                Position = new Vector2(600, 100),
                Speed = 0,
            };

            Coins.Add(coin);
        }

        /// <summary>
        /// If the sprite collides with a coin the coin becomes
        /// invisible and inactive.  A sound is played
        /// </summary>
        public void DetectCollision(Sprite sprite)
        {
            foreach (AnimatedSprite coin in Coins)
            {
                if (coin.HasCollided(sprite) && coin.IsAlive)
                {
                    SoundController.PlaySoundEffect(Sounds.Coins);

                    coin.IsActive = false;
                    coin.IsAlive = false;
                    coin.IsVisible = false;
                }
            }           
        }

        public void Update(GameTime gameTime)
        {
            // TODO: create more coins every so often??
            // or recyle collected coins

            elapsedTime += gameTime.ElapsedGameTime.TotalSeconds;

            if(elapsedTime >= maxTime)
            {
                // TODO: create and add a new coin
                CreateNewCoin();
                elapsedTime = 0;
            }

            foreach(AnimatedSprite coin in Coins)
            {
                coin.Update(gameTime);
            }
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            foreach (AnimatedSprite coin in Coins)
            {
                coin.Draw(spriteBatch, gameTime);
            }
        }
    }
}
