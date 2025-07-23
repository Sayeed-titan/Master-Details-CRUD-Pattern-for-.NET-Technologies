public interface IMasterDetailsRepository<TMaster, TDetail>
{
    int Create(TMaster master, List<int> detailIds);
    TMaster GetById(int id);
    void Update(TMaster master, List<int> detailIds);
    void Delete(int masterId);
}
