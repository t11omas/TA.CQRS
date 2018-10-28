namespace TA.CQRS
{
    using System.Collections.Generic;

    public abstract class ExecutionHandlerBase<TResponse>
        where TResponse : class
    {
        protected ExecutionResponse Ok(TResponse content, Dictionary<string, object> additionalData = null)
        {
            return ExecutionResponse.CreateAsOK(content, additionalData);
        }

        protected ExecutionResponse Created(TResponse content, Dictionary<string, object> additionalData = null)
        {
            return ExecutionResponse.CreateAsCreated(content, additionalData);
        }

        protected ExecutionResponse PreconditionFailed(TResponse content = null)
        {
            return ExecutionResponse.CreateAsPreconditionFailed(content);
        }

        protected ExecutionResponse BadRequest<T>(T content = default(T))
        {
            return ExecutionResponse.CreateAsBadRequest(content);
        }

        protected ExecutionResponse NotFound(TResponse content = null)
        {
            return ExecutionResponse.CreateAsNotFound(content);
        }

        protected ExecutionResponse NoContent()
        {
            return ExecutionResponse.CreateAsNoContent();
        }

        protected ExecutionResponse Status(ActionOutcome actionOutcome, TResponse content = null, Dictionary<string, object> additionalData = null)
        {
            return ExecutionResponse.CreateAsStatus(content, actionOutcome, additionalData);
        }
    }
}