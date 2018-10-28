namespace TA.CQRS
{
    using System.Collections.Generic;
    using System.Linq;

    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;

    public class ExecutionResponse
    {
        private ExecutionResponse()
        {
        }

        public Dictionary<string, object> AdditionalData { get; private set; } = new Dictionary<string, object>();

        public object GetContent { get; private set; }

        public ActionOutcome ActionOutcomeCode { get; private set; }

        public object ValidationResults { get; private set; }

        public List<object> Links { get; private set; } = new List<object>();

        public JObject ToFullResponse(JsonSerializer jsonSerializer = null)
        {
            if (jsonSerializer == null)
            {
                jsonSerializer = JsonSerializer.CreateDefault();
            }

            //JObject validationJObject = JObject.FromObject(ValidationResultCollection.Create(this.ValidationResults), jsonSerializer);


            JObject validationJObject = this.ValidationResults == null
                                            ? new JObject()
                                            : JObject.FromObject(this.ValidationResults, jsonSerializer);
            JObject jObject = null;
            if (this.GetContent != null)
            {
                jObject = JObject.FromObject(this.GetContent, jsonSerializer);
                validationJObject.Properties().ToList().ForEach(p => jObject.Add(p));
            }
            else
            {
               jObject = validationJObject;
            }

            if (this.Links != null)
            {
                jObject.Add("links", JArray.FromObject(this.Links, jsonSerializer));
            }

            return jObject;
        }

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
                           ////ValidationResults = {
                           ////                            ValidationResult.CreateWithError("Another system or user has changed some information while you were making your changes. Unfortunately, your changes cannot be saved.", "412")
                           ////                        }
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

        public void AddValidationResults(object validationResults)
        {
            if (validationResults == null)
                return;
            this.ValidationResults = validationResults;
        }

        public void AddLinks(IEnumerable<object> links)
        {
            if (links == null)
                return;
            this.Links.AddRange(links);
        }
    }
}