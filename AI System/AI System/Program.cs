using System;

namespace AI_System
{
    static class Program
    {
        static void Main(string[] args)
        {
            using (AI_System_Graphics game = new AI_System_Graphics())
            {
                game.Run();
            }
        }
    }
}

