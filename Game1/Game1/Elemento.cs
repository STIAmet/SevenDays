using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Game1
{
    public class Elemento
    {
        public bool tipo; //Arma ou poção
        public string nome;
        public int peso;
        public int ataque; //arma
        public int cura; //poção
        private SpriteFont font;
        private int score = 0;
        public int xBase;
        public int yBase;
        public int xTamanho;
        public int yTamanho;
        public Texture2D textura;
        public Color cor;
        public bool IsVisible;

        //public Elemento(Game game) : base(game)
        //{
        //    Texture = game.Content.Load<Texture2D>("poção");
        //    font = game.Content.Load<SpriteFont>("score");

        //}
        public Elemento(int xBasinha, int yBasinha, int xTamainho, int yTamainho, Texture2D text, Color corzinha, bool visible = true)
        {
            xBase = xBasinha;
            yBase = yBasinha;
            xTamanho = xTamainho;
            yTamanho = yTamainho;
            textura = text;
            cor = corzinha;
            IsVisible = visible;
        }


        public void Draw(SpriteBatch spriteBatch, GameTime gameTime, heroi personagem)
        {
            //spriteBatch.Draw(Texture, new Vector2(0, 26), null, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None,0);
            if (!IsVisible)
                spriteBatch.Draw(textura, new Rectangle(xBase, yBase, xTamanho, yTamanho), Color.Transparent);
            else
            if ((new Rectangle(xBase, yBase, xTamanho, yTamanho).Contains(new Point(Mouse.GetState().X, Mouse.GetState().Y))) && !(Mouse.GetState().LeftButton == ButtonState.Pressed))
            {
                spriteBatch.Draw(textura, new Rectangle(xBase, yBase, xTamanho, yTamanho), Color.Blue);
            }
            else if ((new Rectangle(xBase, yBase, xTamanho, yTamanho).Contains(new Point(Mouse.GetState().X, Mouse.GetState().Y))) && (Mouse.GetState().LeftButton == ButtonState.Pressed))
            {
                spriteBatch.Draw(textura, new Rectangle(xBase, yBase, xTamanho, yTamanho), Color.Red);
                IsVisible = false;
                personagem.cinto.Add(this);
            }
            else
                spriteBatch.Draw(textura, new Rectangle(xBase, yBase, xTamanho, yTamanho), cor);
            
        }
    }
}
