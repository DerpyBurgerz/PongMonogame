using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGameWindowsDesktopApplication1;

public abstract class Object
{
    protected Texture2D sprite;//de sprite moet static zijn. Deze kan dan gebruikt worden door alle instances van deze class.
    protected Vector2 position;

    public abstract void LoadContent(ContentManager content);
    public abstract void Update(GameTime gameTime);

    public virtual void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(sprite, position, Color.White);
    }
}