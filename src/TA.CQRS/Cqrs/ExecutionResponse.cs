namespace TA.CQRS
{
    using System.Collections.Generic;

    public class ExecutionResponse
    {
        private ExecutionResponse()
        {
        }

        public Dictionary<string, object> AdditionalData { get; private set; } = new Dictionary<string, object>();

        public object GetContent { get; private set; }

        public ActionOutcome ActionOutcomeCode { get; private set; }

        public static ExecutionResponse CreateAsStatus<T>(T content, ActionOutcome actionOutcome, Dictionary<string, object> additionalData)
        {
            return new ExecutionResponse()
                       {
                           GetContent = content,
                           ActionOutcomeCode = actionOutcome,
                           AdditionalData = additionalData,
                       };
        }

        public static ExecutionResponse CreateAsOK<T>(T content, Dictionary<string, object> additionalData)
        {
            return new ExecutionResponse()
                       {
                           GetContent = content,
                           ActionOutcomeCode = ActionOutcome.Ok,
                           AdditionalData = additionalData,
            };
        }

        public static ExecutionResponse CreateAsCreated<T>(T content, Dictionary<string, object> additionalData)
        {
            return new ExecutionResponse()
                       {
                           GetContent = content,
                           ActionOutcomeCode = ActionOutcome.Created,
                           AdditionalData = additionalData,
            };
        }

        public static ExecutionResponse CreateAsSeeOther(Dictionary<string, object> additionalData)
        {
            return new ExecutionResponse()
                       {
                           ActionOutcomeCode = ActionOutcome.SeeOther,
                           AdditionalData = additionalData,
            };
        }

        public static ExecutionResponse CreateAsBadRequest<T>(T content = default(T))
        {
            return new ExecutionResponse()
                       {
                           GetContent = content,
                           ActionOutcomeCode = ActionOutcome.BadRequest,
                       };
        }

        public static ExecutionResponse CreateAsPreconditionFailed()
        {
            return CreateAsPreconditionFailed<object>();
        }


        public static ExecutionResponse CreateAsPreconditionFailed<T>(T content = default(T))
        {
            return new ExecutionResponse()
                       {
                           GetContent = content,
                           ActionOutcomeCode = ActionOutcome.PreconditionFailed,
                       };
        }

        public static ExecutionResponse CreateAsNotFound<T>(T content = default(T))
        {
            return new ExecutionResponse()
                       {
                           GetContent = content,
                           ActionOutcomeCode = ActionOutcome.NotFound,
                       };
        }

        public static ExecutionResponse CreateAsNoContent()
        {
            return new ExecutionResponse()
            {
                ActionOutcomeCode = ActionOutcome.NoContent
            };
        }
    }
}