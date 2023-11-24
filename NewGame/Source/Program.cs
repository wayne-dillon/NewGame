using System;
using System.IO;

try
{
    using var game = new Main();
    game.Run();
} catch (Exception e)
{
    File.WriteAllText("log.txt", e.Message);
}
