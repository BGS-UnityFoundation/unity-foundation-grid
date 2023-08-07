using UnityFoundation.Code;

namespace UnityFoundation.Grid
{
    public class GridScreenManager<T>
        where T : new()
    {
        private readonly GridLimitXY gridLimits;
        private readonly GridScreenView<T> gridView;

        public GridXY<T> Grid { get; private set; }
        public GridScreenSelection<T> ValueSelection { get; private set; }

        private IDependencyContainer container;

        public GridScreenManager(
            GridLimitXY gridLimits,
            GridScreenView<T> gridView
        )
        {
            this.gridLimits = gridLimits;
            this.gridView = gridView;

            Bind();
        }

        private void Bind()
        {
            var binder = new DependencyBinder();
            binder.Register(gridLimits);
            binder.RegisterSingleton<CursorSelection, CursorSelection>();
            binder.RegisterSingleton<GridScreenSelection<T>, GridScreenSelection<T>>();
            binder.RegisterSingleton<GridXY<T>, GridXY<T>>();
            binder.RegisterSetup(gridView);

            binder.RegisterSingleton<KeyboardInputs, KeyboardInputs>();
            binder.Register<GridScreenCommands>();
            binder.Register<MoveCursorCommand>();
            binder.Register<SelectedItemCommand<T>>();

            container = binder.Build();
        }

        public void Start()
        {
            RegisterCommands();

            Grid = container.Resolve<GridXY<T>>();

            container.Resolve<CursorSelection>().Set(new(0, 0));
            ValueSelection = container.Resolve<GridScreenSelection<T>>();
        }

        public void Display()
        {
            container.Resolve<GridScreenView<T>>().Display();
        }

        private void RegisterCommands()
        {
            var commands = container.Resolve<GridScreenCommands>();
            commands.Register(container.Resolve<MoveCursorCommand>());
            commands.Register(container.Resolve<SelectedItemCommand<T>>());

            SingletonUpdateProcessor.I.Register(container.Resolve<KeyboardInputs>());
            SingletonUpdateProcessor.I.Register(commands);
        }
    }
}
