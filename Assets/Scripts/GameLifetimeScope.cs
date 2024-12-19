using VContainer;
using VContainer.Unity;

namespace Quiz_Bezuglyi
{
    public class GameLifetimeScope : LifetimeScope
    { 
        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterComponentInHierarchy<SymbolsSetup>();           
            builder.RegisterComponentInHierarchy<GuessChecker>();
            builder.RegisterComponentInHierarchy<CellsGenerator>();        
            builder.RegisterComponentInHierarchy<TweenAnimator>();
            builder.RegisterComponentInHierarchy<UpdaterUI>();
        }        
    }
}
