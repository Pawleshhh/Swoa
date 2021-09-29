namespace Swoa.ViewModel
{
    public interface IViewModel<TModel>
    {

        bool IsViewModelOf(TModel model);

    }
}