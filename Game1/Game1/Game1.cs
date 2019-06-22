using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Mono.Helper.Input;
using Mono.Helper;


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

        private SpriteFont font;

        Song music;

        heroi _personagem = new heroi();

        public List<BaseElementoX> listTree = new List<BaseElementoX>();
        public List<Inimigo> listInimigos = new List<Inimigo>();
        public List<Elemento> listElementos = new List<Elemento>();

        KeyboardHelper key = new KeyboardHelper();
        
        public int faseAtual = 0;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }
       
        protected override void Initialize()
        {
            music = Content.Load<Song>("GOTTA BE YOU");
            //MediaPlayer.Play(music);

            graphics.PreferredBackBufferHeight = 600;
            graphics.PreferredBackBufferWidth = 1200;
            graphics.ApplyChanges();

            base.Initialize();
        }

        protected override void LoadContent()
        {

            spriteBatch = new SpriteBatch(GraphicsDevice);
            Services.AddService(typeof(SpriteBatch), spriteBatch);

            font = Content.Load<SpriteFont>("score");

            _personagem.xFixoCenario = Window.ClientBounds.Width;

            //BARRA DE VIDA
            _personagem.barraVida = new BarraVida(50, 23, 100, 25, Content.Load<Texture2D>("Vida"), Content.Load<Texture2D>("Barra_vida"),Content.Load<Texture2D>("fundo_barra"), Color.White, 3, Content.Load<Texture2D>("qtd_vida"));

            // INSTANCIAMENTO DO CINTO
            _personagem.cinto = new Cinto(600, 25, 400, 25, Content.Load<Texture2D>("buraco"), Color.White);
            _personagem.mochila = new Mochila((Window.ClientBounds.Width-100), 25, 50, 25, Content.Load<Texture2D>("buraco"), Color.White);
            _personagem.armaEquipada = new ArmaEquipada(370, 25, 50, 25, Content.Load<Texture2D>("buraco"), Color.White);

            _personagem.LoadContent(Content, "character");

            #region Fase 0 - Tutorial

            LoadNivel(0, true);


            #endregion

        }

        protected  void LoadNivel( int fase, bool reloadElementos)
        {
            switch (fase)
            {
                case 0:
                    {
                        cenario = new Cenario(0, 0, Content.Load<Texture2D>("back1"), Color.White, Window.ClientBounds.Width, Window.ClientBounds.Height);

                        listTree.AddRange(
                         new List<BaseElementoX>()
                         {
                              new BaseElementoX(-50, 130, 300, 400, Content.Load<Texture2D>("casa2"), Color.White),
                              new BaseElementoX(350, 130, 300, 400, Content.Load<Texture2D>("casa3"), Color.White),
                              new BaseElementoX(750, 130, 300, 400, Content.Load<Texture2D>("casa2"), Color.White),
                              new BaseElementoX(1150, 160, 300, 400, Content.Load<Texture2D>("casa4"), Color.White)

                         }
                         );

                        listInimigos.AddRange(
                        new List<Inimigo>()
                        {
                        //new Inimigo("zumbie1",100, 2000,420,2, 4, 6,0, 300, 150,450),
                        new Inimigo("esqueleto1",50, 100, 520, 0, 4, 7, 60, 180, 0, 120,200, 10, 100,700),
                        //new Inimigo("golem2",300, 2800, 380, 2, 4, 7, 200, 580, 0, 400, 200)

                        }
                        );

                        foreach (var inimigo in listInimigos)
                        {
                            inimigo.LoadContent(Content, inimigo.nome);
                        }
                        if (reloadElementos)
                        {
                            listElementos.AddRange(
                            new List<Elemento>()
                            {
                                new Elemento("pocao", 400, 500, 60, 60, Content.Load<Texture2D>("poção"), Color.White, 30, 0),
                                new Elemento("espada", 1000, 500, 100, 100, Content.Load<Texture2D>("espada"), Color.White, 0, 100, 200),
                                new Elemento("EspadaComum", 1500, 500, 100, 100, Content.Load<Texture2D>("EspadaComum"), Color.White, 0, 30, 100)

                            }
                            );
                        }

                        foreach (var elemento in listElementos)
                        {
                            elemento.xBase = elemento.posicaoInicial;
                        }

                        break;
                    }
                case 1:
                    {

                        cenario = new Cenario(0, 0, Content.Load<Texture2D>("background"), Color.White, Window.ClientBounds.Width, Window.ClientBounds.Height);

                        listTree.AddRange(
                         new List<BaseElementoX>()
                         {
                              new BaseElementoX(50, 200, 200, 300, Content.Load<Texture2D>("Tree3"), Color.White),
                              new BaseElementoX(250, 200, 200, 300, Content.Load<Texture2D>("Tree2"), Color.White),
                              new BaseElementoX(450, 300, 200, 200, Content.Load<Texture2D>("Tree"), Color.White),
                              new BaseElementoX(650, 300, 200, 200, Content.Load<Texture2D>("Tree3"), Color.White),
                              new BaseElementoX(850, 200, 200, 300, Content.Load<Texture2D>("Tree"), Color.White),
                              new BaseElementoX(1050, 300, 200, 200, Content.Load<Texture2D>("Tree"), Color.White),
                              new BaseElementoX(1250, 200, 200, 300, Content.Load<Texture2D>("Tree3"), Color.White),
                              new BaseElementoX(1450, 200, 200, 300, Content.Load<Texture2D>("Tree2"), Color.White),
                              new BaseElementoX(150, 300, 200, 200, Content.Load<Texture2D>("Tree3"), Color.White),
                              new BaseElementoX(350, 200, 200, 300, Content.Load<Texture2D>("Tree2"), Color.White),
                              new BaseElementoX(550, 300, 200, 200, Content.Load<Texture2D>("Tree"), Color.White),
                              new BaseElementoX(750, 200, 200, 300, Content.Load<Texture2D>("Tree3"), Color.White),
                              new BaseElementoX(950, 300, 200, 200, Content.Load<Texture2D>("Tree"), Color.White),
                              new BaseElementoX(1150, 300, 200, 200, Content.Load<Texture2D>("Tree"), Color.White),
                              new BaseElementoX(1350, 200, 200, 300, Content.Load<Texture2D>("Tree3"), Color.White),
                              new BaseElementoX(1550, 300, 200, 200, Content.Load<Texture2D>("Tree2"), Color.White)
                         }
                         );

                        break;
                    }


            }


        }
        
        protected override void UnloadContent()
        {

        }
        
        protected override void Update(GameTime gameTime)
        {
            if (_personagem.barraVida.vidaAgora <= 0 && _personagem.barraVida.qtdVida<=0)
            {
                if (Mouse.GetState().LeftButton == ButtonState.Pressed)
                {
                    Exit();
                }
                return;
            }
                if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            //elemento.Update(gameTime);
            if (_personagem.nome == "" && _personagem.distance <= 100)
            {
                key.Update(gameTime, _personagem);
                
                return;
            }

            if (_personagem.barraVida.vidaAgora <= 0)
            {
                _personagem.Posicao.X = 0;
                _personagem.barraVida.vidaAgora = 100;
                _personagem.barraVida.qtdVida--;
                _personagem.armaEquipada.Remover();
                listInimigos.Clear();
                listTree.Clear();
                LoadNivel(faseAtual, false);
              
            }

            _personagem.Mover(ref gameTime);


            foreach (var inimigo in listInimigos)
            {
                inimigo.Mover(ref gameTime, _personagem);
            }

            if (_personagem.distance >= 5000 && faseAtual == 0)
            {
                if (!listInimigos.Any(x => x.isVisible))
                {
                    faseAtual++;
                    listTree.Clear();
                    listInimigos.Clear();
                    LoadNivel(faseAtual, true);
                    _personagem.distance = 0;
                }
               
            }
            if (_personagem.distance >= 20000 && faseAtual == 1)
            {
                faseAtual++;
                listTree.Clear();
                LoadNivel(faseAtual, true);
            }

            base.Update(gameTime);
            
        }
        
        protected override void Draw(GameTime gameTime)
        {
            //if (_personagem.barraVida.GameOver() != -1)
            //{
                GraphicsDevice.Clear(Color.Red);
            if (_personagem.barraVida.qtdVida <= 0 && _personagem.barraVida.vidaAgora <= 0)
            {
                spriteBatch.Begin();

                spriteBatch.DrawString(font, "GAME OVER\n SEU LIXO", new Vector2((Window.ClientBounds.Width/2)-100, (Window.ClientBounds.Height/2)-100), Color.Black);

                spriteBatch.End();
                return;
            }
            spriteBatch.Begin();
            

            cenario.Draw(spriteBatch, gameTime);
            spriteBatch.Draw(Content.Load<Texture2D>("Shawn"), new Rectangle(0, 0, Window.ClientBounds.Width, Window.ClientBounds.Height), Color.White);

            spriteBatch.DrawString(font, "Distancia: " + _personagem.distance.ToString(), new Vector2(800, 100), Color.Red);

            foreach (BaseElementoX var in listTree)
            {
                var.Draw(spriteBatch,gameTime,_personagem);
            }

            spriteBatch.DrawString(font, _personagem.nome, new Vector2(50, 5), Color.Red);

            spriteBatch.Draw(Content.Load<Texture2D>("mochila"), new Rectangle((Window.ClientBounds.Width - 180), 25, 80, 60), Color.White);
            spriteBatch.Draw(Content.Load<Texture2D>("cinto2"), new Rectangle(500, 25, 80, 60), Color.White);
            spriteBatch.Draw(Content.Load<Texture2D>("espadaCruzada"), new Rectangle(300, 25, 60, 60), Color.White);

            _personagem.cinto.Draw(spriteBatch, gameTime,_personagem);
            _personagem.mochila.Draw(spriteBatch, gameTime, _personagem);
            _personagem.armaEquipada.Draw(spriteBatch, gameTime, _personagem);
            _personagem.Draw(ref spriteBatch);

            foreach(var inimigo in listInimigos)
            {
                inimigo.Draw(ref spriteBatch);
            }
            

            _personagem.barraVida.Draw(spriteBatch, gameTime, _personagem);


            //TUTORIAL
            if (faseAtual == 0)
            {
                if (_personagem.nome == "")
                {
                    spriteBatch.Draw(Content.Load<Texture2D>("Balawn"), new Rectangle(-20, 70, 700, 160), Color.White);
                    spriteBatch.DrawString(font, "Digite seu nome e aperte enter para salvar:\n" + key._stringValue, new Vector2(60, 110), Color.Black);
                }
                if (_personagem.distance < 100 && _personagem.nome != "")
                {
                    spriteBatch.Draw(Content.Load<Texture2D>("Balawn"), new Rectangle(-20, 70, 700, 160), Color.White);
                    spriteBatch.DrawString(font, "USE OS COMANDOS PARA SE MOVIMENTAR:\nSETA ESQUERDA OU A: DESLOCAR PARA ESQUERDA\nSETA DIREITA OU D: DESLOCAR A DIREITA", new Vector2(60, 110), Color.Black);
                }
                if (_personagem.distance > 300 && _personagem.distance < 600)
                {
                    spriteBatch.Draw(Content.Load<Texture2D>("Balawn"), new Rectangle(-20, 90, 800, 200), Color.White);
                    spriteBatch.DrawString(font, "PEGUE SEU ELEMENTO:\nBOTAO ESQUERDO: ELEMENTO VAI PARA O CINTO\nBOTAO DIREITO: ELEMENTO VAI PARA MOCHILA\nPARA JOGAR FORA BASTA APERTAR O SCROLL DO MOUSE", new Vector2(60, 110), Color.Black);
                }
                if (_personagem.distance > 1000 && _personagem.distance < 1300)
                {
                    spriteBatch.Draw(Content.Load<Texture2D>("Balawn"), new Rectangle(-20, 90, 800, 220), Color.White);
                    spriteBatch.DrawString(font, "AO POSICIONAR O MOUSE SOBRE ELEMENTOS SEUS\nATRIBUTOS SAO EXIBIDOS. \nPOCOES NO CINTO OU NA MOCHILA PODEM SER\nCONSUMIDAS AO POSICIONAR O MOUSE SOBRE\n ELAS E APERTAR ESPACO.", new Vector2(60, 110), Color.Black);
                }
                if (_personagem.distance > 1500 && _personagem.distance < 1900)
                {
                    spriteBatch.Draw(Content.Load<Texture2D>("Balawn"), new Rectangle(-20, 90, 800, 220), Color.White);
                    spriteBatch.DrawString(font, "ARMAS NO CINTO OU NA MOCHILA PODEM SER\nEQUIPADAS AO POSICIONAR O MOUSE SOBRE\n ELAS E APERTAR ESPACO. \nELAS ALTERAM O DANO CAUSADO A MONSTROS.", new Vector2(60, 110), Color.Black);
                }

                if (_personagem.distance > 2100 && _personagem.distance < 2500)
                {
                    spriteBatch.Draw(Content.Load<Texture2D>("Balawn"), new Rectangle(-20, 90, 800, 220), Color.White);
                    spriteBatch.DrawString(font, "PARA ATACAR INIMIGOS, BASTA CLICAR SOBRE ELES. \nVALE LEMBRAR QUE AS ARMAS POSSUEM DIFERENTE\nALCANCES. \n FIQUE ATENTO, SEU PRIMEIRO INIMIGO ESTA POR PERTO.", new Vector2(60, 110), Color.Black);
                }

            }

            foreach (var elemento in listElementos)
            {
                elemento.Draw(spriteBatch, gameTime, Content, _personagem);
            }
            if (_personagem.distance >= 5000 && faseAtual == 0)
            {
                if (listInimigos.Any(x => x.isVisible))
                {
                    spriteBatch.Draw(Content.Load<Texture2D>("Balawn"), new Rectangle(-20, 70, 700, 160), Color.White);
                    spriteBatch.DrawString(font, "Mate todos os inimigos.", new Vector2(60, 110), Color.Black);
                }

            }

            spriteBatch.End();
            
            base.Draw(gameTime);
        }
    }
}
