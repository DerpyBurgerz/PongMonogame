using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MonoGameWindowsDesktopApplication1;

public class Game1 : Game
{
    private GraphicsDeviceManager graphics;
    private SpriteBatch spriteBatch;
    public static SpriteFont spriteFont;
    public static Vector2 screen;

    private Ball ball;
    private Player player1;
    private Player player2;
    private Texture2D ballSprite;
    
    MouseState currentMouseState, PreviousMouseState;
    KeyboardState currentKeyboardState, previousKeyboardState;

    enum GameState
    {
        StartScreen,
        Playing
    }
    private GameState CurrentGameState;

    public Game1()
    {
        CurrentGameState = GameState.StartScreen;
        graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        ball = new Ball(new Vector2(400, 200), new Vector2(5, 5));

        player1 = new Player(0, new Vector2(50, 150), Keys.W, Keys.S);
        player2 = new Player(0, new Vector2(750, 150), Keys.Up, Keys.Down);

        base.Initialize();
    }
    
    protected override void LoadContent()
    {
        //Laad hier alle files in.
        spriteBatch = new SpriteBatch(GraphicsDevice);
        spriteFont = Content.Load<SpriteFont>("fontStandard");
        ballSprite = Content.Load<Texture2D>("avgBallSMall");
        Paddle.sprite = Content.Load<Texture2D>("paddleBlue");
        
        screen = new Vector2(GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height);
        //screen.X en screen.Y kan je gebruiken om de breedte en de hoogte van het scherm te krijgen.
        //Kan handig zijn om bijvoorbeeld de paddles helemaal links en rechts op het scherm te tekenen,
        //of om het balletje in het midden van het scherm te laten starten.
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();
        
        //Eerst de inputStates van de vorige frame in previousInputState opslaan
        PreviousMouseState = currentMouseState;
        previousKeyboardState = currentKeyboardState;
        
        //Daarna huidige inputStates updaten.
        currentKeyboardState = Keyboard.GetState();
        currentMouseState = Mouse.GetState();
        
        
        //Een switch case statement is een fancy if statementstructuur.
        switch (CurrentGameState)
        {
            case GameState.StartScreen:
                //Als S op dit moment is ingedrukt, en niet ingedrukt was in de vorige frame.
                //In andere woorden, als S in deze frame geklikt is.
                if (currentKeyboardState.IsKeyDown(Keys.Space) && !previousKeyboardState.IsKeyDown(Keys.Space))
                {
                    CurrentGameState = GameState.Playing;
                }
                break;
            case GameState.Playing:
                ball.UpdateBall(new Vector2(ballSprite.Width, ballSprite.Height), player1.paddle, player2.paddle);
                player1.UpdatePlayer(currentKeyboardState);
                player2.UpdatePlayer(currentKeyboardState);

                //Zet hier je update logica voor wanneer de speler aan het spelen is.
                break;
            default:
                //Als CurrentGameState niet Startscreen en niet Playing is, dan krijg je deze error.
                throw new ArgumentOutOfRangeException();
        }
        
        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);
        //spriteBatch.Begin() moet geroepen worden voor je spriteBatch kan gebruiken.
        //spriteBatch.Draw() kan je gebruiken om sprites te tekenen
        spriteBatch.Begin();
        switch (CurrentGameState)
        {
            case GameState.StartScreen:
                spriteBatch.DrawString(spriteFont, "Press Space To Start", new Vector2(30, 160), Color.Black);
                break;
            case GameState.Playing:
                ball.DrawBall(spriteBatch, ballSprite);
                player1.DrawPlayer(spriteBatch, new Vector2(50, 50));
                player2.DrawPlayer(spriteBatch, new Vector2(740, 50));
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }

        spriteBatch.End();
        base.Draw(gameTime);
    }
}