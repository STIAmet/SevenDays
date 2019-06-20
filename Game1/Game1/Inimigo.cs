using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1
{
    class Inimigo:AnimacaoSprite
    {
        private SpriteFont font;
        private int score = 0;
        private int i = 0;
        public string nome;
        public int vida=100;
        public bool isVisible=true;
        public TimeSpan lastHit = new TimeSpan();


        public int xFixoCenario;

        public override int TotalLinhasNaSprite => 5;
        public override int TotalColunasNaSprite => 8;

        public Vector2 Velocidade = new Vector2(300f);

        public void Mover(ref GameTime gameTime)
        {
            if (vida <= 0)
            {
                isVisible = false;
            }

            if (isVisible)
            {
                lastHit += gameTime.ElapsedGameTime;
                if ((lastHit > TimeSpan.FromMilliseconds(2000)) && (new Rectangle((int)this.Posicao.X, (int)this.Posicao.Y, this._frameLargura, this._frameAltura).Contains(new Point(Mouse.GetState().X, Mouse.GetState().Y))) && (Mouse.GetState().LeftButton == ButtonState.Pressed))
                {
                    RecebeDano(50);
                    lastHit = TimeSpan.Zero;
                }
                double tempoDecorridoJogo = gameTime.ElapsedGameTime.TotalSeconds;
                if (isVisible)
                {
                    if (Keyboard.GetState().IsKeyDown(Keys.Left) || Keyboard.GetState().IsKeyDown(Keys.Right) || Keyboard.GetState().IsKeyDown(Keys.Down) || Keyboard.GetState().IsKeyDown(Keys.Up))
                    {
                        Ativado = true;
                        Animacao(ref gameTime, 500);

                    }

                    Ativado = false;
                }

            }
            else {
                this.cor = Color.Transparent;
            }
        }

        public void RecebeDano(int dano)
        {
            vida -= dano;

        }

         

    }
}
