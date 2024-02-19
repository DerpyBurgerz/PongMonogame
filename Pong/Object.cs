using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGameWindowsDesktopApplication1;

public abstract class Object
{
    protected static Texture2D sprite;//de sprite moet static zijn. Deze kan dan gebruikt worden door alle instances van deze class.
    protected Vector2 position;
    
    public static void LoadContent(ContentManager content)
    {
        sprite = content.Load<Texture2D>("avgBallSmall");
    }
    public abstract void Update(GameTime gameTime);
    public abstract void Draw(SpriteBatch spriteBatch);
}