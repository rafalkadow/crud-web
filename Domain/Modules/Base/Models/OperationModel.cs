using Shared.Enums;

namespace Domain.Modules.Base.Models
{
    [Serializable]
    public class OperationModel
    {
        public OperationEnum Operation { get; set; }

        public string ControllerName { get; set; }
        public Guid Id { get; set; }

    }
}