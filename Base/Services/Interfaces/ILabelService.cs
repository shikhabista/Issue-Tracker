using Base.Entities;

namespace Base.Services.Interfaces;

public interface ILabelService
{
    Task CreateLabel(Label label);
    Task<Label> GetLabel(long id);
    Task<List<Label>> GetLabelList();
    Task<Label> UpdateLabel(Label label);
    Task<bool> DeleteLabel(long id);
}