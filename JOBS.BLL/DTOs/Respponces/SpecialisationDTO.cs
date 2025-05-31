using JOBS.BLL.Common.Mappings;
using JOBS.DAL.Entities;

namespace JOBS.BLL.DTOs.Respponces
{
    public class SpecialisationDTO : IMapFrom<Specialisation>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

    }
}
