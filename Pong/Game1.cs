using System;
using System.Collections.Generic;
using System.Drawing.Text;
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
    private Player player1, player2;
    private string winningPlayer;

    MouseState currentMouseState, PreviousMouseState;
    KeyboardState currentKeyboardState, previousKeyboardState;

    enum GameState
    {
        StartScreen,
        Playing,
        GameOver
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
        base.Initialize();
    }

    protected override void LoadContent()
    {
        //Laad hier alle files in.
        spriteBatch = new SpriteBatch(GraphicsDevice);
        spriteFont = Content.Load<SpriteFont>("fontStandard");
        Ball.sprite = Content.Load<Texture2D>("avgBallSMall");
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
                    StartGame(Vector2.Divide(screen, 2), new Vector2(5, 0));
                }
                break;
            case GameState.Playing:
                ball.UpdateBall(player1, player2, this);
                player1.UpdatePlayer(currentKeyboardState);
                player2.UpdatePlayer(currentKeyboardState);
                //Zet hier je update logica voor wanneer de speler aan het spelen is.
                break;
            case GameState.GameOver:
                //Als S op dit moment is ingedrukt, en niet ingedrukt was in de vorige frame.
                //In andere woorden, als S in deze frame geklikt is.
                if (currentKeyboardState.IsKeyDown(Keys.Space) && !previousKeyboardState.IsKeyDown(Keys.Space))
                {
                    CurrentGameState = GameState.Playing;
                    StartGame(Vector2.Divide(screen, 2), new Vector2(5, 0));
                }
                break;
            default:
                //Als CurrentGameState niet Startscreen en niet Playing is, dan krijg je deze error.
                throw new ArgumentOutOfRangeException();
        }

        base.Update(gameTime);


        void StartGame(Vector2 startPosition, Vector2 startSpeed)
        {
            ball = new Ball(startPosition, startSpeed);

            player1 = new Player(0, new Vector2(50, 150), Keys.W, Keys.S);
            player2 = new Player(0, new Vector2(screen.X - 50 - Paddle.sprite.Width, 150), Keys.Up, Keys.Down);
        }

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
                ball.DrawBall(spriteBatch, Ball.sprite);
                player1.DrawPlayer(spriteBatch, new Vector2(50, 50));
                player2.DrawPlayer(spriteBatch, new Vector2(740, 50));
                break;
            case GameState.GameOver:
                spriteBatch.DrawString(spriteFont, $"{winningPlayer} won! Press Space To Start New Game", new Vector2(30, 160), Color.Black);
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }

        spriteBatch.End();
        base.Draw(gameTime);
    }

    public void EndRound(string playerString)
    {
        Player player;
        if (playerString == "Player 1")
        {
            player = player1;
        }
        else if (playerString == "Player 2")
        {
            player = player2;
        }
        else
        {
            throw new Exception("Not Player 1 or Player 2 string");
        }

            player.UpdateScore();

        if (player.Score == 10)
        {
            winningPlayer = playerString;
            CurrentGameState = GameState.GameOver;
        }
        else 
        {
            NewRound();
        }
    }
    public void NewRound()
    {
        ball.ResetBall();
        player1.paddle.ResetPaddle();
        player2.paddle.ResetPaddle();
    }
}