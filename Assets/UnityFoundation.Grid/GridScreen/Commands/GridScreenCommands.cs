using System.Collections.Generic;
using UnityFoundation.Code;

namespace UnityFoundation.Grid
{
    public class GridScreenCommands : IUpdatable
    {
        private readonly List<IGridScreenCommand> commands = new();

        public void Register(IGridScreenCommand command)
        {
            commands.Add(command);
        }

        public void Update(float deltaTime = 0)
        {
            foreach(var command in commands)
                command.Execute();
        }
    }
}
