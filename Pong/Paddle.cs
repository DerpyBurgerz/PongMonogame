using Microsoft.VisualBasic.Devices;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MonoGameWindowsDesktopApplication1;

public class Paddle
{
    public Vector2 position;
    public Keys upKey, downKey;
    public static Texture2D sprite;
    public Vector2 Size => new (Paddle.sprite.Width, Paddle.sprite.Height);

    public Paddle(Vector2 startPosition, Keys upKey, Keys downKey)
    {
        // constructor
        position = startPosition;
        this.upKey = upKey;
        this.downKey = downKey;
    }
    
    public void MovePaddle(KeyboardState keyboardState)
    {
        Vector2 screen = Game1.screen;
        if (keyboardState.IsKeyDown(upKey) && position.Y > 0)
        {
            position.Y -= 5;
                }
        if (keyboardState.IsKeyDown(downKey) && position.Y + sprite.Height < screen.Y)
        {
            position.Y += 5;
        }
    }
    public void DrawPaddle(SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(sprite, position, Color.White);
    }
}