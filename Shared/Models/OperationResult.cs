using Shared.Interfaces;
using Shared.Enums;
using FluentValidation.Results;
using Shared.Validation;
using Shared.Attributes;
using NSwag.Annotations;
using Newtonsoft.Json;

namespace Shared.Models
{
    [Serializable]
    public class OperationResult
    {
        //[SwaggerIgnore]
        //[Attributes.SwaggerIgnore]
        //[OpenApiIgnore]
        [JsonIgnore]
        public Guid? EntityId { get; set; }
        [JsonIgnore]
        public IEntity entity { get; set; }
		public string Message { get; set; }

        public string ErrorMessage { get; set; }

		public bool OperationStatus { get; set; } = true;

		public IEnumerable<ErrorMessage> Errors { get; private set; }
		public OperationEnum Operation { get; set; }
        [JsonIgnore]
        public string GuidRecord { get; set; }
        public Guid? Id { get; set; }

        public OperationResult(bool operationStatus)
		{
            OperationStatus = operationStatus;
		}
        public OperationResult(bool operationStatus, OperationEnum operation)
        {
            OperationStatus = operationStatus;
            this.Operation = operation;
        }
        public OperationResult(bool operationStatus, string errorMessage = "", OperationEnum Operation = OperationEnum.None)
		{
			this.OperationStatus = operationStatus;
			this.ErrorMessage = errorMessage;
			this.Operation = Operation;
		}

		public OperationResult(IEntity entity, OperationEnum operation)
		{
			this.entity = entity;
			this.EntityId = entity.Id;
			this.OperationStatus = true;
			this.GuidRecord = entity.Id.ToString();
			this.Operation = operation;
		}

        public OperationResult(Guid entityId)
        {
            this.EntityId = entityId;
            this.OperationStatus = true;
            this.GuidRecord = entityId.ToString();
        }

        public void FailureAdd(IList<ValidationFailure> validationFailures)
		{
			OperationStatus = false;
			Errors = validationFailures.Select(v => new ErrorMessage()
			{
				PropertyName = v.PropertyName,
				Message = v.ErrorMessage
			});
		}
	}
}