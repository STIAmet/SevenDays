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

        public int vida;
        public bool isVisible=true;
        public TimeSpan lastHit = new TimeSpan();
        public TimeSpan lastAtak = new TimeSpan();
        public int nivelDano;
        public int frequenciaDano;
        public int distanciaPerseguir;


        public override int TotalLinhasNaSprite => 4;
        public override int TotalColunasNaSprite => 6;

        public Vector2 Velocidade = new Vector2(300f);


        public Inimigo(int _vida, int _posX, int _posY, int _nivelDano, int _frequenciaDano=3000, int _distanciaPerseguir=1000)
        {
            vida = _vida;
            Posicao = new Vector2(_posX, _posY);
            nivelDano = _nivelDano;
            frequenciaDano = _frequenciaDano;
            distanciaPerseguir = _distanciaPerseguir;

        }

        public void Mover(ref GameTime gameTime, heroi personagem)
        {
            
            if (vida <= 0)
            {
                isVisible = false;
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Left) || Keyboard.GetState().IsKeyDown(Keys.Right))
            {
                // Direita
                if (Keyboard.GetState().IsKeyDown(Keys.Right))
                {
                    if (personagem.Posicao.X >= (0.7 * personagem.xFixoCenario))
                    {
                        Posicao.X -= (int)(personagem.Velocidade.X * (gameTime.ElapsedGameTime.TotalSeconds));
                    }
                }
                else
                {
                    if (personagem.Posicao.X <= (0.1 * personagem.xFixoCenario))
                    {
                        Posicao.X += (int)(personagem.Velocidade.X * (gameTime.ElapsedGameTime.TotalSeconds));
                    }
                }
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
                if (isVisible && (personagem.Posicao.X - this.Posicao.X < distanciaPerseguir && personagem.Posicao.X - this.Posicao.X > -1*distanciaPerseguir))
                {

                    if (personagem.Posicao.X - this.Posicao.X > 75)
                    {
                        Ativado = true;
                        Animacao(ref gameTime, 000);
                        this.Posicao.X += 2;
                    }
                    else if (personagem.Posicao.X - this.Posicao.X < -75)
                    {
                        Ativado = true;
                        Animacao(ref gameTime, 300);
                        this.Posicao.X -= 2;
                    }
                    else
                    {
                        #region Atacar!!
                        lastAtak += gameTime.ElapsedGameTime;
                        if (personagem.Posicao.X - this.Posicao.X > 0)
                        {
                            Ativado = true;
                            Animacao(ref gameTime, 150, 200);
                            if (lastAtak >= TimeSpan.FromMilliseconds(frequenciaDano))
                            {
                                personagem.barraVida.GameOver(nivelDano);
                                lastAtak = TimeSpan.Zero;
                            }
                            
                        }
                        else if (personagem.Posicao.X - this.Posicao.X < 0)
                        {
                            Ativado = true;
                            Animacao(ref gameTime, 450, 200);
                            if (lastAtak >= TimeSpan.FromMilliseconds(frequenciaDano))
                            {
                                personagem.barraVida.GameOver(nivelDano);
                                lastAtak = TimeSpan.Zero;
                            }
                        }
                        #endregion
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
