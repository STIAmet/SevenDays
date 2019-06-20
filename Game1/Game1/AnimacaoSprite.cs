using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Game1
{
    public abstract class AnimacaoSprite
    {
        public bool Ativado = false;
        public Vector2 Posicao = new Vector2(0, 450);

        protected abstract int TotalLinhasNaSprite { get; }
        protected abstract int TotalColunasNaSprite { get; }

        private Texture2D _textura;
        private int _frameLargura;
        private int _frameAltura;
        private Rectangle _regiaoDaTextura = Rectangle.Empty;
        private int _frameAtualDaColuna;
        private TimeSpan _acumulaTempo = TimeSpan.Zero;

        public void LoadContent(ContentManager content, string assetName)
        {
            _textura = content.Load<Texture2D>(assetName);

            _frameLargura = _textura.Width / TotalColunasNaSprite;
            _frameAltura = _textura.Height / TotalLinhasNaSprite;

            _regiaoDaTextura = new Rectangle(0, 0, _frameLargura, _frameAltura);
        }

        public void Animacao(ref GameTime gameTime, int regiaoPosY)
        {
            if (!Ativado)
                return;

            _acumulaTempo += gameTime.ElapsedGameTime;

            if (_acumulaTempo >= TimeSpan.FromMilliseconds(100))
            {
                _frameAtualDaColuna++;

                if (_frameAtualDaColuna == TotalColunasNaSprite)
                    _frameAtualDaColuna = 0;

                _acumulaTempo = TimeSpan.Zero;
            }

            _regiaoDaTextura.X = _frameAtualDaColuna * _frameLargura;
            _regiaoDaTextura.Y = regiaoPosY;
        }

        public void Draw(ref SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_textura, Posicao, _regiaoDaTextura, Color.White);
        }
    }
}
