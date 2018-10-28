namespace TA.CQRS
{
    using System.Collections.Generic;

    public class ActionOutcome
    {
        public static ActionOutcome Ok => new ActionOutcome("Ok", 200, true);

        public static ActionOutcome Created => new ActionOutcome("Created", 201, true);

        public static ActionOutcome NoContent => new ActionOutcome("NoContent", 204, true);

        public static ActionOutcome SeeOther => new ActionOutcome("SeeOther", 303, true);

        public static ActionOutcome NotModified => new ActionOutcome("NotModified", 304, true);

        public static ActionOutcome BadRequest => new ActionOutcome("BadRequest", 400, false);

        public static ActionOutcome Conflict => new ActionOutcome("Conflict", 409, false);

        public static ActionOutcome Forbidden => new ActionOutcome("Forbidden", 403, false);

        public static ActionOutcome NotFound => new ActionOutcome("NotFound", 404, false);

        public static ActionOutcome PreconditionFailed => new ActionOutcome("PreconditionFailed", 412, false);

        public static ActionOutcome UnknownError => new ActionOutcome("UnknownError", 500, false);

        public static ActionOutcome ServiceUnavailable => new ActionOutcome("ServiceUnavailable", 503, false);

        public ActionOutcome(string outcome, int statusCode, bool isSuccessOutcome)
        {
            this.Outcome = outcome;
            this.StatusCode = statusCode;
            this.IsSuccessOutcome = isSuccessOutcome;
        }

        public string Outcome { get; }

        public int StatusCode { get; }

        public bool IsSuccessOutcome { get; }
    }
}