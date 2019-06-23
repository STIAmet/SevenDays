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
        Mago maguinho;
        Princesa princesa;
        heroi _personagem = new heroi();

        public int Lore = 0;
        public int texto = -1;
        public int falaMago=-1;
        public int falaPricesa=-1;
        public bool haPocao = false;

        public bool enterApertado = false;
        TimeSpan tempoPress=TimeSpan.Zero;
        public bool mago=false;

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
            maguinho =new Mago(1000, 380, 150, 200, Content.Load<Texture2D>("mago"), Color.White);
        }

        protected  void LoadNivel( int fase, bool reloadElementos)
        {
            switch (fase)
            {
                case 0:
                    {
                        _personagem.Posicao.Y = 450;
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
                        new Inimigo("esqueleto1",15, 4000, 520, 0, 4, 7, 60, 180, 0, 120,200, 50, 1000,1000),
                        //new Inimigo("golem2",300, 2800, 380, 2, 4, 7, 200, 580, 0, 400, 200)
                        //new Inimigo("ogro",30, 1100, 280, 1, 4, 7, -50, 220, 570, 900,120, 200, 800,900),

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
                                new Elemento("EspadaComum", 1000, 500, 100, 100, Content.Load<Texture2D>("EspadaComum"), Color.White, 0, 5, 200),
                                new Elemento("Espada2", 1500, 500, 100, 100, Content.Load<Texture2D>("EspadaComum"), Color.Red, 0, 10, 100)

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
                        _personagem.Posicao.Y = 400;
                        cenario = new Cenario(0, 0, Content.Load<Texture2D>("ForestDay"), Color.White, Window.ClientBounds.Width, Window.ClientBounds.Height);

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

                       listInimigos.AddRange(
                       new List<Inimigo>()
                       {
                           new Inimigo("esqueleto1",30, 1000, 480, 0, 4, 7, 60, 180, 0, 120,200, 50, 1000,800),
                           new Inimigo("esqueleto1",30, 1100, 480, 0, 4, 7, 60, 180, 0, 120,200, 50, 1000,900),

                           new Inimigo("esqueleto1",50, 2440, 480, 0, 4, 7, 60, 180, 0, 120,200, 50, 1000,800),
                           new Inimigo("esqueleto1",50, 2500, 480, 0, 4, 7, 60, 180, 0, 120,200, 50, 1000,800),
                           new Inimigo("esqueleto1",50, 2550, 480, 0, 4, 7, 60, 180, 0, 120,200, 50, 1000,800),
                           new Inimigo("esqueleto1",50, 2400, 480, 0, 4, 7, 60, 180, 0, 120,200, 50, 1000,800),
                           new Inimigo("esqueleto1",50, 2600, 480, 0, 4, 7, 60, 180, 0, 120,200, 50, 1000,800),
                           new Inimigo("esqueleto1",50, 2510, 480, 0, 4, 7, 60, 180, 0, 120,200, 50, 1000,800),

                           new Inimigo("groot",100, 4500, 380, 1, 4, 5, 0, 300, 150, 450,120, 200, 800,900),
                           new Inimigo("groot",100, 4550, 380, 1, 4, 5, 0, 300, 150, 450,120, 200, 800,900),
                           new Inimigo("groot",100, 4600, 380, 1, 4, 5, 0, 300, 150, 450,120, 200, 800,900),
                           new Inimigo("groot",100, 4650, 380, 1, 4, 5, 0, 300, 150, 450,120, 200, 800,900),
                           new Inimigo("groot",100, 4700, 380, 1, 4, 5, 0, 300, 150, 450,120, 200, 800,900),
                           new Inimigo("groot",100, 4750, 380, 1, 4, 5, 0, 300, 150, 450,120, 200, 800,900),
                           new Inimigo("groot",100, 4800, 380, 1, 4, 5, 0, 300, 150, 450,120, 200, 800,900),




                            //new Inimigo("zumbie1",100, 2000,420,2, 4, 6,0, 300, 150,450),
                            //new Inimigo("esqueleto1",50, 4000, 520, 0, 4, 7, 60, 180, 0, 120,200, 50, 1000,1000),
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
                                new Elemento("pocao2", 1500, 500, 60, 60, Content.Load<Texture2D>("poção"), Color.White, 10, 0),
                                new Elemento("pocao3", 1520, 500, 60, 60, Content.Load<Texture2D>("poção"), Color.White, 10, 0),
                                new Elemento("pocao4", 1540, 500, 60, 60, Content.Load<Texture2D>("poção"), Color.White, 10, 0),

                                new Elemento("espadaUm", 3200, 500, 100, 100, Content.Load<Texture2D>("espada"), Color.Blue, 0, 30, 100)

                                /*new Elemento("pocao", 400, 500, 60, 60, Content.Load<Texture2D>("poção"), Color.White, 30, 0),
                                new Elemento("EspadaComum", 1000, 500, 100, 100, Content.Load<Texture2D>("EspadaComum"), Color.White, 0, 10, 200),
                                new Elemento("EspadaComum", 1500, 500, 100, 100, Content.Load<Texture2D>("EspadaComum"), Color.Blue, 0, 30, 100)*/

                            }
                            );
                        }

                        foreach (var elemento in listElementos)
                        {
                            if(elemento != null)
                            elemento.xBase = elemento.posicaoInicial;
                        }

                        listElementos.AddRange(_personagem.cinto.elementos);
                        listElementos.AddRange(_personagem.mochila.elementos);
                        listElementos.Add(_personagem.armaEquipada.elementos);
                        
                        break;
                        

                    }

                case 2:
                    {
                        _personagem.Posicao.Y = 450;
                        cenario = new Cenario(0, 0, Content.Load<Texture2D>("backNeve"), Color.White, Window.ClientBounds.Width, Window.ClientBounds.Height);

                        listTree.AddRange(
                         new List<BaseElementoX>()
                         {
                              new BaseElementoX(50, 250, 200, 300, Content.Load<Texture2D>("arvoreNeve"), Color.White),
                              new BaseElementoX(250, 380, 200, 200, Content.Load<Texture2D>("pedraNeve"), Color.White),
                              new BaseElementoX(450, 350, 200, 200, Content.Load<Texture2D>("arvoreNeve"), Color.White),
                              new BaseElementoX(650, 380, 200, 200, Content.Load<Texture2D>("pedraNeve"), Color.White),
                              new BaseElementoX(850, 250, 200, 300, Content.Load<Texture2D>("arvoreNeve"), Color.White),
                              new BaseElementoX(1050, 380, 200, 200, Content.Load<Texture2D>("pedraNeve"), Color.White),
                              new BaseElementoX(1250, 250, 200, 300, Content.Load<Texture2D>("arvoreNeve"), Color.White),
                              new BaseElementoX(1450, 380, 200, 200, Content.Load<Texture2D>("pedraNeve"), Color.White),
                              new BaseElementoX(150, 350, 200, 200, Content.Load<Texture2D>("arvoreNeve"), Color.White),
                              new BaseElementoX(350, 380, 200, 200, Content.Load<Texture2D>("pedraNeve"), Color.White),
                              new BaseElementoX(550, 350, 200, 200, Content.Load<Texture2D>("arvoreNeve"), Color.White),
                              new BaseElementoX(750, 380, 200, 200, Content.Load<Texture2D>("pedraNeve"), Color.White),
                              new BaseElementoX(950, 350, 200, 200, Content.Load<Texture2D>("arvoreNeve"), Color.White),
                              new BaseElementoX(1150, 380, 200, 200, Content.Load<Texture2D>("pedraNeve"), Color.White),
                              new BaseElementoX(1350, 250, 200, 300, Content.Load<Texture2D>("arvoreNeve"), Color.White),
                              new BaseElementoX(1550, 380, 200, 200, Content.Load<Texture2D>("pedraNeve"), Color.White)
                         }
                         );

                        listInimigos.AddRange(
                       new List<Inimigo>()
                       {
                           new Inimigo("golem2",300, 1500, 380, 2, 4, 7, 200, 580, 0, 400, 200, 200, 1000,800),
                           new Inimigo("golem2",300, 1500, 380, 2, 4, 7, 200, 580, 0, 400, 200, 200, 1000,800),

                           new Inimigo("golem2",300, 2800, 380, 2, 4, 7, 200, 580, 0, 400, 200, 200, 1000,800),
                           new Inimigo("golem2",300, 2800, 380, 2, 4, 7, 200, 580, 0, 400, 200, 200, 1000,800),
                           new Inimigo("golem2",300, 2800, 380, 2, 4, 7, 200, 580, 0, 400, 200, 200, 1000,800),
                           new Inimigo("golem2",300, 2800, 380, 2, 4, 7, 200, 580, 0, 400, 200, 200, 1000,800),
                           new Inimigo("golem2",300, 2800, 380, 2, 4, 7, 200, 580, 0, 400, 200, 200, 1000,800),
                           new Inimigo("golem2",300, 2800, 380, 2, 4, 7, 200, 580, 0, 400, 200, 200, 1000,800),

                           new Inimigo("ogro",300, 4000, 280, 3, 4, 7, -50, 220, 570, 900,300, 200, 800,900),
                           new Inimigo("ogro",300, 4050, 280, 3, 4, 7, -50, 220, 570, 900,110, 200, 800,900),
                           new Inimigo("ogro",300, 4100, 280, 3, 4, 7, -50, 220, 570, 900,180, 200, 800,900),
                           new Inimigo("ogro",300, 4150, 280, 3, 4, 7, -50, 220, 570, 900,300, 200, 800,900),
                           new Inimigo("ogro",300, 4200, 280, 3, 4, 7, -50, 220, 570, 900,260, 200, 800,900),
                           new Inimigo("ogro",300, 4250, 280, 3, 4, 7, -50, 220, 570, 900,220, 200, 800,900),
                           new Inimigo("ogro",300, 4300, 280, 3, 4, 7, -50, 220, 570, 900,200, 200, 800,900),




                            //new Inimigo("zumbie1",100, 2000,420,2, 4, 6,0, 300, 150,450),
                            //new Inimigo("esqueleto1",50, 4000, 520, 0, 4, 7, 60, 180, 0, 120,200, 50, 1000,1000),
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
                                new Elemento("pocao2", 300, 500, 60, 60, Content.Load<Texture2D>("poção"), Color.White, 15, 0),
                                new Elemento("pocao3", 320, 500, 60, 60, Content.Load<Texture2D>("poção"), Color.White, 15, 0),
                                new Elemento("pocao4", 340, 500, 60, 60, Content.Load<Texture2D>("poção"), Color.White, 15, 0),

                                new Elemento("espadaUm", 3200, 500, 100, 100, Content.Load<Texture2D>("espada"), Color.Red, 0, 50, 100)

                                /*new Elemento("pocao", 400, 500, 60, 60, Content.Load<Texture2D>("poção"), Color.White, 30, 0),
                                new Elemento("EspadaComum", 1000, 500, 100, 100, Content.Load<Texture2D>("EspadaComum"), Color.White, 0, 10, 200),
                                new Elemento("EspadaComum", 1500, 500, 100, 100, Content.Load<Texture2D>("EspadaComum"), Color.Blue, 0, 30, 100)*/

                            }
                            );
                        }

                        foreach (var elemento in listElementos)
                        {
                            if (elemento != null)
                                elemento.xBase = elemento.posicaoInicial;
                        }

                        listElementos.AddRange(_personagem.cinto.elementos);
                        listElementos.AddRange(_personagem.mochila.elementos);
                        listElementos.Add(_personagem.armaEquipada.elementos);

                        break;
                
                    }

                case 3:
                    {
                        _personagem.Posicao.Y = 400;
                        cenario = new Cenario(0, 0, Content.Load<Texture2D>("forestDark"), Color.White, Window.ClientBounds.Width, Window.ClientBounds.Height);

                        listTree.AddRange(
                         new List<BaseElementoX>()
                         {
                              new BaseElementoX(50, 230, 200, 300, Content.Load<Texture2D>("arvoreDark1"), Color.White),
                              new BaseElementoX(250, 330, 200, 200, Content.Load<Texture2D>("arvoreDark2"), Color.White),
                              new BaseElementoX(450, 300, 200, 230, Content.Load<Texture2D>("arvoreDark3"), Color.White),
                              new BaseElementoX(650, 330, 200, 200, Content.Load<Texture2D>("arvoreDark1"), Color.White),
                              new BaseElementoX(850, 230, 200, 300, Content.Load<Texture2D>("arvoreDark2"), Color.White),
                              new BaseElementoX(1050, 330, 200, 200, Content.Load<Texture2D>("arvoreDark3"), Color.White),
                              new BaseElementoX(1250, 230, 200, 300, Content.Load<Texture2D>("arvoreDark1"), Color.White),
                              new BaseElementoX(1450, 330, 200, 200, Content.Load<Texture2D>("arvoreDark2"), Color.White),
                              new BaseElementoX(150, 300, 200, 230, Content.Load<Texture2D>("arvoreDark3"), Color.White),
                              new BaseElementoX(350, 330, 200, 200, Content.Load<Texture2D>("arvoreDark1"), Color.White),
                              new BaseElementoX(550, 300, 200, 230, Content.Load<Texture2D>("arvoreDark2"), Color.White),
                              new BaseElementoX(750, 330, 200, 200, Content.Load<Texture2D>("arvoreDark3"), Color.White),
                              new BaseElementoX(950, 300, 200, 230, Content.Load<Texture2D>("arvoreDark1"), Color.White),
                              new BaseElementoX(1150, 330, 200, 200, Content.Load<Texture2D>("arvoreDark2"), Color.White),
                              new BaseElementoX(1350, 230, 200, 300, Content.Load<Texture2D>("arvoreDark3"), Color.White),
                              new BaseElementoX(1550, 330, 200, 200, Content.Load<Texture2D>("arvoreDark1"), Color.White)
                         }
                         );

                        listInimigos.AddRange(
                       new List<Inimigo>()
                       {

                           new Inimigo("groot",300, 1500, 380, 3, 4, 5, 0, 300, 150, 450,120, 230, 500,900),
                            new Inimigo("groot",300, 1550, 380, 3, 4, 5, 0, 300, 150, 450,120, 230, 500,900),
                            new Inimigo("groot",300, 1600, 380, 3, 4, 5, 0, 300, 150, 450,120, 230, 500,900),
                            new Inimigo("groot",300, 1650, 380, 3, 4, 5, 0, 300, 150, 450,120, 230, 500,900),
                            new Inimigo("groot",300, 1700, 380, 3, 4, 5, 0, 300, 150, 450,120, 230, 500,900),
                            new Inimigo("groot",300, 1750, 380, 3, 4, 5, 0, 300, 150, 450,120, 230, 500,900),
                            new Inimigo("groot",300, 1800, 380, 3, 4, 5, 0, 300, 150, 450,120, 230, 500,900),

                            new Inimigo("zumbie1",400, 4000,420,3, 4, 6,0, 300, 150,450,210,200,300),
                            new Inimigo("zumbie1",400, 4050,420,3, 4, 6,0, 300, 150,450,200,200,300),
                            new Inimigo("zumbie1",400, 4100,420,3, 4, 6,0, 300, 150,450,270,200,300),
                            new Inimigo("zumbie1",400, 4150,420,3, 4, 6,0, 300, 150,450,280,200,300),
                            new Inimigo("zumbie1",400, 4200,420,3, 4, 6,0, 300, 150,450,200,200,300),
                            new Inimigo("zumbie1",400, 4250,420,3, 4, 6,0, 300, 150,450,240,200,300),
                            new Inimigo("zumbie1",400, 4300,420,3, 4, 6,0, 300, 150,450,200,200,300),
                            new Inimigo("zumbie1",400, 4350,420,3, 4, 6,0, 300, 150,450,230,200,300),
                            new Inimigo("zumbie1",400, 4400,420,3, 4, 6,0, 300, 150,450,200,200,300),
                            new Inimigo("zumbie1",400, 4450,420,3, 4, 6,0, 300, 150,450,210,200,300),
                            new Inimigo("zumbie1",400, 4500,420,3, 4, 6,0, 300, 150,450,200,200,300),
                            new Inimigo("zumbie1",400, 4550,420,3, 4, 6,0, 300, 150,450,260,200,300),
                            new Inimigo("zumbie1",400, 4600,420,3, 4, 6,0, 300, 150,450,200,200,300),
                            new Inimigo("zumbie1",400, 4650,420,3, 4, 6,0, 300, 150,450,250,200,300),
                            new Inimigo("zumbie1",400, 4700,420,3, 4, 6,0, 300, 150,450,240,200,300),
                            new Inimigo("zumbie1",400, 4750,420,3, 4, 6,0, 300, 150,450,200,200,300),
                            new Inimigo("zumbie1",400, 4800,420,3, 4, 6,0, 300, 150,450,200,200,300),
                            new Inimigo("zumbie1",400, 4850,420,3, 4, 6,0, 300, 150,450,230,200,300),
                            new Inimigo("zumbie1",400, 4900,420,3, 4, 6,0, 300, 150,450,220,200,300),
                            new Inimigo("zumbie1",400, 4950,420,3, 4, 6,0, 300, 150,450,200,200,300),
                            new Inimigo("zumbie1",400, 5000,420,3, 4, 6,0, 300, 150,450,230,200,300),
                            new Inimigo("zumbie1",400, 5050,420,3, 4, 6,0, 300, 150,450,260,200,300),
                            new Inimigo("zumbie1",400, 5100,420,3, 4, 6,0, 300, 150,450,280,200,300),
                            new Inimigo("zumbie1",400, 5150,420,3, 4, 6,0, 300, 150,450,240,200,300),
                            new Inimigo("zumbie1",400, 5200,420,3, 4, 6,0, 300, 150,450,230,200,300),
                            new Inimigo("zumbie1",400, 5250,420,3, 4, 6,0, 300, 150,450,220,200,300),
                            new Inimigo("zumbie1",400, 5300,420,3, 4, 6,0, 300, 150,450,290,200,300)




                           //new Inimigo("zumbie1",100, 2000,420,2, 4, 6,0, 300, 150,450),
                           //new Inimigo("esqueleto1",50, 4000, 520, 0, 4, 7, 60, 180, 0, 120,200, 50, 1000,1000),
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
                               

                                new Elemento("espadaBoa", 2500, 500, 100, 100, Content.Load<Texture2D>("espada"), Color.Gold, 0, 70, 200)

                                /*new Elemento("pocao", 400, 500, 60, 60, Content.Load<Texture2D>("poção"), Color.White, 30, 0),
                                new Elemento("EspadaComum", 1000, 500, 100, 100, Content.Load<Texture2D>("EspadaComum"), Color.White, 0, 10, 200),
                                new Elemento("EspadaComum", 1500, 500, 100, 100, Content.Load<Texture2D>("EspadaComum"), Color.Blue, 0, 30, 100)*/

                            }
                            );
                        }

                        foreach (var elemento in listElementos)
                        {
                            if (elemento != null)
                                elemento.xBase = elemento.posicaoInicial;
                        }

                        listElementos.AddRange(_personagem.cinto.elementos);
                        listElementos.AddRange(_personagem.mochila.elementos);
                        listElementos.Add(_personagem.armaEquipada.elementos);

                        break;

                    }
                case 4:
                    {
                        _personagem.Posicao.Y = 400;
                        cenario = new Cenario(0, 0, Content.Load<Texture2D>("BossCenary"), Color.White, Window.ClientBounds.Width, Window.ClientBounds.Height);

                        princesa = new Princesa("princesa", 1000, 600, 400, 3, 4,5,0,260,130,390,250,200,300,10000);
                        princesa.LoadContent(Content, "princesa");

                        foreach (var elemento in listElementos)
                        {
                            if (elemento != null)
                                elemento.xBase = elemento.posicaoInicial;
                        }

                        listElementos.AddRange(_personagem.cinto.elementos);
                        listElementos.AddRange(_personagem.mochila.elementos);
                        listElementos.Add(_personagem.armaEquipada.elementos);

                        break;

                    }

            }


        }
        
        protected override void UnloadContent()
        {

        }
        
        protected override void Update(GameTime gameTime)
        {
            if (faseAtual == 4 && falaPricesa != 10 && princesa.isVisible && Lore!=5)
            {
                tempoPress += gameTime.ElapsedGameTime;
                princesa.Posicao.X = 600;
                _personagem.Posicao.X = princesa.Posicao.X - 200;               
                if (falaPricesa == 1)
                {
                    if (Keyboard.GetState().IsKeyDown(Keys.S))
                    {
                        Lore = 5;
                    }
                    if (Keyboard.GetState().IsKeyDown(Keys.N))
                    {
                        falaPricesa = 10;
                    }
                }
                else
                {
                    
                    if (Keyboard.GetState().IsKeyDown(Keys.Enter))
                    {
                        if (falaPricesa == 0 && !haPocao && enterApertado == false)
                        {
                            falaPricesa = 10;
                            
                        }
                        else
                        if (enterApertado == false)
                        {
                            falaPricesa++;
                        }
                        enterApertado = true;

                    }
                    if (tempoPress >= TimeSpan.FromMilliseconds(2000))
                    {
                        enterApertado = false;
                        tempoPress = TimeSpan.Zero;
                    }
                }
                return;

            }
            if(mago==true && (_personagem.Posicao.X - maguinho.xBase >=-300))
            {
                maguinho.xBase = 600;        
                _personagem.Posicao.X = maguinho.xBase - 200;
                tempoPress += gameTime.ElapsedGameTime;
                if (Keyboard.GetState().IsKeyDown(Keys.Enter))
                {

                    if (enterApertado == false)
                    {
                        falaMago++;
                    }
                    enterApertado = true;
                    if (falaMago == 2)
                    {
                        mago = false;
                        Lore = 3;
                    }

                }
                if (tempoPress >= TimeSpan.FromMilliseconds(2000))
                {
                    enterApertado = false;
                    tempoPress = TimeSpan.Zero;
                }               
                return;
            }
            if (Lore == 0)
            {
                cenario = new Cenario(0, 0, Content.Load<Texture2D>("startTutorial"), Color.White, Window.ClientBounds.Width, Window.ClientBounds.Height);
                tempoPress += gameTime.ElapsedGameTime;
                if (Keyboard.GetState().IsKeyDown(Keys.Enter))
                {
                    
                    if (enterApertado == false)
                    {
                        texto++;
                    }
                    enterApertado = true;
                    if (texto == 3)
                    {
                        Lore = -1;
                        #region Fase 0 - Tutorial
                        LoadNivel(0, true);
                    }

                    #endregion
                }
                if (tempoPress >= TimeSpan.FromMilliseconds(2000))
                {
                    enterApertado = false;
                    tempoPress = TimeSpan.Zero;
                }

                return;
            }

            if (Lore == 1)
            {
                cenario = new Cenario(0, 0, Content.Load<Texture2D>("startFloresta"), Color.White, Window.ClientBounds.Width, Window.ClientBounds.Height);

                if (Keyboard.GetState().IsKeyDown(Keys.Enter))
                {
                    Lore = -1;
                    #region Fase 1 - Floresta 
                    faseAtual++;
                    listTree.Clear();
                    listInimigos.Clear();
                    listElementos.Clear();
                    LoadNivel(faseAtual, true);
                    _personagem.distance = 0;
                    _personagem.Posicao.X = 0;


                    #endregion
                }

                return;
            }

            if (Lore == 2)
            {
                cenario = new Cenario(0, 0, Content.Load<Texture2D>("startMontanha"), Color.White, Window.ClientBounds.Width, Window.ClientBounds.Height);

                if (Keyboard.GetState().IsKeyDown(Keys.Enter))
                {
                    Lore = -1;
                    #region Fase 1 - Floresta
                    faseAtual++;
                    listTree.Clear();
                    listInimigos.Clear();
                    listElementos.Clear();
                    LoadNivel(faseAtual, true);
                    _personagem.distance = 0;
                    _personagem.Posicao.X = 0;


                    #endregion
                }

                return;
            }

            if (Lore == 3)
            {
                cenario = new Cenario(0, 0, Content.Load<Texture2D>("startFloresta"), Color.White, Window.ClientBounds.Width, Window.ClientBounds.Height);
                tempoPress += gameTime.ElapsedGameTime;
                if (Keyboard.GetState().IsKeyDown(Keys.Enter))
                {
                    if ( enterApertado ==false)
                    {

                        Lore = -1;
                        #region Fase 1 - Floresta
                        faseAtual++;
                        listTree.Clear();
                        listInimigos.Clear();
                        listElementos.Clear();
                        LoadNivel(faseAtual, true);
                        _personagem.distance = 0;
                        _personagem.Posicao.X = 0;
                    }
                    enterApertado = true;

                    #endregion
                }
                if (tempoPress >= TimeSpan.FromMilliseconds(2000))
                {
                    enterApertado = false;
                    tempoPress = TimeSpan.Zero;
                }

                return;
            }

            if (Lore == 4)
            {
                cenario = new Cenario(0, 0, Content.Load<Texture2D>("startVilaBack"), Color.White, Window.ClientBounds.Width, Window.ClientBounds.Height);

                if (Keyboard.GetState().IsKeyDown(Keys.Enter))
                {
                    Lore = -1;
                    #region Fase 1 - Floresta
                    faseAtual++;
                    listTree.Clear();
                    listInimigos.Clear();
                    listElementos.Clear();
                    LoadNivel(faseAtual, true);
                    _personagem.distance = 0;
                    _personagem.Posicao.X = 0;


                    #endregion
                }

                return;
            }

            if (Lore == 5)
            {
                cenario = new Cenario(0, 0, Content.Load<Texture2D>("finalRuim"), Color.White, Window.ClientBounds.Width, Window.ClientBounds.Height);

                if (Keyboard.GetState().IsKeyDown(Keys.Enter))
                {
                    Exit();

                }

                return;
            }

            if (Lore == 6)
            {
                cenario = new Cenario(0, 0, Content.Load<Texture2D>("finalBom"), Color.White, Window.ClientBounds.Width, Window.ClientBounds.Height);

                if (Keyboard.GetState().IsKeyDown(Keys.Enter))
                {
                    Exit();
                }

                return;
            }

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
                _personagem.distance = 0;
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
            if(faseAtual ==4 && falaPricesa == 10)
            princesa.Mover(ref gameTime, _personagem);

            if (_personagem.distance >= 5000 && faseAtual == 0)
            {
                if (!listInimigos.Any(x => x.isVisible))
                {                  
                    Lore = 1;
                }
               
            }
            if (_personagem.distance >= 5500 && faseAtual == 1)
            {
                if (!listInimigos.Any(x => x.isVisible))
                {
                    Lore = 2;
                }
            }

            if (_personagem.distance >= 5000 && faseAtual == 2)
            {
                if (!listInimigos.Any(x => x.isVisible))
                {
                    mago = true;

                }
            }

            if (_personagem.distance >= 6000 && faseAtual == 3)
            {
                if (!listInimigos.Any(x => x.isVisible))
                {
                    Lore = 4;

                }
            }
            if (faseAtual == 4 && princesa.vida <= 0)
            {
                Lore = 6;
            }

            base.Update(gameTime);
            
        }
        
        protected override void Draw(GameTime gameTime)
        {
            //if (_personagem.barraVida.GameOver() != -1)
            //{
                GraphicsDevice.Clear(Color.Red);

            if (Lore ==0)
            {
                spriteBatch.Begin();
                cenario.Draw(spriteBatch, gameTime);
                spriteBatch.Draw(Content.Load<Texture2D>("quadroFala"), new Rectangle( 300, 100, 600, 350), Color.White);
                spriteBatch.DrawString(font, "Enter para continuar", new Vector2(Window.ClientBounds.Width - 250, 10), Color.Black);

                if (texto==0)
                    spriteBatch.DrawString(font, "Existe uma historia, que por muitas geracoes\nfoi contada... \nEla se inicia no antigo reino de Anar, onde ha\nmuito tempo nada crescia ou florescia, e a\npopulacao lutava cada vez mais para nao morrer\nde fome.A princesa do reino nada podia fazer\ncom relacao a isso, ja que estava muito doente e\nnao lhe restavam forcas para governar.\n", new Vector2(325, 150),  Color.DarkGray );
                if(texto==1)
                    spriteBatch.DrawString(font, "Desesperados com essa situacao, os\nexploradores do reino encontraram uma possivel\ncura. No entanto, alem do fato de ser uma\njornada longa e ardua, ela estava sob os\ncuidados de um antigo mago, que se recusava a\nentrega - la.Para contornar essa situacao, um\ntitulo de nobreza foi oferecido para quem\nconseguisse essa cura.Nesse mesmo reino, vivia\num jovem solitario.", new Vector2(325, 150), Color.DarkGray);
                if(texto==2)
                    spriteBatch.DrawString(font, "Nao lembrava-se de algum dia ter tido familia\nou amigos. Nao via sentido em sua vida ate ver\nesse anuncio real, e sentiu como se aquela\nrecompensa fosse algo que preenchesse o seu\nvazio.\n\nE assim se inicia a nossa historia!\n...", new Vector2(325, 150), Color.DarkGray);
       
                    spriteBatch.End();
                return;
            }

            if (Lore == 1)
            {
                spriteBatch.Begin();
                cenario.Draw(spriteBatch, gameTime);
                spriteBatch.DrawString(font, _personagem.nome+", agora equipado, chega na floresta Alaco, que divide o reino de Anar e a montanha de\nMaranwe, seu destino.Mas talvez esse armamento nao seja o suficiente para impedir as criaturas\nmagicas do lugar de o machucarem...ou ate pior!!", new Vector2(50, 450), Color.DarkGray);

                spriteBatch.End();
                return;
            }

            if (Lore == 2)
            {
                spriteBatch.Begin();
                cenario.Draw(spriteBatch, gameTime);
                spriteBatch.Draw(Content.Load<Texture2D>("quadroFala"), new Rectangle(300, 100, 600, 350), Color.White);
                spriteBatch.DrawString(font, _personagem.nome+" finalmente chega a Maranwe.E possivel\nnotar um ar mistico rondando o local.\n O mago esta por perto...", new Vector2(325, 150), Color.DarkGray);

                spriteBatch.End();
                return;
            }
            if (Lore == 3)
            {
                spriteBatch.Begin();
                cenario.Draw(spriteBatch, gameTime);
                spriteBatch.DrawString(font, "Com a cura em maos, "+_personagem.nome + " retorna pelo mesmo caminho que veio, mesmo sabendo se trata de um\ncaminho conhecido, parece que algo mudou... \nAlaco, a floresta escantada, parece mais...sombria...", new Vector2(50, 450), Color.DarkGray);
                
                spriteBatch.End();
                return;
            }

            if (Lore == 4)
            {
                spriteBatch.Begin();
                cenario.Draw(spriteBatch, gameTime);
                spriteBatch.Draw(Content.Load<Texture2D>("quadroFala"), new Rectangle(300, 100, 600, 350), Color.White);
                spriteBatch.DrawString(font, _personagem.nome+" sente que algo esta errado. \nO que aconteceu com o seu lar?\nE melhor investigar...", new Vector2(325, 150), Color.DarkGray);

                spriteBatch.End();
                return;
            }
            if (Lore == 5)
            {
                spriteBatch.Begin();
                cenario.Draw(spriteBatch, gameTime);
                spriteBatch.Draw(Content.Load<Texture2D>("quadroFala"), new Rectangle(300, 100, 600, 350), Color.White);
                spriteBatch.DrawString(font,  "Ao se unir a princesa, voce foi responsavel pela\nmorte de varios inocentes. ", new Vector2(325, 150), Color.DarkGray);

                spriteBatch.End();
                return;
            }

            if (Lore == 6)
            {
                spriteBatch.Begin();
                cenario.Draw(spriteBatch, gameTime);
                spriteBatch.Draw(Content.Load<Texture2D>("quadroFala"), new Rectangle(300, 100, 600, 350), Color.White);
                spriteBatch.DrawString(font, "Gracas a voce, o povo de Anar conheceu a paz. \nComo recompensa por seus atos heroicos, \nfoi coroado como o novo rei.", new Vector2(325, 150), Color.DarkGray);

                spriteBatch.End();
                return;
            }


            if (_personagem.barraVida.qtdVida <= 0 && _personagem.barraVida.vidaAgora <= 0)
            {
                spriteBatch.Begin();

                spriteBatch.DrawString(font, "GAME OVER\n SEU LIXO", new Vector2((Window.ClientBounds.Width/2)-100, (Window.ClientBounds.Height/2)-100), Color.Black);

                spriteBatch.End();
                return;
            }

            spriteBatch.Begin();
            

            cenario.Draw(spriteBatch, gameTime);

            
            if (faseAtual == 0 || faseAtual == 3 || faseAtual==4)
            {
                spriteBatch.Draw(Content.Load<Texture2D>("Shawn"), new Rectangle(0, 0, Window.ClientBounds.Width, Window.ClientBounds.Height), Color.White);
            }
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
                if(elemento !=null)
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
            if (_personagem.distance >= 6000 && faseAtual == 1)
            {
                if (listInimigos.Any(x => x.isVisible))
                {
                    spriteBatch.Draw(Content.Load<Texture2D>("Balawn"), new Rectangle(-20, 70, 700, 160), Color.White);
                    spriteBatch.DrawString(font, "Mate todos os inimigos.", new Vector2(60, 110), Color.Black);
                }

            }

            if (faseAtual == 2 && mago == true)
            {
                maguinho.Draw(spriteBatch, gameTime, Content, _personagem);
            }
            if (mago && falaMago == 0)
            {
                spriteBatch.Draw(Content.Load<Texture2D>("Balawn"), new Rectangle(maguinho.xBase+50, maguinho.yBase-100, 500, 200), Color.White);
                spriteBatch.DrawString(font, "Ola aventureiro. Imagino que \ntenha vindo em busca da pocao \nsim, eu sei de tudo, afinal, sou o \nmago dos magos. ", new Vector2(maguinho.xBase + 100, maguinho.yBase - 75), Color.Black);
            }
            if (mago && falaMago == 1)
            {
                spriteBatch.Draw(Content.Load<Texture2D>("Balawn"), new Rectangle(maguinho.xBase + 50, maguinho.yBase - 100, 500, 200), Color.White);
                spriteBatch.DrawString(font, "Nunca alguem foi tao persistente \ne chegou tao longe quanto voce. \nE digno de levar a pocao", new Vector2(maguinho.xBase + 100, maguinho.yBase - 75), Color.Black);
                if (_personagem.mochila.elementos.Count == 0 || _personagem.mochila.elementos.Peek().nome != "SuperPocao")
                    _personagem.mochila.Add(new Elemento("SuperPocao", 400, 550, 100, 100, Content.Load<Texture2D>("poção"), Color.Blue, 100, 0,0,false));              
            }   

            if(faseAtual == 4)
            {
                princesa.Draw(ref spriteBatch);
                if (falaPricesa == 0)
                {
                    if (_personagem.cinto.elementos.Count(x=>x !=null)!=0)
                    {
                        if (_personagem.cinto.elementos.Any(x => x.nome == "SuperPocao"))
                        {
                            haPocao = true;
                        }

                    }
                    if (_personagem.mochila.elementos.Count != 0)
                    {
                        if (_personagem.mochila.elementos.Any(x => x.nome == "SuperPocao"))
                        {
                            haPocao = true;
                        }

                    }


                    if (haPocao)
                    {
                        spriteBatch.Draw(Content.Load<Texture2D>("Balawn"), new Rectangle((int)princesa.Posicao.X + 50, (int)princesa.Posicao.Y - 250, 500, 350), Color.White);
                        spriteBatch.DrawString(font, "Meus parabens " + _personagem.nome + ",\nvoce e uma das pessoas mais\ncorajosas do reino...\nE tambem umas das que eu\nprecisava manter o mais\nlonge possivel para que eu\npudesse instaurar o meu\nnovo imperio eterno.", new Vector2((int)princesa.Posicao.X + 100, (int)princesa.Posicao.Y - 225), Color.Black);
                    }
                    else
                    {
                        spriteBatch.Draw(Content.Load<Texture2D>("Balawn"), new Rectangle((int)princesa.Posicao.X + 50, (int)princesa.Posicao.Y - 250, 500, 350), Color.White);
                        spriteBatch.DrawString(font, "Meus parabens " + _personagem.nome + ",\nvoce e uma das pessoas mais\ncorajosas do reino... \nMas espere, onde esta a\npocao?\nEU PRECISAVA DELA PARA ME\nTORNAR IMORTAL!!", new Vector2((int)princesa.Posicao.X + 100, (int)princesa.Posicao.Y - 225), Color.Black);
                        
                    }

                }
                if (falaPricesa == 1)
                {
                    spriteBatch.Draw(Content.Load<Texture2D>("Balawn"), new Rectangle((int)princesa.Posicao.X + 50, (int)princesa.Posicao.Y - 200, 500, 300), Color.White);
                    spriteBatch.DrawString(font, "Sabia que o que tem em maos e um\npasso para a imortalidade?\n A chave final que eu precisava. \nEntregue - a para mim e una - se\nao meu imperio, ou morra!\n[  S- para unir-se. \n N- Batalhar. ]", new Vector2((int)princesa.Posicao.X + 100, (int)princesa.Posicao.Y - 180), Color.Black);
                }
            }
            

            spriteBatch.End();
            
            base.Draw(gameTime);
        }
    }
}
