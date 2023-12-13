using System.Collections.Generic;
using Microsoft.Xna.Framework;

public class ValueSelector
{
    private TextComponent text;
    private TextComponent value;
    private List<Button> buttons = new();
    private Variable variable;

    public enum Variable
    {
        H_ACCEL,
        H_DECEL,
        MAX_SPEED,
        DASH_SPEED,
        DASH_TIME,
        DASH_DECEL,
        JUMP_SPEED,
        JUMP_HOLD_TIME,
        GRAVITY,
        MAX_FALL_SPEED
    }

    public ValueSelector(Variable VARIABLE, Vector2 OFFSET)
    {
        variable = VARIABLE;

        text = new TextComponentBuilder().WithText(Name())
                                        .WithOffset(OFFSET - new Vector2(500, 0))
                                        .Build();
        
        value = new TextComponentBuilder().WithText(GetValue().ToString())
                                        .WithOffset(OFFSET + new Vector2(250, 0))
                                        .Build();

        SpriteBuilder buttonBuilder = new SpriteBuilder().WithPath("UI//Button168x100")
                                                        .WithDims(new Vector2(60, 40))
                                                        .WithButtonAction(UpdateVariable);
        
        buttons.Add(buttonBuilder.WithText("-100")
                                .WithButtonInfo(-0.1f)
                                .WithOffset(OFFSET)
                                .BuildButton());
        buttons.Add(buttonBuilder.WithText("-10")
                                .WithButtonInfo(-0.01f)
                                .WithOffset(OFFSET + new Vector2(75, 0))
                                .BuildButton());
        buttons.Add(buttonBuilder.WithText("-1")
                                .WithButtonInfo(-0.001f)
                                .WithOffset(OFFSET + new Vector2(150, 0))
                                .BuildButton());
        buttons.Add(buttonBuilder.WithText("+1")
                                .WithButtonInfo(0.001f)
                                .WithOffset(OFFSET + new Vector2(350, 0))
                                .BuildButton());
        buttons.Add(buttonBuilder.WithText("+10")
                                .WithButtonInfo(0.01f)
                                .WithOffset(OFFSET + new Vector2(425, 0))
                                .BuildButton());
        buttons.Add(buttonBuilder.WithText("+100")
                                .WithButtonInfo(0.1f)
                                .WithOffset(OFFSET + new Vector2(500, 0))
                                .BuildButton());
    }

    public void Update()
    {
        text.Update();
        value.Update(GetValue().ToString());
        foreach (Button button in buttons)
        {
            button.Update();
        }
    }
    
    private string Name() => variable switch
    {
        Variable.H_ACCEL => "Horizontal Acceleration",
        Variable.H_DECEL => "Horizontal Deceleration",
        Variable.MAX_SPEED => "Max Horizontal Speed",
        Variable.DASH_SPEED => "Dash Speed",
        Variable.DASH_TIME => "Dash Time",
        Variable.DASH_DECEL => "Dash Deceleration",
        Variable.JUMP_SPEED => "Jump Speed",
        Variable.JUMP_HOLD_TIME => "Jump Hold Time",
        Variable.GRAVITY => "Fall Speed",
        Variable.MAX_FALL_SPEED => "Max Fall Speed",
        _ => ""
    };
    
    private int GetValue() => variable switch
    {
        Variable.H_ACCEL => (int)(PlayerMovementValues.horizontalAcceleration * 1000),
        Variable.H_DECEL => (int)(PlayerMovementValues.horizontalDeceleration * 1000),
        Variable.MAX_SPEED => (int)(PlayerMovementValues.maxSpeed * 1000),
        Variable.DASH_SPEED => (int)(PlayerMovementValues.dashSpeed * 1000),
        Variable.DASH_TIME => PlayerMovementValues.dashTime,
        Variable.DASH_DECEL => (int)(PlayerMovementValues.dashDeceleration * 1000),
        Variable.JUMP_SPEED => (int)(PlayerMovementValues.jumpSpeed * 1000),
        Variable.JUMP_HOLD_TIME => PlayerMovementValues.jumpHoldTime,
        Variable.GRAVITY => (int)(PlayerMovementValues.gravity * 1000),
        Variable.MAX_FALL_SPEED => (int)(PlayerMovementValues.maxFallSpeed * 1000),
        _ => 0
    };

    private void UpdateVariable(object SENDER, object INFO)
    {
        if (INFO is float value)
        {
            switch (variable)
            {
                case Variable.H_ACCEL:
                    PlayerMovementValues.horizontalAcceleration += value;
                    break;
                case Variable.H_DECEL:
                    PlayerMovementValues.horizontalDeceleration += value;
                    break;
                case Variable.MAX_SPEED:
                    PlayerMovementValues.maxSpeed += value;
                    break;
                case Variable.DASH_SPEED:
                    PlayerMovementValues.dashSpeed += value;
                    break;
                case Variable.DASH_TIME:
                    PlayerMovementValues.dashTime += (int)(value * 1000);
                    break;
                case Variable.DASH_DECEL:
                    PlayerMovementValues.dashDeceleration += value;
                    break;
                case Variable.JUMP_SPEED:
                    PlayerMovementValues.jumpSpeed += value;
                    break;
                case Variable.JUMP_HOLD_TIME:
                    PlayerMovementValues.jumpHoldTime += (int)(value * 1000);
                    break;
                case Variable.GRAVITY:
                    PlayerMovementValues.gravity += value;
                    break;
                case Variable.MAX_FALL_SPEED:
                    PlayerMovementValues.maxFallSpeed += value;
                    break;
                default:
                    break;
            }
        }
    }

    public void Draw()
    {
        text.Draw();
        value.Draw();
        foreach (Button button in buttons)
        {
            button.Draw();
        }
    }
}