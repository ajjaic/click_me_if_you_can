using Godot;
using System;

public class TouchMeButton : Node2D
{
    private Random _rndGen = new Random();
    private Sprite _sprite = null;
    private Area2D _spriteArea = null;
    private Timer _timer = null;

    public override void _Ready()
    {
        _sprite = GetNode<Sprite>("Sprite");

        _spriteArea = GetNode<Area2D>("Sprite/Area2D");
        _spriteArea.Connect("mouse_entered", this, nameof(TriggerTimer));

        _timer = GetNode<Timer>("Timer");
        _timer.Connect("timeout", this, nameof(TimerTriggered));

        SetRandomPos();
    }

    public override void _Process(float delta)
    {
        if (Input.IsMouseButtonPressed((int)ButtonList.Left))
        {
            Vector2 mousePos = GetLocalMousePosition();
            var xNeg = _sprite.Position.x - 30;
            var xPos = _sprite.Position.x + 30;
            var yNeg = _sprite.Position.y - 30;
            var yPos = _sprite.Position.y + 30;

            Vector2 topLeft = new Vector2(xNeg, yNeg);
            Vector2 topRight = new Vector2(xPos, yPos);

            if (topLeft < mousePos && mousePos < topRight)
            {
                GD.Print("Yup");
            }
            else
            {
                GD.Print("Nop");
            }
        }
    }

    private void TimerTriggered()
    {
        _timer.Stop();
        SetRandomPos();
    }

    private void TriggerTimer()
    {
        if (_timer.IsStopped())
        {
            _timer.Start();
        }
    }

    private void SetRandomPos()
    {
        Int32 x = _rndGen.Next(0, 1024);
        Int32 y = _rndGen.Next(0, 600);

        _sprite.Position = new Vector2(x, y);
    }

}
