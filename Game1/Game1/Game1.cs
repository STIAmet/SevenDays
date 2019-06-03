using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;


namespace Game1
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Cenario cenario;

        Elemento elemento;

        private SpriteFont font;

        Song music;

        heroi _personagem = new heroi();

        BaseElementoX cenoura,cenoura2,cenoura3;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }
       
        protected override void Initialize()
        {
            music = Content.Load<Song>("GOTTA BE YOU");
            MediaPlayer.Play(music);

            graphics.PreferredBackBufferHeight = 950;
            graphics.PreferredBackBufferWidth = 1700;
            graphics.ApplyChanges();

            base.Initialize();
        }
        
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            Services.AddService(typeof(SpriteBatch), spriteBatch);
            
            font = Content.Load<SpriteFont>("score");
            elemento = new Elemento(this);
            elemento.Position = new Vector2(300, 300);
            elemento.escala =0.5f;

            _personagem.xFixoCenario = Window.ClientBounds.Width;
            cenario = new Cenario(0, 0, Content.Load<Texture2D>("background"), Color.White, Window.ClientBounds.Width, Window.ClientBounds.Height);

            cenoura = new BaseElementoX(700, 570, 200, 200, Content.Load<Texture2D>("idle001"), Color.White);
            cenoura2 = new BaseElementoX(100, 570, 200, 200, Content.Load<Texture2D>("arma"), Color.White);
            cenoura3 = new BaseElementoX(800, 570, 200, 200, Content.Load<Texture2D>("espada"), Color.White);

            _personagem.LoadContent(Content, "character");
        }
        
        protected override void UnloadContent()
        {

        }
        
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            elemento.Update(gameTime);

            _personagem.Mover(ref gameTime);

            base.Update(gameTime);
        }
        
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();

            cenario.Draw(spriteBatch, gameTime);

            if ((((elemento.Position.X-elemento.Texture.Width) <= _personagem.Posicao.X) && ((elemento.Position.X + elemento.Texture.Width) >= _personagem.Posicao.X)) && (((elemento.Position.Y) >= _personagem.Posicao.Y) && (((elemento.Position.Y) ) <= (_personagem.Posicao.Y + elemento.Texture.Height))))
            {
                //spriteBatch.Begin();
                spriteBatch.DrawString(font, "Achou o bagulho porra", new Vector2(0, 200), Color.Black);
                //spriteBatch.End();
            }

            cenoura.Draw(spriteBatch, gameTime,_personagem);
            cenoura2.Draw(spriteBatch, gameTime, _personagem);
            cenoura3.Draw(spriteBatch, gameTime, _personagem);

            _personagem.Draw(ref spriteBatch);

            //spriteBatch.Draw(Content.Load<Texture2D>("espada"), new Vector2(500, 0), null, Color.White, 0f, Vector2.Zero, 0.5f, SpriteEffects.None,0);
                

            elemento.Draw(spriteBatch, gameTime);
            spriteBatch.End();
            
            base.Draw(gameTime);
        }
    }
}
