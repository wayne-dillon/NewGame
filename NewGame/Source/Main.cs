using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

public class Main : Game
{
    // private Backdrop backdrop;
    private MainMenu mainMenu;
    private OptionsMenu optionsMenu;
    private AboutMenu aboutMenu;
    private GamePlay gamePlay;
    private DevConsole devConsole;
    public UI ui;

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
        Fonts.Init();

        // TODO: use this.Content to load your game content here
        cursor = new Cursor();

        Globals.keyboard = new MyKeyboard();
        Globals.mouse = new MyMouseControl();

        // backdrop = new Backdrop();
        mainMenu = new MainMenu();
        optionsMenu = new OptionsMenu();
        aboutMenu = new AboutMenu();
        gamePlay = new GamePlay();
        devConsole = new DevConsole();
        ui = new UI(gamePlay.ResetWorld);
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

        // backdrop.Update();

        switch (Globals.gameState)
        {
            case GameState.MAIN_MENU:
                mainMenu.Update();
                break;
            case GameState.OPTIONS:
                optionsMenu.Update();
                break;
            case GameState.ABOUT:
                aboutMenu.Update();
                break;
            case GameState.GAME_PLAY:
                gamePlay.Update();
                break;
            case GameState.DEV_CONSOLE:
                devConsole.Update();
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
        // backdrop.Draw();

        if (TransitionManager.transState != TransitionState.PAUSE)
        {
            switch (Globals.gameState)
            {
                case GameState.MAIN_MENU:
                    mainMenu.Draw();
                    break;
                case GameState.OPTIONS:
                    optionsMenu.Draw();
                    break;
                case GameState.ABOUT:
                    aboutMenu.Draw();
                    break;
                case GameState.GAME_PLAY:
                    gamePlay.Draw();
                    break;
                case GameState.DEV_CONSOLE:
                    devConsole.Draw();
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