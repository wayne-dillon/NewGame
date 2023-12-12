using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

public class Main : Game
{
    private MainMenu mainMenu;
    private AboutMenu aboutMenu;
    private GamePlay gamePlay;
    private LevelEditor levelEditor;
    private Music music;
    public UI ui;

    public Sprite background;

    Cursor cursor;

    public Main()
    {
        Globals.graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
    }

    protected override void Initialize()
    {
        // TODO: Add your initialization logic here
        Globals.screenWidth = Coordinates.screenWidth = 1920;
        Globals.screenHeight = Coordinates.screenHeight = 1080;

        Globals.graphics.PreferredBackBufferWidth = Globals.screenWidth;
        Globals.graphics.PreferredBackBufferHeight = Globals.screenHeight;

        Globals.graphics.ApplyChanges();

        base.Initialize();
    }

    protected override void LoadContent()
    {
        Globals.content = Content;
        Globals.spriteBatch = new SpriteBatch(GraphicsDevice);

        Persistence.LoadPreferences();
        Scores.ReadFromXML();

        Fonts.Init();
        Globals.defaultFont = Fonts.defaultFont24;

        // TODO: use this.Content to load your game content here
        cursor = new Cursor();

        Globals.keyboard = new MyKeyboard();
        Globals.mouse = new MyMouseControl();

        GameGlobals.currentLevel = Persistence.preferences.levelsComplete > 3 ? 
                LevelSelection.LEVEL_3 : (LevelSelection)Persistence.preferences.levelsComplete;

        levelEditor = new LevelEditor();
        music = new Music();
        mainMenu = new MainMenu();
        aboutMenu = new AboutMenu(levelEditor.Init);
        gamePlay = new GamePlay();
        ui = new UI(gamePlay.ResetWorld);

        background = new SpriteBuilder().WithPath("Background//background")
                                        .WithDims(new Vector2(Coordinates.screenWidth, Coordinates.screenHeight))
                                        .WithTransitionable(false)
                                        .Build();
    }

    protected override void Update(GameTime gameTime)
    {
        if (TransitionManager.transState == TransitionState.OUT_REQUESTED)
        {
            TransitionManager.transState = TransitionState.BEGIN_OUT;
        }

        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        // TODO: Add your update logic here
        Globals.gameTime = gameTime;
        Globals.keyboard.Update();
        Globals.mouse.Update();

        background.Update();

        switch (Globals.gameState)
        {
            case GameState.MAIN_MENU:
                mainMenu.Update();
                break;
            case GameState.ABOUT:
                aboutMenu.Update();
                break;
            case GameState.GAME_PLAY:
                gamePlay.Update();
                break;
            case GameState.LEVEL_EDITOR:
                levelEditor.Update();
                break;
        }

        ui.Update();

        TransitionManager.Update();

        Globals.keyboard.UpdateOld();
        Globals.mouse.UpdateOld();

        cursor.Update();

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Colors.Background);

        Globals.spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.NonPremultiplied);

        // TODO: Add your drawing code here
        background.Draw();

        if (TransitionManager.transState != TransitionState.PAUSE)
        {
            switch (Globals.gameState)
            {
                case GameState.MAIN_MENU:
                    mainMenu.Draw();
                    break;
                case GameState.ABOUT:
                    aboutMenu.Draw();
                    break;
                case GameState.GAME_PLAY:
                    gamePlay.Draw();
                    break;
                case GameState.LEVEL_EDITOR:
                    levelEditor.Draw();
                    break;
            }
        }

        ui.Draw();

        if (Globals.gameState != GameState.GAME_PLAY || GameGlobals.roundState == RoundState.END)
        {
            cursor.Draw();
        }
        Globals.spriteBatch.End();

        base.Draw(gameTime);
    }
}