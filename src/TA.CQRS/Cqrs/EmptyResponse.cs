namespace TA.CQRS
{
    /// <summary>
    /// The empty response.
    /// </summary>
    public class EmptyResponse
    {
        private static EmptyResponse emptyResponse = new EmptyResponse();
        public static EmptyResponse Create()
        {
            return emptyResponse;
        }
    }
}