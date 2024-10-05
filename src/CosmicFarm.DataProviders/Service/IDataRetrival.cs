namespace CosmicFarm.DataProviders.Service;

public interface IDataRetrival<TModel>
{
    Task<TModel> GetDataFromSource(DateTime startDate);
}
