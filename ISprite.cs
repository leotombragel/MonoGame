using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

public interface ISprite
{
    //choose one draw
    Texture2D textureAtlas { get; set; }

    List<Rectangle> Frames { get; set; }
    //void Draw(SpriteBatch sb, Texture2D texture, Vector2 pos);
    void Draw(SpriteBatch sb, Vector2 pos);
    //void Draw(SpriteBatch sb, Texture2D texture);
    //void Draw();
    void Update();
}