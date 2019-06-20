using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Mono.Helper.Input;
using Mono.Helper;

namespace Game1
{
    public class heroi: AnimacaoSprite
    {
        private SpriteFont font;
        private int score = 0;
        public float caixas;
        private int i = 0;
        public string nome = "";
        public int vida;
        public Mochila mochila;
        public Cinto cinto ;
        public float distance = 0;


        public int xFixoCenario;

        public override int TotalLinhasNaSprite => 2;
        public override int TotalColunasNaSprite => 8;

        public Vector2 Velocidade = new Vector2(300f);
        
        public void Mover(ref GameTime gameTime)
        {
            double tempoDecorridoJogo = gameTime.ElapsedGameTime.TotalSeconds;

            if (Keyboard.GetState().IsKeyDown(Keys.Left) || Keyboard.GetState().IsKeyDown(Keys.Right) || Keyboard.GetState().IsKeyDown(Keys.Down) || Keyboard.GetState().IsKeyDown(Keys.Up))
            {
                Ativado = true;

                if (Keyboard.GetState().IsKeyDown(Keys.Left) || Keyboard.GetState().IsKeyDown(Keys.Right))
                {
                    // Direita
                    if (Keyboard.GetState().IsKeyDown(Keys.Right))
                    {
                        Animacao(ref gameTime, 0);

                        distance += (float)(Velocidade.X * tempoDecorridoJogo);

                        if (Posicao.X <= (0.7 * xFixoCenario))
                        {
                            Posicao.X += (float)(Velocidade.X * tempoDecorridoJogo);
                        }

                    }
                    else
                    // Esquerda
                    {
                        Animacao(ref gameTime, 135);

                        distance -= (float)(Velocidade.X * tempoDecorridoJogo);

                        if (Posicao.X >= (0.1 * xFixoCenario))
                        {
                             Posicao.X -= (float)(Velocidade.X * tempoDecorridoJogo);
                        }

                    }
                }
                else
                {
                    //// Para Baixo
                    //if (Keyboard.GetState().IsKeyDown(Keys.Down))
                    //{
                    //    Animacao(ref gameTime, 0);
                    //    Posicao.Y += (float)(Velocidade.Y * tempoDecorridoJogo);
                    //}
                    //else
                    //// Para Cima
                    //{
                    //    Animacao(ref gameTime, 299);
                    //    Posicao.Y -= (float)(Velocidade.Y * tempoDecorridoJogo);
                    //}
                }
            }

            Ativado = false;
        }
    }
}
