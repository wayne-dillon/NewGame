using Microsoft.Xna.Framework;
using System.Collections.Generic;

public class AboutMenu
{
    private readonly List<LinkedButton> buttons = new();
    private readonly Sprite background;
    private Tab currentTab = Tab.HOW_TO;
    private readonly TextComponent howToText;
    private readonly TextComponent creditsText;

    private readonly string howTo = "Charlie is off on a well earned holiday, but some rude kittens on the flight\n"
                                  + "haven't switched their phones to airplane mode. In order to avoid CATastrophy\n"
                                  + "he'll have to tap into his many (more specifically, three) animalistic modes\n"
                                  + "in order to dash, hop, and climb his way around the cabin, righting these\n"
                                  + "egregious wrongs!"; 
    private readonly string credits = "Programming, Gameplay and Level Design By\nWayne Dillon\n\n"
                                    + "Illustations and Animations By\nFairy Elina\n\n"
                                    + "Music and Sound Effects By\nAbraham Putnam"; 

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