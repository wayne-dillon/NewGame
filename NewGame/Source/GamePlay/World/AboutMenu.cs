using Microsoft.Xna.Framework;
using System.Collections.Generic;

public class AboutMenu
{
    private readonly List<LinkedButton> buttons = new();
    private readonly Sprite background;
    private Tab currentTab = Tab.HOW_TO;
    private readonly TextComponent howToText;
    private readonly TextComponent creditsText;

    private readonly string howTo = "The aim of the game, like life under capitalism, is to accumulate wealth.\n\n\nIn order to accomplish this you'll be drafting poker hands.\n\nEach round you'll be dealt 2* flop cards, then pick 5* more cards from packs of 5*.\n\nYou'll score based on the best possible hand of 5 cards that can be made from these 7* cards.\n\n\n*These numbers will vary based on the table you are playing at.\n\n\nTo join a table you'll need to pay an ante, the bigger the ante the bigger the payouts\n\nThe Free Roller table has no ante, it's financed with the change we find between the cushions\nat The High Roller table.\n\nIn House Game you'll earn coins based on the rank of the hand\n\nIn Bot Game the winner profits, second place gets the ante back and everyone else leaves\nwith nothing."; 
    private readonly string credits = "Created By\n\nWayne Dillon"; 

    private enum Tab
    {
        HOW_TO,
        STATS,
        CREDITS
    }

    public AboutMenu()
    {
        background = new SpriteBuilder().WithPath("rect")
                                        .WithColor(Colors.AirForceBlue)
                                        .WithDims(new Vector2(1400, 700))
                                        .Build();

        howToText = new TextComponentBuilder().WithText(howTo).Build();
        creditsText = new TextComponentBuilder().WithText(credits).Build();

        SpriteBuilder buttonBuilder = new SpriteBuilder().WithPath("rect")
                                                        .WithDims(new Vector2(250,40))
                                                        .WithScreenAlignment(Alignment.TOP_LEFT)
                                                        .WithColor(Colors.Buttons)
                                                        .WithHoverColor(Colors.Hover)
                                                        .WithUnavailableColor(Colors.AirForceBlue)
                                                        .WithHoverScale(new Vector2(1.01f, 1.01f))
                                                        .WithButtonAction(SwitchTabs);

        buttons.Add(buttonBuilder.WithText("Statistics").WithOffset(new Vector2(480,82)).WithButtonInfo(Tab.STATS).BuildLinkedButton());
        buttons.Add(buttonBuilder.WithText("Credits").WithOffset(new Vector2(735,82)).WithButtonInfo(Tab.CREDITS).BuildLinkedButton());
        buttons.Add(buttonBuilder.WithText("How to Play").WithOffset(new Vector2(225,82)).WithButtonInfo(Tab.HOW_TO).WithAvailable(false).BuildLinkedButton());

        foreach (LinkedButton button in buttons)
        {
            button.SetLinkedList(buttons);
        }
    }

    public void Update()
    {
        background.Update();
        foreach (LinkedButton button in buttons)
        {
            button.Update();
        }

        switch (currentTab)
        {
            case Tab.HOW_TO:
                howToText.Update();
                break;
            case Tab.STATS:
                break;
            case Tab.CREDITS:
                creditsText.Update();
                break;
        }
    }

    public void SwitchTabs(object SENDER, object INFO)
    {
        if (INFO is Tab tab)
        {
            currentTab = tab;
        }
    }

    public void Draw()
    {
        background.Draw();
        foreach (LinkedButton button in buttons)
        {
            button.Draw();
        }

        switch (currentTab)
        {
            case Tab.HOW_TO:
                howToText.Draw();
                break;
            case Tab.STATS:
                break;
            case Tab.CREDITS:
                creditsText.Draw();
                break;
        }
    }
}