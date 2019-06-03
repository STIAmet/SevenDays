using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Game1
{
    class Elemento:Entity
    {
        public bool tipo; //Arma ou poção
        public string nome;
        public int peso;
        public int ataque; //arma
        public int cura; //poção
        private SpriteFont font;
        private int score = 0;

        public Elemento(Game game) : base(game)
        {
            Texture = game.Content.Load<Texture2D>("poção");
            font = game.Content.Load<SpriteFont>("score");

        }


        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            //spriteBatch.Draw(Texture, new Vector2(0, 26), null, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None,0);
            base.Draw(spriteBatch, gameTime);
        }
    }
}
