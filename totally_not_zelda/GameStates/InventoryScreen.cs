using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Sprint.Interfaces;
using Sprint.Item;
using Sprint.UI;

namespace Sprint.GameStates;

internal class InventoryScreen : IGameState
{
    // private static StaticSprite inventoryBackground;
    // private static StaticSprite mapBackground;

    private Inventory inventory;
    private HUDBar hud;
    private IGameState restoreState;

    public InventoryScreen(Inventory inventory, HUDBar hud, IGameState restoreState)
    {
        this.inventory = inventory;
        this.hud = hud;
        this.restoreState = restoreState;
    }

    public void Update(GameTime time)
    {

    }
    public void Draw(SpriteBatch sb)
    {

    }


    public void Enter() { }
    public void LoadContent() { }
    public void Exit() { }
}
